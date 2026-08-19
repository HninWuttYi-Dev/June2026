using System.Text;
using June2026.ConsoleApp3.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

HttpClient client = new HttpClient();
Start:
Console.WriteLine("========User list=========");
Console.WriteLine("1. View all users: ");
Console.WriteLine("2. Add new user");
Console.WriteLine("3. Update existing user");
Console.WriteLine("4. Delete User");
Console.WriteLine("5. Exit the program");
int choiceNumber;
Console.Write("Choose an option by typing number: ");
string strNumber = Console.ReadLine();
choiceNumber = Convert.ToInt32(strNumber);
HttpResponseMessage response;
switch (choiceNumber)
{
   case 1:
      {
         response = await client.GetAsync("http://localhost:5201/api/User");
         if (response.IsSuccessStatusCode)
         {
            string content = await response.Content.ReadAsStringAsync();  //raw to JSON string
            var responseModel = JsonConvert.DeserializeObject<UserListResponseModel>(content); //JSON to object
            if (responseModel != null && responseModel.isSuccess && responseModel.Users != null)
            {
               int count = 0;
               foreach (var item in responseModel.Users)
               {
                  Console.WriteLine($"{++count}: UserId: {item.UserId}, Username: {item.Username}");
               }
            }
            else
            {
               Console.WriteLine(responseModel?.Message ?? "Failed to retrieve users.");
            }
         }
      ;
      }
      break;
   case 2:
      {
         Console.Write("Enter Username: ");
         string username = Console.ReadLine();
         Console.Write("Enter Password: ");
         string password = Console.ReadLine();
         UserCreateRequestModel requestModel = new UserCreateRequestModel
         {
            Username = username!,
            Password = password!
         };
         string json = JsonConvert.SerializeObject(requestModel);
         StringContent stringContent = new StringContent(json, Encoding.UTF8, Application.Json);
         response = await client.PostAsync("http://localhost:5201/api/User", stringContent);
         if (response.IsSuccessStatusCode)
         {
            string content = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<UserCreateResponseModel>(content);
            Console.WriteLine(responseModel.Message);
         }
      }
      break;
   case 3:
      {
         Console.Write("Enter User Id: ");
         string userId = Console.ReadLine();
         Console.Write("Update username: ");
         string username = Console.ReadLine();
         Console.Write("Update password: ");
         string password = Console.ReadLine();
         UserPatchRequestModel requestModel = new UserPatchRequestModel
         {
            Username = username,
            Password = password
         };
         string json = JsonConvert.SerializeObject(requestModel);
         var stringContent = new StringContent(json, Encoding.UTF8, Application.Json);
         response = await client.PatchAsync($"http://localhost:5201/api/User/{userId}", stringContent);
         if (response.IsSuccessStatusCode)
         {
            string content = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<UserPatchResponseModel>(content);
            Console.WriteLine(responseModel.Message);
         }

      }
      break;
   case 4:
      {
         Console.Write("Enter User Id: ");
         string userId = Console.ReadLine();
         response = await client.DeleteAsync($"http://localhost:5201/api/User/{userId}");
         if (response.IsSuccessStatusCode)
         {
            string content = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<UserDeleteResponseModel>(content);
            Console.WriteLine(responseModel.Message);
         }
      }
      break;
   case 5:
      goto Exit;
   default:
      Console.WriteLine("Invalid Choice, try again");
      goto Start;
}

goto Start;
Exit:
Console.WriteLine("Existing the program");
return;