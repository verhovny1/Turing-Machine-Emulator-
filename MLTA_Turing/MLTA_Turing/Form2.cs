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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
/*
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            Form1 dfg = new Form1();

            for (int i = 0; i < dfg.Kol_Rows; i++)
            {
                //comboBox2.Items.Add(dfg.MasSimv[i]);
                comboBox2.Items[i+1 ] = dfg.MasSimv[i];
            }

            for (int i = 0; i < dfg.Kol_Columns; i++)
            {
                comboBox1.Items.Add("Q" + Convert.ToString(i));
                //comboBox1.Items[i+1 ] = "Q" + Convert.ToString(i);
            }

            
           /* comboBox3.Items[0] = "L";
            comboBox3.Items[1] = "R";
            comboBox3.Items[2] = "None";
           */ 

          /*  comboBox3.Items.Add("L");
            comboBox3.Items.Add("R");
            comboBox3.Items.Add("None");
            */

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != ""  /* &&  comboBox2.Text != "" */ &&  comboBox3.Text !="" )
            {
                //Form1 dfg = new Form1();                    
                this.Close();

            }
        }

       
       
        
    }
}
