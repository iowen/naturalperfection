using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class AppCookieAuthenticationProvider : CookieAuthenticationProvider
    {
        public override Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            var result =  base.ValidateIdentity(context);
            if(context.Identity.IsAuthenticated)
            {
                Console.WriteLine("is");
            }
            else
            {
                Console.WriteLine("not");

            }
            return result;
            
        }
    }
}
