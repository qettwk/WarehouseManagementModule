using Application.Commands;
using Application.Queries;
using Application.Queries.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WarehouseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<List<Guid>>> SendAutomobilesToOrder(List<Guid> receivedGuid)
        {
            GetAutomobileIDsQuery query = new GetAutomobileIDsQuery
            {
                IDs = receivedGuid
            };
            var result =  await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPut]
        public Task<Guid> EditAutomobile(EditAutomobileCommand command)
        {
            return _mediator.Send(command);
        }


        [HttpPut]
        public async Task<ActionResult<AutomobileGetDTO>> ChangeCountAutomobile(ChangeCountCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpGet("{automobileId}")]
        public Task<AutomobileGetDTO> GetAutomobileWithID(Guid automobileId)
        {
            return _mediator.Send(new GetAutomobileQuery { Id = automobileId });
        }

        [HttpGet("{automobileName}")]
        public Task<AutomobileGetDTO> GetAutomobileWithName(string automobileName)
        {
            return _mediator.Send(new GetAutomobileWithNameQuery { name = automobileName });
        }

        [HttpGet("{CategoryName}")]
        public Task<List<AutomobileGetDTO>> GetAutomobilesWithCategory(string CategoryName)
        {
            return _mediator.Send(new GetAutomobilesWithCategoryQuery { categoryName = CategoryName });
        }

        [HttpGet("{CategoryName}")]
        public Task<List<CategoryGetDTO>> GetCategoriesWithCategoryName(string CategoryName)
        {
            return _mediator.Send(new GetCategoryByNameQuery { categoryName = CategoryName });
        }









        [HttpPost]
        public async Task<ActionResult> AddAutomobile(AddAutomobileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(string name)
        {
            AddCategoryCommand command = new AddCategoryCommand
            {
                Name = name,
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
