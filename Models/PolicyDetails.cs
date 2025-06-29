using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PolicyDetails
    {
        public InsurerDetails insurerDetails { get; set; }
        private int _tenure;
        public string PolicyNumber { get; set; }
        public string Scheme { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Tenure { 
            get {
                DateTime startDate = DateTime.Parse(StartDate);
                DateTime endDate = DateTime.Parse(EndDate);

                return endDate.Year - startDate.Year;
            }
            set
            {
            }
        }
        public decimal YearlyPremium { get; set; }

    }
}