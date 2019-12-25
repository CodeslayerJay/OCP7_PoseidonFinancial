using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            AppLogger.LogResourceRequest(nameof(GetAll), "test");

            try
            {
                return Ok(_bidService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetAll));
            }
        }

                
        [HttpPost]
        public IActionResult Create([FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
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

        [HttpGet("{id}")]
        public IActionResult GetBid(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetBid), "test");

            try
            {
                var bid = _bidService.FindById(id);
                if (bid == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                return Ok(bid);

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetBid));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    if (_bidService.FindById(id) == null)
                        return NotFound(AppConstants.ResourceNotFoundById + id);
                    
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), "test");

            try
            {
                if (_bidService.FindById(id) == null)
                    return NotFound(AppConstants.ResourceNotFoundById + id);

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