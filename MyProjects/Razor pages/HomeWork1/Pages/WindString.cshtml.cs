using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace HomeWork1.Pages
{
    public class WindStringModel : PageModel
    {
        public string MHL {get; set;}
        private readonly IConfiguration configuration;

        public WindStringModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            MHL = "Microsoft.Hosting.Lifetime: " + configuration.GetSection("Logging:LogLevel:Microsoft.Hosting.Lifetime").Value;
        }
    }
}
