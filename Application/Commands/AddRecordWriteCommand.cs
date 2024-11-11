using Domain;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddRecordWriteCommand : IRequest<Guid>
    {
        public Guid AutomobileID { get; set; }
        public int Value { get; set; }
    }

    public class AddRecordWriteHandler : IRequestHandler<AddRecordWriteCommand, Guid>
    {

        private readonly WarehouseDbContext _warehouseDbContext;

        public AddRecordWriteHandler(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext;
        }
        public async Task<Guid> Handle(AddRecordWriteCommand command, CancellationToken cancellationToken)
        {
            var RecordWrite = new RecordWrite
            {
                Id = new Guid(),
                AutomobileID = command.AutomobileID,
                Value = command.Value,
                CreatedDateTime = DateTime.Now
            };
            await _warehouseDbContext.RecordWrites.AddAsync(RecordWrite);
            return RecordWrite.Id;
        }
    }
}
