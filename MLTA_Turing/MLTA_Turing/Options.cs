using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTA_Turing
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        public int r= 0;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            r = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            r = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            r = 3;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
            try
            {
                if (textBox3.Text != "")
                    if (Convert.ToDouble (textBox3.Text) > 50 ) textBox3.Text = "3";
                    else
                        if (Convert.ToDouble(textBox3.Text) < 0.001 ) textBox3.Text = "3";
            }
            catch
            {
                MessageBox.Show("Можна вводити тільки числа , які не менше 0,001 і не більше 50,0", "Помилка вводу", MessageBoxButtons.OK);
                textBox3.Text = "3";
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                    if (Convert.ToInt32(textBox1.Text) >99999999) textBox1.Text = "50000";
                    else
                        if (Convert.ToInt32(textBox1.Text) < 0) textBox1.Text = "50000";
            }
            catch
            {
                MessageBox.Show("Можна вводити тільки числа , які не менше 0 і не більше 99999999", "Помилка вводу", MessageBoxButtons.OK);
                textBox1.Text = "3";
            }
        }

    }
}
