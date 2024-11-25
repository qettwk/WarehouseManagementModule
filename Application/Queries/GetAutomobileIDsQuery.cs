using Application.Commands;
using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Queries
{
    public class GetAutomobileIDsQuery : IRequest<CheckedAutoIDsDTO>
    {
        public OrderAddAutomobilesDTO orderAddAutomobilesDTO { get; set; }
    }

    public class GetAutomobileIDsHandler : IRequestHandler<GetAutomobileIDsQuery, CheckedAutoIDsDTO>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMediator _mediator;
        public GetAutomobileIDsHandler(WarehouseDbContext warehouseDbContext, IMapper mapper, HttpClient httpClient, IMediator mediator)
        {
            _warehouseDbContext = warehouseDbContext;
            _mediator = mediator;
        }
        public async Task<CheckedAutoIDsDTO> Handle(GetAutomobileIDsQuery request, CancellationToken cancellationToken)
        {
            var CheckedAutoIDsDTO = new CheckedAutoIDsDTO();

            //var allAutomobiles = await _warehouseDbContext.Automobiles.Include(a => a.Categories).ToListAsync();
            var requestedAutoIDs = request.orderAddAutomobilesDTO.AutomobileIDsAndCountsDTO.Select(dto => dto.AutomobileID).ToList();
            var automobiles = await _warehouseDbContext.Automobiles.Include(auto => auto.Categories).Where(auto => requestedAutoIDs.Contains(auto.Id)).ToListAsync();
            // если лишние ID
            if (automobiles.Count != request.orderAddAutomobilesDTO.AutomobileIDsAndCountsDTO.Count)
            {
                CheckedAutoIDsDTO.excepted = true;
                CheckedAutoIDsDTO.AutomobileIDs = requestedAutoIDs
                .Except(automobiles.Select(a => a.Id))
                    .ToList();
                return CheckedAutoIDsDTO;
            }



            var IDsWithCounts = request.orderAddAutomobilesDTO.AutomobileIDsAndCountsDTO.ToDictionary(dto => dto.AutomobileID, dto => dto.count);

            // если какой-то автомобиль не смог удалиться другим автомобилем в БД, значит, что в БД его нет
            // остатки отправляются обратно на клиент


            CheckedAutoIDsDTO.excepted = false; // по умолчанию пока исключений нет
            var exceptingCounts = new List<int>(); 
            foreach (var idAndCount in IDsWithCounts)
            {
                var foundAuto = automobiles
                    .Where(auto => auto.Id == idAndCount.Key)
                        .Select(auto => new
                        {
                            auto.Count,
                            auto.Price,
                            auto.Discount,
                            auto.Categories
                        }).SingleOrDefault();

                if (idAndCount.Value > foundAuto.Count)
                {
                    CheckedAutoIDsDTO.excepted = true;
                    CheckedAutoIDsDTO.TotalSum = 0;
                    CheckedAutoIDsDTO.AutomobileIDs.Add(idAndCount.Key);
                    CheckedAutoIDsDTO.Counts.Add(idAndCount.Value);
                }
                else if (CheckedAutoIDsDTO.excepted == false)
                {
                    // не считать внутри, а просто собрать все скидки и отправить на заказ -- заказ считаем уже сам
                    decimal maxDiscount = 0; // поиск наибольшей скидки через дополнительную переменную
                    if (foundAuto.Categories.FirstOrDefault().Discount > maxDiscount)
                    {
                        maxDiscount = foundAuto.Categories.FirstOrDefault().Discount;
                    }
                    if (foundAuto.Discount > maxDiscount)
                    {
                        maxDiscount = foundAuto.Discount;
                    }
                    if (request.orderAddAutomobilesDTO.Discount > maxDiscount)
                    {
                        maxDiscount = request.orderAddAutomobilesDTO.Discount;
                    }

                    decimal decimalCount = idAndCount.Value; // int to decimal
                    CheckedAutoIDsDTO.TotalSum = CheckedAutoIDsDTO.TotalSum + decimalCount * foundAuto.Price * (1.0m - (maxDiscount / 100));
                    // копить Id и ChangeValue, менять после проверки каждого элемента, перед отправкой DTO (в идеале после 
                    // подтверждения от OrderDTO о создании заказа
                    // +
                    // одним запросом к БД менять значения
                    var command = new ChangeCountCommand
                    {
                        Id = idAndCount.Key,
                        ChangeValue = -idAndCount.Value
                    };
                    await _mediator.Send(command);

                }
            }

            // если всё ок
            //CheckedAutoIDsDTO.AutomobileIDs = automobileIDs;
            //CheckedAutoIDsDTO.Counts = counts;

            return CheckedAutoIDsDTO;
        }
    }

}



//{
//    var automobileIDs = await _warehouseDbContext.Automobiles
//        .Where(a => request.IDs.Contains(a.Id))
//            .Select(a => a.Id)
//                .ToListAsync();