using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using GroupProject.Extensions;
using GroupProject.Models;
using GroupProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GroupProject.Views.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string AntiforgeryToken => 
            HttpContext.GetAntiForgeryTokenForJs();

        public bool IsAdmin =>
            HttpContext.User.HasClaim("IsAdmin", bool.TrueString);
    }
}
