using SPM_Core;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class AutorizeForm : Form
    {
        public AutorizeInfo AUTORIZED { get; private set; }
        private static string _prewUserFileName = Path.Combine(SERVICE_INFO.APP_DATA_PATCH,
            "prewuser.txt");
        public AutorizeForm()
        {
            AUTORIZED = new AutorizeInfo();

            InitializeComponent();

            using (Font _font = AppFonts.LogoFont(16))
            {
                label1.Font = _font;
                label2.Font = _font;
                butLogin.Font = _font;
            }

            if (File.Exists(_prewUserFileName))
            {
                using (StreamReader _prewUser = new StreamReader(_prewUserFileName))
                {
                    tbLogin.Text = _prewUser.ReadLine().Trim();
                    tbPass.Select();
                    tbPass.Focus();
                }
            }
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            AUTORIZED = Answer(tbLogin.Text.Trim(), tbPass.Text.Trim());

            if (AUTORIZED.success)
            {
                using (StreamWriter _prewUser = new StreamWriter(_prewUserFileName, false))
                {
                    _prewUser.WriteLine(tbLogin.Text.Trim());
                }

                Close();
            }
            else
            {
                CustomMessage.Show(AUTORIZED.message, MessageBoxIcon.Error);
            }
        }

        public AutorizeInfo Answer(string login, string pass)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_autorize";
            string postData = $"login={login}&password={pass}";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                // Отримання відповіді
                string responseText;
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                // Парсинг JSON
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return serializer.Deserialize<AutorizeInfo>(responseText);
            }
            catch (Exception ex)
            {
                return new AutorizeInfo() { message = ex.Message };
            }
        } 
    }
}
