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

        int num = 0;
        

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> imagePaths = new List<string>
            {
                @"C:\Users\Flying\Desktop\numsPic\1.png",
                @"C:\Users\Flying\Desktop\numsPic\1(1).png",
               // @"C:\Users\Flying\Desktop\numsPic\3.png"
            };

            scrollPicturebox1.LoadImages(imagePaths);
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.scrollPicturebox1.Size = new Size(this.panel2.Size.Width - 10, this.panel2.Size.Height - 10);
            this.scrollPicturebox1.SetLabelLongPressHandler(this.scrollPicturebox1.label1,addNum);
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void addNum(object sender,EventArgs e) 
        {
            num++;
            textBox1.Text = num.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scrollPicturebox1.LoadBackgroundImage(@"C:\Users\Flying\Desktop\rock.png");
            scrollPicturebox1.SetCrosshairLineWidth(4);
            scrollPicturebox1.SetCrosshairColor(Color.Red);
            scrollPicturebox1.SetscrollSpeed(20);
        }


    }
}
