using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace onboarding.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        protected Hashtable GetErrors()
        {
            Hashtable errors = new Hashtable();

            Dictionary<string, string[]> modelStateErrors = ModelState.ToDictionary(
                modelState => modelState.Key.UnCapitalize(),
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            foreach (var error in modelStateErrors)
            {
                string[] split = error.Key.Split('.');

                if (split.Length == 1)
                {
                    errors.Add(error.Key, error.Value);
                }
                else
                {
                    if (!errors.ContainsKey(split[0]))
                    {
                        errors.Add(split[0], new Hashtable());
                    }

                    ((Hashtable)errors[split[0]]).Add(split[1].UnCapitalize(), error.Value);
                }
            }

            return errors;
        }

        protected Hashtable FormatErrors(FluentValidation.Results.ValidationResult results)
        {
            var errors = new Hashtable();
            var propertyErrors = results.Errors.ToDictionary(error => error.PropertyName.UnCapitalize(), error => new string[] { error.ErrorMessage });

            foreach (var error in propertyErrors)
            {
                string[] split = error.Key.Split('.');

                if (split.Length == 1)
                {
                    errors.Add(error.Key, error.Value);
                }
                else if (split.Length == 2)
                {
                    if (!errors.ContainsKey(split[0]))
                    {
                        errors.Add(split[0], new Hashtable());
                    }

                    ((Hashtable)errors[split[0]]).Add(split[1].UnCapitalize(), error.Value);
                }
                else if (split.Length == 3)
                {
                    if (!errors.ContainsKey(split[0]))
                    {
                        errors.Add(split[0], new Hashtable());
                        ((Hashtable)errors[split[0]]).Add(split[1], new Hashtable());
                    }

                    if (!((Hashtable)errors[split[0]]).ContainsKey(split[1]))
                    {
                        ((Hashtable)errors[split[0]]).Add(split[1], new Hashtable());
                    }

                    ((Hashtable)((Hashtable)errors[split[0]])[split[1]]).Add(split[2].UnCapitalize(), error.Value);
                }
                else
                {
                    if (!errors.ContainsKey(split[0]))
                    {
                        errors.Add(split[0], new Hashtable());
                    }

                    if (!((Hashtable)errors[split[0]]).ContainsKey(split[1]))
                    {
                        ((Hashtable)errors[split[0]]).Add(split[1], new Hashtable());
                        ((Hashtable)((Hashtable)errors[split[0]])[split[1]]).Add(split[2], new Hashtable());
                    }

                    ((Hashtable)((Hashtable)((Hashtable)errors[split[0]])[split[1]])[split[2]]).Add(split[3].UnCapitalize(), error.Value);
                }
            }

            return errors;
        }

        protected string GetEmailBody(string emailTemplateFile)
        {
            string webRoot = Directory.GetCurrentDirectory();
            string pathToFile = webRoot + Path.DirectorySeparatorChar.ToString() + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplate" + Path.DirectorySeparatorChar.ToString() + emailTemplateFile;

            BodyBuilder builder = new BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            return builder.HtmlBody;
        }
    }
}