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

        [HttpGet]
        public IActionResult GetAll()
        {
            AppLogger.LogResourceRequest(nameof(GetAll), "test");

            try
            {
                return Ok(_ratingService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetAll));
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateRatingResource rating)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
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

        [HttpGet("{id}")]
        public IActionResult GetRating(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetRating), "test");

            try
            {
                var rating = _ratingService.FindById(id);

                if (rating == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetRating));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditRatingResource rating)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var checkRating = _ratingService.FindById(id);

                    if (checkRating == null)
                        return BadRequest(AppConstants.ResourceNotFoundById + id);

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


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), "test");

            try
            {
                var checkRating = _ratingService.FindById(id);

                if (checkRating == null)
                    return BadRequest(AppConstants.ResourceNotFoundById + id);

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