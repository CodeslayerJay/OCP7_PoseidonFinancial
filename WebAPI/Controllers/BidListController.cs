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
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), GetUsernameForToken());

            try
            {
                return Ok(_bidService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }
        }

                
        [HttpPost]
        public IActionResult Create([FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), GetUsernameForToken());

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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), GetUsernameForToken());

            try
            {
                var bid = _bidService.FindById(id);
                if (bid == null)
                    return BadRequest(AppConfig.ResourceNotFoundById + id);

                return Ok(bid);

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditBidResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), GetUsernameForToken());

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), GetUsernameForToken());

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