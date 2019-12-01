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

        [HttpGet]
        public IActionResult GetAll()
        {
            AppLogger.LogResourceRequest(nameof(GetAll), "test");

            try
            {
                return Ok(_curveService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetAll));
            }
            
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateCurvePointResource curvePoint)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
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

        [HttpGet("{id}")]
        public IActionResult GetCurve(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetCurve), "test");

            try
            {
                var curve = _curveService.FindById(id);

                if (curve == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);
                
                return Ok(curve);
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetCurve));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditCurvePointResource curvePoint)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var curve = _curveService.FindById(id);

                    if (curve == null)
                        return BadRequest(AppConstants.ResourceNotFoundById + id);

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


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), "test");

            try
            {
                var curve = _curveService.FindById(id);

                if (curve == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

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