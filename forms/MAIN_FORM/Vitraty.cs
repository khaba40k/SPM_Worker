using SPM_Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class Vitraty : UserControl
    {
        public Vitraty()
        {
            InitializeComponent();

            soldInterface1.SET(null, 0, true);

            tableJurnal.CellPainting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var row = tableJurnal.Rows[e.RowIndex];
                    var cell1Empty = string.IsNullOrEmpty(row.Cells[0].Value?.ToString());
                    var cell2Empty = string.IsNullOrEmpty(row.Cells[1].Value?.ToString());

                    // Якщо 1-ша і 2-га клітинки пусті
                    if (cell1Empty && cell2Empty)
                    {
                        // Малюємо фон
                        e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);

                        // Малюємо текст
                        TextRenderer.DrawText(e.Graphics,
                                              e.FormattedValue?.ToString() ?? "",
                                              e.CellStyle.Font,
                                              e.CellBounds,
                                              e.CellStyle.ForeColor,
                                              TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                        // Малюємо жирну нижню рамку
                        using (var pen = new Pen(Color.Black, 3)) // товщина 3 пікселі
                        {
                            e.Graphics.DrawLine(pen,
                                                e.CellBounds.Left,
                                                e.CellBounds.Bottom - 1,
                                                e.CellBounds.Right,
                                                e.CellBounds.Bottom - 1);
                        }

                        e.Handled = true; // вимикає стандартне малювання бордерів
                    }
                }
            };

            tableJurnal.CellContentClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var cell = tableJurnal.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (cell is DataGridViewButtonCell && cell.Tag is JurnalRecord)
                    {
                        JurnalRecord _val = (JurnalRecord)cell.Tag;

                        if (DialogResult.Yes == CustomMessage.Show($"{_val.DATE} " +
                            $"{_val.SERVICE_NAME} {_val.TYPE_NAME ?? ""} " +
                            $"{_val.COUNT} x {_val.COSTS.ToString("F2")} грн.?", "Видалення",
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Exclamation, 
                            new MessButText { Yes = "Видалити запис" }))
                        {
                            //Видалення з журналу
                            if (SERVICE_INFO.DELETE_VITRATY_BY_ID(new int[1] { _val.ID }, 
                                out string message))
                            {
                                RefreshJurnal();
                                CustomMessage.Show(message, MessageBoxIcon.Information);
                            }
                            else
                            {
                                CustomMessage.Show(message, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };

            datetimeJurnal.Value = DateTime.Now;

            tabJurnal.HandleCreated += Vitraty_SizeChanged;
            tabWrite.HandleCreated += Vitraty_SizeChanged;
        }

        private void Vitraty_SizeChanged(object sender, EventArgs e)
        {
            soldInterface1.Height = tabWrite.Height - 42;
            soldInterface1.Width = tabWrite.ClientSize.Width;
            
            tableJurnal.Height = tabJurnal.ClientSize.Height - datetimeJurnal.Height;
        }

        private void datetimeJurnal_ValueChanged(object sender, EventArgs e)
        {
            RefreshJurnal();
        }

        private void RefreshJurnal()
        {
            Jurnal _j = SERVICE_INFO.GetJurnal(
                    datetimeJurnal.Value.Year,
                    (byte)datetimeJurnal.Value.Month);

            tableJurnal.Rows.Clear();

            if (_j.success)
            {
                SHOW_JURNAL(_j);
            }
        }

        private void SHOW_JURNAL(Jurnal _input)
        {
            List<JurnalRecord> _temp;

            string[] Redaktors = _input.GetRedaktors();

            DataGridViewRow _row;

            DataGridViewCell cellHead;

            bool HasHead;

            for (byte i = 31; i > 0; i--)
            {
                HasHead = false;

                foreach (string _redaktor in Redaktors)
                {
                    _temp = _input.GroupByDayRed(i, _redaktor);

                    if (_temp.Count > 0)
                    {
                        if (!HasHead)
                        {
                            cellHead = new DataGridViewTextBoxCell
                            {
                                Value = i,
                                Style = new DataGridViewCellStyle()
                                {
                                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                                    Font = AppFonts.LogoFont(20)
                                }
                            };

                            HasHead = true;

                            _row = new DataGridViewRow();

                            _row.Cells.Add(cellHead);
                            _row.Cells.Add(new DataGridViewTextBoxCell());
                            _row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
                            _row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
                            _row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });

                            tableJurnal.Rows.Add(_row);
                        }

                        JurnalTableUppend(_temp, _redaktor);
                    }
                }
            }
        }

        private void JurnalTableUppend(List<JurnalRecord> _input, string redaktor)
        {
            DataGridViewRow _row;

            DataGridViewCell cellTime, cellName,
                cellColor, cellCount, cellCost, 
                cellComm, cellDelete,
                cellSum, cellRedaktor;

            float sum = 0f;

            foreach (JurnalRecord _rec in _input)
            {
                _row = new DataGridViewRow();

                cellTime = new DataGridViewTextBoxCell
                {
                    Value = _rec.DATE.ToShortTimeString()
                };

                cellName = new DataGridViewTextBoxCell
                {
                    Value = _rec.SERVICE_NAME + (_rec.TYPE_NAME != null ? " (" + _rec.TYPE_NAME + ")" : "")
                };

                cellColor = new DataGridViewTextBoxCell
                {
                    Value = _rec.COLOR_NAME
                };

                cellCount = new DataGridViewTextBoxCell
                {
                    Value = _rec.COUNT
                };

                cellCost = new DataGridViewTextBoxCell
                {
                    Value = _rec.COSTS
                };

                cellComm = new DataGridViewTextBoxCell
                {
                    Value = _rec.COMM
                };

                cellDelete = new DataGridViewButtonCell
                {
                    Tag = _rec
                };

                sum += _rec.COSTS;

                _row.Cells.Add(cellTime);
                _row.Cells.Add(cellName);
                _row.Cells.Add(cellColor);
                _row.Cells.Add(cellCount);
                _row.Cells.Add(cellCost);
                _row.Cells.Add(cellComm);
                _row.Cells.Add(cellDelete);

                tableJurnal.Rows.Add(_row);
            }

            cellSum = new DataGridViewTextBoxCell
            {
                Value = sum,
                Style = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = AppFonts.TextFont(14, FontStyle.Bold | FontStyle.Italic)
                }
            };

            cellRedaktor = new DataGridViewTextBoxCell
            {
                Value = redaktor,
                Style = new DataGridViewCellStyle()
                {
                    Font = AppFonts.TextFont(15)
                }
            };

            _row = new DataGridViewRow();

            _row.Cells.Add(new DataGridViewTextBoxCell());
            _row.Cells.Add(new DataGridViewTextBoxCell());
            _row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
            _row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
            _row.Cells.Add(cellSum);
            _row.Cells.Add(cellRedaktor);

            tableJurnal.Rows.Add(_row);
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            VitratyList _OUT = new VitratyList(soldInterface1.PRODUCTS);

            if (DialogResult.Yes == CustomMessage.Show(
                _OUT.ToString(), "Підтвердження", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                new MessButText { Yes = "Зберегти", No = "Редагувати" }))
            {
                _OUT.Date = DateTime.Now;
                _OUT.token = SERVICE_INFO.TOKEN;

                if (SERVICE_INFO.SAVE_VITRATY(_OUT, out string message))
                {
                    RefreshJurnal();
                    tabControl1.SelectedIndex = 1;
                    soldInterface1.SET(null, 0, true);
                    CustomMessage.Show(message, "Результат", MessageBoxIcon.Information);
                }
                else
                {
                    CustomMessage.Show(message, "Результат", MessageBoxIcon.Hand);
                }
            }
        }
    }
}
