using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PolicyData
    {
        List<PolicyDetails> policyDetailsList = new List<PolicyDetails>();

        public List<PolicyDetails> GetPolicies()
        {
            //policyDetailsList.Add(new PolicyDetails {
            //    PolicyNumber = "C1234567899",
            //    Scheme = "Maharaksha Supreme",
            //    StartDate = "01/01/2000",
            //    EndDate = "31/12/2050",
            //    YearlyPremium = 40000M,
            //    insurerDetails = new InsurerDetails{
            //        FirstName = "Pravin",
            //        LastName = "Ghule",
            //        DOB = "12/10/1992"
            //    }
            //});

            //policyDetailsList.Add(new PolicyDetails {
            //    PolicyNumber = "U1234567899",
            //    Scheme = "Raksha Supreme",
            //    StartDate = "01/01/2010",
            //    EndDate = "31/12/2040",
            //    YearlyPremium = 30000M,
            //    insurerDetails = new InsurerDetails{
            //        FirstName = "Vishal",
            //        LastName = "Dukare",
            //        DOB = "27/09/1992"
            //    }
            //});

            return policyDetailsList;
        }
    }
}