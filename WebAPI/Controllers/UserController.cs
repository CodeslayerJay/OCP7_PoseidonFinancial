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

        [HttpGet]
        public IActionResult GetUserByUsername(string username)
        {
            AppLogger.LogResourceRequest(nameof(GetUserByUsername), "test");
            try
            {
                var result = _userService.FindByUsername(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetUserByUsername));
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]EditUserResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Create), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var result = _userService.CreateUser(resource);
                    return CreatedAtAction(nameof(Create), result);
                }

                return ValidationProblem();
            }
            catch(Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(Create));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            AppLogger.LogResourceRequest(nameof(GetUserById), "test");

            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                    return BadRequest("User not found.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetUserById));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EditUserResource resource)
        {
            AppLogger.LogResourceRequest(nameof(Update), "test");

            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.GetUserById(id);

                    if (user == null)
                        return BadRequest("User not found.");

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppLogger.LogResourceRequest(nameof(Delete), "test");

            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                    return BadRequest("User not found.");

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