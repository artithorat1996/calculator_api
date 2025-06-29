using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Models;
using API.Models.Request;
using API.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly PolicyContext _context;
        private readonly TokenHandler _tokenHandler;
        public LoginController(PolicyContext context, TokenHandler tokenHandler)
        {
            _context = context;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("IsAutehticatedUser")]
        public IsAutehticatedUserResponse IsAutehticatedUser(IsAutehticatedUserRequest request)
        {
            var user = _context.UserDetails.Where(x=>x.UserId == request.UserId && x.Password == request.Password).FirstOrDefault();

            if (user != null)
            {
                return new IsAutehticatedUserResponse
                {
                    IsAuthenticated = true,
                    Message = "User Found!",
                    UserName = user.UserName,
                    Token = _tokenHandler.GenerateJwtToken(user.UserId, user.UserName)
                };
            }
            else
            {
                return new IsAutehticatedUserResponse
                {
                    IsAuthenticated = false,
                    Message = "Invalid Credentials!",
                    UserName = "",
                    Token = ""
                };
            }
        }
    }
}