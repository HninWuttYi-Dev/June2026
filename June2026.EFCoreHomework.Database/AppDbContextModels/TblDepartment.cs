using System;
using System.Collections.Generic;

namespace June2026.EFCoreHomework.Database.AppDbContextModels;

public partial class TblDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Location { get; set; }
}
