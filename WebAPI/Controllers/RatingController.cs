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
    public class RatingController : BaseApiController<RatingController>
    {
        private readonly IRatingService _ratingService;

        public RatingController(IAppLogger<RatingController> appLogger, IRatingService ratingService) :base(appLogger)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Get all Ratings
        /// </summary>
        /// <returns>json object containing all ratings</returns>
        [HttpGet]
        public IActionResult Get()
        {
            AppLogger.LogResourceRequest(nameof(Get), base.GetUsernameForRequest());

            try
            {
                return Ok(_ratingService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Get));
            }

        }

        /// <summary>
        /// Create a new rating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns>json object of newly created rating with identity key</returns>
        [HttpPost]
        public IActionResult Create([FromBody]EditRatingResource rating)
        {
            AppLogger.LogResourceRequest(nameof(Create), base.GetUsernameForRequest());

            try
            {
                var result = _ratingService.ValidateResource(rating);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    _ratingService.Add(rating);
                    
                    return CreatedAtAction(nameof(Create), rating);
                }

                return ValidationProblem();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        /// <summary>
        /// Get rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of rating</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetById), base.GetUsernameForRequest());

            try
            {
                var rating = _ratingService.FindById(id);

                if (rating == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetById));
            }
        }

        /// <summary>
        /// Update rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditRatingResource rating)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {
                var result = _ratingService.ValidateResource(rating);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var checkRating = _ratingService.FindById(id);

                    if (checkRating == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);

                    _ratingService.Update(id, rating);


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
        /// Delete rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                var checkRating = _ratingService.FindById(id);

                if (checkRating == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                _ratingService.Delete(id);
                

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }


    }
}