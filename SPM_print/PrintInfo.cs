using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace SPM_print
{
    public class PrintInfo
    {
        private static List<Bitmap> _allPages;
        private static int _currentIndex;
        private const string fontFamily = "Arial";
        private const int fontSize = 20;

        public static void Print(List<PRINT_TABLE_INFO> _input, PrintType _type)
        {
            // Створюємо PrintDocument
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = false;
            pd.PrintPage += Pd_PrintPage;

            // Рендеримо всі сторінки
            _allPages = new List<Bitmap>();
            foreach (PRINT_TABLE_INFO z in _input)
            {
                switch (_type)
                {
                    case PrintType.Worker:
                        _allPages.Add(RenderDocumentFromWorker(z));
                        break;
                    case PrintType.Short:
                        _allPages.Add(RenderDocumentStandart(z, false));
                        break;
                    default:
                        _allPages.Add(RenderDocumentStandart(z));
                        break;
                }
            }

            // Показуємо прев’ю
            PreviewForm _preview = new PreviewForm(_allPages);
            if (DialogResult.OK == _preview.ShowDialog())
            {
                _currentIndex = 0;   // обнуляємо перед друком
                try
                {
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static Bitmap RenderDocumentStandart(PRINT_TABLE_INFO Z, bool WithKomplekt = true)
        {
            Size a4 = new Size(2480, 3508);

            Bitmap bmp = new Bitmap(a4.Width / 2, a4.Height / 4);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Font _fontBold = new Font(fontFamily, fontSize, FontStyle.Bold);
                Font _fontRegular = new Font(fontFamily, fontSize, FontStyle.Regular);

                g.Clear(Color.White);

                int[] cellWidth = new int[2] { 350, 840 }; //1190

                string text = $"ІНФОРМАЦІЯ ПО {(Z.TYPE == 0 ? "ШОЛОМУ" : "ЗАМОВЛЕННЮ")}" +
                     $"{(!string.IsNullOrEmpty(Z.NUMBER) && Z.NUMBER != "0" ? $" №{Z.NUMBER}" : "")}";

                SizeF textSize = g.MeasureString(text, _fontBold);

                int leftX, topY = 20;
                int rowHeight = (int)(textSize.Height * 1.1f), rowInterval = 0, rowHeigthFact;

                //ШАПКА (по центру)

                leftX = ((cellWidth[0] + cellWidth[1]) - (int)(textSize.Width / 2)) / 2;

                g.DrawString(text, _fontBold, Brushes.Black, leftX, topY);

                leftX = 25;

                string[] cellText = new string[4];

                //Дата / Термін
                cellWidth = new int[3] { 350, 420, 420 };
                cellText[0] = "Дата / Термін";
                cellText[1] = Z.DATE_IN;
                cellText[2] = Z.DATE_MAX;

                topY += rowHeight;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY, width: cellWidth[0], 
                    rowHeigthFact, cellText[0]);
                DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY, width: cellWidth[1], 
                    rowHeigthFact, cellText[1], _fontRegular, StringAlignment.Center);
                DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY, width: cellWidth[1],
                    rowHeigthFact, cellText[2], _fontRegular, StringAlignment.Center);
                cellWidth = new int[2] { 350, 840 };

                //Дата відправки

                if (!string.IsNullOrEmpty(Z.DATE_OUT))
                {
                    cellText[0] = "Дата відправки";
                    cellText[1] = Z.DATE_OUT;

                    topY += rowInterval + rowHeigthFact;
                    rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                    DrawCell(g, leftX, topY, width: cellWidth[0],
                        rowHeigthFact, cellText[0]);
                    DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                        rowHeigthFact, cellText[1]);

                }

                //Номер телефону
                cellText[0] = "Номер телефону";
                cellText[1] = Z.PHONE;

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);

                //Прізвище, ім`я
                cellText[0] = "Прізвище, ім`я";
                cellText[1] = Z.CLIENT_NAME;

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);

                //Реквізити
                cellText[0] = "Реквізити";
                cellText[1] = Z.REQV;

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);

                //ТТН
                if (!string.IsNullOrEmpty(Z.TTN_IN) || !string.IsNullOrEmpty(Z.TTN_OUT))
                {
                    cellWidth = new int[3] { 350, 420, 420 };

                    cellText[0] = "ТТН вх./вих.";
                    cellText[1] = Z.TTN_IN;
                    cellText[2] = Z.TTN_OUT;

                    topY += rowInterval + rowHeigthFact;
                    rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                    DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY, width: cellWidth[0],
                        rowHeigthFact, cellText[0]);
                    DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY, width: cellWidth[1],
                        rowHeigthFact, cellText[1], _fontRegular, StringAlignment.Center);
                    DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY, width: cellWidth[1],
                        rowHeigthFact, cellText[2], _fontRegular, StringAlignment.Center);

                    cellWidth = new int[2] { 350, 840 };
                }

                //Врахована знижка
                if (!string.IsNullOrEmpty(Z.DISCOUNT))
                {
                    cellText[0] = "Врахована знижка";
                    cellText[1] = Z.DISCOUNT + "%";

                    topY += rowInterval + rowHeigthFact;
                    rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                    DrawCell(g, leftX, topY, width: cellWidth[0],
                        rowHeigthFact, cellText[0]);
                    DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                        rowHeigthFact, cellText[1]);
                }

                if (WithKomplekt)
                {
                    //КОМПЛЕКТУЮЧІ 2
                    topY += rowInterval + rowHeigthFact;
                    DrawCell(g, leftX, topY,
                        width: cellWidth[0] + cellWidth[1],
                        rowHeight,
                        "КОМПЛЕКТУЮЧІ",
                        null, StringAlignment.Center);

                    //Коментар 2
                    if (!string.IsNullOrEmpty(Z.COMM))
                    {
                        topY += rowInterval + rowHeight;

                        cellText[0] = Z.COMM.Replace("\n", " ").Replace("\r", " ");

                        rowHeigthFact = FaktHeigth(g, cellText, new int[1] { cellWidth[0] + cellWidth[1] }, rowHeight, _fontBold);

                        DrawCell(g, leftX, topY,
                            width: cellWidth[0] + cellWidth[1],
                            rowHeigthFact,
                            cellText[0], _fontBold,
                            StringAlignment.Center);
                    }

                    //Сервіси
                    cellWidth = new int[] { 770, 70, 210, 140 }; //1190

                    Z.SERVICES.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));

                    foreach (Service S in Z.SERVICES)
                    {
                        cellText[0] = S.NAME_TYPE;
                        cellText[1] = S.COUNT;
                        cellText[2] = S.COLOR;
                        cellText[3] = S.PRICE;

                        topY += rowInterval + rowHeigthFact;
                        rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                        DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY,
                                 width: cellWidth[0],
                                 rowHeigthFact,
                                 cellText[0], _fontRegular,
                                 StringAlignment.Near);

                        DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY,
                                 width: cellWidth[1],
                                 rowHeigthFact,
                                 cellText[1], _fontRegular,
                                 StringAlignment.Center);

                        DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY,
                                 width: cellWidth[2],
                                 rowHeigthFact,
                                 cellText[2], _fontRegular,
                                 StringAlignment.Center);

                        DrawCell(g, NextCellPosition(leftX, 3, cellWidth), topY,
                                 width: cellWidth[3],
                                 rowHeigthFact,
                                 cellText[3], _fontRegular,
                                 StringAlignment.Far);
                    }

                }

                //ДО СПЛАТИ 2
                cellWidth = new int[] { 770, 420 }; //1190

                cellText[0] = "ДО СПЛАТИ";
                cellText[1] = Z.SUM;

                topY += rowInterval + rowHeigthFact;
                DrawCell(g, leftX, topY, width: cellWidth[0], rowHeight, cellText[0], _fontBold);
                DrawCell(g, leftX + cellWidth[0], topY, 
                    width: cellWidth[1], 
                    rowHeight,
                    cellText[1], _fontBold, StringAlignment.Far);
            }
            return bmp;
        }

        private static Bitmap RenderDocumentFromWorker(PRINT_TABLE_INFO Z)
        {
            Size a4 = new Size(2480, 3508);

            Bitmap bmp = new Bitmap(a4.Width / 2, a4.Height / 4);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Font _fontBold = new Font(fontFamily, fontSize, FontStyle.Bold);
                Font _fontRegular = new Font(fontFamily, fontSize, FontStyle.Regular);

                g.Clear(Color.White);

                int[] cellWidth = new int[2] { 350, 840 }; //1190

                string text = $"ІНФОРМАЦІЯ ПО {(Z.TYPE == 0 ? "ШОЛОМУ" : "ЗАМОВЛЕННЮ")}" +
                     $"{(!string.IsNullOrEmpty(Z.NUMBER) && Z.NUMBER != "0" ? $" №{Z.NUMBER}" : "")}";

                SizeF textSize = g.MeasureString(text, _fontBold);

                int leftX, topY = 20;
                int rowHeight = (int)(textSize.Height * 1.2f), rowInterval = 0, rowHeigthFact;

                //ШАПКА (по центру)

                leftX = ((cellWidth[0] + cellWidth[1]) - (int)(textSize.Width / 2)) / 2;

                g.DrawString(text, _fontBold, Brushes.Black, leftX, topY);

                leftX = 25;

                string[] cellText = new string[4];

                //Дата / Термін
                cellWidth = new int[3] { 350, 420, 420 };
                cellText[0] = "Дата / Термін";
                cellText[1] = Z.DATE_IN;
                cellText[2] = Z.DATE_MAX;

                topY += rowHeight;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1], _fontRegular, StringAlignment.Center);
                DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY, width: cellWidth[1],
                    rowHeigthFact, cellText[2], _fontRegular, StringAlignment.Center);
                cellWidth = new int[2] { 350, 840 };

                //Номер телефону
                cellText[0] = "Номер телефону";
                cellText[1] = "..." + Z.PHONE.Substring(Z.PHONE.Length > 3 
                    ? Z.PHONE.Length - 4 : 0);

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);

                //Реквізити
                cellText[0] = "Реквізити";
                cellText[1] = Z.REQV;

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);

                //ТТН
                if (!string.IsNullOrEmpty(Z.TTN_IN) || !string.IsNullOrEmpty(Z.TTN_OUT))
                {
                    cellWidth = new int[3] { 350, 420, 420 };

                    cellText[0] = "ТТН вх./вих.";
                    cellText[1] = Z.TTN_IN;
                    cellText[2] = Z.TTN_OUT;

                    topY += rowInterval + rowHeigthFact;
                    rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                    DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY, width: cellWidth[0],
                        rowHeigthFact, cellText[0]);
                    DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY, width: cellWidth[1],
                        rowHeigthFact, cellText[1], _fontRegular, StringAlignment.Center);
                    DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY, width: cellWidth[1],
                        rowHeigthFact, cellText[2], _fontRegular, StringAlignment.Center);

                    cellWidth = new int[2] { 350, 840 };
                }

                //ВІДПОВІДАЛЬНИЙ

                cellText[0] = "Відповідальний";
                cellText[1] = Z.REDAKTOR;

                topY += rowInterval + rowHeigthFact;
                rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                DrawCell(g, leftX, topY, width: cellWidth[0],
                    rowHeigthFact, cellText[0]);
                DrawCell(g, leftX + cellWidth[0], topY, width: cellWidth[1],
                    rowHeigthFact, cellText[1]);


                //КОМПЛЕКТУЮЧІ 2
                topY += rowInterval + rowHeigthFact;
                DrawCell(g, leftX, topY,
                    width: cellWidth[0] + cellWidth[1],
                    rowHeight,
                    "КОМПЛЕКТУЮЧІ",
                    null, StringAlignment.Center);

                //Коментар 2
                if (!string.IsNullOrEmpty(Z.COMM))
                {
                    cellText[0] = Z.COMM.Replace("\n", " ").Replace("\r", " ");

                    if (cellText[0].IndexOf("!!!") > -1)
                    {
                        cellText[0] = cellText[0].Split(new string[1] { "!!!" }, StringSplitOptions.None)[1].Trim().ToUpper();
                    }

                    if (cellText[0] != string.Empty)
                    {
                        topY += rowInterval + rowHeight;

                        rowHeigthFact = FaktHeigth(g, cellText, new int[1] { cellWidth[0] + cellWidth[1] }, rowHeight, _fontBold);

                        DrawCell(g, leftX, topY,
                            width: cellWidth[0] + cellWidth[1],
                            rowHeigthFact,
                            cellText[0], _fontBold,
                            StringAlignment.Center);
                    }
                }

                //Сервіси
                cellWidth = new int[] { 940, 70, 180 }; //1190

                Z.SERVICES.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));

                foreach (Service S in Z.SERVICES)
                {
                    cellText[0] = S.NAME_TYPE;
                    cellText[1] = S.COUNT;
                    cellText[2] = S.COLOR;

                    topY += rowInterval + rowHeigthFact;
                    rowHeigthFact = FaktHeigth(g, cellText, cellWidth, rowHeight, _fontRegular);

                    DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY,
                             width: cellWidth[0],
                             rowHeigthFact,
                             cellText[0], _fontRegular,
                             StringAlignment.Near);

                    DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY,
                             width: cellWidth[1],
                             rowHeigthFact,
                             cellText[1], _fontRegular,
                             StringAlignment.Center);

                    DrawCell(g, NextCellPosition(leftX, 2, cellWidth), topY,
                             width: cellWidth[2],
                             rowHeigthFact,
                             cellText[2], _fontRegular,
                             StringAlignment.Center);
                }

                //Працівник

                cellWidth = new int[] { 940, 250 }; //1190
                topY += rowInterval + rowHeigthFact;
                cellText[0] = "Працівник";
                cellText[1] = Z.WORKER ?? "";

                DrawCell(g, NextCellPosition(leftX, 0, cellWidth), topY,
                         width: cellWidth[0],
                         rowHeigthFact,
                         cellText[0], _fontRegular,
                         StringAlignment.Near);

                DrawCell(g, NextCellPosition(leftX, 1, cellWidth), topY,
                         width: cellWidth[1],
                         rowHeigthFact,
                         cellText[1], _fontRegular,
                         StringAlignment.Center);
            }
            return bmp;
        }


        private static int NextCellPosition(int prew, int index, int[] _arr)
        {
            int _out = prew;

            for (int i = 0; i < index; i++)
            {
                _out += _arr[i];
            }
            return _out;
        }

        private static int FaktHeigth(Graphics g, string[] _text, int[] cellWidth, int defHeight, Font _font)
        {
            int _out = defHeight, _temp;
            SizeF textSize;

            for(int i = 0; i < cellWidth.Length; i++)
            {
                textSize = g.MeasureString(_text[i], _font);

                _temp = (int)((float)(textSize.Width / cellWidth[i]) * defHeight);

                if (_temp > _out) _out += defHeight;
            }

            return _out;
        }

        private static void DrawCell(Graphics g,
                      int x, int y,
                      int width, int height,
                      string text,
                      Font font = null,
                      StringAlignment hAlign = StringAlignment.Near,
                      StringAlignment vAlign = StringAlignment.Center)
        {
            // Шрифт за замовчуванням
            if (font == null)
                font = new Font(fontFamily, fontSize);

            // Малюємо рамку
            g.DrawRectangle(Pens.Black, x, y, width, height);

            // Текст усередині
            RectangleF rect = new RectangleF(x, y, width, height);
            StringFormat format = new StringFormat
            {
                Alignment = hAlign,           // вирівнювання по горизонталі
                LineAlignment = vAlign,       // вирівнювання по вертикалі
                FormatFlags = StringFormatFlags.LineLimit // не вилазити за межі
            };

            g.DrawString(text, font, Brushes.Black, rect, format);
        }

        private static void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Мінімальні поля принтера (в пікселях)
            int leftMargin = e.PageSettings.HardMarginX > 0 ? 
                (int)e.PageSettings.HardMarginX : e.MarginBounds.Left;
            int topMargin = e.PageSettings.HardMarginY > 0 ? 
                (int)e.PageSettings.HardMarginY : e.MarginBounds.Top;

            // Розміри друкованої області, мінус поля
            int printableWidth = e.PageBounds.Width - (leftMargin * 2);
            int printableHeight = e.PageBounds.Height - (topMargin * 2);

            // Верхня половина сторінки
            Rectangle topHalf = new Rectangle(
                leftMargin,
                topMargin,
                printableWidth,
                printableHeight / 2);

            // Нижня половина сторінки
            Rectangle bottomHalf = new Rectangle(
                leftMargin,
                topMargin + printableHeight / 2,
                printableWidth,
                printableHeight / 2);

            if (_currentIndex < _allPages.Count)
            {
                DrawScaledImage(e.Graphics, _allPages[_currentIndex], topHalf);
                _currentIndex++;
            }

            if (_currentIndex < _allPages.Count)
            {
                DrawScaledImage(e.Graphics, _allPages[_currentIndex], bottomHalf);
                _currentIndex++;
            }

            e.HasMorePages = _currentIndex < _allPages.Count;
        }

        private static void DrawScaledImage(Graphics g, Bitmap bmp, Rectangle target)
        {
            bmp.SetResolution(96, 96);

            float koefA4 = 3508f / 2480f;
            float koefA4width = 50f;
            float koefA4heigth = koefA4width * koefA4;

            float scale = Math.Min(target.Width / (bmp.Width + koefA4width),
                                   target.Height / (bmp.Height + koefA4heigth));

            int drawWidth = (int)(bmp.Width * scale);
            int drawHeight = (int)(bmp.Height * scale);

            int offsetX = target.Left + (target.Width - drawWidth) / 2;
            int offsetY = target.Top + (target.Height - drawHeight) / 2;

            g.DrawImage(bmp, new Rectangle(offsetX, offsetY, drawWidth, drawHeight));
        }
    }

    public class PreviewForm : Form
    {
        public List<Bitmap> PAGES { get; }
        private DoubleBufferedPanel _container;

        public PreviewForm(List<Bitmap> _input)
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Text = "Попередній перегляд";
            Width = 800;
            Height = 700;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;

            PAGES = _input;

            //Створюєио панель
            _container = new DoubleBufferedPanel();
            _container.Padding = new Padding(100, 0, 100, 0);
            _container.Margin = new Padding(0);
            _container.AutoScroll = true;
            _container.BackColor = Color.Gray;
            _container.Name = "PAGE";
            _container.Height = Height - 60;
            _container.Width = Width - Padding.Horizontal - 200;
            _container.Location = new Point(100, 10);

            int totalHeight = PAGES.Sum(img => img.Height + 10);
            _container.AutoScrollMinSize = new Size(0, totalHeight);

            //Заповнюємо панель
            _container.Paint += AppendPanel;

            Controls.Add(_container);

            //Створюємо кнопку друку
            Button _printBtn = new Button();
            _printBtn.Text = "Друк";
            _printBtn.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            _printBtn.Size = new Size(60, 30);
            _printBtn.Location = 
                new Point(Width - Padding.Horizontal - (_printBtn.Width + 30), 
                          Height - Padding.Vertical - (_printBtn.Height + 50));
            _printBtn.DialogResult = DialogResult.OK;
            _printBtn.Name = "PRINT";

            _printBtn.NotifyDefault(true);

            Controls.Add(_printBtn);

            UpdateDefaultButton();

        }

        private void AppendPanel(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Зміщення через прокрутку
            int y = _container.AutoScrollPosition.Y;

            float scaleKoef = (_container.Width / (float)PAGES[0].Width);

            Size _scale = new Size(
                (int)((PAGES[0].Width - 25) * scaleKoef),
                (int)((PAGES[0].Height - 17.5f) * scaleKoef));

            Bitmap _temp;

            foreach (Bitmap img in PAGES)
            {
                _temp = new Bitmap(img, _scale);
                g.DrawImage(_temp, 0, y);
                //g.DrawImage(img, 0, y);

                y += _temp.Height + 10;
            }
        }

        public class DoubleBufferedPanel : FlowLayoutPanel
        {
            public DoubleBufferedPanel()
            {
                this.DoubleBuffered = true;
                this.ResizeRedraw = true;
            }
        }
    }

    public class PRINT_TABLE_INFO
    {
        /// <summary>
        /// 0 = Переобладнання
        /// 1 = Продаж
        /// </summary>
        public byte TYPE { get; set; }
        public string NUMBER { get; set; }
        public string DATE_IN { get => _date_in; set { 
                _date_in = GetDateTime(value); 
            } }
        private string _date_in;
        public string DATE_MAX { get => _date_max; set { _date_max = GetDateTime(value, true); } }
        private string _date_max;
        public string DATE_OUT { get => _date_out; set { 
                _date_out = !string.IsNullOrEmpty(value) ? GetDateTime(value) : null; } }
        private string _date_out;
        public string PHONE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string REQV { get; set; }
        public string TTN_IN { get; set; }
        public string TTN_OUT { get; set; }
        public string REDAKTOR { get; set; }
        public string WORKER { get; set; }
        public string COMM { get; set; }
        public string DISCOUNT { get; set; }
        public List<Service> SERVICES { get; set; }
        public string SUM { get => _sum; set {
                string _value = value.Replace(".", ",").Trim();

                if (float.TryParse(_value, out float _val))
                {
                    _sum = _val.ToString("F2");
                }
                else
                {
                    _sum = "0,00";
                }
            } }
        private string _sum;

        private string GetDateTime(string _input, bool _short = false)
        {
            if (!DateTime.TryParse(_input, out DateTime _d)) return _input;

            string _out = _d.Day.ToString().PadLeft(2, '0') + "."
                        + _d.Month.ToString().PadLeft(2, '0') + "."
                        + _d.Year.ToString().Substring(2);

            if (!_short)
            {
                _out += " [" + _d.Hour.ToString().PadLeft(2, '0')
                       + ":" + _d.Minute.ToString().PadLeft(2, '0') + "]";
            }

            return _out;
        }
    }

    public class Service
    {
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string COUNT { get; set; }
        public string COLOR { get; set; }
        public byte ORDER { get; set; } = byte.MaxValue;
        public string PRICE { get => _price.ToString("F2"); set { 
            
                if (float.TryParse(value, out float _f))
                {
                    _price = _f;
                }
                else
                {
                    _price = 0f;
                }

            } }
        private float _price;
        public string NAME_TYPE { get {
                string type = string.IsNullOrEmpty(TYPE) ? "" :
                    " (" + TYPE + ")";
                return NAME.Insert(NAME.Length, type);
            } }

        public Service(string _name, string _type, int _count, string _color, float _price, byte order = byte.MaxValue)
        {
            NAME = _name;
            TYPE = _type;
            COUNT = _count.ToString();
            COLOR = _color;
            PRICE = _price.ToString();
            ORDER = order;
        }
    }

    public enum PrintType : byte {
        EMPTY = 255,
        Standart = 0,
        Worker = 1,
        Short = 2
    }

}
