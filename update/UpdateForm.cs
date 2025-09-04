using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace update
{
    public partial class UpdateForm : Form
    {
        private string ftpServer = "ftp://sholompromax.com/";
        private string ftpUser = "spmsoft@sholompromax.com";
        private string ftpPassword = "R2bn=5?ALPz09z&J";

        private string[] filesToUpdate;
        private Timer _timerIn = new Timer();
        private Timer _timerExit = new Timer();

        public UpdateForm(string[] args)
        {
            InitializeComponent();

            Text = $"Оновлення [{args?.Length??0}]";

            filesToUpdate = args; //.Select(f=>("\\" + f)).ToArray();

            progressUpdate.Minimum = 0;
            progressUpdate.Maximum = filesToUpdate.Length;
            progressUpdate.Value = 0;

            _timerIn.Interval = 3000;
            _timerExit.Interval = 5000;
            _timerExit.Tick += _timer_Tick;
            _timerIn.Tick += _timerIn_Tick;

            _timerIn.Start();
        }

        private void _timerIn_Tick(object sender, EventArgs e)
        {
            // Запускаємо оновлення асинхронно
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            string MainExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SPM_Worker.exe");

            try
            {
                System.Diagnostics.Process.Start(MainExe);
                // Можна закрити поточний додаток, якщо потрібно
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося запустити {MainExe}: {ex.Message}");
            }
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            for (int i = 0; i < filesToUpdate.Length; i++)
            {
                string fileName = filesToUpdate[i];
                string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                string ftpPath = ftpServer + fileName;

                try
                {
                    bw.ReportProgress(i, $"Завантаження: {fileName}...");

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                    request.UseBinary = true;

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    using (Stream responseStream = response.GetResponseStream())
                    using (FileStream fs = new FileStream(localPath, FileMode.Create))
                    {
                        responseStream.CopyTo(fs);
                    }

                    bw.ReportProgress(i + 1, $"Оновлено: {fileName}");
                }
                catch (Exception ex)
                {
                    bw.ReportProgress(i + 1, $"ПОМИЛКА ОНОВЛЕННЯ {fileName}: {ex.Message}");
                }
            }
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressUpdate.Value = e.ProgressPercentage;
            string message = e.UserState as string;
            if (!string.IsNullOrEmpty(message))
            {
                listBox1.Items.Add(message);
                listBox1.TopIndex = listBox1.Items.Count - 1; // автоскрол
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBox1.Items.Add("ОНОВЛЕНО ВДАЛО!");
            progressUpdate.Value = progressUpdate.Maximum;

            _timerExit.Start();
        }
    }
}
