//Read from Tbl_Customer
using June2026.EFCoreHomework.Database.AppDbContextModels;

HomeworkAppDbContext db = new HomeworkAppDbContext();
List<TblCustomer> lst = db.TblCustomers.ToList();
foreach (TblCustomer item in lst)
{
    Console.WriteLine(item.CustomerId);
    Console.WriteLine(item.CustomerName);
    Console.WriteLine(item.CustomerEmail);
    Console.WriteLine(item.Phone);
}
/* ================================================================================
   After deleting the second table (Tbl_Department) from the database and running 
   the scaffold command again with the --force flag, EF Core completely overwrote 
   the DbContext file. 
   
   As a result, DbSet<TblDepartment> was removed from HomeworkAppDbContext.cs. 
   This causes the existing C# read code for Tbl_Department to throw a compile-time 
   error. (Note: The Tbl_Department.cs model file is left orphaned in the Models 
   folder, but it is no longer linked to the database context).
   
   The code below has been commented out to demonstrate this expected error.
================================================================================ */
//Read from Tbl_Department
// List<TblDepartment> lst1 = db.TblDepartments.ToList();
// foreach (TblDepartment item in lst1)
// {
//     Console.WriteLine(item.DepartmentId);
//     Console.WriteLine(item.DepartmentName);
// }

//Read From Tbl_Vehichle
List<TblVehicle> lst2 = db.TblVehicles.ToList();
foreach (TblVehicle item in lst2)
{
    Console.WriteLine(item.VehicleId);
    Console.WriteLine(item.VehicleType);
    
}