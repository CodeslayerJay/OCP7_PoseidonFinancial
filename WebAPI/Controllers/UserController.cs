using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{

    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService, IAppLogger<UserController> appLogger) : base(appLogger)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get a user by their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>json object of user</returns>
        [HttpGet("username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            AppLogger.LogResourceRequest(nameof(GetUserByUsername), base.GetUsernameForRequest());
            try
            {
                var result = _userService.FindByUsername(username);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetUserByUsername));
            }
        }
        

        /// <summary>
        /// Create a new user account
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>json object of newly created user account</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody]EditUserResource resource)
        {
            try
            {
                var result = _userService.ValidateResource(resource, isUpdate: false);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var user = _userService.CreateUser(resource);
                    return CreatedAtAction(nameof(Create), user);
                }

                return ValidationProblem();
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }


        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>json object of user</returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetUserById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetUserById), base.GetUsernameForRequest());

            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetUserById));
            }
        }


        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns>json object of user</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditUserResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), base.GetUsernameForRequest());

            try
            {

                var result = _userService.ValidateResource(resource, isUpdate: true);

                if (!result.IsValid)
                {
                    GetErrorsForModelState(result.ErrorMessages);
                }

                if (ModelState.IsValid)
                {
                    var user = _userService.GetUserById(id);

                    if (user == null)
                        return NotFound(AppConfig.ResourceNotFoundById + id);

                    _userService.UpdateUser(id, resource);

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
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), base.GetUsernameForRequest());

            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                    return NotFound(AppConfig.ResourceNotFoundById + id);

                // Make sure the current user can only delete themselves
                //if (user.Id != base.GetCurrentUserId())
                //    return Unauthorized();

                _userService.DeleteUser(id);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Delete));
            }
        }

    }
}