using System;
using System.Collections.Generic;

namespace June2026.EFCoreHomework.Database.AppDbContextModels;

public partial class TblCustomer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? CustomerEmail { get; set; }

    public string? Phone { get; set; }
}
