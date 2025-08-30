using SPM_Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SPM_Worker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutorizeInfo _ans;
            AutorizeForm _login = new AutorizeForm();

            if (UpdateINFO.CheckUpdate(out string[] files, "update.exe"))
            {
                //ОБНОВА

                // Шлях до Update.exe (той самий каталог)
                string updateExe = Path
                    .Combine(AppDomain
                    .CurrentDomain
                    .BaseDirectory, 
                    "update.exe");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = updateExe,
                    Arguments = string.Join(" ", files),
                    Verb = "runas" // запустить з UAC-підвищенням
                };

                try
                {
                    Process.Start(psi);
                    
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося запустити {updateExe}: {ex.Message}");
                    Application.Exit();
                }
            }
            else
            {
                #if DEBUG
                    _ans = _login.Answer("Administrator", "And00rey");
                #else
                    _login.ShowDialog();
                    _ans = _login.AUTORIZED;
                #endif

                if (_ans.success)
                {
                    Application.Run(new MAIN_FORM(_ans));
                }
                else
                {
                    Application.Exit();
                }
            }
        }

    }

}
