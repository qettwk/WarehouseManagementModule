using Application.Queries.ViewModels;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetAutomobilesWithCategoryQuery : IRequest <List<AutomobileGetDTO>>
    {
        public string categoryName {  get; set; }
    }
    public class GetAutomobilesCategoryHandler : IRequestHandler<GetAutomobilesWithCategoryQuery, List<AutomobileGetDTO>>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        public GetAutomobilesCategoryHandler(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext;
        }
        public async Task<List<AutomobileGetDTO>> Handle(GetAutomobilesWithCategoryQuery request, CancellationToken cancellationToken)
        {
            List<AutomobileGetDTO> automobileDTOs = new List<AutomobileGetDTO>();

            var automobiles = await _warehouseDbContext.Automobiles
                .Include(a => a.Categories)
                .Where(auto => auto.Categories.Any(category => category.Name == request.categoryName))
                .ToListAsync();



            foreach (var automobile in automobiles)
            {
                Console.WriteLine(automobile.Name);
                AutomobileGetDTO automobileGetDTO = new AutomobileGetDTO()
                {
                    Name = automobile.Name,
                    Count = automobile.Count,
                    CategoriesName = automobile.Categories.Select(c => c.Name).ToList()
                };
                automobileDTOs.Add(automobileGetDTO);
            }

            return automobileDTOs;
        }
    }
}
