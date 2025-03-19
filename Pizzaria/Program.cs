using System;
using System.Collections.Generic;

namespace Pizzaria
{
    interface IPizza
    {
        string Type { get; }
        string Size { get; }
        int Quantity { get; }
        int DoughWeight { get; }
        int IngredientWeight { get; }
        string IngredientName { get; }
        int PricePerPizza { get; }
        void PrintPreparation();
    }

    abstract class Pizza : IPizza
    {
        public abstract string Type { get; }
        public string Size { get; }
        public int Quantity { get; }
        public int DoughWeight { get; }
        public int IngredientWeight { get; protected set; }
        public string IngredientName { get; protected set; }
        public int PricePerPizza { get; protected set; }

        public Pizza(string size, int quantity)
        {
            Size = size;
            Quantity = quantity;

            switch (size.ToLower())
            {
                case "small": DoughWeight = 300; break;
                case "medium": DoughWeight = 500; break;
                case "large": DoughWeight = 800; break;
                default: throw new ArgumentException("Invalid size");
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
    class Margarita : Pizza
    {
        public override string Type { get; } = "Margarita";


        public Margarita(string size, int quantity) : base(size, quantity)
        {
            IngredientName = "Tomatoes";
            IngredientWeight = 1;

            PricePerPizza = size.ToLower() switch
            {
                "small" => 5,
                "medium" => 10,
                "large" => 15,
                _ => throw new ArgumentException("Invalid size")
            };
        }
    }
    class BossPizza : Pizza
    {
        public override string Type { get; } = "Boss' Pizza";

        public BossPizza(string size, int quantity) : base(size, quantity)
        {
            IngredientName = "Ham";
            IngredientWeight = 100;

            PricePerPizza = size.ToLower() switch
            {
                "small" => 20,
                "medium" => 25,
                "large" => 30,
                _ => throw new ArgumentException("Invalid size")
            };
        }
    }

    class Order
    {
        public string Date { get; set; }
        public List<IPizza> Pizzas { get; set; }

        public Order(string date)
        {
            Date = date;
            Pizzas = new List<IPizza>();
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
            Console.WriteLine($"Order Date: {Date}");
            Console.WriteLine($"Total Pizzas Ordered: {Pizzas.Sum(pizza => pizza.Quantity)}");

            var pizzaTypes = Pizzas.Select(p => p.Type).Distinct();
            foreach (var type in pizzaTypes)
            {
                Console.WriteLine($"{type}: {CountPizzaType(type)}");
            }

            Console.WriteLine($"Total Income: ${TotalIncome()}\n");
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
                    pizzaType = parts[1];
                    if (!int.TryParse(parts[2], out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity!");
                        continue;
                    }

                    size = parts[3];
                    date = parts[4];
                }
                else if (parts.Length == 6)
                {
                    pizzaType = parts[1] + " " + parts[2];
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
                    Pizza pizza;
                    switch (pizzaType.ToLower())
                    {
                        case "margarita":
                            pizza = new Margarita(size, quantity);
                            break;
                        case "boss` pizza":
                            pizza = new BossPizza(size, quantity);
                            break;
                        default:
                            Console.WriteLine("Invalid pizza type!");
                            continue;
                    }

                    pizza.PrintPreparation();

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

            Console.WriteLine("\nCash register reset:");
            foreach (var order in orders)
            {
                order.PrintSummary();
            }
        }
    }

}

