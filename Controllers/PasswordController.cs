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

namespace SSO.Controllers
{
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<User> _userRepository;
        
        public PasswordController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _userRepository = new BaseRepository<User>(context);
        }

        [HttpGet("New", Name = "SSO/PASSWORD/NEW")]
        public IActionResult NewPassword(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }            
            
            User user = _userRepository.List().FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            if(user == null)
            {
                return NotFound();
            }

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            SmtpClientHelper smtpClientHelper = new SmtpClientHelper(_configuration["SMTP_PORT"],
                                                            "smtp.sendgrid.net",
                                                            _configuration["SMTP_USERNAME"],
                                                            _configuration["SMTP_PASSWORD"]);

            string body = String.Format("?token=" + token + "&i=" + user.Id);
            string subject = _configuration["EMAIL_RESET_PASSWORD_SUBJECT"];

            smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_RESET_PASSWORD_SUBJECT"]),
                                new MailAddress(email),
                                body,
                                subject);                                                            

            return new OkResult();
        }

        [HttpPost("New", Name = "SSO/PASSWORD/NEW")]
        public IActionResult NewPassword([FromQuery]string token, [FromBody]User obj)
        {
            if(User == null || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            byte[] data = Convert.FromBase64String(token);

            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            if (when < DateTime.UtcNow.AddHours(-24)) 
            {
                return BadRequest();
            }

            User user = _userRepository.GetById(obj.ExternalId);

            if(user == null)
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