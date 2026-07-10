using June2026.Database.AppDbContextModels;
using June2026.EntityFrameworkCore;

June2026AppDbContext db = new June2026AppDbContext();
List<UserEntity> lst = db.Users.ToList();
//get all user
foreach (UserEntity item in lst)
{
    Console.WriteLine(item.userId);
    Console.WriteLine(item.name);
    Console.WriteLine("==================");
}
//get staff by id (if the user exist, return user and if not exist, return null)
//question mark ? is shown because we are using FirstOrDefault() method
UserEntity? user1 = lst.Where(user => user.userId == 1).FirstOrDefault();
UserEntity? user2 = lst.Where(user => user.userId == 1002).FirstOrDefault();

//create user
UserEntity userEntity = new UserEntity()
{
    name = "Hla Hla"
};
db.Users.Add(userEntity);
db.SaveChanges();

//update
//we need to find that user exist or not in the db before update
UserEntity? existingUser = db.Users.Where(user => user.userId == 1004).FirstOrDefault();
if(existingUser == null)
{
    Console.WriteLine("User is not found");
}
else
{
    existingUser.name = "Hla Hla Win";
    db.SaveChanges();
}
//delete user
UserEntity? existingUser1 =  db.Users.Where(user => user.userId == 1004).FirstOrDefault();
if(existingUser1 == null)
{
   Console.WriteLine("User is not found");
}
else
{
    db.Users.Remove(existingUser1);
    db.SaveChanges();
}

AppDbContext db2 = new AppDbContext();
db2.TblStaffs.ToList();
db2.TblStudents.ToList();
db2.TblUsers.ToList();