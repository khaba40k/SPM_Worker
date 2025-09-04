using OfficeOpenXml;
using OfficeOpenXml.Style;
using SPM_Core;
using SPM_Worker.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class MAIN_FORM : Form
    {
        public Z_STATUS MAIN_STATUS = Z_STATUS.NEW;
        private ZakazListControl _zlc = new ZakazListControl();
        private Color _buttonBackColor = Color.Olive;
        private Color _buttonMenuHoverColor = Color.Orange;
        private event EventHandler<BUTTONS_MENU> ButtonTagClick;

        public MAIN_FORM(AutorizeInfo _autorizeinfo)
        {
            SERVICE_INFO.AUTORIZE_INFO = _autorizeinfo;

            InitializeComponent();

            Text = $"SholomProMax v{Application.ProductVersion} Користувач: [{_autorizeinfo.data.NAME.ToUpper()}]";

            panelMenu.ControlAdded += (s, e) => {
                Button _btn = (Button)e.Control;

                _btn.Cursor = Cursors.Hand;
                _btn.AutoSize = true;
                _btn.Width = panelMenu.Width - panelMenu.Padding.Horizontal - panelMenu.Margin.Horizontal;
                _btn.BackColor = _buttonBackColor;
                _btn.ForeColor = Color.White;
                _btn.Font = new Font(e.Control.Font, FontStyle.Bold);
                _btn.Margin = new Padding(0);
                _btn.Padding = new Padding(0, 5, 0, 5);
                _btn.Click += (s1, e1) => {
                    ResetBackButColor();
                    _btn.BackColor = _buttonMenuHoverColor;
                };
            };

            panelContent.ControlAdded += (s, e) =>
            {
                e.Control.MinimumSize = new Size(0, 0);
                e.Control.Dock = DockStyle.Fill;
            };

            //СТВОРЕННЯ КНОПОК МЕНЮ
            Button _ZakazListBut = new Button() { Text = "Замовлення", Tag = BUTTONS_MENU.ZAKAZ_LIST };
            Button _VitratyBut = new Button() { Text = "Витрати", Tag = BUTTONS_MENU.VYTRATY };
            Button _SkladBut = new Button() { Text = "Склад", Tag = BUTTONS_MENU.SKLAD };

            //ПРИСВОЄННЯ ДІЇ

            _ZakazListBut.Click += SetZakazList;
            _VitratyBut.Click += SetVitraty;
            _SkladBut.Click += SetSklad;

            //ПРОРИСОВКА КНОПОК

            panelMenu.Controls.Add(_ZakazListBut);
            panelMenu.Controls.Add(_VitratyBut);
            panelMenu.Controls.Add(_SkladBut);

            if (_autorizeinfo.data.ID == 0)
            {
                Button _LogsBut = new Button() { Text = "Логи", Tag = BUTTONS_MENU.LOG_LIST };
                _LogsBut.Click += ShowLogs;
                panelMenu.Controls.Add(_LogsBut);
            }

            ButtonTagClick += (s, e) =>
            {

            };

            //ПЕРШЕ ВІКНО ПІСЛЯ ЗАПУСКУ
            _ZakazListBut.BackColor = _buttonMenuHoverColor;
            SetZakazList(_ZakazListBut, EventArgs.Empty);
        }

        private void ResetBackButColor()
        {
            foreach (Control _c in panelMenu.Controls)
            {
                _c.BackColor = Color.Olive;
            }
        }

        private void ClickedMenu(Button sender)
        {
            ButtonTagClick?.Invoke(sender, (BUTTONS_MENU)sender?.Tag);
        }

        private void SetZakazList(object sender, EventArgs e)
        {
            ClickedMenu(sender as Button);

            panelContent.Controls.Clear();
            panelContent.Controls.Add(_zlc);
        }

        private void SetSklad(object sender, EventArgs e)
        {
            ClickedMenu(sender as Button);

            panelContent.Controls.Clear();

            List<SERVICE_ID_INFO> _ans = SERVICE_INFO.GET_SKLAD(DateTime.Now);

            ONE_COLOR_INFO[] colorNames = _ans
                .Select(c => new ONE_COLOR_INFO { 
                    ID = (c.COLOR_ID ?? -1),
                    NAME = c.COLOR_NAME ?? "б/к" })
                .Distinct().ToArray();

            int colorCount = colorNames.Length;

            Panel _container = new Panel() {
               Name = "SKLAD"
            };

            DataGridView _sklad = new DataGridView() { 
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                ColumnCount = 2 + colorCount,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    NullValue = "-"
                }
            };

            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Історія виділеного товару", Resources.Save, (s1, e1) =>
            {
                if (_sklad.CurrentCell != null)
                {
                    int rowInd = _sklad.CurrentCell.RowIndex;

                    SERVICE_ID_INFO _inf = _sklad.Rows[rowInd].Tag as SERVICE_ID_INFO;

                    //MessageBox.Show(_inf.FULL_NAME);

                    List<EXCEL_HISTORY_LINE> content = SERVICE_INFO
                    .GetServHistInfo(_inf.ID, _inf.TYPE_ID);

                    if (content == null || content.Count == 0)
                    {
                        CustomMessage.Show("Дані по товару відсутні.", MessageBoxIcon.Hand);
                        return;
                    }

                    using (SaveFileDialog _sfd = new SaveFileDialog
                    {
                        AutoUpgradeEnabled = true,
                        FileName = _inf.FULL_NAME.Replace(" ", ""),
                        AddExtension = true,
                        Filter = "Excel | *.xlsx",
                        CheckPathExists = true })
                    {
                        _sfd.FileOk += (s2, e2) => {
                            if (!SaveExcel(_sfd.FileName, content)) return;

                            try
                            {
                                Process.Start(_sfd.FileName);
                            }
                            catch (Exception ex)
                            {
                                CustomMessage.Show(ex.Message, MessageBoxIcon.Error);
                            }
                        };

                        _sfd.ShowDialog();
                    }
                }    
            });

            _sklad.ContextMenuStrip = cms;

            _sklad.SuspendLayout();

            // Очистимо старі стовпці
            _sklad.Columns.Clear();

            // Додаємо перший стовпець
            _sklad.Columns.Add(new DataGridViewTextBoxColumn { 
                HeaderText = "НАЗВА",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Додаємо стовпці кольорів
            foreach (var color in colorNames)
            {
                _sklad.Columns.Add(new DataGridViewTextBoxColumn { 
                    HeaderText = color.ToString().ToLower(), 
                    DefaultCellStyle = new DataGridViewCellStyle { 
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });
            }

            // Додаємо останній стовпець
            _sklad.Columns.Add(new DataGridViewTextBoxColumn { 
                HeaderText = "ЗАЛИШОК",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = AppFonts.TextFont(12, FontStyle.Bold)
                },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            DataGridViewRow _row = new DataGridViewRow();

            int[] colorIds = SERVICE_INFO.GetAllID(_ans);

            foreach (int id in colorIds)
            {
                List<SERVICE_ID_INFO> _list = 
                    _ans.Where(s => s.ID == id)
                    .ToList();

                int[] types = _list.Select(i => i.TYPE_ID).Distinct().ToArray();

                foreach (int _type in types)
                {
                    _list = _ans.Where(s => s.ID == id && s.TYPE_ID == _type)
                    .ToList();

                    _row = new DataGridViewRow();

                    _row.Tag = _list.First();

                    _row.Cells.Add(new DataGridViewTextBoxCell { Value = _list.First().FULL_NAME });

                    int totalCount = 0; int? curCount = 0;

                    foreach (ONE_COLOR_INFO color in colorNames)
                    {
                        curCount = _list.Find(s => s.COLOR_ID == color.ID)?.COUNT;

                        if (curCount == 0) curCount = null;

                        _row.Cells.Add(new DataGridViewTextBoxCell
                        {
                            Value = curCount,
                            Style = new DataGridViewCellStyle
                            {
                                BackColor = curCount < 0 ? Color.Red : Color.White,
                                ForeColor = curCount < 0 ? Color.White : Color.Black
                            }
                        });

                        totalCount += curCount ?? 0;
                    }

                    if (totalCount < 0) { 
                        _row.DefaultCellStyle.BackColor = Color.Red;
                        _row.DefaultCellStyle.ForeColor = Color.White;
                    }

                    _row.Cells.Add(new DataGridViewTextBoxCell { Value = totalCount });

                    _sklad.Rows.Add(_row);
                }
            }

            _sklad.ResumeLayout();

            _container.Controls.Add(_sklad);

            panelContent.Controls.Add(_container);
        }

        public static bool SaveExcel(string filePath, List<EXCEL_HISTORY_LINE> data)
        {
            if (data == null || data.Count == 0) return false;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                string sheetName = Path.GetFileNameWithoutExtension(filePath);

                var existingSheet = package.Workbook.Worksheets.FirstOrDefault(wsh => wsh.Name == sheetName);
                if (existingSheet != null)
                {
                    package.Workbook.Worksheets.Delete(existingSheet);
                }

                var ws = package.Workbook.Worksheets.Add(sheetName);

                ws.Protection.IsProtected = true;
                ws.Protection.AllowAutoFilter = true;
                ws.Protection.SetPassword("0218");

                // --- Заголовки
                ws.Cells[1, 1].Value = "Дата";
                ws.Cells[1, 2].Value = "Статус";
                ws.Cells[1, 3].Value = "Колір";
                ws.Cells[1, 4].Value = "К-ть";
                ws.Cells[1, 5].Value = "Сума";
                ws.Cells[1, 6].Value = "Коментар";
                ws.Cells[1, 7].Value = "Провів";

                // --- Дані
                int row = 2;
                foreach (var line in data)
                {
                    ws.Cells[row, 1].Value = line.DATE;
                    ws.Cells[row, 1].Style.Numberformat.Format = "dd.mm.yy HH:mm"; // формат дати
                    ws.Cells[row, 2].Value = line.STATUS;

                    ws.Cells[row, 3].Value = line.COLOR;
                    ws.Cells[row, 4].Value = line.COUNT;
                    ws.Cells[row, 4].Style.HorizontalAlignment = 
                        ExcelHorizontalAlignment.Center;

                    ws.Cells[row, 5].Value = line.COSTS;
                    ws.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 6].Value = line.COMM;
                    ws.Cells[row, 7].Value = line.REDAKTOR;
                    row++;
                }

                int lastRow = row - 1;
                int lastCol = 7;

                // --- Обрамлення (чорна рамка навколо всієї таблиці)
                using (var range = ws.Cells[1, 1, lastRow, lastCol])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.Black);
                    range.Style.Border.Left.Color.SetColor(Color.Black);
                    range.Style.Border.Right.Color.SetColor(Color.Black);
                    range.Style.Border.Bottom.Color.SetColor(Color.Black);
                }

                ws.View.FreezePanes(2, 1);

                using (var headerRange = ws.Cells[1, 1, 1, lastCol])
                {
                    // Жирний шрифт
                    headerRange.Style.Font.Bold = true;

                    // Сірий фон
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    // Центрування заголовків
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                ws.PrinterSettings.Orientation = eOrientation.Portrait;

                lastRow++;

                // Область друку — вся таблиця
                ws.PrinterSettings.PrintArea = ws.Cells[1, 1, lastRow, lastCol];

                ws.PrinterSettings.RepeatRows = new ExcelAddress("1:1");

                ws.PrinterSettings.FitToPage = true;
                ws.PrinterSettings.FitToWidth = 1;   // по ширині вміщується на 1 сторінку
                ws.PrinterSettings.FitToHeight = 0;

                // Мінімальні поля
                ws.PrinterSettings.LeftMargin = 0.1M;   // дюйми
                ws.PrinterSettings.RightMargin = 0.1M;

                // Верхній колонтитул — назва файлу
                ws.HeaderFooter.OddHeader.CenteredText = sheetName; // &F вставляє назву файла
                ws.HeaderFooter.OddHeader.LeftAlignedText = "Автор: " + SERVICE_INFO.AUTORIZE_INFO.data.NAME;
                ws.HeaderFooter.OddHeader.RightAlignedText = DateTime.Now.ToString();

                // Нижній колонтитул — сторінка N/Всього
                ws.HeaderFooter.OddFooter.CenteredText = "&P/&N";

                ws.Cells[lastRow, 4].Formula = $"SUBTOTAL(109,D2:D{lastRow - 1})";
                ws.Cells[lastRow, 4].Style.HorizontalAlignment =
                        ExcelHorizontalAlignment.Center;
                ws.Cells[lastRow, 5].Formula = $"SUBTOTAL(109,E2:E{lastRow - 1})";
                ws.Cells[lastRow, 5].Style.Numberformat.Format = "#,##0.00";

                ws.Cells[ws.Dimension.Address].Style.Locked = true;

                // --- Фільтр по колонці B (COLOR)
                ws.Cells["A1:G1"].AutoFilter = true;

                using (var bottomRange = ws.Cells[lastRow, 1, lastRow, lastCol])
                {
                    // Жирний шрифт
                    bottomRange.Style.Font.Bold = true;

                    // Сірий фон
                    bottomRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    bottomRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    bottomRange.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    bottomRange.Style.Border.Top.Color.SetColor(Color.Black);
                }

                // --- Автоширина/COMM фіксована
                for (int col = 1; col <= lastCol; col++)
                {
                    if (col == 6)
                    {
                        ws.Column(col).Width = 50;
                        ws.Column(col).Style.WrapText = true;
                    }
                    else if (col == 5)
                    {
                        ws.Column(col).AutoFit();
                        ws.Column(col).Width += 1;
                    }
                    else
                    {
                        ws.Column(col).AutoFit();
                    }
                }

                try
                {
                    package.Save();

                    return true;
                }
                catch
                {
                    CustomMessage.Show("Спочатку закрий Попередній файл!", MessageBoxIcon.Hand);
                }

                return false;
            }
        }


        private void ShowLogs(object sender, EventArgs e)
        {
            ClickedMenu(sender as Button);

            using (LogList _ll = new LogList())
            {
                _ll.ShowDialog();
            }
        }

        private void SetVitraty(object sender, EventArgs e)
        {
            ClickedMenu(sender as Button);

            panelContent.Controls.Clear();
            Vitraty _vitr = new Vitraty();

            panelContent.Controls.Add(_vitr);
        }
    }

    public enum BUTTONS_MENU : byte
    {
        ZAKAZ_LIST,
        VYTRATY,
        SKLAD,
        LOG_LIST
    }
}
