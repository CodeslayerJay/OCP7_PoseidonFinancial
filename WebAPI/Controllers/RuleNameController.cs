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

        /// <summary>
        /// Get all rulenames
        /// </summary>
        /// <returns>json object of all rulenames</returns>
        [HttpGet]
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), base.GetUsernameForRequest());

            try
            {
                return Ok(_ruleService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }

        }

        /// <summary>
        /// Create a new rulename
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>json object of newly created rulename containing identity key</returns>
        [HttpPost]
        public IActionResult Create([FromBody]EditRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), base.GetUsernameForRequest());

            try
            {
                var result = _ruleService.ValidateResource(resource);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

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

        /// <summary>
        /// Get rulename by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of rulename</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), base.GetUsernameForRequest());

            try
            {
                var rulename = _ruleService.FindById(id);

                if (rulename == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                return Ok(rulename);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        /// <summary>
        /// Update rulename by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditRuleNameResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {
                var result = _ruleService.ValidateResource(resource);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var rulename = _ruleService.FindById(id);

                    if (rulename == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);

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

        /// <summary>
        /// Delete rulename by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                var checkRating = _ruleService.FindById(id);

                if (checkRating == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

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