using System.Drawing;
using System.Windows.Forms;

namespace SunUi
{
    partial class scrollPicturebox
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }




        #region Windows Form Designer generated code



        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pictureBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(19, 17);
            panel1.Margin = new Padding(4, 1, 4, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(407, 227);
            panel1.TabIndex = 0;
            panel1.Scroll += panel1_Scroll;
            panel1.Dock = DockStyle.None;
            
            //panel1.Anchor = AnchorStyles.None;
            // 
            // pictureBox1
            // 
            pictureBox1.Controls.Add(label4);
            pictureBox1.Controls.Add(label2);
            pictureBox1.Controls.Add(label1);
            pictureBox1.Controls.Add(label3);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 1, 41, 17);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 220);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.LoadCompleted += PictureBox1_LoadCompleted;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(50, 255, 255, 255);
            label4.Font = new Font("Webdings", 9F, FontStyle.Regular, GraphicsUnit.Point, 2);
            label4.Location = new Point(100, 50);
            label4.Name = "label4";
            label4.Size = new Size(21, 19);
            label4.TabIndex = 7;
            label4.Text = "6";
            label4.Click += label4_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(50, 255, 255, 255);
            label2.Font = new Font("Webdings", 9F, FontStyle.Regular, GraphicsUnit.Point, 2);
            label2.Location = new Point(100, 23);
            label2.Name = "label2";
            label2.Size = new Size(21, 19);
            label2.TabIndex = 5;
            label2.Text = "5";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(50, 255, 255, 255);
            label1.Font = new Font("Webdings", 9F, FontStyle.Regular, GraphicsUnit.Point, 2);
            label1.Location = new Point(100, 50);
            label1.Name = "label1";
            label1.Size = new Size(21, 19);
            label1.TabIndex = 4;
            label1.Text = "3";
            label1.Click += Label_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(50, 255, 255, 255);
            label3.Font = new Font("Webdings", 9F, FontStyle.Regular, GraphicsUnit.Point, 2);
            label3.Location = new Point(100, 50);
            label3.Name = "label3";
            label3.Size = new Size(21, 19);
            label3.TabIndex = 6;
            label3.Text = "4";
            // 
            // scrollPicturebox
            // 
            AutoSize = true;
            Controls.Add(panel1);
            Margin = new Padding(4, 1, 4, 1);
            Name = "scrollPicturebox";
            Size = new Size(467, 261);
            Load += scrollPicturebox_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pictureBox1.ResumeLayout(false);
            pictureBox1.PerformLayout();
            ResumeLayout(false);
        }


        #endregion

        private Panel panel1;


        public Label label4;
        public Label label3;
        public Label label2;
        public Label label1;
       
        private PictureBox pictureBox1;
        
    }
}
