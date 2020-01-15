using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Repositories;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    
    public class BidListController : BaseApiController<BidListController>
    {
        private readonly IBidService _bidService;
        
        public BidListController(IBidService bidService, IAppLogger<BidListController> appLogger) : base(appLogger)
        {
            _bidService = bidService;
        }

        /// <summary>
        /// Get all bids
        /// </summary>
        /// <returns>json object containing all bids</returns>
        [HttpGet]
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), base.GetUsernameForRequest());

            try
            {
                return Ok(_bidService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }
        }

        /// <summary>
        /// Create a new bid
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>json object of newly created bid containing identity key</returns>        
        [HttpPost]
        public IActionResult Create([FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), base.GetUsernameForRequest());

            try
            {
                var result = _bidService.ValidateResource(resource);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var bid = _bidService.Add(resource);
                    
                    return CreatedAtAction(nameof(Create), bid);
                }

                return ValidationProblem();
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        /// <summary>
        /// Get a bid by id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of bid</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), base.GetUsernameForRequest());

            try
            {
                var bid = _bidService.FindById(id);
                if (bid == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                return Ok(bid);

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        /// <summary>
        /// Update a bid by Id number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {
                var result = _bidService.ValidateResource(resource);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    if (_bidService.FindById(id) == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);
                    
                    _bidService.Update(id, resource);
                    
                    return Ok();
                }

                return ValidationProblem();

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Update));
            }
            
        }

        /// <summary>
        /// Delete a bid by an id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                if (_bidService.FindById(id) == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                _bidService.Delete(id);
                
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }


    }
}