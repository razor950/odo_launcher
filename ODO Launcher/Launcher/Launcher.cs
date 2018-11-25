using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ODO_Launcher
{
    public partial class Launcher : Form
    {
        #region Variables

        private CefSettings browserSettings;
        public static ChromiumWebBrowser browser;
        public static Launcher thisForm;

        #endregion

        #region Dont Touch Me
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public Launcher()
        {
            InitializeComponent();
            FormStyle();
            Browser("configure");
            thisForm = this; //not the best solution, but it works!
            Config.First(); //This need to be loaded first, for compatibility things
            Config.LoadConfig(); // This will download the config file (XML)
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            Browser("launch");

        }

        private void Launcher_Shown(object sender, EventArgs e)
        {
            /*
            if (!Globals.CheckIfExist(new string[] { "current", "\\BlackDesert64.exe" }))
            {
                DialogResult result = MessageBox.Show("Black Desert Online is missing!\nMake sure the launcher is in the 'bin64' folder.\nThis folder also contains: BlackDesert64.exe", "Important! Game files are missing!");
                if (result == DialogResult.OK)
                    if (!Config.GetBool("debugMode"))
                        Exit();
            }
            */
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void Browser(string _case)
        {
            /*******************************************************************
             * Cef Integration by Hinotori 
             * http://hinotori.moe
            *******************************************************************/

            switch (_case)
            {
                case "configure":
                    browserSettings = new CefSettings();
                    CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                    break;

                case "launch":
                    Cef.Initialize(browserSettings);
                    browser = new ChromiumWebBrowser("https://hinotori.moe/dev/ODOL"); //Source on WEB Branch
                    browser.RegisterJsObject("JSODO", new JavaScript());
                    Controls.Add(browser);
                    break;
            }
        }

        private void FormStyle()
        {
            navPanel.BackColor = ColorTranslator.FromHtml(Globals.mainColor);
        }

        public static void Exit()
        {
            Cef.Shutdown();
            Environment.Exit(0);
        }

        public static void Minimice()
        {
            thisForm.WindowState = FormWindowState.Minimized;
        }

        public async static void LaunchGame(string username, string password)
        {
            await Globals.ServiceIni();
            await Globals.LauncherIni();
            await Globals.OpenGameExe("..\\bin64\\BlackDesert64.exe", username, password); //OpenGameExe have's a 1.5 seconds delay by default
            await Globals.GetProcessIdx("BlackDesert64");
            
        }

        #region NAV ELEMENTS

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void NavPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            Minimice();
        }
        #endregion


    }
}
