using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Controllers
{
    public class BaseController : Controller
    {
        public Hashtable GetErrors()
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

    }
}