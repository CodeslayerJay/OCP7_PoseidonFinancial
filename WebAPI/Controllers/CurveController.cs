using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Repositories;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    
    public class CurveController : BaseApiController<CurveController>
    {
        private readonly ICurveService _curveService;

        public CurveController(IAppLogger<CurveController> appLogger, ICurveService curveService) :base(appLogger)
        {
            _curveService = curveService;
        }

        /// <summary>
        /// Get all CurvePoints
        /// </summary>
        /// <returns>json object containing all curvepoints</returns>
        [HttpGet]
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), base.GetUsernameForRequest());

            try
            {
                return Ok(_curveService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }
            
        }

        /// <summary>
        /// Create a new CurvePoint
        /// </summary>
        /// <param name="curvePoint"></param>
        /// <returns>json object of newly created curve with identity key</returns>
        [HttpPost]
        public IActionResult Create([FromBody]EditCurveResource curvePoint)
        {
            AppLogger.LogResourceRequest(nameof(Create), base.GetUsernameForRequest());

            try
            {
                var result = _curveService.ValidateResource(curvePoint);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    _curveService.Add(curvePoint);
                    

                    return CreatedAtAction(nameof(Create), curvePoint);
                }

                return ValidationProblem();
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        /// <summary>
        /// Get CurvePoint by Id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of curve point</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), base.GetUsernameForRequest());

            try
            {
                var curve = _curveService.FindById(id);

                if (curve == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);
                
                return Ok(curve);
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        /// <summary>
        /// Update a curve point by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curvePoint"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditCurveResource curvePoint)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {
                var result = _curveService.ValidateResource(curvePoint);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var curve = _curveService.FindById(id);

                    if (curve == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);

                    _curveService.Update(id, curvePoint);


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
        /// Delete curve point by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                var curve = _curveService.FindById(id);

                if (curve == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                _curveService.Delete(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }
    }
}