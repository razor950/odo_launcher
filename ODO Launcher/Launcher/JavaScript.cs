using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODO_Launcher
{
    class JavaScript
    {
        /*******************************************************************
         * JavaScript Integration by Hinotori 
         * http://hinotori.moe
        *******************************************************************/

        public void LaunchGame(string username, string password)
        {
            Launcher.LaunchGame(username, password);
        }

        public void ExitClient()
        {
            Launcher.Exit();
        }

        public void OpenURL(string url)
        {
            Globals.OpenURL("browser", url);
        }
    }
}
