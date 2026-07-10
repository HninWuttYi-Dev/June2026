using System;
using System.Collections.Generic;

namespace June2026.EFCoreHomework.Database.AppDbContextModels;

public partial class TblVehicle
{
    public int VehicleId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string? VehicleType { get; set; }
}
