using System;
using System.Collections.Generic;
using System.Linq;

namespace SydneyHotel
{
    class Program
    {
        class ReservationDetail
        {
            public string customerName { get; set; }
            public int nights { get; set; }
            public string roomService { get; set; }
            public double totalPrice { get; set; }
        }

        // Calculation of room service charges
        static double Price(int night, string isRoomService)
        {
            double price = 0;

            // Robust calculation based on number of nights
            if (night >= 1 && night <= 3)
                price = 100 * night;
            else if (night >= 4 && night <= 10)
                price = 80.5 * night;
            else if (night >= 11 && night <= 20)
                price = 75.3 * night;

            // Add 10% if room service requested
            if (isRoomService.ToLower() == "yes")
                return price * 1.1;
            else
                return price;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(".................Welcome to Sydney Hotel...............");
            Console.Write("\nEnter number of customers: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n--------------------------------------------------------------------\n");

            ReservationDetail[] rd = new ReservationDetail[n];

            for (int i = 0; i < n; i++)
            {
                rd[i] = new ReservationDetail();

                Console.Write("Enter customer name: ");
                rd[i].customerName = Console.ReadLine();

                // Input for number of nights with validation
                Console.Write("Enter the number of nights: ");
                rd[i].nights = Convert.ToInt32(Console.ReadLine());
                while (!(rd[i].nights > 0 && rd[i].nights <= 20))
                {
                    Console.Write("Number of nights should be between 1 to 20. Enter again: ");
                    rd[i].nights = Convert.ToInt32(Console.ReadLine());
                }

                // Input for room service with validation
                Console.Write("Enter yes/no to indicate whether you want a room service: ");
                rd[i].roomService = Console.ReadLine().ToLower();
                while (rd[i].roomService != "yes" && rd[i].roomService != "no")
                {
                    Console.Write("Invalid input! Enter 'yes' or 'no': ");
                    rd[i].roomService = Console.ReadLine().ToLower();
                }

                // Calculate total price
                rd[i].totalPrice = Price(rd[i].nights, rd[i].roomService);
                Console.WriteLine($"The total price for {rd[i].customerName} is ${rd[i].totalPrice}");
                Console.WriteLine("\n--------------------------------------------------------------------");
            }

            // Find min and max spenders
            var minIndex = rd.Select((x, i) => new { x.totalPrice, i }).OrderBy(x => x.totalPrice).First().i;
            var maxIndex = rd.Select((x, i) => new { x.totalPrice, i }).OrderByDescending(x => x.totalPrice).First().i;

            ReservationDetail minrd = rd[minIndex];
            ReservationDetail maxrd = rd[maxIndex];

            // Display summary
            Console.WriteLine("\n\t\t\t\tSummary of reservation");
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("Name\t\tNumber of nights\t\tRoom service\t\tCharge");
            Console.WriteLine($"{minrd.customerName}\t\t\t{minrd.nights}\t\t\t{minrd.roomService}\t\t\t{minrd.totalPrice}");
            Console.WriteLine($"{maxrd.customerName}\t\t{maxrd.nights}\t\t\t{maxrd.roomService}\t\t\t{maxrd.totalPrice}");
            Console.WriteLine("\n--------------------------------------------------------------------\n");
            Console.WriteLine($"The customer spending most is {maxrd.customerName} ${maxrd.totalPrice}");
            Console.WriteLine($"The customer spending least is {minrd.customerName} ${minrd.totalPrice}");
            Console.WriteLine("Press any key to continue....");
            Console.ReadLine();
        }
    }
}
