using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOOS_API.Contracts;
using NOOS_API.Data;
using NOOS_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.Controllers
{
    /// <summary>
    /// endpoint used to interact with Products in NOOS. this will be mostly used
    /// by seller to view product based cutomer demand
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //inject dependency of subscriptions repository
        private readonly IProductRepository _productRepository; // private property
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        //constructor
        public ProductsController(IProductRepository productRepository, ILoggerService logger, IMapper mapper)
        {
            _productRepository = productRepository;   // declaired variable = whatever the value applicaiton passes in
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {

            var location = GetControllerActionNames(); //prepend with the location when logging | location = products
            try
            {
                _logger.LogInfo($"{location}: Attempted Call");
                var products = await _productRepository.FindAll();
                var response = _mapper.Map<IList<ProductDTO>>(products);
                _logger.LogInfo($"{location}: Successful");
                return Ok(response);
            }
            catch (Exception e)
            {

                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }


        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {

            var location = GetControllerActionNames(); //prepend with the location when logging | location = products
            try
            {
                _logger.LogInfo($"{location}: Attempted Call for {id}");
                var product = await _productRepository.FindById(id);
                if (product == null)
                {
                    _logger.LogWarn($"{location}: failed to retrieve the record withproduct id: {id}");
                    return NotFound();
                }
                var response = _mapper.Map<ProductDTO>(product);
                _logger.LogInfo($"{location}: Successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: NOOS button triggerd");
                if (productDTO == null)
                {
                    _logger.LogWarn($"{location}:Empty request was submitted");
                    return BadRequest(ModelState); // model state trackes data flow
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location} Invalid data entry/ incomplete"); // postman error+description display
                    return BadRequest(ModelState);
                }

                var product = _mapper.Map<Product>(productDTO); //take from DTO and map to Sub
                var isSuccess = await _productRepository.Create(product);
                if (!isSuccess)
                {
                    _logger.LogWarn($"{location}:Data entry failed!");
                }
                return Created("Subscription recorded", new { product });

            }
            catch (Exception e)
            {

                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }
        
        
        }




        // To know which controller and which action made an error, following logging function implemented

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return ($"{controller} -{action}");
        }

        // errormessage function
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }


    }
}
