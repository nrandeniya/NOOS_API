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
            try
            {
                //map to the DTOs
                _logger.LogInfo("Attempted to retrieve all Subscriptions");
                var subscriptions = await _subscriptionRepository.FindAll();
                var response = _mapper.Map<IList<SubscriptionDTO>>(subscriptions);
                _logger.LogInfo("Successfully retrieved all Subscriptions");
                return Ok(response);  //returning payload

            }
            catch (Exception e)  //catch the exception
            {
                _logger.LogError($"{e.Message} - {e.InnerException}");
                return StatusCode(500);
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
            try
            {
                //map to the DTOs
                _logger.LogInfo("Attempted to retrieve a Subscription");
                var subscription = await _subscriptionRepository.FindById(id);
                if (subscription == null)
                {
                    _logger.LogInfo($"Subscription with id: {id} was not found");
                    return NotFound();
                }
                var response = _mapper.Map<SubscriptionDTO>(subscription);
                _logger.LogInfo("Successfully retrieved all Subscriptions");
                return Ok(response);  //returning payload

            }
            catch (Exception e)  //catch the exception
            {
                _logger.LogError($"{e.Message} - {e.InnerException}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Triggered by the NOOS button in the product page
        /// </summary>
        /// <param name="subscriptionDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Create([FromQuery] ButtonTriggerDTO subscriptionDTO)
        {
            try
            {
                _logger.LogWarn("NOOS Button triggerd");
                if (subscriptionDTO == null)
                {
                    _logger.LogWarn("Empty request was submitted");
                    return BadRequest(ModelState); // model state trackes data flow
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Invalid data entry/ incomplete"); // postman error+description display
                    return BadRequest(ModelState);
                }

                var subscription = _mapper.Map<Subscription>(subscriptionDTO); //take from DTO and map to Sub
                var isSuccess = await _subscriptionRepository.Create(subscription);
                if (!isSuccess)
                {
                    _logger.LogWarn($"Data entry failed!");
                }
                return Created("Subscription recorded", new { subscription });
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message} - {e.InnerException}");
                return StatusCode(400);
            }
        }

    }
}
