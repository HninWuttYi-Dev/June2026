using June2026.NLayer.BLL;

class Program
{
    static void Main(string[] args)
    {
        UserService userService = new UserService();
        while(true)
        {
            Console.WriteLine("1. Add new user");
            Console.WriteLine("2. View User List");
            Console.Write("Choose 1 or 2: ");
            var strChoice = Console.ReadLine();
            int choiceNo = Convert.ToInt32(strChoice);
            if(choiceNo == 1)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                try
                {
                    userService.RegisterUser(name, email);
                    Console.WriteLine("User is added successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            } 
            else if(choiceNo == 2)
            {
                var users = userService.GetUserList();
                Console.WriteLine("========User List========");
                foreach (var item in users)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Email: {item.Email}");
                }
            }
        }
    }
}