using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace walidator2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkData();
        }

        private void checkData()
        {
            //Widoczne usunięcie spacji
            textBox1.Text = textBox1.Text.Replace(" ", "");
            //Niewidoczne usunięcie myślników
            string input = textBox1.Text.Replace("-", "");
            char[] arrayChar = input.ToCharArray();
            int[] arrayInt = new int[input.Length];
            string output = "";
            try
            {
                for (int x = 0; x < input.Length; x++)
                {
             
                    arrayInt[x] = int.Parse(arrayChar[x].ToString());
                }

                if (checkBox1.Checked == true)
                {
                    output = checkPesel(arrayInt);
                }
                else if (checkBox2.Checked == true)
                {
                    output = checkNip(arrayInt);
                }

            }
            catch (FormatException)
            {
                output = "Dane są nieprawidłowe.";
            }
            label2.Text = output;

        }

        private string checkPesel(int [] array)
        {
            int[] pesel = array;
            int[] multiplyer = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int controlNumber = 0;
            bool valid = false;
            string sex = "";
            string day = pesel[4].ToString() + pesel[5].ToString();
            string month = pesel[2].ToString() + pesel[3].ToString();
            string year = pesel[0].ToString()+pesel[1].ToString();

            if (pesel.Length < 11 || pesel.Length > 11)
            {
                return "Długość PESEL jest niewłaściwa.";
            }
            for(int y = 0; y < pesel.Length-1; y++)
            {
                controlNumber = controlNumber + (pesel[y] * multiplyer[y]);
            }
            controlNumber = controlNumber % 10;
            controlNumber = 10 - controlNumber;
            controlNumber = controlNumber % 10; 
            if(controlNumber == pesel[10])
            {
                valid = true;
            }

            if(valid)
            {
                //Sprawdzanie płci 
                if(pesel[9]%2==0)
                {
                    sex = "Kobieta";
                }
                else
                {
                    sex = "Mężczyzna";
                }
                //Sprawdzenie daty urodzenia
                int chMonth = int.Parse(month);
                if(chMonth/10 >=8)
                {
                    year = "18" + year;
                    chMonth = chMonth - 80;
                }
                else if(chMonth / 10 < 2)
                {
                    year = "19" + year;
                }
                else if(chMonth / 10 >= 2 && chMonth / 10 < 4)
                {
                    year = "20" + year;
                    chMonth = chMonth - 20;
                }
                else if(chMonth / 10 >=4 && chMonth / 10 < 6)
                {
                    year = "21" + year;
                    chMonth = chMonth - 40;
                }
                else
                {
                    year = "22" + year;
                    chMonth = chMonth - 60;
                }
                if (chMonth < 10)
                {
                    month = "0" + chMonth.ToString();
                }
                else
                {
                    month = chMonth.ToString();
                }
                string dateBirth = day + "-" + month + "-" + year + " r.";

                return "Pesel jest prawidłowy. " + sex + ", data urodzenia: " +dateBirth;
            }
            else
            {
                return "Pesel jest nieprawidłowy";
            }
        }

        private string checkNip(int[] array)
        {
            int[] nip = array;
            int[] multiplyer = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            int controlNumber = 0;
            if (nip.Length<10 || nip.Length>10)
            {
                return "Długość NIP jest niewłaściwa.";
            }
            for(int z = 0; z < 9; z++)
            {
                controlNumber = controlNumber + (nip[z] * multiplyer[z]);
            }
            controlNumber = controlNumber % 11;
            if(controlNumber == nip[9])
            {
                return "NIP jest prawidłowy.";
            }
            else
            {
                return "NIP jest nieprawidłowy.";
            }
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Tu wprowadź NIP..." || textBox1.Text == "Tu wprowadź PESEL...")
            {
                textBox1.Text = "";
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false && checkBox2.Checked == false)
            {
                textBox1.Text = "Określ dane.";
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            else if(checkBox1.Checked==true && checkBox2.Checked == true)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Text = "Tu wprowadź PESEL...";
                textBox1.MaxLength = 11;
                checkBox2.Checked = false;
                label2.Text = "";
            }
            else if(checkBox1.Checked == true && checkBox2.Checked == false)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Text = "Tu wprowadź PESEL...";
                textBox1.MaxLength = 11;
                label2.Text = "";
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                textBox1.Text = "Określ dane.";
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            else if (checkBox2.Checked == true && checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Text = "Tu wprowadź NIP...";
                textBox1.MaxLength = 13;
                checkBox1.Checked = false;
                label2.Text = "";
            }
            else if (checkBox2.Checked == true && checkBox1.Checked == false)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Text = "Tu wprowadź NIP...";
                textBox1.MaxLength = 13;
                label2.Text = "";
            }
        }
    }
}
