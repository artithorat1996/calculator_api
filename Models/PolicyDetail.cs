using System;
using System.Collections.Generic;

namespace API.Models;

public partial class PolicyDetail
{
    public decimal Id { get; set; }

    public decimal? InsuredId { get; set; }

    public string? PolicyNumber { get; set; }

    public string? Scheme { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Tenure { get; set; }

    public decimal? YearlyPremium { get; set; }

    public virtual InsuredDetail? Insured { get; set; }
}
