using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserDetail
{
    public string UserName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;
}
