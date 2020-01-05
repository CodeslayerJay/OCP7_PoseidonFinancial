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
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), GetUsernameForToken());

            try
            {
                return Ok(_ruleService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody]EditRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), GetUsernameForToken());

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
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), GetUsernameForToken());

            try
            {
                var rulename = _ruleService.FindById(id);

                if (rulename == null)
                    return BadRequest(AppConfig.ResourceNotFoundById + id);

                return Ok(rulename);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), GetUsernameForToken());

            try
            {
                if (ModelState.IsValid)
                {
                    var rulename = _ruleService.FindById(id);

                    if (rulename == null)
                        return BadRequest(AppConfig.ResourceNotFoundById + id);

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
            AppLogger.LogResourceRequest(nameof(Delete), GetUsernameForToken());

            try
            {
                var checkRating = _ruleService.FindById(id);

                if (checkRating == null)
                    return BadRequest(AppConfig.ResourceNotFoundById + id);

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