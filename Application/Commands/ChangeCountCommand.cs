using Application.Queries.ViewModels;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class ChangeCountCommand : IRequest<AutomobileGetDTO>
    {
        public Guid Id { get; set; }
        public int ChangeValue { get; set; }
    }

    public class ChangeCountHandler : IRequestHandler<ChangeCountCommand, AutomobileGetDTO>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChangeCountHandler(WarehouseDbContext warehouseDbContext, IMediator mediator, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AutomobileGetDTO> Handle(ChangeCountCommand command, CancellationToken cancellationToken)
        {
            var automobile = await _warehouseDbContext.Automobiles
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(auto => auto.Id == command.Id);

            automobile.Count += command.ChangeValue;
            await _warehouseDbContext.SaveChangesAsync();

            var automobileGetDTO = _mapper.Map<AutomobileGetDTO>(automobile);
            return automobileGetDTO;
        }
    }
}
