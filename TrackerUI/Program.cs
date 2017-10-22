using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initialize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(true,true);
            Application.Run(new CreatePrizeForm());

           // Application.Run(new TournamentDashboardForm());
        }
    } 
}
