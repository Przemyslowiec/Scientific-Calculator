﻿using System;
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
            double a;
            textBox1.Text = String.Empty;
            textBox1.Text = n.calculate(text).ToString();
        }

    }
}
