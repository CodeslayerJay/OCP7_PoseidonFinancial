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
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), GetUsernameForToken());

            try
            {
                return Ok(_tradeService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody]EditTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Create), GetUsernameForToken());

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
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), GetUsernameForToken());

            try
            {
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return BadRequest(AppConfig.ResourceNotFoundById + id);

                return Ok(trade);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Update), GetUsernameForToken());

            try
            {
                if (ModelState.IsValid)
                {
                    var checkTrade = _tradeService.FindById(id);

                    if (checkTrade == null)
                        return BadRequest(AppConfig.ResourceNotFoundById + id);

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
            AppLogger.LogResourceRequest(nameof(Delete), GetUsernameForToken());

            try
            {
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return BadRequest(AppConfig.ResourceNotFoundById + id);

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