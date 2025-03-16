using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_1
{
    internal class Test
    {

    }

    public static class Vehicle 
    {
        public static void GetTime()
        {
            Console.WriteLine(DateTime.Now);
        } 
    }
    

    public class Car
    {
        public static int Id { get; set; }
        public Car(int id) 
        {
            Id = id;
        }
    }
}
