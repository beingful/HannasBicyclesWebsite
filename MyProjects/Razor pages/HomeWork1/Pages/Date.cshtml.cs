using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork1.Pages
{
    public class DateModel : PageModel
    {
        public string Now { get; set; }
        public void OnGet()
        {
            Now = DateTime.Now.ToString();
        }
    }
}
