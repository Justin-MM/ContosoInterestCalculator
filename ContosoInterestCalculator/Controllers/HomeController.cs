using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoInterestCalculator.Models;
using Microsoft.AspNetCore.Authorization;
using ContosoInterestCalculator.Data;

namespace ContosoInterestCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IList<InterestFormValues> calculations { get; set; }

        public HomeController(ApplicationDbContext context)
        {            
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Contoso Corp. Interest Calculator.";

            return View();
        }
        
        public IActionResult YourHistory()
        {
            ViewData["Message"] = "Contoso Corp. Interest Calculator.";
            calculations = _context.InterestFormValues.ToList<InterestFormValues>();            
            foreach (var calculation in calculations.ToList<InterestFormValues>())
            {
                if (HttpContext.User.Identity.Name != null)
                {
                    if (calculation.CalculatedBy != HttpContext.User.Identity.Name.ToString())
                    {
                        calculations.Remove(calculation);
                    }
                }
            }
            return View(calculations);
        }
        [Authorize]
        public IActionResult InterestCalculator()
        {
            ViewData["Message"] = "Contoso Corp. Interest Calculator.";
            InterestFormValues values = new InterestFormValues(HttpContext.User.Identity.Name.ToString(), _context);
            ViewBag.Message = values;
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult InterestCalculator(InterestFormValues formValues)
        {
            bool AllowedPrincipalValues = formValues.ValidatePrincipleInput(formValues.Principle);
            bool AllowedRateValues = formValues.ValidatePrincipleInput(formValues.rate);
            bool AllowedTimeValues = formValues.ValidatePrincipleInput(formValues.Time);

            if (ModelState.IsValid)
            {
                if (AllowedPrincipalValues == true && AllowedRateValues == true && AllowedTimeValues == true)
                {
                    InterestFormValues values = new InterestFormValues(HttpContext.User.Identity.Name.ToString(), _context);
                    values.Principle = formValues.Principle;
                    values.rate = formValues.rate;
                    values.Time = formValues.Time;
                                        
                    values.Interest = values.CalculateInterest();
                    values.Total = values.CalculateTotal();
                    ViewBag.Message = values;
                    SaveToDB(values);
                }                
            }
            
            return View();
        }

        public void SaveToDB(InterestFormValues calculation)
        {

            _context.InterestFormValues.Add(calculation);
            _context.SaveChanges();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "our contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
