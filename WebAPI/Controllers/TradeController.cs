using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Repositories;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    public class TradeController : BaseApiController<TradeController>
    {
        private readonly ITradeService _tradeService;

        public TradeController(IAppLogger<TradeController> appLogger, ITradeService tradeService) : base(appLogger)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            AppLogger.LogResourceRequest(nameof(GetAll), "test");

            try
            {
                return Ok(_tradeService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetAll));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    _tradeService.Add(trade);
                    
                    return CreatedAtAction(nameof(Create), trade);
                }

                return ValidationProblem();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTrade(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetTrade), "test");

            try
            {
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                return Ok(trade);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetTrade));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var checkTrade = _tradeService.FindById(id);

                    if (checkTrade == null)
                        return BadRequest(AppConstants.ResourceNotFoundById + id);

                    _tradeService.Update(id, trade);

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
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                _tradeService.Delete(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }


    }
}