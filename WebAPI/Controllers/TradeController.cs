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

        /// <summary>
        /// Get all trades
        /// </summary>
        /// <returns>json object of all trades</returns>
        [HttpGet]
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), base.GetUsernameForRequest());

            try
            {
                return Ok(_tradeService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }

        }

        /// <summary>
        /// Create a new trade
        /// </summary>
        /// <param name="trade"></param>
        /// <returns>json object of newly created trade containing identity key</returns>
        [HttpPost]
        public IActionResult Create([FromBody]EditTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Create), base.GetUsernameForRequest());

            try
            {
                var result = _tradeService.ValidateResource(trade);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

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


        /// <summary>
        /// Get trade by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of trade</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), base.GetUsernameForRequest());

            try
            {
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                return Ok(trade);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }


        /// <summary>
        /// Update trade by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trade"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditTradeResource trade)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {
                var result = _tradeService.ValidateResource(trade);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var checkTrade = _tradeService.FindById(id);

                    if (checkTrade == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);

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

        /// <summary>
        /// Delete trade by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                var trade = _tradeService.FindById(id);

                if (trade == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

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