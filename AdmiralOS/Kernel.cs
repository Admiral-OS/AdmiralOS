using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.Threading;

namespace AdmiralOS
{
    public class Kernel : Sys.Kernel
    {
        CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Would you like to Format the Hard Drive in this system to FAT32?");
            Console.WriteLine("This is required for all Filesystem commands to work");
            Console.Write("Y/N? ");

            string initinput;
            string initinLow;
            initinput = Console.ReadLine();
            initinLow = initinput.ToLower();

            if (initinLow == "y")
            {
                try
                {
                    Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                    Thread.Sleep(3000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(3000);
                }
                
            }
            else if (initinLow == "n")
            {
                Console.WriteLine("Skipping...");
                Thread.Sleep(3000);
            }
            
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine("AdmiralOS - Alpha V0.1");
            Console.Beep();
        }

        protected override void Run()
        {
            string input;
            string inLow;
            Console.Write("0:\\");
            input = Console.ReadLine();
            inLow = input.ToLower();

            if (inLow == "help")
            {
                Console.WriteLine("Help:");
            }
            else if (inLow == "about")
            {
                Console.WriteLine("Admiral OS - Alpha 0.1");
                Console.WriteLine("Copyright 2022 BritishGeekGuy");
                Console.WriteLine("This Operating System's Soruce Code is licensed under GPL-3.0");
            }
            else
            {
                Console.WriteLine("Bad Command!");
            }
        }
    }
}
