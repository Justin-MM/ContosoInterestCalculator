using ContosoInterestCalculator.Data;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace ContosoInterestCalculator.Controllers
{
    
    public class InterestFormValues
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Principal")]
        public float Principle { get; set; }
        [Required]
        [Display(Name = "Rate per Annum")]
        public float rate { get; set; }
        [Required]
        [Display(Name = "Time in Months")]
        public float Time { get; set; }
        [Display(Name = "Total")]
        public float Total { get; set; }
        public float Interest { get; set; }
        
        [EmailAddress]
        public string CalculatedBy { get; set; }
        
        public string ErrorMessage { get; set; }

        private readonly ApplicationDbContext _context;

        public InterestFormValues()
        {

        }

        public InterestFormValues(string userEmail, ApplicationDbContext context)
        {
            CalculatedBy = userEmail;
            _context = context;
        }

        public bool ValidatePrincipleInput(float Principle)
        {
            bool status = false;
                                    
            if (Principle > 0)
            {
                status = true;
            }
            return status;
        }

        public bool ValidateRateInput(float Rate)
        {
            bool status = false;

            if (rate > 0)
            {
                status = true;
            }
            return status;
        }

        public bool ValidateTimeInput(float Time)
        {
            bool status = false;

            if (Time > 0)
            {
                status = true;
            }
            return status;
        }



        public float CalculateTotal()
        {
            Total = (Principle * (rate / 100) * (Time/12)) + Principle;
            return Total;
        }

        public float CalculateInterest()
        {
            Interest = (Principle * (rate / 100) * (Time/12));
            
            return Interest;
        }

        
    }
}