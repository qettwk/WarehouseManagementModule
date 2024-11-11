using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCategoryByNameQuery : IRequest<List<CategoryGetDTO>>
    {
        public string categoryName { get; set; }
    }

    public class GetCategoryByNameHandler : IRequestHandler<GetCategoryByNameQuery, List<CategoryGetDTO>>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        public GetCategoryByNameHandler(WarehouseDbContext warehouseDbContext, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
        }
        public async Task<List<CategoryGetDTO>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Category> Categories = _warehouseDbContext.Categories
                .Where(category => category.Name
                        .ToLower()
                        .Contains(request.categoryName.ToLower()));
            List<CategoryGetDTO> categoryGetDTOs = new List<CategoryGetDTO>();

            foreach (var category in Categories)
            {
                var categoryGetDTO = new CategoryGetDTO
                {
                    Name = category.Name
                };
                categoryGetDTOs.Add(categoryGetDTO);
            }

            return categoryGetDTOs;
        }
    }
}
