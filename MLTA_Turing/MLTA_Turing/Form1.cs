using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;//для записування в файл і навпаки

namespace MLTA_Turing
{



    public partial class Form1 : Form
    {

        public int Kol_Columns=0;                  // Кількість стовбців
        public int Kol_Rows = 0;                   // Кількість строк
        public String[] MasSimv = new String[50];  // Масив назв строк


        public int[,] MasivStaniv = new int[1, 0];  // Масив станів
        public int[,] MasivSimvoliv = new int[1, 0];
        public String[,] MasivComand = new String[1,0];



        public static void ResizeArray<T>(ref T[,] arr, int a, int b) // Зміна розміру 2Д масива http://freehabr.ru/blog/dotnet/123.html
        {
            T[,] tmp = new T[a, b];
            int с = arr.GetLength(0);
            int d = arr.GetLength(1);
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (i < с && j < d)
                        tmp[i, j] = arr[i, j];
                    else
                        tmp[i, j] = default(T);
                }
            }
            arr = tmp; 
        }

        public void ZminRozmirMas(int x , int y)
        {
            ResizeArray(ref MasivStaniv, x, y);      // Зміна розміру 2Д масива
            ResizeArray(ref MasivSimvoliv, x, y);      // Зміна розміру 2Д масива
            ResizeArray(ref MasivComand, x, y);      // Зміна розміру 2Д масива
        }


        public int Kol_El_Vh_Str = 0;
        public String[] MasivVhidnStroki = new String[4];
        public int Kol_Formul = 0;





        public int MaxColKrociv ;
        public string ZagNazvaStaniv ;

        public double timer = 0;
        public double interval_mish_krok ;
        public bool pidsv_Formul;


        public Form1()
        {
            InitializeComponent();
           

            ZagNazvaStaniv = "Q";
            MaxColKrociv = 50000;
            interval_mish_krok = 500;

            dataGridView1.Columns.Add(ZagNazvaStaniv + Convert.ToString(Kol_Columns), ZagNazvaStaniv + Convert.ToString(Kol_Columns));
            Kol_Columns++;
            dataGridView1.EnableHeadersVisualStyles = false;

            
            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str ++;

            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), ""); 
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;
            
            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str) ,"");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;
            
            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;

            toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);

            dataGridView2.Rows.Add();
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[0].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[1].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[2].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[3].Value));

            comboBox1.Items.Add(ZagNazvaStaniv+"0" );



            interval_mish_krok = 0.5 * 1000;
            pidsv_Formul = true;



            saveFileDialog1.Filter = "tx files (*.tx)|*.tx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            openFileDialog1.Filter = "tx files (*.tx)|*.tx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();



        }



        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Columns.Add(ZagNazvaStaniv + Convert.ToString(Kol_Columns), ZagNazvaStaniv + Convert.ToString(Kol_Columns));
            Kol_Columns++;

            ZminRozmirMas(Kol_Columns, Kol_Rows); // Зміна розміру 2Д масивів
            toolStripStatusLabel3.Text = Kol_Columns + " , " + Kol_Rows; // вивод розмірності матр правил
            comboBox1.Items.Add(ZagNazvaStaniv + Convert.ToString(Kol_Columns - 1));
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Kol_Columns > 0)
            {
                Kol_Columns--;
                dataGridView1.Columns.Remove(ZagNazvaStaniv + Convert.ToString(Kol_Columns));
                ZminRozmirMas(Kol_Columns, Kol_Rows);// Зміна розміру 2Д масивів
                toolStripStatusLabel3.Text = Kol_Columns + " , " + Kol_Rows; // вивод розмірності матр правил
                comboBox1.Items.RemoveAt( Kol_Columns ); 
            }
            
            if (Kol_Columns <= 0) Kol_Rows = 0;
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            if (Kol_Columns > 0)
            {
                    string s = textBox4.Text;
                    bool df = false;
                    for (int i = 0 ; i < Kol_Rows;i++) if ( MasSimv[i] == s )
                    {
                        MessageBox.Show("Не можна додавати вже існуючі символи", "Помилка", MessageBoxButtons.OK);
                        df = true;
                        break;
                    }

                    if ( df == false )
                    {
                        Array.Resize(ref MasSimv, MasSimv.Length + 1); //++
                        MasSimv[Kol_Rows] = s;
                        DataGridViewRow row = new DataGridViewRow();
                        row.HeaderCell.Value = s;
                        dataGridView1.Rows.Add(row);
                        Kol_Rows++;
                        ZminRozmirMas(Kol_Columns, Kol_Rows);// Зміна розміру 2Д масивів
                        toolStripStatusLabel3.Text = Kol_Columns + " , " + Kol_Rows; // вивод розмірності матр правил


                       

                    }

                    textBox4.Text = "";
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            if (Kol_Rows > 0)
            {
                Kol_Rows--;
                dataGridView1.Rows.RemoveAt(Kol_Rows);
                ZminRozmirMas(Kol_Columns, Kol_Rows);// Зміна розміру 2Д масивів
                toolStripStatusLabel3.Text = Kol_Columns + " , " + Kol_Rows; // вивод розмірності матр правил
            }


        }






        private void button7_Click(object sender, EventArgs e)
        {

          //int x = Convert.ToInt32( dataGridView1.CurrentCell.Value ) ;
          

            Form2 asd = new Form2();
            
                for (int i = 0; i < Kol_Rows; i++)
                {
                    asd.comboBox2.Items.Add(MasSimv[i]);
                    asd.comboBox2.Text = MasSimv[0];
                }

                for (int i = 0; i < Kol_Columns; i++)
                {
                    asd.comboBox1.Items.Add(ZagNazvaStaniv + Convert.ToString(i));
                    asd.comboBox1.Text = ZagNazvaStaniv+"0";
                }
                asd.comboBox3.Items.Add("L");
                asd.comboBox3.Items.Add("R");
                asd.comboBox3.Items.Add("N");
                asd.comboBox3.Items.Add("!");
               
                asd.comboBox3.Text = "N";


            asd.ShowDialog();

            if (asd.comboBox1.Text != ""  && asd.comboBox3.Text != "" && dataGridView1.CurrentCellAddress.X >= 0 && dataGridView1.CurrentCellAddress.Y >= 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X].Value = asd.comboBox1.Text + " " + asd.comboBox2.Text + " " + asd.comboBox3.Text;
                MasivStaniv[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] = asd.comboBox1.SelectedIndex; //dataGridView1.CurrentCellAddress.X;      
                MasivSimvoliv[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] = asd.comboBox2.SelectedIndex; // dataGridView1.CurrentCellAddress.Y ;
                MasivComand[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] = asd.comboBox3.Text;
                Kol_Formul++; 
                toolStripStatusLabel7.Text = Convert.ToString ( Kol_Formul ) ; // вивод кількості формул
            }
        }







        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentCellAddress.Y >= 0 && dataGridView1.CurrentCellAddress.X >= 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X].Value = "";
                MasivStaniv[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] = -1; //"";
                MasivSimvoliv[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] =  -1;
                MasivComand[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y] = "";

                if (Kol_Formul > 0) Kol_Formul--;
                toolStripStatusLabel7.Text = Convert.ToString(Kol_Formul); // вивод кількості формул
            }
            // dataGridView1.Rows[0].Cells[0].Value = "111";
            // label1.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
           
        }





        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
           // http://msdn.microsoft.com/ru-ru/library/vstudio/471w8d85(v=vs.100).aspx
            //http://www.csharpcoderr.com/2012/07/blog-post_31.html
           // if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) dataGridView2.Columns.Add("Q", "Q");


            if (e.KeyChar == 13)
            {
                dataGridView2.Columns.Add( Convert.ToString(Kol_El_Vh_Str) , "");
                dataGridView2.Columns[Kol_El_Vh_Str].Width = 50; 
                Kol_El_Vh_Str++;
                toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);

                Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length + 1);// Зміна розміру одномірного масива
                ReorganizaciyaComboBox2();

            }
            else

            if (ConsoleModifiers.Alt !=  0  && e.KeyChar == 13 ) 
            {

                if (dataGridView2.CurrentCellAddress.X > 0)
                {
                    dataGridView2.Columns.Remove(Convert.ToString(Kol_El_Vh_Str - 1));
                    Kol_El_Vh_Str--;
                    toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
                    Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length - 1);// Зміна розміру одномірного масива
                    ReorganizaciyaComboBox2();
                }
                
            }

        }





        private void button11_Click(object sender, EventArgs e)
        {
            if (Kol_El_Vh_Str  > 1)
            {
                dataGridView2.Columns.Remove(Convert.ToString(Kol_El_Vh_Str -1 ));
                Kol_El_Vh_Str--;
                Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length - 1);// Зміна розміру одномірного масива
                ReorganizaciyaComboBox2();
                toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Add( Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;
            toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
            Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length + 1);// Зміна розміру одномірного масива
            ReorganizaciyaComboBox2();

        }

        private void button8_Click(object sender, EventArgs e)
        {
        /*    if (dataGridView1.CurrentCellAddress.Y >= 0)
            {
                dataGridView1.Columns.RemoveAt(dataGridView1.CurrentCellAddress.Y );
            }
         */   
        }

        private void button9_Click(object sender, EventArgs e)
        {
        /*    if (dataGridView1.CurrentCellAddress.X >= 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCellAddress.X );
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( comboBox1.Text != "" )
            {

                string r = "";
                for ( int i = 1 ; i < comboBox1.Text.Length ; i++ ) r += comboBox1.Text[i];
                int sad = Convert.ToInt32(r);


                for (int i = 0; i < Kol_Columns; i++) dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.White;
                dataGridView1.Columns[sad].HeaderCell.Style.BackColor =  Color.Green;
                //for (int i = 0; i < Kol_Rows; i++) dataGridView1[sad, i].Style.BackColor = Color.Green;

                poz_x = comboBox1.SelectedIndex ;
               // MessageBox.Show(Convert.ToString( poz_x ), "comboBox2.SelectedIndex", MessageBoxButtons.OK);
            }
            /*
            dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.Red;
            dataGridView1.Columns[1].HeaderCell.Style.BackColor = Color.Red;
            */
        }


        public void ReorganizaciyaComboBox2()
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;

            for (int i = 0; i < Kol_El_Vh_Str; i++)
            {
                comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[i].Value));
                MasivVhidnStroki[i] = Convert.ToString(comboBox2.Items[i]); //оновлення масиву ввыдноъ строки
            }

        }


        
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ReorganizaciyaComboBox2(); 
               
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                int sad = comboBox2.SelectedIndex;
               // MessageBox.Show( Convert.ToString( sad ) , "comboBox2.SelectedIndex", MessageBoxButtons.OK);
            

                for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;
                dataGridView2.Rows[0].Cells[sad].Style.BackColor = Color.Green;

                pozicia_na_stroke = sad ; // текща позіія на строкі
            }
        }





        public void zapusk ()
        {
                dataGridView2.ReadOnly = true;
             //   dataGridView2.DefaultCellStyle.SelectionBackColor  = Color.White;
             //   dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                виконатиToolStripMenuItem.Enabled = false;
                нілаштуванняToolStripMenuItem.Enabled = false;
                довідкаToolStripMenuItem.Enabled = false;
                проПрограмуToolStripMenuItem.Enabled = false;
                файлToolStripMenuItem.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                toolStripSplitButton1.Enabled = false;
                toolStripSplitButton2.Enabled = true;
                toolStripSplitButton3.Enabled = true;
                toolStripSplitButton4.Enabled = true;
                toolStripSplitButton5.Enabled = false;
        }

        public void ostanovka()
        {
            dataGridView2.ReadOnly = false;
           // dataGridView2.DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
           // dataGridView1.DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
            виконатиToolStripMenuItem.Enabled = true;
            нілаштуванняToolStripMenuItem.Enabled = true;
            довідкаToolStripMenuItem.Enabled = true;
            проПрограмуToolStripMenuItem.Enabled = true;
            файлToolStripMenuItem.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button3.Enabled = true; ;
            button6.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            toolStripSplitButton1.Enabled = false;
            toolStripSplitButton2.Enabled = false;
            toolStripSplitButton3.Enabled = false;
            toolStripSplitButton4.Enabled = false;
            toolStripSplitButton5.Enabled = false;


            //прибрання виділень
            for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;

            for (int i = 0; i < Kol_Columns; i++) dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.White;

            for (int i = 0; i < Kol_Rows; i++)
                for (int j = 0; j < Kol_Columns; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            comboBox2.Text = "";
            comboBox1.Text = "";



            timer1.Stop();
            timer1.Enabled = false;
        }







        public int poz_x;
        public int poz_y;
        public int ko_ko;
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && comboBox1.Text != "")
            {             
                zapusk ();

                timer = 0;
                timer1.Enabled = true;
                timer1.Interval = (int)interval_mish_krok;
                timer1.Start();
                ko_ko = 0;
            }

             
        }



        public int pozicia_na_stroke;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ko_ko +=1;
            
            poz_y = 0;
            for (int i = 0; i < Kol_Rows; i++) if ( MasivVhidnStroki[pozicia_na_stroke] == MasSimv[i] ) poz_y = i; // знаxодження позиції  символу

            //MessageBox.Show("x= " + Convert.ToString(poz_x) + "y= " + Convert.ToString(poz_y) + "pozicia_na_stroke= " + Convert.ToString(pozicia_na_stroke), "comboBox2.SelectedIndex", MessageBoxButtons.OK);

            if (MasivComand[poz_x, poz_y] == "" || MasivStaniv[poz_x, poz_y] < 0 || MasivSimvoliv[poz_x, poz_y] < 0)
            {
                ostanovka();
            }
            else
            {
                //------------------------------------------------------------------------------------------------------------------
                //закраска стану теперішньго стану
                if (pidsv_Formul == true)
                {
                    for (int i = 0; i < Kol_Columns; i++) dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dataGridView1.Columns[poz_x].HeaderCell.Style.BackColor = Color.Green;

                    for (int i = 0; i < Kol_Rows; i++)
                        for (int j = 0; j < Kol_Columns; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        }
                    dataGridView1.Rows[poz_y].Cells[poz_x].Style.BackColor = Color.Green;
                }
                //------------------------------------------------------------------------------------------------------------------




                MasivVhidnStroki[pozicia_na_stroke] = MasSimv[MasivSimvoliv[poz_x, poz_y]]; // заміна  елемента в строкі
                dataGridView2.Rows[0].Cells[pozicia_na_stroke].Value = MasivVhidnStroki[pozicia_na_stroke]; // заміна  елемента на формі

                //------------------------------------------------------------------------------------------------------------------
                if (pidsv_Formul == true)
                {
                    //зафарбовування нової  ячейки на ленті
                    for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;
                    dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[pozicia_na_stroke];
                    dataGridView2.Rows[0].Cells[pozicia_na_stroke].Style.BackColor = Color.Green;
                    
                }
                //------------------------------------------------------------------------------------------------------------------


                //переміщення головки на  ЛЕНТІ 
                if (MasivComand[poz_x, poz_y] == "R")
                {
                    //зафарбовування нової  ячейки на ленті
                        for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;
                        dataGridView2.CurrentCell = dataGridView2[pozicia_na_stroke, 0];
                        dataGridView2.Rows[0].Cells[pozicia_na_stroke].Style.BackColor = Color.Green;
                        

                    pozicia_na_stroke++;
                    if (pozicia_na_stroke >= Kol_El_Vh_Str - 1) //dataGridView2.RowCount - 1)
                    {
                        dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
                        dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
                        Kol_El_Vh_Str++;
                        toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
                        Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length + 1);// Зміна розміру одномірного масива
                        //ReorganizaciyaComboBox2();
                    }
                }
                else if (MasivComand[poz_x, poz_y] == "L")
                {
                    pozicia_na_stroke--;

                    if (pozicia_na_stroke <= 0 ) //dataGridView2.RowCount - 1)
                    {
                        dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
                        dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
                        Kol_El_Vh_Str++;
                        toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
                        Array.Resize(ref MasivVhidnStroki, MasivVhidnStroki.Length + 1);// Зміна розміру одномірного масива
                        
                        for ( int i = Kol_El_Vh_Str-1 ; i > 0 ; i--)
                        {
                            MasivVhidnStroki[i] = MasivVhidnStroki[i - 1];
                            dataGridView2.Rows[0].Cells[i].Value = dataGridView2.Rows[0].Cells[i - 1].Value;
                        }
                        MasivVhidnStroki[0] = "";
                        dataGridView2.Rows[0].Cells[0].Value = "";

                        pozicia_na_stroke++;

                        //зафарбовування нової  ячейки на ленті
                        for (int i = 0; i < Kol_El_Vh_Str; i++) dataGridView2.Rows[0].Cells[i].Style.BackColor = Color.White;
                        dataGridView2.CurrentCell = dataGridView2[pozicia_na_stroke, 0];
                        dataGridView2.Rows[0].Cells[pozicia_na_stroke].Style.BackColor = Color.Green;
                        
                    }


                }
                else if (MasivComand[poz_x, poz_y] == "N") pozicia_na_stroke = pozicia_na_stroke;
                else if (MasivComand[poz_x, poz_y] == "!")
                {
                    ostanovka();
                }
                if (ko_ko >= MaxColKrociv) ostanovka();

                

                //знаходження позиії нового стану
                poz_x = MasivStaniv[poz_x, poz_y];


            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 0.5 * 1000;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 1 * 1000;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 2 * 1000;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 3 * 1000;
        }

        private void користувальницькийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 5 * 1000;
        }

        private void користувальницькийToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 10 * 1000;
        }





        private void новийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            Kol_Columns = 0;
            dataGridView1.Columns.Add(ZagNazvaStaniv + Convert.ToString(Kol_Columns), ZagNazvaStaniv + Convert.ToString(Kol_Columns));
            Kol_Columns++;
            Kol_Rows = 0;
            ZminRozmirMas( Kol_Columns, Kol_Rows);


            dataGridView2.Columns.Clear();
            Kol_El_Vh_Str = 0;

            dataGridView2.Columns.Add(  Convert.ToString(Kol_El_Vh_Str) , "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;

            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;

            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;

            dataGridView2.Columns.Add(Convert.ToString(Kol_El_Vh_Str), "");
            dataGridView2.Columns[Kol_El_Vh_Str].Width = 50;
            Kol_El_Vh_Str++;

            toolStripStatusLabel5.Text = Convert.ToString(Kol_El_Vh_Str);
            dataGridView2.Rows.Add();

            comboBox2.Items.Clear();
            comboBox2.Text = "";
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[0].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[1].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[2].Value));
            comboBox2.Items.Add(Convert.ToString(dataGridView2.Rows[0].Cells[3].Value));

            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Add(ZagNazvaStaniv+"0");

            

            interval_mish_krok = 0.5 * 1000;
        }


        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(saveFileDialog1.FileName);


                sw.WriteLine(Kol_El_Vh_Str);
                for (int i = 0; i < Kol_El_Vh_Str; i++) sw.WriteLine(MasivVhidnStroki[i]);



                sw.WriteLine(Kol_Columns);
                sw.WriteLine(  Kol_Rows  );
                for (int i = 0; i < Kol_Rows; i++) sw.WriteLine(MasSimv[i]);

                for (int i = 0; i < Kol_Columns; i++)
                for (int j = 0; j < Kol_Rows; j++)
                {
                    sw.WriteLine( MasivStaniv[i,j]  );
                    sw.WriteLine(MasivSimvoliv[i, j]);
                    sw.WriteLine( MasivComand[i, j]);   
                }

                sw.WriteLine( comboBox1.SelectedIndex );
                sw.WriteLine(comboBox2.SelectedIndex);
                sw.WriteLine(interval_mish_krok);
                if ( pidsv_Formul == true ) sw.WriteLine( "T") ; else  sw.WriteLine( "F") ;
                sw.WriteLine( MaxColKrociv );
                sw.WriteLine(ZagNazvaStaniv);

                sw.Close();
            }
        }


        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Zagruzka(openFileDialog1.FileName);
            }
        }


        private void Zagruzka(string str)
        {
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            


            StreamReader sr = File.OpenText(str); // привязка до файла



            //Запись    dataGridView2  і  comboBox2  і MasivVhidnStroki
            int kol = Convert.ToInt32(sr.ReadLine());
            toolStripStatusLabel5.Text = Convert.ToString(kol);
            Kol_El_Vh_Str = 0;
            Array.Resize(ref MasivVhidnStroki, kol);   // Зміна розміру одномірного масива

            for ( int i  = 0; i < kol; i++)
            {
                dataGridView2.Columns.Add(Convert.ToString(i), Convert.ToString(i) );
                dataGridView2.Columns[i].Width = 50;

                
                Kol_El_Vh_Str++;
            }
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = "0";
            dataGridView2.Rows.Add(row);

            for (int i = 0; i < Kol_El_Vh_Str; i++)
            {   
                dataGridView2.Rows[0].Cells[i].Value = sr.ReadLine();
            }



            //Запись  MasSimv i comboBox1 i  dataGridView1  і  MasivStaniv  і MasivSimvoliv i MasivComand
            Kol_Columns = Convert.ToInt32(sr.ReadLine());  //считка кількості станів ( стовбців )
            for (int i = 0; i < Kol_Columns; i++)
            {
                dataGridView1.Columns.Add(ZagNazvaStaniv + Convert.ToString(i), ZagNazvaStaniv + Convert.ToString(i));
                comboBox1.Items.Add(ZagNazvaStaniv + Convert.ToString(i));
            }

            Kol_Rows = Convert.ToInt32(sr.ReadLine());   //считка кількості елементів ( алфавіт) і кільк строк
            Array.Resize(ref MasSimv, Kol_Rows);   // Зміна розміру одномірного масива

            for (int i = 0; i < Kol_Rows; i++)
            {
                MasSimv[i] = sr.ReadLine();

                row = new DataGridViewRow();
                row.HeaderCell.Value =  MasSimv[i];
                dataGridView1.Rows.Add(row);
               // dataGridView1.Rows[i].HeaderCell.Value = row.HeaderCell.Value;
            }


            ZminRozmirMas(Kol_Columns, Kol_Rows);// Зміна розміру 2Д масивів
            toolStripStatusLabel3.Text = Kol_Columns + " , " + Kol_Rows; // вивод розмірності матр правил
            Kol_Formul = 0;

            for (int i = 0; i < Kol_Columns; i++)
                for (int j = 0; j < Kol_Rows; j++)
                {
                    MasivStaniv[i, j] = Convert.ToInt32(sr.ReadLine());
                    MasivSimvoliv[i, j]  = Convert.ToInt32(sr.ReadLine());
                    MasivComand[i, j] = sr.ReadLine();


                    if (MasivComand[i, j] != "")
                    {
                        dataGridView1.Rows[j].Cells[i].Value = ZagNazvaStaniv + MasivStaniv[i, j] + " " + MasSimv[MasivSimvoliv[i, j]] + " " + MasivComand[i, j];
                        Kol_Formul++;
                    } 
                }

            toolStripStatusLabel7.Text = Convert.ToString(Kol_Formul); // вивод кількості формул

          
                        comboBox1.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                        comboBox2.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                        interval_mish_krok  = Convert.ToInt32(sr.ReadLine());
                        string s = sr.ReadLine();
                        if ( s == "T") pidsv_Formul = true ;  else pidsv_Formul = false ;
                        MaxColKrociv = Convert.ToInt32( sr.ReadLine() );
                        ZagNazvaStaniv = sr.ReadLine();
                        
            sr.Close();
        }

        private void yesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pidsv_Formul = true;
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pidsv_Formul = false;
        }


        private void відкритиОпціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options op = new Options();
            op.textBox1.Text = Convert.ToString(MaxColKrociv);

            op.textBox2.Text = ZagNazvaStaniv;

            op.textBox3.Text = Convert.ToString( interval_mish_krok/1000 );

            if (pidsv_Formul == true) op.radioButton1.Checked = true; else op.radioButton2.Checked = true;

            op.ShowDialog();

            if (op.r == 2 || op.r == 3)
            {
               
                    MaxColKrociv = Convert.ToInt32(op.textBox1.Text);
                    ZagNazvaStaniv = op.textBox2.Text;
                    interval_mish_krok = Convert.ToDouble(op.textBox3.Text) * 1000;
                    if (op.radioButton1.Checked == true) pidsv_Formul = true; else pidsv_Formul = false;
            
            }
        }


        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stvorenay_Formi("Про програму");
        }



        private void Stvorenay_Formi(string str)
        {
            Form ProProgramu = new Form();
            ProProgramu.Text = str;
            ProProgramu.Width = 500;
            ProProgramu.Height = 400;
            ProProgramu.MaximizeBox = false;
            ProProgramu.Location = this.Location;
            ProProgramu.FormBorderStyle = this.FormBorderStyle;
            ProProgramu.Show();
            //ProProgramu.ShowDialog();

            ProProgramu.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            ProProgramu.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ProProgramu.ClientSize = new System.Drawing.Size(888 - 200, 535);




            RichTextBox kniga = new RichTextBox();
            // kniga.Font = new Font("Tahoma", 9, FontStyle.Italic | FontStyle.Underline   /*  Bold*/);
            kniga.Font = new Font("Tahoma", 9, FontStyle.Italic);

            if (str == "Допомога")
            {
                kniga.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold | FontStyle.Underline);
                kniga.AppendText("1. Загальні правила користування \n");


                kniga.ClientSize = new System.Drawing.Size(888 - 200 - 15, 535 - 15);
                ProProgramu.ClientSize = new System.Drawing.Size(888 - 200, 535);
            }
            else
                if (str == "Про програму")
                {
                    kniga.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold | FontStyle.Underline);
                    kniga.AppendText("Назва програми : Машина Тюрінга  \n");
                    kniga.AppendText("Написана  : 20.11.2014  \n");
                    kniga.AppendText("Мова програмування : C#  \n");
                    kniga.AppendText("Середовище розробки  : Visual Studio Professional 2013 with Update 3 (x86) - DVD (Russian) \n");

                    kniga.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold | FontStyle.Underline);
                    kniga.AppendText("\nПризначення \n");
                    kniga.AppendText("Програма придназначена для створення, тестування та перевірки працездатності алгоритмів  \n");
                    kniga.AppendText("їх налагодженню та покроковій перевірці. \n");

                    kniga.AppendText(" \n");
                    kniga.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold | FontStyle.Underline);
                    kniga.AppendText("\n   Ініформація про розробника   \n");
                    kniga.AppendText("   Автор  : Verhovny Menko   \n");
                    kniga.AppendText("   Skype  : maks999ify   \n");
                    kniga.AppendText("   Пошта : maksim.28.10.1994@gmail.com   \n");
                    kniga.AppendText("   Сайт   :  http://vk.com/verhovny_menko   \n");
                    kniga.AppendText("   Телефон: +380682239563");

                    kniga.ClientSize = new System.Drawing.Size(888 - 200 - 15, 570 - 15 - 300);
                    ProProgramu.ClientSize = new System.Drawing.Size(888 - 200, 570 - 300);
                }


            kniga.ReadOnly = true;
            kniga.Location = new Point(5, 5);
            ProProgramu.Controls.Add(kniga);

        }

        private void довідкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stvorenay_Formi("Довідка");
        }

        private void відсутнійToolStripMenuItem_Click(object sender, EventArgs e)
        {
            interval_mish_krok = 1;
        }

        private void toolStripSplitButton3_ButtonClick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void toolStripSplitButton4_ButtonClick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            ostanovka();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void переносПершоїБуквиВКінецьСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka( Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p1.tx");
        }

        private void дублюванняСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka(Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p2.tx");
        }

        private void збільшенняДесятковогоЧислаНа1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka(Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p3.tx");
        }

        private void множенняДесятковогоЧислаНа2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka(Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p4.tx");
        }

        private void переводЗУнарноїСистемиВДесятковуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka(Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p5.tx");
        }

        private void додаванняУнарнихЧиселToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" ", "Подробиці Прикладу", MessageBoxButtons.OK);
            Zagruzka(Directory.GetCurrentDirectory() + (char)92 + "demo" + (char)92 + "p6.tx");
        }

   
     



      
    }
}
