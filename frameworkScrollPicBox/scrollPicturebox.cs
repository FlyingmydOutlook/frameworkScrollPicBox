
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace SunUi
{
    public partial class scrollPicturebox : UserControl
    {
        //获取picturebox x方向的中点

        private System.Windows.Forms.Timer longPressTimer;
        
        private System.Windows.Forms.Timer scrollTimer;
        private System.Windows.Forms.Timer clickTimer;

        private bool isScrolling = false;
        private Size sizebeforeLoadImg;
        private Point crosshairPosition;
        int scrollX = 0;
        int scrollY = 0;
        private Bitmap backgroundImage;
        private Graphics g;
        private Bitmap myImage;
        private int PBwidth, PBheight;
        private Point p1, p2;
        
        private List<Tuple<Point, Point>> lines = new List<Tuple<Point, Point>>();
        private bool isDrawLine = false;
        private int scrollXfirst;
        private int scrollYfirst;

        static Color lineColor = Color.Red;
        private static Pen blackPen = new Pen(lineColor);

        static Color textColor = Color.Black; // 默认字体颜色为黑色
        static Font textFont = new Font("Arial", 10); // 默认字体

        private static float lineWidth = 1.0f;

        private Point start;
        private Point end;


        
        
        private bool isLongPress = false;
        private Label clickedLabel;
        private bool isAddMouseDown = false;
        


        public delegate void LabelClickEventHandler(object sender, EventArgs e);

        public delegate void LongPressEventHandler(object sender, EventArgs e);

        public event LongPressEventHandler LongPress;

        public delegate void LabelLongPressEventHandler(object sender, EventArgs e);



        private LabelLongPressEventHandler longPressHandler;


        internal System.Windows.Forms.Timer longPrassTimer;

        //private MouseEventArgs mouseEventArgs;

        //private MouseEventArgs resetEventArgs;

        int useMouseHover = 0;

        public scrollPicturebox()
        {

            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            InitializeCustomComponents();
            /*pictureBox1 = new PictureBox();
            panel1 = new Panel();
            Point pointOfscrollPicbox = new Point();
            pointOfscrollPicbox = (Point)this.Size;*/
            
            //this.Load += scrollPicturebox_Load;
            this.Resize += ScrollPicturebox_Resize;
            //this.ParentChanged += scrollPicturebox_ParentChanged;
            //this.Resize += scrollPicturebox_Resize;


            // 确保 panel1 和 pictureBox1 已经初始化
            /*if (this.panel1 != null && this.pictureBox1 != null & this != null)
            {

                this.panel1.Size = new Size(this.Width - 3, this.Height - 3);
                MessageBox.Show(this.Size.Width.ToString());
                MessageBox.Show(panel1.Size.Width.ToString());
                // 设置 pictureBox1 的尺寸等于 panel1 的尺寸减去 5
                this.pictureBox1.Size = new Size(this.panel1.Width - 5, this.panel1.Height - 5);
            }*/

            //this.panel1.MouseHover += new System.EventHandler(this.panel1_MouseHover);
            //this.MouseHover += new System.Windows.Forms.MouseEventHandler(this.ScrollPicturebox_MouseHover);

            //初始化resetEventArgs
            //resetEventArgs = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
            //初始化长按timer
            longPrassTimer =new System.Windows.Forms.Timer();
            longPrassTimer.Interval=50;
            longPrassTimer.Tick+=LongPressTimer_Tick;
            
            

            // 初始化 Timer
            clickTimer = new System.Windows.Forms.Timer();
            clickTimer.Interval = 5; // 设置延迟时间为500毫秒
            clickTimer.Tick += ClickTimer_Tick;

            //panel1.MouseLeave += Panel1_MouseLeave;
            //panel1.MouseEnter += Panel1_MouseEnter;

            label1.Click += Label_Click;
            label2.Click += Label_Click;
            label3.Click += Label_Click;
            label4.Click += Label_Click;


            // 启用双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
            scrollTimer = new System.Windows.Forms.Timer();
            scrollTimer.Interval = 100; // 设置检测间隔
            scrollTimer.Tick += ScrollTimer_Tick;

            //pictureBox1.Paint += pictureBox1_Paint;
            /*int centerX = pictureBox1.ClientSize.Width / 2;
            int centerY = pictureBox1.ClientSize.Height / 2;
            MessageBox.Show(centerX.ToString(), centerY.ToString());*/

            sizebeforeLoadImg = pictureBox1.Size;
            p1 = Point.Empty;
            p2 = Point.Empty;

            //初始化myImage
            myImage = new Bitmap(sizebeforeLoadImg.Width, sizebeforeLoadImg.Height);
            g = Graphics.FromImage(myImage);
            g.Clear(Color.White);

            pictureBox1.Image = myImage;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            

            scrollXfirst = panel1.HorizontalScroll.Value;
            scrollYfirst = panel1.VerticalScroll.Value;

            panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(Panel1_MouseWheel);
        }

        private void ScrollPicturebox_Resize(object sender, EventArgs e)
        {
            if (panel1 != null)
            {
                // 调整 panel1 的大小
                panel1.Size = new Size(this.Size.Width -100, this.Size.Height - 100);
            }
        }

        private void scrollPicturebox_ParentChanged(object sender, EventArgs e)
        {
           /* if (this.Parent != null)
            {
                this.Size = new Size(this.Parent.ClientSize.Width - 5, this.Parent.ClientSize.Height - 5);
                //this.Size = this.Parent.ClientSize;
                MessageBox.Show(this.Parent.Name);
                this.Margin = new Padding(41, 17, 41, 17);
            }*/

            
        }



        /* private void ScrollPicturebox_Load(object? sender, EventArgs e)
         {
             throw new NotImplementedException();
         }*/

        /* private void ScrollPicturebox_MouseMove1(object sender, System.Windows.Forms.MouseEventArgs e)
         {
             throw new NotImplementedException();
         }*/

        private void ScrollPicturebox_MouseHover(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 处理鼠标悬停事件的逻辑
            useMouseHover++; // 每次调用递增 useMouseHover
            Debug.WriteLine("MouseHover" + useMouseHover); // 输出调试信息
            //AutoScrollPanel(e);
        }
        private void panel1_MouseHover(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            useMouseHover++;
            Debug.WriteLine("MouseHover" + useMouseHover);
            //AutoScrollPanel(e);
        }

        private void ScrollPicturebox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Panel1_MouseEnter(object sender, EventArgs e)
        {
            // 释放鼠标捕获
            //Mouse.Capture(null);
        }

        /*private void Panel1_MouseLeave(object sender, EventArgs e)
        {
            // 获取鼠标在屏幕上的位置
            Point screenPosition = System.Windows.Forms.Cursor.Position;

            // 将屏幕坐标转换为相对于 panel1 的坐标
            Point panelPosition = panel1.PointToClient(screenPosition);

            // 计算鼠标移出的距离
            int deltaX = 0;
            int deltaY = 0;

            if (panelPosition.X < 0)
            {
                deltaX = panelPosition.X;
            }
            else if (panelPosition.X > panel1.Width)
            {
                deltaX = panelPosition.X - panel1.Width;
            }

            if (panelPosition.Y < 0)
            {
                deltaY = panelPosition.Y;
            }
            else if (panelPosition.Y > panel1.Height)
            {
                deltaY = panelPosition.Y - panel1.Height;
            }

            // 调整滚动位置
            int newScrollX = panel1.HorizontalScroll.Value + deltaX;
            int newScrollY = panel1.VerticalScroll.Value + deltaY;

            // 确保新的滚动位置不超过滚动范围
            if (newScrollX < 0)
            {
                newScrollX = 0;
            }
            else if (newScrollX > panel1.HorizontalScroll.Maximum)
            {
                newScrollX = panel1.HorizontalScroll.Maximum;
            }

            if (newScrollY < 0)
            {
                newScrollY = 0;
            }
            else if (newScrollY > panel1.VerticalScroll.Maximum)
            {
                newScrollY = panel1.VerticalScroll.Maximum;
            }

            // 设置新的滚动位置
            panel1.HorizontalScroll.Value = newScrollX;
            panel1.VerticalScroll.Value = newScrollY;

            // 手动触发 panel1_Scroll 事件
            ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, panel1.HorizontalScroll.Value);
            panel1_Scroll(sender, scrollEventArgs);

            // 捕获鼠标输入到 panel1
            Mouse.Capture((System.Windows.IInputElement)panel1);

            // 输出坐标以供调试
            Console.WriteLine($"Mouse left panel at: {panelPosition}, scrolled to: ({newScrollX}, {newScrollY})");
        }*/

        

        // 设置长按事件处理程序
        public void SetLabelLongPressHandler(Label label, LabelLongPressEventHandler handler)
        {
            longPressHandler = handler;
            label.MouseDown += Label_MouseDown;
            label.MouseUp += Label_MouseUp;
        }

        private void IncrementTimer_Tick(object sender, EventArgs e)
        {
            if (int.TryParse(this.Text, out int number))
            {
                this.Text = (number + 1).ToString();
            }
        }

        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            if (longPressHandler != null)
            {
                longPressHandler(this, e);
            }
        }



        private void Label_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 使Label变得不透明
                Label label = sender as Label;
                if (label != null)
                {
                    label.BackColor = Color.FromArgb(255, label.BackColor.R, label.BackColor.G, label.BackColor.B);
                }

                longPrassTimer.Start();

            }
        }

        private void Label_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 恢复Label的透明度
                Label label = sender as Label;
                if (label != null)
                {
                    label.BackColor = Color.FromArgb(50, label.BackColor.R, label.BackColor.G, label.BackColor.B); // 128 表示半透明
                }

                longPrassTimer.Stop();
            }
        }


        private void InitializeCustomComponents()
        {
            

        }
        public  void SetLineColor(Color value)
        {
            lineColor = value;
            blackPen.Color = lineColor; // 更新画笔颜色
        }

        public  void SetTextColor(Color value)
        {
            textColor = value;
        }

        public  void SetTextFont(Font value)
        {
            textFont = value;
        }

        public  void SetLineWidth(float value)
        {
            lineWidth = value;
            blackPen.Width = lineWidth; // 更新画笔粗细
        }

        /*public static Color LineColor
        {
            get { return lineColor; }
            set
            {
                lineColor = value;
                blackPen.Color = lineColor; // 更新画笔颜色
            }
        }

        public static Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        public static Font TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        public static float LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                blackPen.Width = lineWidth; // 更新画笔粗细
            }
        }*/

        internal Point GetStart()
        {
            return start;
        }
        internal Point GetEnd()
        {
            return end;
        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            if (clickedLabel != null)
            {

                clickedLabel.BackColor = Color.FromArgb(50, 255, 255, 255); // 恢复半透明状态
                clickedLabel = null;
            }
            clickTimer.Stop();
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {

                label.BackColor = Color.FromArgb(255, 255, 255, 255); // 设置为不透明

                clickedLabel = label;
                clickTimer.Start();
            }
        }



        private void Panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            panel1_Scroll(null, null);

        }
        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawLine=true;
                if (isDrawLine)
                {
                    if (p1.IsEmpty)
                    {
                        p1 = e.Location;
                    }
                    else
                    {
                        lines.Add(new Tuple<Point, Point>(p1, e.Location));
                        p1 = Point.Empty;
                        p2 = Point.Empty;
                        pictureBox1.Invalidate(); // 触发重绘
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawLine = false;
                g.Clear(Color.White);
                if (backgroundImage != null)
                {
                    g.DrawImage(backgroundImage, 0, 0, PBwidth, PBheight);
                }
                lines.Clear();

                p1 = Point.Empty;
                p2 = Point.Empty;
                pictureBox1.Invalidate(); // 触发重绘
                isDrawLine = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isDrawLine && !p1.IsEmpty)
            {
                // 仅在必要时重绘
                if (p2 != e.Location)
                {
                    // 直接在 PictureBox 上绘制临时线条
                    pictureBox1.Invalidate();
                    pictureBox1.Update();
                    using (Graphics tempG = pictureBox1.CreateGraphics())
                    {
                        tempG.DrawLine(blackPen, p1, e.Location);
                    }

                    p2 = e.Location;

                    
                }

                //调用自动滚动方法
                AutoScrollPanel(e);
                /*textBox1.Text = pictureBox1.Size.Width.ToString();
                textBox2.Text = panel1.Size.Width.ToString();
                textBox3.Text = this.Size.Width.ToString();*/
                Debug.WriteLine(pictureBox1.Size.Width.ToString());
                Debug.WriteLine(panel1.Size.Width.ToString());
                Debug.WriteLine(this.Size.Width.ToString());
                

                

            }
            
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isDrawLine && !p1.IsEmpty)
            {
                p2 = e.Location;
                lines.Add(new Tuple<Point, Point>(p1, p2));
                p1 = Point.Empty;
                p2 = Point.Empty;
                pictureBox1.Invalidate(); // 触发重绘
            }
        }

        /*private void Redraw()
        {
            g.Clear(Color.White);
            if (backgroundImage != null)
            {
                g.DrawImage(backgroundImage, 0, 0, PBwidth, PBheight);
            }
            foreach (var line in lines)
            {
                g.DrawLine(blackPen, line.Item1, line.Item2);
            }

        }*/

        private void PictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //MessageBox.Show("图片加载完成");
            pictureBox1.Refresh(); // 图片加载完成后立即重绘
            /* int centerX = sizebeforeLoadImg.Width / 2;
             int centerY = sizebeforeLoadImg.Height / 2;
             MessageBox.Show(centerX.ToString(), centerY.ToString());*/


        }

        private void DrawCrosshair(Graphics g, int x, int y)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                // 绘制水平线
                g.DrawLine(pen, x - 10, y, x + 10, y);
                // 绘制垂直线
                g.DrawLine(pen, x, y - 10, x, y + 10);
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs scrollEvent)
        {

            // 获取当前鼠标位置
            //Point mousePosition = pictureBox1.PointToClient(Cursor.Position);
            //MouseEventArgs mouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0);

            //const int scrollAmount = 10; // 每次滚动的像素数

            
            int pictureBox1halfHeight = pictureBox1.Height / 2;

            int pictureBox1halfWidth = pictureBox1.Width / 2;
            //MessageBox.Show(pictureBox1halfHeight.ToString(), pictureBox1halfWidth.ToString());

            // 获取当前滚动位置
            scrollX = panel1.HorizontalScroll.Value;
            scrollY = panel1.VerticalScroll.Value;

            if (!isScrolling)
            {

                isScrolling = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label1.Visible = false;

            }

            // 重置计时器
            scrollTimer.Stop();
            scrollTimer.Start();

            // 调整Label的位置，使其相对于PictureBox的位置保持不变
            UpdateLabelPosition(label1, sizebeforeLoadImg.Width / 2 - sizebeforeLoadImg.Width / 3, scrollX, sizebeforeLoadImg.Height / 2 - label1.Height / 2, scrollY);
            UpdateLabelPosition(label2, sizebeforeLoadImg.Width / 2 - label1.Width/2, scrollX, label2.Height, scrollY);
            UpdateLabelPosition(label3, sizebeforeLoadImg.Width - 100, scrollX, sizebeforeLoadImg.Height / 2 - label3.Height / 2, scrollY);
            UpdateLabelPosition(label4, sizebeforeLoadImg.Width / 2 - label2.Width / 2, scrollX, sizebeforeLoadImg.Height - label3.Height * 2, scrollY);


            // 触发重绘
            pictureBox1.Invalidate();

            // 重置鼠标事件参数
            //resetEventArgs = ResetMouseEventArgs(mouseEventArgs);
            

        }


        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            scrollTimer.Stop();
            isScrolling = false;
            label2.Visible = true; // 显示按钮
            label3.Visible = true;
            label4.Visible = true; // 确保label4也显示
            label1.Visible = true;
            //pictureBox1.Invalidate(); // 重绘PictureBox
        }

        

        private void UpdateLabelPosition(Label label, int originalX, int scrollX, int originalY, int scrollY)
        {
            label.Location = new Point(originalX + scrollX, originalY + scrollY);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                backgroundImage = new Bitmap(openFileDialog.FileName);
                PBwidth = backgroundImage.Width;
                PBheight = backgroundImage.Height;
                pictureBox1.WaitOnLoad = false; // 确保异步加载
                pictureBox1.LoadAsync(openFileDialog.FileName); // 异步加载图片
            }

            // 手动调用 Paint 事件处理程序
            //pictureBox1_Paint(pictureBox1, new PaintEventArgs(pictureBox1.CreateGraphics(), pictureBox1.ClientRectangle));
        }

        public void SetLabelClickHandler(Label label, LabelClickEventHandler handler)
        {
            label.Click += new EventHandler(handler);
        }

       /* public void SetLabelLongPressHandler(Label label, LabelLongPressEventHandler handler)
        {
            //给label传入事件
            label.MouseDown += new MouseEventHandler(handler);
        }*/

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            // 如果有图片，先绘制图片
            if (backgroundImage != null)
            {
                e.Graphics.DrawImage(backgroundImage, 0, 0, PBwidth, PBheight);
            }
            // 绘制所有的线条推送
            foreach (var line in lines)
            {
                var start = new Point(line.Item1.X - scrollXfirst, line.Item1.Y - scrollYfirst);
                var end = new Point(line.Item2.X - scrollXfirst, line.Item2.Y - scrollYfirst);
                e.Graphics.DrawLine(blackPen, line.Item1, line.Item2);

                double length = Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));

                var midPoint = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);

                // 计算角度
                float angle = CalculateAngle(start, end);

                // 保存当前的图形状态
                GraphicsState state = e.Graphics.Save();

                // 应用旋转变换
                e.Graphics.TranslateTransform(midPoint.X, midPoint.Y);
                if (angle > 90 || angle < -90)
                {
                    angle += 180; // 确保文本不倒置
                }
                e.Graphics.RotateTransform(angle);
                e.Graphics.DrawString(length.ToString("F2"), textFont, new SolidBrush(textColor), 0, 0);

                // 恢复图形状态
                e.Graphics.Restore(state);
            }
            
            // 在 PictureBox 的中心绘制十字准心
            int centerX = sizebeforeLoadImg.Width / 2 + scrollX;
            int centerY = sizebeforeLoadImg.Height / 2 + scrollY;

            DrawCrosshair(e.Graphics, centerX, centerY);

            // 如果正在绘制线条，绘制当前的临时线条
            if (isDrawLine && !p1.IsEmpty && !p2.IsEmpty)
            {
                e.Graphics.DrawLine(blackPen, p1, p2);
            }

        }

        private float CalculateAngle(Point start, Point end)
        {
            float deltaX = end.X - start.X;
            float deltaY = end.Y - start.Y;
            return (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void scrollPicturebox_Load(object sender, EventArgs e)
        {
/*
            if (panel1 != null)
            {

                panel1.Size = new Size(this.Width - 5, this.Height - 5);
            }

            if (pictureBox1 != null)
            {
                pictureBox1.Size = new Size(panel1.Width - 5, this.panel1.Height - 5);
            }

            if (pictureBox1 != null)
            {
                pictureBox1.Size = panel1.Size;
            }*/

        }

        /*private void scrollPicturebox_Resize(object sender, EventArgs e)
        {
            if (panel1 != null)
            {
                panel1.Size = this.Size;
            }
           *//* if (pictureBox1 !=null)
            {
                pictureBox1.Size = panel1.Size;
            }*//*
        }*/

        /*private void Parent_Resize(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Size = this.Parent.ClientSize;
            }
            MessageBox.Show(this.Parent.Name);
        }*/


        public void LoadImage(string path)
        {
            
                backgroundImage = new Bitmap(path);
                PBwidth = backgroundImage.Width;
                PBheight = backgroundImage.Height;
                pictureBox1.WaitOnLoad = false; // 确保异步加载
                pictureBox1.LoadAsync(path); // 异步加载图片
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        /*private MouseEventArgs ResetMouseEventArgs(MouseEventArgs e)
        {
            return new MouseEventArgs(e.Button, e.Clicks, panel1.Size.Width - e.X, e.Y, e.Delta);
        }*/


        private void AutoScrollPanel(System.Windows.Forms.MouseEventArgs e)
        {
            const int scrollMargin = 1; // 定义一个边距，当鼠标接近边缘时开始滚动
            const int scrollAmount = 2; // 每次滚动的像素数

            // 获取当前滚动位置
            int currentScrollX = panel1.HorizontalScroll.Value;
            int currentScrollY = panel1.VerticalScroll.Value;

            // 计算实际的鼠标坐标
            int actualMouseX = e.X - currentScrollX;
            int actualMouseY = e.Y - currentScrollY;

            bool needScroll = false;

            // 检查鼠标是否接近左边缘
            if (actualMouseX < scrollMargin)
            {
                panel1.HorizontalScroll.Value = Math.Max(panel1.HorizontalScroll.Value - scrollAmount, panel1.HorizontalScroll.Minimum);
                needScroll = true;
            }
            // 检查鼠标是否接近右边缘
            else if (actualMouseX > panel1.ClientSize.Width - scrollMargin)
            {
                panel1.HorizontalScroll.Value = Math.Min(panel1.HorizontalScroll.Value + scrollAmount, panel1.HorizontalScroll.Maximum);
                needScroll = true;
            }

            // 检查鼠标是否接近上边缘
            if (actualMouseY < scrollMargin)
            {
                panel1.VerticalScroll.Value = Math.Max(panel1.VerticalScroll.Value - scrollAmount, panel1.VerticalScroll.Minimum);
                needScroll = true;
            }
            // 检查鼠标是否接近下边缘
            else if (actualMouseY > panel1.ClientSize.Height - scrollMargin)
            {
                panel1.VerticalScroll.Value = Math.Min(panel1.VerticalScroll.Value + scrollAmount, panel1.VerticalScroll.Maximum);
                needScroll = true;
            }

            // 仅在需要滚动时触发重绘和滚动事件
            if (needScroll)
            {
                panel1.Invalidate();
                ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.ThumbTrack, panel1.HorizontalScroll.Value);
                panel1_Scroll(panel1, scrollEventArgs);
            }

        }
    }
}