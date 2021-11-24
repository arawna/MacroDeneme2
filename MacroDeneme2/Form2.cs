using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroDeneme2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //b14ca5898a4e4133bbce2ea2315a1916
            string keyText = textBox1.Text;
            try
            {
                Form1.XText = Cyription.DecryptString(keyText, Form1.XText);
                Form1.YText = Cyription.DecryptString(keyText, Form1.YText);
                Form1.MoveText = Cyription.DecryptString(keyText, Form1.MoveText);
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong Key Endered Retry", "Wrong Key", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
            
        }
    }
}
