using System;
using SendGrid;
using SSO.Models;
using System.Linq;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using SSO.Data.Entity;
using System.Net.Mail;
using SSO.ViewModels;

namespace SSO.Controllers
{
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<User> _userRepository;
        private readonly TokenHelper _tokenHelper;

        public PasswordController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _userRepository = new BaseRepository<User>(context);
            _tokenHelper = new TokenHelper();
        }

        [HttpGet("New", Name = "SSO/PASSWORD/NEW")]
        public IActionResult NewPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            User user = _userRepository.List().FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return NotFound();
            }

            ResetToken resetToken = new ResetToken { UserId = user.Id, Expirated = DateTimeOffset.Now.AddHours(2) };
            string token = _tokenHelper.Generate<ResetToken>(resetToken);

            SmtpClientHelper smtpClientHelper = new SmtpClientHelper("587",
                                                            "smtp.sendgrid.net",
                                                            _configuration["SMTP_USERNAME"],
                                                            _configuration["SMTP_PASSWORD"]);

            string body = String.Format("?token=" + token);
            string subject = "RESETAR SENHA - CMMG";

            smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_SENDER"]),
                                new MailAddress(email),
                                body,
                                subject);

            return new OkResult();
        }

        [HttpPost("New", Name = "SSO/PASSWORD/NEW")]
        public IActionResult NewPassword([FromQuery]string token, [FromBody]User obj)
        {
            if (User == null || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            ResetToken resetToken = _tokenHelper.GetObject<ResetToken>(token);

            if (!resetToken.IsValid())
            {
                return BadRequest();
            }

            User user = _userRepository.GetById(resetToken.UserId);

            if (user == null)
            {
                return NotFound();
            }

            User newUser = (User)user.Clone();
            newUser.Password = obj.Password;

            _userRepository.Update(user, newUser);

            return new OkResult();
        }
    }
}
