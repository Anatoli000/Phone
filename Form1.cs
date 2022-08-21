using System;
using System.IO;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Абонент(*.txt)|*.txt|All files(*.*)|*.*";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private PrintDocument printDocument = new PrintDocument();
        private string stringToPrint;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tel_SpravDataSet.Tel_Sprav". При необходимости она может быть перемещена или удалена.
            this.tel_SpravTableAdapter.Fill(this.tel_SpravDataSet.Tel_Sprav);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите данные дляпоиска.", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)

                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.Show();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Выйти из программы?",
                "Выход",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button3);
            if (dr == DialogResult.OK)
            {
                this.tel_SpravTableAdapter.Update(this.tel_SpravDataSet);
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить запись?",
                "Удаление",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button3);
            if (dr == DialogResult.OK)
            {
                dataGridView1.AllowUserToDeleteRows = true;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                    this.tel_SpravTableAdapter.Update(tel_SpravDataSet);
                    this.tel_SpravTableAdapter.Fill(this.tel_SpravDataSet.Tel_Sprav);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "Добавить новый контакт?",
           "Сообщение",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1,
           MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                dataGridView1.AllowUserToAddRows = true;
                Form3 form3 = new Form3();
                form3.Owner = this;
                form3.Show();
            }
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            Opacity = 0.5;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Opacity = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox3.Text == "") || (maskedTextBox4.Text == ""))
            {
                MessageBox.Show("Поля Имя и Телефон должны быть заполненны.", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show(
          "Сохранить изменения?",
          "Сообщение",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {    
                    this.tel_SpravTableAdapter.Update(this.tel_SpravDataSet);
                    dataGridView1.Refresh();
                    dataGridView1.AllowUserToDeleteRows = false;
                    button5.Visible = false;
                    button4.Visible = true;
                    button3.Visible = true;
                    maskedTextBox10.Enabled = false;
                    maskedTextBox9.Enabled = false;
                    maskedTextBox7.Enabled = false;
                    maskedTextBox4.Enabled = false;
                    button7.Visible = true;
                    button2.Visible = true;
                    button6.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    this.dataGridView1.AllowUserToAddRows = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
          "Хотите внести изменения?",
          "Сообщение",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                dataGridView1.AllowUserToAddRows = true;
                button5.Visible = true;
                button3.Visible = false;
                button6.Visible = true;
                button7.Visible = false;
                button4.Visible = false;
                button2.Visible = false;
                label3.Visible = true;
                textBox3.Visible = true;
                maskedTextBox10.Enabled = true;
                maskedTextBox9.Enabled = true;
                maskedTextBox7.Enabled = true;
                maskedTextBox4.Enabled = true;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
            ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
        }

        private void maskedTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text.Length == 1)
                ((MaskedTextBox)sender).Text = ((MaskedTextBox)sender).Text.ToUpper();
            ((MaskedTextBox)sender).Select(((MaskedTextBox)sender).Text.Length, 0);
        }

        private void maskedTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text.Length == 1)
                ((MaskedTextBox)sender).Text = ((MaskedTextBox)sender).Text.ToUpper();
            ((MaskedTextBox)sender).Select(((MaskedTextBox)sender).Text.Length, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
          "Отменить изменения?",
          "Сообщение",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                button5.Visible = false;
                button7.Visible = true;
                button3.Visible = true;
                textBox3.Visible = true;
                label3.Visible = false;
                textBox3.Visible = false;
                maskedTextBox10.Enabled = true;
                dataGridView1.ReadOnly = true;
                maskedTextBox10.Enabled = false;
                maskedTextBox9.Enabled = false;
                maskedTextBox7.Enabled = false;
                maskedTextBox4.Enabled = false;
                button6.Visible = false;
                button4.Visible = true;
                button2.Visible = true;
                this.dataGridView1.AllowUserToAddRows = false;
                this.tel_SpravTableAdapter.Fill(this.tel_SpravDataSet.Tel_Sprav);
            }
        }
        private void ReadFile()
        {
            string docName = "Абонент.txt";
            string docPath = @"";
            printDocument1.DocumentName = docName;
            using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                stringToPrint = reader.ReadToEnd();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "Распечатоть данные об обоненте?",
           "Сообщение",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1,
           MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                // сохраняем текст в файл
                StreamWriter streamWraiter = new StreamWriter(saveFileDialog1.FileName);
                {
                    if (maskedTextBox7.Text == "")
                        maskedTextBox7.Text = "Данные не указаны";
                    if (maskedTextBox9.Text == "")
                        maskedTextBox9.Text = "Данные не указаны";
                    if (maskedTextBox10.Text == "")
                        maskedTextBox10.Text = "Данные не указаны";
                    textBox3.Visible = true;
                    streamWraiter.WriteLine("Имя абонента:\n" + textBox3.Text);
                    streamWraiter.WriteLine("\nТелефон абонента:\n" + maskedTextBox4.Text);
                    streamWraiter.WriteLine("\nEmail абонента:\n" + maskedTextBox7.Text);
                    streamWraiter.WriteLine("\nМесто работы:\n" + maskedTextBox9.Text);
                    streamWraiter.WriteLine("\nЗанемаемая должность:\n" + maskedTextBox10.Text);
                    streamWraiter.Close();
                    textBox3.Visible = false;
                    this.tel_SpravTableAdapter.Fill(this.tel_SpravDataSet.Tel_Sprav);
                    ReadFile();
                    PrintDialog printDialog = new PrintDialog();
                    // установка объекта печати для его настройки
                    printDialog.Document = printDocument1;
                    // если в диалоге было нажато ОК
                    if (printDialog.ShowDialog() == DialogResult.OK)
                        printDialog.Document.Print(); // печатаем
                } 
            } 
        } 

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
            ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Устанавливает значение charactersOnPage в количество символов
            // из stringToPrint, которая будет вписываться в границы страницы.
            e.Graphics.MeasureString(stringToPrint, this.Font,
            e.MarginBounds.Size, StringFormat.GenericTypographic,
            out charactersOnPage, out linesPerPage);

            // Рисует строку в границах страницы
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            // Удалить часть строки, которая была напечатана.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Проверяем, нужно ли печатать больше страниц.
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tel_SpravTableAdapter.Update(this.tel_SpravDataSet);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "help.chm";
            var openHelp = new System.Diagnostics.Process();
            openHelp.StartInfo.FileName = help;
            openHelp.StartInfo.UseShellExecute = true;
            openHelp.Start();
        }
    }
}