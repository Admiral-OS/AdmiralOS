using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.Threading;
using System.IO;

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
            initinput = Console.ReadLine().ToLower();

            if (initinput == "y")
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
            else if (initinput == "n")
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
            string[] args;
            Console.Write("0:\\");
            input = Console.ReadLine().ToLower();
            args = input.Split(' ');


            if (args[0] == "help")
            {
                Console.WriteLine("Help:");
                Console.WriteLine("Commands are not CasE-sEnSiTiVe");
                Console.WriteLine("About - Tells you about the system");
                Console.WriteLine("Shutdown - Turns off the system (Does not work in QEMU)");
                Console.WriteLine("Reboot - Reboots your system (Does not work in QEMU)");
                Console.WriteLine("Dir - Shows list of files and folders");
                Console.WriteLine("Cat - Displays content of files");
                Console.WriteLine("Clear - Clears the screen");
            }
            else if (args[0] == "about")
            {
                Console.WriteLine("Admiral OS - Alpha 0.1");
                Console.WriteLine("Copyright 2022 BritishGeekGuy");
                Console.WriteLine("This Operating System's Soruce Code is licensed under GPL-3.0");
            }
            else if (args[0] == "shutdown")
            {
                Cosmos.System.Power.Shutdown();
            }
            else if (args[0] == "reboot")
            {
                Cosmos.System.Power.Reboot();
            }
            else if (args[0] == "dir")
            {
                    try
                    {
                        long availableSpace = Sys.FileSystem.VFS.VFSManager.GetAvailableFreeSpace("0:\\");
                        var directoryList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing("0:\\" + Directory.GetCurrentDirectory());

                        foreach (var directoryEntry in directoryList)
                        {
                            Console.Write("   ");
                            Console.WriteLine(directoryEntry.mName);
                        }

                        Console.WriteLine(availableSpace + " Bytes Free");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    
            }
            else if (args[0] == "cat")
            {
                try
                {
                    Console.WriteLine(File.ReadAllText("0:\\" + Directory.GetCurrentDirectory() + "\\" + args[1]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (args[0] == "clear")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Bad Command!");
            }
        }
    }
}
