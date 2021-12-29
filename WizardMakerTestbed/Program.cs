using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardMakerTestbed
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            MainProgramWindow mainWindow = new MainProgramWindow();
            Application.Run( mainWindow );
        }
    }
}
