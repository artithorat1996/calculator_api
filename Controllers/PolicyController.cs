using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyContext _context;
        public PolicyController(PolicyContext context)
        {
            _context = context;
        }

        [HttpGet("GetInsureds")]
        [Authorize]
        public async Task<ResponseModel> GetInsureds()
        {
            ResponseModel responseModel = new ResponseModel();
            List<InsurerDetails> insurerDetails = new List<InsurerDetails>();
            if (_context.InsuredDetails == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to fetch data!";
            }
            else
            {
                var insurers = await _context.InsuredDetails.Include(x => x.PolicyDetails).ToListAsync();

                insurerDetails = insurers
                    .Select(i => new InsurerDetails
                    {
                        Id = i.Id,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Dob = DateOnly.Parse(i.Dob),
                        AgeInYears = i.AgeInYears,
                        Mobile = i.Mobile
                    })
                    .ToList();

                //var policies = await _context.InsuredDetails.Select(x => x.PolicyDetails).ToListAsync();
                //var policy = await _context.InsuredDetails.Where(x=>x.Id == 1).Select(x => x.PolicyDetails).SingleAsync();

                responseModel.Status = "SUCCESS";
                responseModel.Message = "Data fetch successfully!";
                responseModel.Details = insurerDetails;
            }

            return responseModel;
        }

        [HttpGet("GetInsuredByMobile")]
        public async Task<ResponseModel> GetInsuredByMobile(string mobileNumber)
        {
            ResponseModel responseModel = new ResponseModel();
            InsurerDetails insurerDetails = new InsurerDetails();
            if (_context.InsuredDetails == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to fetch data!";
            }
            else
            {
                var insurers = await _context.InsuredDetails.Where(x => x.Mobile == Convert.ToDecimal(mobileNumber)).FirstOrDefaultAsync();

                if (insurers != null)
                {
                    insurerDetails =
                        new InsurerDetails
                        {
                            Id = insurers.Id,
                            FirstName = insurers.FirstName,
                            LastName = insurers.LastName,
                            Dob = DateOnly.Parse(insurers.Dob),
                            AgeInYears = insurers.AgeInYears,
                            Mobile = insurers.Mobile
                        };

                    responseModel.Status = "SUCCESS";
                    responseModel.Message = "Data fetch successfully!";
                    responseModel.Details = insurerDetails;
                }
                else
                {
                    responseModel.Status = "FAILED";
                    responseModel.Message = "Insured not found!";
                }

            }

            return responseModel;
        }

        [HttpGet("GetInsuredById")]
        public async Task<ResponseModel> GetInsuredById(decimal Id)
        {
            ResponseModel responseModel = new ResponseModel();
            InsurerDetails insurerDetails = new InsurerDetails();
            if (_context.InsuredDetails == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to fetch data!";
            }
            else
            {
                var insurers = await _context.InsuredDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (insurers != null)
                {
                    insurerDetails =
                        new InsurerDetails
                        {
                            Id = insurers.Id,
                            FirstName = insurers.FirstName,
                            LastName = insurers.LastName,
                            Dob = DateOnly.Parse(insurers.Dob),
                            AgeInYears = insurers.AgeInYears,
                            Mobile = insurers.Mobile
                        };

                    responseModel.Status = "SUCCESS";
                    responseModel.Message = "Data fetch successfully!";
                    responseModel.Details = insurerDetails;
                }
                else
                {
                    responseModel.Status = "FAILED";
                    responseModel.Message = "Insured not found!";
                }
            }

            return responseModel;
        }

        [HttpPost("CreateInsurer")]
        public async Task<ResponseModel> CreateInsurer(NewInsurer newInsurer)
        {
            ResponseModel responseModel = new ResponseModel();
            decimal insertedId = 0;
            if (newInsurer == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to create insurer!";
            }
            else
            {
                try
                {
                    var test = newInsurer.Dob.ToString();
                    InsuredDetail insuredDetail = new InsuredDetail
                    {
                        FirstName = newInsurer.FirstName,
                        LastName = newInsurer.LastName,
                        Dob = newInsurer.Dob.ToString(), //Convert.ToDateTime(newInsurer.Dob)
                        Mobile = newInsurer.Mobile,
                        AgeInYears = DateTime.Now.Year - newInsurer.Dob.Value.Year,
                        Id = 0
                    };

                    _context.InsuredDetails.Add(insuredDetail);
                    await _context.SaveChangesAsync();

                    insertedId = insuredDetail.Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                responseModel.Status = "SUCCESS";
                responseModel.Message = "Insurer created successfully!";
                responseModel.Details = insertedId;
            }

            return responseModel;
        }

        [HttpPut("UpdateInsurer")]
        public async Task<ResponseModel> UpdateInsurer(UpdateInsurerDetails updatedInsurer)
        {
            ResponseModel responseModel = new ResponseModel();
            if (updatedInsurer == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to create insurer!";
                return responseModel;
            }
            else
            {
                try
                {
                    var existingInsurer = await _context.InsuredDetails.FindAsync(updatedInsurer.Id);
                    if (existingInsurer == null)
                    {
                        responseModel.Status = "FAILED";
                        responseModel.Message = "Insurer not found!";

                        return responseModel;
                    }

                    existingInsurer.FirstName = updatedInsurer.FirstName;
                    existingInsurer.LastName = updatedInsurer.LastName;
                    existingInsurer.Dob = updatedInsurer.Dob.ToString();
                    existingInsurer.AgeInYears = DateTime.Now.Year - updatedInsurer.Dob.Value.Year;

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                responseModel.Status = "SUCCESS";
                responseModel.Message = "Insurer details updated successfully!";
                responseModel.Details = "";
            }

            return responseModel;
        }

        [HttpDelete("DeleteInsurer")]
        public async Task<ResponseModel> DeleteInsurer(int Id)
        {
            ResponseModel responseModel = new ResponseModel();
            if (Id == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Failed to delete insurer!";
                return responseModel;
            }
            else
            {
                try
                {
                    var existingInsurer = await _context.InsuredDetails.FindAsync(Convert.ToDecimal(Id));
                    if (existingInsurer == null)
                    {
                        responseModel.Status = "FAILED";
                        responseModel.Message = "Insurer not found!";

                        return responseModel;
                    }

                    _context.InsuredDetails.Remove(existingInsurer);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                responseModel.Status = "SUCCESS";
                responseModel.Message = "Insurer deleted successfully!";
                responseModel.Details = "";
            }

            return responseModel;
        }

        PolicyData policyData = new PolicyData();
        ResponseModel responseModel = new ResponseModel();
        [HttpGet("GetPolicyDetails")]
        public ResponseModel GetPolicyDetails(string PolicyNumer)
        {
            string policyPattern = @"^[CU]\d{10}$";

            if (!Regex.IsMatch(PolicyNumer, policyPattern))
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Invalid policy number!";

                return responseModel;
            }

            var policyDetails = policyData.GetPolicies().SingleOrDefault(x => x.PolicyNumber == PolicyNumer);

            if (policyDetails == null)
            {
                responseModel.Status = "FAILED";
                responseModel.Message = "Policy details not found!";
            }
            else
            {
                responseModel.Status = "SUCCESS";
                responseModel.Message = "Policy details fetch successfully!";
                responseModel.Details = policyDetails;
            }

            return responseModel;
        }
    }
}