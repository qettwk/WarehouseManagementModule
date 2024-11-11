using Application.Queries.ViewModels;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAutomobileWithNameQuery : IRequest<AutomobileGetDTO>
    {
        public string name {  get; set; }
    }

    public class GetAutomobileWithNameHandler : IRequestHandler<GetAutomobileWithNameQuery, AutomobileGetDTO>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;
        public GetAutomobileWithNameHandler(WarehouseDbContext warehouseDbContext, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }
        public async Task<AutomobileGetDTO> Handle(GetAutomobileWithNameQuery request, CancellationToken cancellationToken)
        {
            var automobile = await _warehouseDbContext.Automobiles
                .Include(a => a.Categories)
                .Where(a => a.Name == request.name)
                .FirstOrDefaultAsync(cancellationToken);


            var automobileGetDTO = _mapper.Map<AutomobileGetDTO>(automobile);


            return automobileGetDTO;
        }
    }
}
