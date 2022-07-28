using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WizardMakerPrototype;

namespace WizardMakerTestbed
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Tester mainWindow = new Tester();

            // Disabling the current window in favor of new Tester
            //MainProgramWindow mainWindow = new MainProgramWindow();
            Application.Run( mainWindow );
        }
    }
}
