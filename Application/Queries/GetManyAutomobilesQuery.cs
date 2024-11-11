using Application.Queries.ViewModels;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetManyAutomobilesQuery : IRequest<List<AutomobileGetDTO>>
    {
        public List<Guid> AutomobilesId { get; set; }
        
        // StringContent content {  get; set; } // предполагал что в команду пойдёт JSON

    }

    public class GetManyAutomobilesHandler : IRequestHandler<GetManyAutomobilesQuery, List<AutomobileGetDTO>>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly HttpClient _httpClient;
        public GetManyAutomobilesHandler(WarehouseDbContext warehouseDbContext, HttpClient httpClient)
        {
            _warehouseDbContext = warehouseDbContext;
        }
        public async Task<List<AutomobileGetDTO>> Handle(GetManyAutomobilesQuery request, CancellationToken cancellationToken)
        {
            List<AutomobileGetDTO> automobileDTOs = new List<AutomobileGetDTO>();

            foreach (Guid automobileId in request.AutomobilesId) 
            {
                var automobile = await _warehouseDbContext.Automobiles
                .Include(a => a.Categories)
                .Where(a => a.Id == automobileId)
                .FirstOrDefaultAsync(cancellationToken);

                AutomobileGetDTO automobileDTO = new AutomobileGetDTO
                {
                    Name = automobile.Name,
                    Count = automobile.Count,
                    CategoriesName = automobile.Categories.Select(c => c.Name).ToList()
                };
                automobileDTOs.Add(automobileDTO);
            }
            var json = JsonSerializer.Serialize(automobileDTOs);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
           // var response = await _httpClient.PostAsync("http://localhost:5001/api/receive", content);
            //return content;
            return automobileDTOs;
        }
    }
}
