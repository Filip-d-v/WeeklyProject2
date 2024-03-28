// I know i dont have to include these imports, but i still want them to show incase.
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagement
{
    class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Category}) - ${Price}";
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();

        static void Main(string[] args)
        {
            Console.WriteLine("Product Manager");

            while (true)
            {
                ShowMenu();
                Console.Write("What do you want to do?: ");
                string menuInput = Console.ReadLine();

                switch (menuInput.ToLower())
                {
                    case "1":
                        Console.Clear();
                        AddProduct();
                        break;
                    case "2":
                        Console.Clear();
                        ShowProductList();
                        Console.WriteLine("Press Enter to return to menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        SearchProduct();
                        break;
                    case "4":
                        Console.Clear();
                        RemoveProduct();
                        break;
                    case "5":
                    case "q":
                        Environment.Exit(0);            //Exits the menu/application
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a new product");
            Console.WriteLine("2. Show current list");
            Console.WriteLine("3. Search for a product");
            Console.WriteLine("4. Remove a product");
            Console.WriteLine("5. Exit");
        }
        //Add product to list method
        static void AddProduct()
        {
            Console.WriteLine("\nEnter product details, 'q' to quit:");
            Console.Write("Category: ");
            string category = Console.ReadLine();
            if (category.ToLower() == "q") return;

            Console.Write("Product Name: ");
            string name = Console.ReadLine();
            if (name.ToLower() == "q") return;

            double price;
            Console.Write("Price: ");
            string priceInput = Console.ReadLine();
            if (priceInput.ToLower() == "q") return;

            if (!double.TryParse(priceInput, out price))        //Error catch if not number as price
            {
                Console.WriteLine("Invalid price! Please enter a valid number");
                return;
            }

            Product newProduct = new Product { Category = category, Name = name, Price = price };
            products.Add(newProduct);

            Console.WriteLine("\nProduct added successfully!");
        }
        //Display List method
        static void ShowProductList()
        {
            Console.WriteLine("===================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCurrent Product List:");
            if (products.Any())
            {
                var sortedProducts = products.OrderBy(p => p.Price);
                foreach (var product in sortedProducts)
                {
                    Console.WriteLine(product);
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Total Price: ${products.Sum(p => p.Price)}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("List is empty");
            }
            Console.ResetColor();
        }
        // Search method
        static void SearchProduct()
        {
            Console.Write("\nEnter search keyword: ");
            string searchKeyword = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                bool found = false;

                Console.WriteLine("\nCurrent Product List:");
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        string productInfo = $"{product.Name} ({product.Category}) - ${product.Price}";
                        if (product.Name.ToLower().Contains(searchKeyword.ToLower()) || product.Category.ToLower().Contains(searchKeyword.ToLower()))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(productInfo);
                            Console.ResetColor();
                            found = true;
                        }
                        else
                        {
                            Console.WriteLine(productInfo);
                        }
                    }
                    Console.WriteLine($"Total Price: ${products.Sum(p => p.Price)}");
                }
                else
                {
                    Console.WriteLine("List is empty");
                }

                if (!found)
                {
                    Console.WriteLine("Product not found");
                }
            }
            else
            {
                Console.WriteLine("Invalid search keyword");
            }
        }
        // Remove method
        static void RemoveProduct()
        {
            Console.Write("\nEnter product name to remove: ");
            string nameToRemove = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nameToRemove))
            {
                var productToRemove = products.FirstOrDefault(p => p.Name.Equals(nameToRemove, StringComparison.OrdinalIgnoreCase));
                if (productToRemove != null)
                {
                    products.Remove(productToRemove);
                    Console.WriteLine("Product removed successfully!");
                }
                else
                {
                    Console.WriteLine("Product not found");
                }
            }
            else
            {
                Console.WriteLine("Invalid product name");
            }
        }
    }
}
