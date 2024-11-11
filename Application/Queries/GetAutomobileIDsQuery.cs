using Application.Queries.ViewModels;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
        public List<Guid> IDs { get; set; }
    }

    public class GetAutomobileIDsHandler : IRequestHandler<GetAutomobileIDsQuery, CheckedAutoIDsDTO>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        public GetAutomobileIDsHandler(WarehouseDbContext warehouseDbContext, IMapper mapper, HttpClient httpClient)
        {
            _warehouseDbContext = warehouseDbContext;
        }
        public async Task<CheckedAutoIDsDTO> Handle(GetAutomobileIDsQuery request, CancellationToken cancellationToken)
        { 

            var checkedAutoIDsDTO = new CheckedAutoIDsDTO();
            var exceptingAutomobileIDs = request.IDs
                .Except(_warehouseDbContext.Automobiles.Select(a => a.Id))
                    .ToList();

            if (exceptingAutomobileIDs.Any())
            {
                checkedAutoIDsDTO.Excepted = true;
                checkedAutoIDsDTO.automobileIDs = exceptingAutomobileIDs;
                return checkedAutoIDsDTO;
            }

            checkedAutoIDsDTO.Excepted = false;

            checkedAutoIDsDTO.automobileIDs = await _warehouseDbContext.Automobiles
                .Where(a => request.IDs.Contains(a.Id))
                    .Select(a => a.Id).ToListAsync();

            checkedAutoIDsDTO.automobileIDs = request.IDs;

            return checkedAutoIDsDTO;
        }
    }
    
}

        //{
        //    var automobileIDs = await _warehouseDbContext.Automobiles
        //        .Where(a => request.IDs.Contains(a.Id))
        //            .Select(a => a.Id)
        //                .ToListAsync();