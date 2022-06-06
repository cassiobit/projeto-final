using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Domain.Customers.Dtos;
using Store.Domain.Dtos;
using Store.Domain.Services.Interfaces;

namespace Store.Api.Controllers
{

    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// Add new Customer.
        /// </summary>
        /// <response code="201">Success.</response>
        /// <response code="422">Processing Fail.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddNewCustomer([FromBody] NewCustomerDto newCustomer)
        {
            GenericResultDto result = await _customerService.AddNewCustomer(newCustomer);

            if (result.ValidationResult.IsValid)
                return Created("customers", result.Result);

            return UnprocessableEntity(result.ValidationResult.Errors);
        }
    }
}