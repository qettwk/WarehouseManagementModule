using Application.Commands.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class EditAutomobileCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public AutomobileAddEditDTO automobileAddEditDTO { get; set; }
    }

    public class EditAutomobileHandler : IRequestHandler<EditAutomobileCommand, Guid>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;

        public EditAutomobileHandler(WarehouseDbContext warehouseDbContext, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }


        public async Task<Guid> Handle (EditAutomobileCommand command, CancellationToken cancellationToken)
        {
            Automobile automobile = _warehouseDbContext.Automobiles
                .Single(a => a.Id == command.Id);

            //List<Category> categories = new List<Category>();

            //foreach (var commandCategoryID in command.addAutomobileDTO.CategoryIDs)
            //{
            //    var category = await _warehouseDbContext.Categories
            //        .SingleOrDefaultAsync(category => category.Id == commandCategoryID);
            //    categories.Add(category);
            //}

            var categories = _warehouseDbContext.Categories
                .Where(c => command.automobileAddEditDTO.CategoryIDs.Contains(c.Id))
                .ToList();


            automobile = _mapper.Map<Automobile>(command.automobileAddEditDTO);

            await _warehouseDbContext.SaveChangesAsync(cancellationToken);
            return automobile.Id;
        }
    }
}


//public AutomobileAddEditDTO GetOriginal(EditAutomobileCommand command, CancellationToken cancellationToken)
//{
//    var automobile = _warehouseDbContext.Automobiles.Single(a => a.Id == command.automobileId);
//    AutomobileAddEditDTO automobileAddEditDTO = new AutomobileAddEditDTO()
//    {
//        Name = automobile.Name,
//        Count = automobile.Count,
//        CategoryIDs = automobile.Categories.Select(c => c.Id).ToList()
//    };
//    return addAutomobileDTO;
//}
//public async Task<Guid> Handle(EditAutomobileCommand command, CancellationToken cancellationToken)
//{
//    var automobile = _warehouseDbContext.Automobiles.SingleOrDefault(auto => auto.Id == command.automobileId);
//    automobile.Name = command.automobileAddEditDTO.Name;
//    automobile.Count = command.automobileAddEditDTO.Count;
//    await _warehouseDbContext.SaveChangesAsync(cancellationToken);
//    return automobile.Id;
//}





//return _warehouseDbContext.Automobiles.Select(auto => new AddAutomobileDTO
//{
//    Name = auto.Name,
//    Count = auto.Count,
//    CategoryIDs = auto.Categories.Select(c => c.Id).ToList()
//})
//.Single(a => a.Id == command.automobileId);



//public async Task<Guid> Handle(EditAutomobileCommand command, CancellationToken cancellationToken)
//{
//    var automobile = await _warehouseDbContext.Automobiles.FindAsync(command.Id);

//    automobile.Name = command.Name;
//    automobile.Count = command.Count;

//    await _warehouseDbContext.SaveChangesAsync();
//    return automobile.Id;
//}