using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form3 : Form
         
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e) 
                {
           
                }
            
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if ((maskedTextBox2.Text == "") || (maskedTextBox4.Text == ""))
            {
                MessageBox.Show("Поля Имя и Телефон должны быть заполненны.", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
                if (main != null)
                
            {
             DialogResult result = MessageBox.Show(
             "Добавить запись?",
             "Сообщение",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Information,
             MessageBoxDefaultButton.Button1,
             MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    DataRow row = main.tel_SpravDataSet.Tables[0].NewRow();
                    int iD = main.dataGridView1.RowCount + 1;
                    row[0] = iD;
                    row[1] = maskedTextBox2.Text;
                    row[2] = maskedTextBox4.Text;
                    row[3] = maskedTextBox7.Text;
                    row[4] = maskedTextBox9.Text;
                    row[5] = maskedTextBox10.Text;
                    main.tel_SpravDataSet.Tables[0].Rows.Add(row);
                    main.tel_SpravTableAdapter.Update(main.tel_SpravDataSet.Tel_Sprav);
                    main.tel_SpravDataSet.Tables[0].AcceptChanges();
                    main.dataGridView1.Refresh();
                    maskedTextBox2.Text = "";
                    maskedTextBox4.Text = "";
                    maskedTextBox7.Text = "";
                    maskedTextBox9.Text = "";
                    maskedTextBox10.Text = "";
                    main.tel_SpravTableAdapter.Update(main.tel_SpravDataSet);
                    main.dataGridView1.AllowUserToAddRows = false;
                    main.tel_SpravTableAdapter.Fill(main.tel_SpravDataSet.Tel_Sprav);
                    Close();
                }
            }     
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            DialogResult result = MessageBox.Show(
             "Отменить изменения?  ",
             "Сообщение",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Information,
             MessageBoxDefaultButton.Button1,
             MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                maskedTextBox2.Text = "";
                maskedTextBox4.Text = "";
                maskedTextBox7.Text = "";
                maskedTextBox9.Text = "";
                maskedTextBox10.Text = "";
                main.dataGridView1.AllowUserToAddRows = false;
                Close();
            }
        }

        private void Form3_ResizeBegin(object sender, EventArgs e)
        {
            Opacity = 0.5;
        }

        private void Form3_ResizeEnd(object sender, EventArgs e)
        {
            Opacity = 1;
        }
            private void button3_Click(object sender, EventArgs e)
        {
           
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 main = this.Owner as Form1;
            main.dataGridView1.AllowUserToAddRows = false;
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text.Length == 1)
                ((MaskedTextBox)sender).Text = ((MaskedTextBox)sender).Text.ToUpper();
            ((MaskedTextBox)sender).Select(((MaskedTextBox)sender).Text.Length, 0);
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
    }
}
