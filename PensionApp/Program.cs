using System;
using Grpc.Net.Client;
using PensionServiceApp;
/*
 * Project: Final Practical gRPC
 * Purpose: Demonstrate learning and understanding of gRPC
 * Coder: Sonia Friesen, 0813682        
 * Date: April 19th 2021
 */
namespace PensionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var pension = new Pension.PensionClient(channel);

            //write out to collect daata
            Console.WriteLine("Full Pension Estimator by Sonia Friesen\n");
            //create a request
            PensionRequest request = new PensionRequest();
            Console.Write("Age at retirement: ");
            string age = Console.ReadLine();
            request.Age = int.Parse(age);           
            Console.Write("Years of pensionable service at retirement: ");
            string YearsOfService = Console.ReadLine();
            request.NumYears = int.Parse(YearsOfService);            
            Console.Write("Average annual salary at retirement: ");
            string salary = Console.ReadLine();
            request.Salary = int.Parse(salary);
            Console.WriteLine("");

            PensionResponse response = pension.GetFullPensionAmount(request);
            String BaseAmount = response.BaseAmount.ToString("C");
            String BridgeAmount = response.BridgeAmount.ToString("C");
            Console.WriteLine($"Your anunal base pension is {BaseAmount}");
            Console.WriteLine($"Your annual bridge amount is {BridgeAmount}");
            Console.WriteLine();
            Console.WriteLine("Thansk for using, By Sonia Friesen 0813682");

        }
    }
}
