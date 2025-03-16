namespace Homework_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User("Pesho", 100.00);
            SpecialUser user2 = new SpecialUser("Gosho", 200.00);
            Console.WriteLine("Enter sum in Leva: ");
            double bgnSum = double.Parse(Console.ReadLine());

            Console.WriteLine($"BGN - {bgnSum} is equal to {BGN.ConvertToEuro(bgnSum)} Euro");
            

            user1.GetInfo();
            user2.GetInfo();
        }
    }

    internal static class BGN
    {
        public static readonly string code = "BGN";
        private static double bgnToUsd = 0.56;
        private static double bgnToEur = 0.51;

        public static double ConvertToUsDollar(double bgnSum)
        {
            return bgnSum * bgnToUsd;
        }

        public static double ConvertToEuro(double bgnSum)
        {
            return bgnSum * bgnToEur;
        }
    }
    internal static class USD
    {
        public static readonly string code = "USD";
        private static double usdToEur = 0.9;
        private static double usdToBgn = 1.8;

        public static double ConvertToEuro(double usdSum)
        {
            return usdSum * usdToEur;
        }

        public static double ConvertToBgn(double usdSum)
        {
            return usdSum * usdToBgn;
        }
    }
    internal static class EUR
    {
        public static readonly string code = "EUR";
        private static double eurToUsd = 1.09;
        private static double eurToBgn = 1.96;

        public static double ConvertToUsDollar(double eurSum)
        {
            return eurSum * eurToUsd;
        }

        public static double ConvertToBgn(double eurSum)
        {
            return eurSum * eurToBgn;
        }
    }
    internal class User
    {
        private string _name;
        private double _balance;

        public string Name { get { return _name; } }
        public double Balance { 
            get { return _balance; } 
            set 
            {
                if (value < 0)
                {
                    throw new Exception("Balance can't be negative");
                }
                else
                {
                    _balance = value;
                }
            } 
        }

        public User(string name, double balance)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty or null.");

            _name = name;
            Balance = balance;
        }

        public virtual void GetInfo()
        {
            Console.WriteLine($"Name: {Name}; Balance: {Balance}"); 
        }
    }

    internal class SpecialUser : User 
    {
        public SpecialUser(string name, double balance) : base(name, balance) { }

        public override void GetInfo()
        {

            Console.WriteLine($"Name: {Name}; Balance: {Balance} Hello");

        }
    }
    
}
