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
    /// endpoint used for interacting with the Sellers in the NOOS database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    public class SellersController : ControllerBase
    {
        //inject dependency of sellers repository
        private readonly ISellerRepository _sellerRepository; // private property
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        //constructor
        public SellersController(ISellerRepository sellerRepository, ILoggerService logger, IMapper mapper)
        {
            _sellerRepository = sellerRepository;   // declaired variable = whatever the value applicaiton passes in
            _logger = logger;
            _mapper = mapper;
        }
       
        //[Route("sellerregister")]
        //[HttpPost]
        //public async Task<IActionResult> Seller([FromQuery] SellerDTO sellerDTO) // userDTO same in both for registration and login to simplify

        //{
        //    //var location = GetControllerActionNames();
        //    //try
        //    //{

        //        var sellerid = sellerDTO.SellerId;
        //        var businessname = sellerDTO.BusinessName;
        //        var businessurl = sellerDTO.BusinessURL;
        //        var industry = sellerDTO.Industry;

        //       // _logger.LogInfo($"{location}: Registration attempt from a user {businessname}");

        //        return Ok();

            //}

            //catch (Exception e)
            //{
            //    return InternalError($"{location}: {e.Message} - {e.InnerException}");
            //}

        
    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

    //public async Task<IActionResult> GetSellers()
    //{
    //    var location = GetControllerActionNames(); //prepend with the location when logging | location = Sellers             try
    //    try
    //    {
    //        //map to the DTOs
    //        _logger.LogInfo($"{location}:Attempted to retrieve all Sellers");
    //        var sellers = await _sellerRepository.FindAll();
    //        var response = _mapper.Map<IList<SellerDTO>>(sellers);
    //        _logger.LogInfo($"{location}:Successfully retrieved all Sellers");
    //        return Ok(response);  //returning payload

    //    }
    //    catch (Exception e)  //catch the exception
    //    {
    //        return InternalError($"{location} -{e.Message} - {e.InnerException}");
    //    }

    //}
    
    //    [HttpGet ("{id}")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public async Task<IActionResult> GetSeller(int id)
    //    {
    //        var location = GetControllerActionNames(); //prepend with the location when logging | location
    //        try
    //        {
    //            //map to the DTOs
    //            _logger.LogInfo($"{location}:Attempted to retrieve a Subscription");
    //            var seller = await _sellerRepository.FindById(id);
    //            if (seller == null)
    //            {
    //                _logger.LogInfo($"{location}:Subscription with id: {id} was not found");
    //                return NotFound();
    //            }
    //            var response = _mapper.Map<SellerDTO>(seller);
    //            _logger.LogInfo($"{location}:Successfully retrieved all Sellers");
    //            return Ok(response);  //returning payload

    //        }
    //        catch (Exception e)  //catch the exception
    //        {
    //            return InternalError($"{location} -{e.Message} - {e.InnerException}");
    //        }
    //    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Seller(string BusinessName, string BusinessURL, string Industry)
    {

             var location = GetControllerActionNames(); //prepend with the location when logging | location = products
            try
            {
                 _logger.LogInfo($"{location}: Attempted register seller {BusinessName}");

            var sellerDTO = new SellerDTO();
            //sellerDTO.Id = id;
            sellerDTO.BusinessName = BusinessName;
            sellerDTO.BusinessURL = BusinessURL;
            sellerDTO.Industry = Industry;



             var sellerDbDt = _mapper.Map<Seller>(sellerDTO); //take from DTO and map to Sub
            //var sellerDbDt = _mapper.Map<Seller>(new Seller { SellerId = id, BusinessName = BusinessName, BusinessURL = BusinessURL, Industry = Industry });
            var isSuccess = await _sellerRepository.Create(sellerDbDt);



            //  var product = await _productRepository.FindById(id);
            //if (product == null)
            //{
            //    _logger.LogWarn($"{location}: failed to retrieve the record withproduct id: {id}");
            //    return NotFound();
            //}
            var response = _mapper.Map<SellerDTO>(sellerDTO);
            _logger.LogInfo($"{location}: Successful");
            return Ok(response);
        }
        catch (Exception e)
        {
            return InternalError($"{location} -{e.Message} - {e.InnerException}");
        }
}


        //    /// <summary>
        //    /// Triggered by the NOOS button in the product page
        //    /// </summary>
        //    /// <param name="sellerDTO"></param>
        //    /// <returns></returns>
        //    [HttpPost]
        //    [ProducesResponseType(StatusCodes.Status201Created)]
        //    public async Task<IActionResult> Create([FromBody] ButtonTriggerDTO sellerDTO)
        //    {
        //        var location = GetControllerActionNames();
        //        try
        //        {
        //            _logger.LogInfo($"{location}: NOOS button triggerd");
        //            if (sellerDTO == null)
        //            {
        //                _logger.LogWarn($"{location}:Empty request was submitted");
        //                return BadRequest(ModelState); // model state trackes data flow
        //            }
        //            if (!ModelState.IsValid)
        //            {
        //                _logger.LogWarn($"{location} Invalid data entry/ incomplete"); // postman error+description display
        //                return BadRequest(ModelState);
        //            }

        //            var seller = _mapper.Map<Seller>(sellerDTO); //take from DTO and map to Sub
        //            var isSuccess = await _sellerRepository.Create(seller);
        //            if (!isSuccess)
        //            {
        //                _logger.LogWarn($"{location}:Data entry failed!");
        //            }
        //            return Created("Seller recorded", new { seller });
        //        }
        //        catch (Exception e)
        //        {
        //            return InternalError($"{location} -{e.Message} - {e.InnerException}");
        //        }
        //    }
        //    // To know which controller and which action made an error, following logging function implemented

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
