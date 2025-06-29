using System;
using System.Collections.Generic;

namespace API.Models;

public partial class InsuredDetail
{
    public decimal Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Dob { get; set; }

    public int? AgeInYears { get; set; }

    public decimal? Mobile { get; set; }

    public virtual ICollection<PolicyDetail> PolicyDetails { get; } = new List<PolicyDetail>();
}
