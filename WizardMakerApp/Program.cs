namespace WizardMaker;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        bool test = false;
#if DEBUG
        test = true;
#endif
        // Disabling the current window in favor of new Tester
        Form mainWindow;
        if (!test)
         mainWindow = new MainProgramWindow();
        else
         mainWindow = new Tester();

        Application.Run(mainWindow);
    }
}