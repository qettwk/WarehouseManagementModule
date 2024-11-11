using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAutomobileQuery : IRequest<AutomobileGetDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetAutomobileHandler : IRequestHandler<GetAutomobileQuery, AutomobileGetDTO>
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;
        public GetAutomobileHandler(WarehouseDbContext warehouseDbContext, IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }
        public async Task<AutomobileGetDTO> Handle(GetAutomobileQuery request, CancellationToken cancellationToken)
        {
            var automobile = await _warehouseDbContext.Automobiles
                .Include(a => a.Categories)
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);


            var automobileGetDTO = _mapper.Map<AutomobileGetDTO>(automobile);
 
            return automobileGetDTO;
        }
    }
}
