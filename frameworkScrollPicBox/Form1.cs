using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frameworkScrollPicBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            scrollPicturebox1.LoadImage(@"C:\Users\Flying\Desktop\rock.png");
            this.textBox1.Text=panel2.Size.Width.ToString();
            this.textBox2.Text= scrollPicturebox1.Size.Width.ToString();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.scrollPicturebox1.Size = new Size(this.panel2.Size.Width - 10, this.panel2.Size.Height - 10);
            
        }
 
    }
}
