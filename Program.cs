using System;
using System.Collections.Generic;
using EcoSyncSmartWasteNetwork.Models;
using EcoSyncSmartWasteNetwork.Services;
using EcoSyncSmartWasteNetwork.Interfaces;

namespace EcoSyncSmartWasteNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Eco-Sync Smart Waste Network Simulation Started...");

            var bins = new List<WasteBin>
            {
                new RecyclableBin("R1", "Manila"),
                new HazardousBin("H1", "Quezon City"),
                new OrganicBin("O1", "Makati")
            };

            ILogger logger = new ConsoleLogger();
            var truckService = new TruckDispatchService(logger);
            var analyticsService = new AnalyticsService(logger);

            foreach (var bin in bins)
            {
                bin.BinFull += truckService.OnBinFull;
                bin.BinFull += analyticsService.OnBinFull;
            }

            foreach (var bin in bins)
            {
                for (int i = 0; i < 6; i++)
                {
                    bin.AddWaste(10.0);
                    logger.Log($"{bin.BinID} impact: {bin.CalculateImpact()}");
                }
            }

            Console.WriteLine("Simulation complete.");
        }
    }
}
