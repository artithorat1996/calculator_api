using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class InsurerDetails
    {
        public decimal Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? Dob { get; set; }

        public int? AgeInYears { get; set; }

        public decimal? Mobile { get; set; }
        //public int AgeInYears { 
        //    get
        //    {
        //        DateTime dob = DateTime.Parse(DOB);
        //        return DateTime.Now.Year - dob.Year;
        //    } 
        //    set
        //    {

        //    } 
        //}
    }
}