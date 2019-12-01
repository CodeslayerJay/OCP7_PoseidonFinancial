using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    
    public class RuleNameController : BaseApiController<RuleNameController>
    {
        private readonly IRuleService _ruleService;

        public RuleNameController(IAppLogger<RuleNameController> appLogger, IRuleService ruleService) : base(appLogger)
        {
            _ruleService = ruleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            AppLogger.LogResourceRequest(nameof(GetAll), "test");

            try
            {
                return Ok(_ruleService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetAll));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    _ruleService.Add(resource);

                    return CreatedAtAction(nameof(Create), resource);
                }

                return ValidationProblem();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRule(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetRule), "test");

            try
            {
                var rulename = _ruleService.FindById(id);

                if (rulename == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                return Ok(rulename);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetRule));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var rulename = _ruleService.FindById(id);

                    if (rulename == null)
                        return BadRequest(AppConstants.ResourceNotFoundById + id);

                    _ruleService.Update(id, resource);


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
                var checkRating = _ruleService.FindById(id);

                if (checkRating == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                _ruleService.Delete(id);


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }
    }
}