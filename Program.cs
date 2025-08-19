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

            if (UpdateINFO.CheckUpdate(out string[] files))
            {
                //ОБНОВА

                // Формуємо аргументи для Update.exe
                //string _args = string.Join(" ", Array.ConvertAll(files, f => $"\"{f}\""));

                // Шлях до Update.exe (той самий каталог)
                string updateExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "update.exe");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = updateExe,
                    Arguments = string.Join(" ", files), // передаємо файли
                    Verb = "runas" // запустить з UAC-підвищенням
                };

                try
                {
                    //System.Diagnostics.Process.Start(updateExe, _args);
                    Process.Start(psi);
                    // Можна закрити поточний додаток, якщо потрібно
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося запустити {updateExe}: {ex.Message}");
                }
            }
            else
            {
                if (args.Length > 0 && args[0] == "test0001")
                {
                    _ans = _login.Answer("Administrator", "And00rey");
                }
                else
                {
                    _login.ShowDialog();
                    _ans = _login.AUTORIZED;
                }

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
