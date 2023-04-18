using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Account
{
    public string EmployeeNik { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee EmployeeNikNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AccountRole>? TbTrAccountRoles { get; set; } = new List<AccountRole>();
}
