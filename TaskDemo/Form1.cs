using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                int sum = 0;
                ShowValue method = () => { textBox1.Text = sum.ToString(); };
                for (int i = 0; i < 10000; i++)
                {
                    Thread.Sleep(1000);
                    sum += i;
                    
                    textBox1.Invoke(method,null);
                }
            });
        }

        private delegate void ShowValue();
    }
}
