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
    /// endpoint used for interacting with the Subscriptions in the NOOS database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    public class SubscriptionsController : ControllerBase
    {
        //inject dependency of subscriptions repository
        private readonly ISubscriptionRepository _subscriptionRepository; // private property
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public string ProductName { get; private set; }

        //constructor
        public SubscriptionsController(ISubscriptionRepository subscriptionRepository, ILoggerService logger, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;   // declaired variable = whatever the value applicaiton passes in
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all Subscriptions
        /// </summary>
        /// <returns>All Subs</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<IActionResult> GetSubscriptions()
        {
            var location = GetControllerActionNames(); //prepend with the location when logging | location = Subscriptions             try
            try
            {
                //map to the DTOs
                _logger.LogInfo($"{location}:Attempted to retrieve all Subscriptions");
                var subscriptions = await _subscriptionRepository.FindAll();
                var response = _mapper.Map<IList<SubscriptionDTO>>(subscriptions);
                _logger.LogInfo($"{location}:Successfully retrieved all Subscriptions");
                return Ok(response);  //returning payload

            }
            catch (Exception e)  //catch the exception
            {
                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Get a Subscription by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Subscription record</returns>
        [HttpGet ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var location = GetControllerActionNames(); //prepend with the location when logging | location
            try
            {
                //map to the DTOs
                _logger.LogInfo($"{location}:Attempted to retrieve a Subscription");
                var subscription = await _subscriptionRepository.FindById(id);
                if (subscription == null)
                {
                    _logger.LogInfo($"{location}:Subscription with id: {id} was not found");
                    return NotFound();
                }
                var response = _mapper.Map<SubscriptionDTO>(subscription);
                _logger.LogInfo($"{location}:Successfully retrieved all Subscriptions");
                return Ok(response);  //returning payload

            }
            catch (Exception e)  //catch the exception
            {
                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }
        }

        [HttpPost("{id:int}/ {ProductName}/ {SellerID:int}/ {BuyerEmail}/ {OriginalPrice:decimal}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SubscribeToProduct(int id, string ProductName, int SellerID, string BuyerEmail, decimal OriginalPrice)  
        {

            var location = GetControllerActionNames(); //prepend with the location when logging | location = products
            try
            {
                _logger.LogInfo($"{location}: Attempted Call for {id}");

                var subscribeDTO = new SubscriptionDTO();
                subscribeDTO.ProductId = id;
                subscribeDTO.ProductName = ProductName;
                subscribeDTO.SellerId = SellerID;
                subscribeDTO.BuyerEmail = BuyerEmail;
                subscribeDTO.OriginalPrice = OriginalPrice;



                // var subscriptionDbDt = _mapper.Map<Subscription>(subscribeDTO); //take from DTO and map to Sub
                var subscriptionDbDt = _mapper.Map<Subscription>(new Subscription { SellerId = SellerID, ProductId = id, ProductName=ProductName, OriginalPrice = OriginalPrice, BuyerEmail = BuyerEmail, Timestamp = DateTime.Now }); 
                var isSuccess = await _subscriptionRepository.Create(subscriptionDbDt);



                //  var product = await _productRepository.FindById(id);
                //if (product == null)
                //{
                //    _logger.LogWarn($"{location}: failed to retrieve the record withproduct id: {id}");
                //    return NotFound();
                //}
                var response = _mapper.Map<SubscriptionDTO>(subscribeDTO);
                _logger.LogInfo($"{location}: Successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location} -{e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Triggered by the NOOS button in the product page
        /// </summary>
        /// <param name="subscriptionDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] ButtonTriggerDTO subscriptionDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: NOOS button triggerd");
                if (subscriptionDTO == null)
                {
                    _logger.LogWarn($"{location}:Empty request was submitted");
                    return BadRequest(ModelState); // model state trackes data flow
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location} Invalid data entry/ incomplete"); // postman error+description display
                    return BadRequest(ModelState);
                }

                var subscription = _mapper.Map<Subscription>(subscriptionDTO); //take from DTO and map to Sub
                var isSuccess = await _subscriptionRepository.Create(subscription);
                if (!isSuccess)
                {
                    _logger.LogWarn($"{location}:Data entry failed!");
                }
                return Created("Subscription recorded", new { subscription });
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
