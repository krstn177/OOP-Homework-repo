using System;
using System.Collections.Generic;

namespace Pizzaria
{
    class Pizza
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int DoughWeight { get; set; }
        public int IngredientWeight { get; set; }
        public string IngredientName { get; set; }
        public int PricePerPizza { get; set; }

        public Pizza(string type, string size, int quantity)
        {
            Type = type;
            Size = size;
            Quantity = quantity;

            if (type == "Margarita")
            {
                IngredientName = "Tomatoes";
                switch (size)
                {
                    case "small": DoughWeight = 300; PricePerPizza = 5; break;
                    case "medium": DoughWeight = 500; PricePerPizza = 10; break;
                    case "large": DoughWeight = 800; PricePerPizza = 15; break;
                    default: throw new ArgumentException("Invalid size");
                }
                IngredientWeight = 1; // One tomato per pizza
            }
            else if (type == "Boss` Pizza")
            {
                IngredientName = "Ham";
                switch (size)
                {
                    case "small": DoughWeight = 300; PricePerPizza = 20; break;
                    case "medium": DoughWeight = 500; PricePerPizza = 25; break;
                    case "large": DoughWeight = 800; PricePerPizza = 30; break;
                    default: throw new ArgumentException("Invalid size");
                }
                IngredientWeight = 100; // 100g of ham per pizza
            }
            else
            {
                throw new ArgumentException("Invalid pizza type");
            }
        }

        public void PrintPreparation()
        {
            Console.WriteLine($"{Type} preparing…");
            Console.WriteLine($"Pizza dough {Quantity}*{DoughWeight} = {Quantity * DoughWeight}gr");
            Console.WriteLine($"{IngredientName} {Quantity}*{IngredientWeight} = {Quantity * IngredientWeight}{(IngredientName == "Tomatoes" ? "" : "gr")}");
            Console.WriteLine($"Total: ${Quantity * PricePerPizza}\n");
        }
    }

    class Order
    {
        public string Date { get; set; }
        public List<Pizza> Pizzas { get; set; }

        public Order(string date)
        {
            Date = date;
            Pizzas = new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
        }

        public int TotalIncome()
        {
            int total = 0;
            foreach (var pizza in Pizzas)
            {
                total += pizza.Quantity * pizza.PricePerPizza;
            }
            return total;
        }

        public int CountPizzaType(string type)
        {
            int count = 0;
            foreach (var pizza in Pizzas)
            {
                if (pizza.Type == type)
                    count += pizza.Quantity;
            }
            return count;
        }

        public void PrintSummary()
        {
            Console.WriteLine($"{Date}");
            Console.WriteLine($"Total pizzas {Pizzas.Count}");
            Console.WriteLine($"Margarita {CountPizzaType("Margarita")}");
            Console.WriteLine($"Boss` Pizza {CountPizzaType("Boss` Pizza")}");
            Console.WriteLine($"Total Income = {TotalIncome()}\n");
        }
    }

    class Program
    {
        static void Main()
        {
            List<Order> orders = new List<Order>();

            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                string[] parts = input.Split(' ');

                string pizzaType;
                string size;
                string date;
                int quantity;

                if (parts.Length == 5)
                {
                    pizzaType = parts[1]; // Handle spaces in pizza names
                    if (!int.TryParse(parts[2], out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity!");
                        continue;
                    }

                    size = parts[3];
                    date = parts[4];
                }
                else if(parts.Length == 6)
                {
                    pizzaType = parts[1] + " " + parts[2]; // Handle spaces in pizza names
                    if (!int.TryParse(parts[3], out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity!");
                        continue;
                    }

                    size = parts[4];
                    date = parts[5];
                }
                else
                {
                    continue;

                }

                try
                {
                    Pizza pizza = new Pizza(pizzaType, size, quantity);
                    pizza.PrintPreparation();

                    // Find or create order for the specific date
                    Order order = orders.Find(o => o.Date == date);
                    if (order == null)
                    {
                        order = new Order(date);
                        orders.Add(order);
                    }
                    order.AddPizza(pizza);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Print summary
            Console.WriteLine("\nCash register reset:");
            foreach (var order in orders)
            {
                order.PrintSummary();
            }
        }
    }

}

