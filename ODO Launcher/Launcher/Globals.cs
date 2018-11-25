using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODO_Launcher
{
    class Globals
    {
        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(long hProcess, long lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, long lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);


        public static string mainColor = "#343A40";
        
        public static string serverIP = "127.0.0.1";
        
        public static void OpenURL(string from, string url)
        {
            switch (from)
            {
                case "browser":
                    /*
                    *This will open the Default Browser from Windows (Ex: Firefox, Chrome, IE, Etc)
                    */
                    System.Diagnostics.Process.Start(url);
                    break;

                case "ui":
                    /*
                    *Call the url from href and target="_blank" on the browser
                    *Example <a href="http://url.com" target="_blank"> Text </a> c:
                    */
                    break;

                default:
                    break;
            }
        }

        public static bool CheckIfExist(string[] args)
        {
            /*
             * args[0] = Directory (if u put current it will use GetCurrentDirectory;
             * EX: args[0] = C:/Program Files(x86)/Folder
             * 
             * args[1] = ExeFileName.exe
             * EX: args[1] = \\ExeFileName.exe
             * 
             * Based on The Jackal Launcher load check
             * Member URL: http://forum.ragezone.com/members/1333398410.html
             * Found on: http://forum.ragezone.com/f1000/release-simple-bdo-private-launcher-1146951/
             */
            string currentDir;
            string exeFile;
            bool ready0;

            switch (args[0])
            {
                case "current":
                    currentDir = Application.StartupPath; //I THINK IS FIXED
                    break;
                default:
                    currentDir = @args[0];
                    break;

            }

            exeFile = currentDir + args[1];

            ready0 = System.IO.File.Exists(exeFile);

            if(!ready0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ExecuteJSScript(string script)
        {
            Launcher.browser.ExecuteScriptAsync(script);
        }

        public static int GetProcessId(string proc)
        {
            return Process.GetProcessesByName(proc)[0].Id;
        }

        public static void WriteProcessMemory(long adress, byte[] processBytes, int processHandle)
        {
            WriteProcessMemory(processHandle, adress, processBytes, processBytes.Length, 0);
        }

        public async static Task ServiceIni()
        {
            /*******************************************************************
             * Ini implementation by The Jackal (http://forum.ragezone.com/members/1333398410.html)
             * Found on: http://forum.ragezone.com/f1000/release-simple-bdo-private-launcher-1146951/
            *******************************************************************/

            if (File.Exists(@"..\\service.ini"))
            {
                File.Delete(@"..\\service.ini");
            }

            var MyIni = new IniFile("..\\service.ini");

            /*******************************************************************
             *  if TYPE is not set under the section [SERVICE]
             *  create it and set it to NA like this
             *  
             *  [SERVICE]
             *  TYPE=NA
             *******************************************************************/

            if (!MyIni.KeyExists("TYPE", "SERVICE"))            // check if key exists
            {
                MyIni.Write("TYPE", Config.GetString("TYPE"), "SERVICE");           // if it doesnt add it and set it
            }
            if (!MyIni.KeyExists("RES", "SERVICE"))             // repeat
            {
                MyIni.Write("RES", Config.GetString("RES"), "SERVICE");
            }
            if (!MyIni.KeyExists("nationType", "SERVICE"))
            {
                MyIni.Write("nationType", Config.GetString("nationType"), "SERVICE");
            }
            if (!MyIni.KeyExists("damageMeter", "SERVICE"))
            {
                MyIni.Write("damageMeter", Config.GetString("damageMeter"), "SERVICE");
            }
            if (!MyIni.KeyExists("AUTHENTIC_DOMAIN", "NA"))
            {
                MyIni.Write("AUTHENTIC_DOMAIN", Config.GetString("IPAddress"), "NA");
            }
            if (!MyIni.KeyExists("AUTHENTIC_PORT", "NA"))
            {
                MyIni.Write("AUTHENTIC_PORT", Config.GetString("PortNumber"), "NA");
            }
            if (!MyIni.KeyExists("PATCH_URL", "NA"))
            {
                MyIni.Write("PATCH_URL", Config.GetString("PATCH_URL"), "NA");
            }

            if (!Config.GetBool("debugMode"))
            {
                MyIni.Write("AUTHENTIC_DOMAIN", Config.GetString("IPAddress"), "NA"); // If server ip/port keys do exist in the file override them with the ip/port from the textboxes
                MyIni.Write("AUTHENTIC_PORT", Config.GetString("PortNumber"), "NA"); // same as above
            }
            await Task.Delay(1500);
        }
        
        public async static Task LauncherIni()
        {
            var LauncherOptionsIni = new IniFile("launcher.ini");

             if (LauncherOptionsIni.KeyExists("Server_IP", "SERVICE"))
            {
                serverIP = LauncherOptionsIni.Read("Server_IP", "SERVICE");
            }

            await Task.Delay(1500);
        }

        public async static Task OpenGameExe(string exeFile, string username, string password)
        {
            Process.Start(exeFile, (username + "," + password) ?? "");
        }

        public async static Task GetProcessIdx(string processName)
        {
            // Initialisation
            await Task.Delay(1500); // Wait for the launcher to start.

            // Start process meddling
            int processId = GetProcessId(processName); // Get the process ID.
            Process[] processesByName = Process.GetProcessesByName(processName); // Get the actual process.
            int processHandle = OpenProcess(0x1F0FFF, false, processesByName[0].Id); // Open the process for data stream manipulation.
            IntPtr baseAddress = processesByName[0].MainModule.BaseAddress; // Set the data stream manipulation to start at the base address of the process.

            // Start server IP initialisation
            byte[] serverIPtranslation = Encoding.ASCII.GetBytes(serverIP); // Translate server IPv4 address

            // Start data stream manipulation
            WriteProcessMemory(baseAddress.ToInt64() + 0x0A29306, new byte[] { 0x90, 0x90 }, processHandle); // Crypto fix by Matt
            WriteProcessMemory(baseAddress.ToInt64() + 0x07A5B0A, new byte[] { 0x90, 0x90 }, processHandle); // XC Fix 1 by Matt
            WriteProcessMemory(baseAddress.ToInt64() + 0x07A5BF0, new byte[] { 0xEB }, processHandle); // XC Fix 2 by Matt
            WriteProcessMemory(baseAddress.ToInt64() + 0x2B41A38, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, processHandle); // WIPE IP
            WriteProcessMemory(baseAddress.ToInt64() + 0x2B41A38, serverIPtranslation, processHandle); // Server IP fix by Matt
            //WriteProcessMemory(processesByName[0].MainModule.BaseAddress.ToInt64() + 0x7A8E64, new byte[] { 0x90, 0x90 }, processHandle);
            //WriteProcessMemory(processesByName[0].MainModule.BaseAddress.ToInt64() + 0x7A9334, new byte[] { 0x90, 0x90 }, processHandle);
            //WriteProcessMemory(processesByName[0].MainModule.BaseAddress.ToInt64() + 0x7A9804, new byte[] { 0x90, 0x90 }, processHandle);

        }
    }
}
