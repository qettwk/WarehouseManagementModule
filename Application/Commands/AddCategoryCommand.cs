using Domain;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }

    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly WarehouseDbContext _warehouseDbContext;

        public AddCategoryHandler(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext;
        }

        public async Task<Guid> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = new Guid(),
                Name = command.Name
            };

            await _warehouseDbContext.Categories.AddAsync(category);
            await _warehouseDbContext.SaveChangesAsync();

            return category.Id;
        }
    }
}
