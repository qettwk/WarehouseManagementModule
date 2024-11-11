using Application.Commands.ViewModels;
using Application.Options;
using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Application.Commands
{
    public class AddAutomobileCommand : IRequest<Guid>
    {
        public AutomobileAddEditDTO automobileAddEditDTO {  get; set; }
    }

    public class AddAutomobileHandler : IRequestHandler<AddAutomobileCommand, Guid>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;

        public AddAutomobileHandler(WarehouseDbContext warehouseDbContext, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddAutomobileCommand command, CancellationToken cancellationToken)
        {
            List<Category> categories = _warehouseDbContext.Categories
                .Where(c => command.automobileAddEditDTO.CategoryIDs.Contains(c.Id))
                .ToList();

            var automobile = new Automobile();


            automobile = _mapper.Map<Automobile>(command.automobileAddEditDTO);
            automobile.Id = new Guid(); 
            automobile.Categories = categories;
            

            await _warehouseDbContext.Automobiles.AddAsync(automobile);
            await _warehouseDbContext.SaveChangesAsync();

            return automobile.Id;
        }
    }
}
