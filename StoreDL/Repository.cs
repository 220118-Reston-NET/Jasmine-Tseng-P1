// using System.Text.Json;
// using StoreModel;

// namespace StoreDL
// {
//     public class Repository : IRepository
//     {
//         //Relative filepath is from the StoreUI since that is the starting point of our application
//         private string _filepath = "../StoreDL/Database/";
//         private string _jsonString;

//         {
//             //So we can change which JSON files we can pick on other methods
//             string path = _filepath + "Inventory.json";
//             List<ItemClass> listOfItem = GetAllItem();
//             listOfItem.Add(pp_item);

//             _jsonString = JsonSerializer.Serialize(listOfItem, new JsonSerializerOptions {WriteIndented = true});

//             File.WriteAllText(path , _jsonString);

//             return pp_item;
//         }

//         public List<ItemClass> GetAllItem()
//         {
//             //Grab information from the JSON file and store it in a string
//             _jsonString = File.ReadAllText(_filepath + "Inventory.json");

//             //Deserialize the jsonString into a List<ItemClass> object and return it
//             return JsonSerializer.Deserialize<List<ItemClass>>(_jsonString);
//         }
//     }
// }-------------------------------------

// using System.Text.Json;
// using StoreModel;

// namespace StoreDL
// {
//     public class Repository : IRepository
//     {
//         private string _filepath = "../StoreDL/Database/";
//         private string _jsonString;

//         public Customer AddCustomer(Customer p_customer)
//         {
//             string path = _filepath + "Customers.json"; // establishing the file path location to a string
//             _jsonString = JsonSerializer.Serialize(p_customer, new JsonSerializerOptions {WriteIndented = true}); // p_customer is the userinputed customer information being passed into JsonSerialize.serialize() method and initialized as _jsonString, after the comma is to make the inputted text to one line
//             File.WriteAllText(path, _jsonString); 
//             return p_customer; //the method was set to return the parameter
//         }

//         public List<Customer> GetAllCustomers()
//         {
//              _jsonString = File.ReadAllText(_filepath + "Customers.json"); //Grab information from the JSON file and store it in a string

            
//              return JsonSerializer.Deserialize<List<Customer>>(_jsonString); //Deserialize the jsonString into a List<ItemClass> object and return it
//         }
//     }
// }