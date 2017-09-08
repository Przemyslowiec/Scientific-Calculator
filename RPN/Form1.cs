using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private rpn n = new rpn();
      
    
        private void button_click(object sender, EventArgs e)
        {

            string text = textBox1.Text;
            textBox1.Text = n.calculate(text).ToString();
        }

        private void button_click_to_text(object sender, EventArgs e)
        {
            textBox1.Text += ((sender as Button).Text.ToString());
        }

        private void button33_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            double s;
            double.TryParse(textBox1.Text, out s);
            n.M = s;
           
        }

        private void button35_Click(object sender, EventArgs e)
        {
            textBox1.Text += n.M.ToString();
        }
    }
}
