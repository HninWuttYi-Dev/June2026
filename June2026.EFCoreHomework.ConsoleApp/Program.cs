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