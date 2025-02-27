using Microsoft.VisualBasic.Devices;
using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace sapper
{
    public partial class Form1 : Form
    {
        
        private Graphics graphics;
        bool[,] field;
        bool[,] cLicked;
        int[,] moreField;
        int cols;
        int rows;
        static int resolution = 25;
        int count, k;
        Icon icon = new Icon("bomb1.ico", resolution, resolution);

        Icon flag = new Icon("12795019.ico", resolution - 2, resolution - 2);
        public Form1()
        {
            InitializeComponent();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            k = 0;
            count = 0;
            timer1.Stop();
            textBox2.Text = Convert.ToString(k);
            cols = pictureBox1.Width / resolution;
            rows = pictureBox1.Height / resolution;
            field = new bool[cols, rows];
            cLicked = new bool[cols, rows];
            moreField = new int[cols, rows];
            Random random = new Random();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            System.Drawing.Font font = new System.Drawing.Font("MS Sans Serif", 12);
            pictureBox1.Font = font;
            Brush bruh = new SolidBrush(Color.Black);
            if(count < 10)
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    cLicked[x, y] = false;
                    if (count < 10)
                        field[x, y] = random.Next(8) == 0;

                    else
                        field[x, y] = false;
                    graphics.FillRectangle(Brushes.DarkGray, x * resolution, y * resolution, resolution, resolution );
                    graphics.FillRectangle(Brushes.Gray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    if (field[x, y])
                    {
                        count++;
                        //graphics.DrawIcon(icon, x * resolution , y * resolution - 6);
                        //graphics.DrawString("#", font, bruh, x * resolution, y * resolution);
                        //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    }

                }

            }
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    CountNeighbour(x, y);
                }
            }
            textBox1.Text = Convert.ToString(count);
            timer1.Start();
            pictureBox1.Refresh();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            textBox2.Text = Convert.ToString((++k));
        }

        private int forMethod(int col, int row, int x, int y, int count)
        {
            bool hasbomb = field[col, row];
            bool key = col == x && row == y;

            System.Drawing.Font font = pictureBox1.Font;
            Brush bruh = new SolidBrush(Color.Black);
            if (hasbomb && !key && !field[x, y])
            {
                count++;
                Brush bru– = new SolidBrush(Color.LightGray);
                if (count == 1)
                {
                    moreField[x, y] = 1;
                    //graphics.DrawString("1", font, bruh, x * resolution, y * resolution);
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                }
                if (count == 2)
                {

                    moreField[x, y] = 2;
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    //graphics.DrawString("2", font, bruh, x * resolution, y * resolution);
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                }

                if (count == 3)
                {
                    moreField[x, y] = 3;
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    //graphics.DrawString("3", font, bruh, x * resolution, y * resolution);
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                }

                if (count == 4)
                {
                    moreField[x, y] = 4;
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    //graphics.DrawString("4", font, bruh, x * resolution, y * resolution);
                    //graphics.FillRectangle(Brushes.LightGray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                }
            }

            return count;
        }

        private int CountNeighbour(int x, int y)
        {
            int count = 0;
            System.Drawing.Font font = pictureBox1.Font;
            Brush bruh = new SolidBrush(Color.Black);
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i);
                    int row = (y + j);
                    if (col >= 0 && col < cols && row >= 0 && row < rows)
                    {
                        count = forMethod(col, row, x, y, count);
                    }
                }
            }
            return count;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            int x = 0;
            int y = 0;
            int h = 0;
            if (e.Button == MouseButtons.Right)
            { 
                x = e.Location.X / resolution;
                y = e.Location.Y / resolution;
            }
            if (e.Button == MouseButtons.Right && cLicked[x, y] == true)
            {
                if (moreField[x, y] != -1)
                {
                    graphics.FillRectangle(Brushes.Gray, x * resolution, y * resolution, resolution - 3, resolution - 3);
                    cLicked[x, y] = false;
                }
            }
            for (int i = 0; i < cols; i++) 
            {
                for (int j = 0; j < rows; j++) 
                {
                    if(cLicked[i, j] == true)
                    {
                        h++;
                    }
                }
            }

            textBox1.Text = Convert.ToString(count - h);
            pictureBox1.Refresh();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (!timer1.Enabled)
                return;
            int x, y;
            int h = 0;
            System.Drawing.Font font = pictureBox1.Font;
            Brush bruh = new SolidBrush(Color.Black);
            if (e.Button == MouseButtons.Right)
            {
                x = e.Location.X / resolution;
                y = e.Location.Y / resolution;
                if (moreField[x, y] == -1)
                    graphics.DrawString("", font, bruh, x * resolution, y * resolution);
                else
                {
                    cLicked[x, y] = true;
                    graphics.DrawIcon(flag, x * resolution + 3, y * resolution + 2);
                }
            }
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (cLicked[i, j] == true)
                    {
                        h++;
                    }
                }
            }
            textBox1.Text = Convert.ToString(count - h);
            if (e.Button == MouseButtons.Left)
            {
                x = e.Location.X / resolution;
                y = e.Location.Y / resolution;
                
                RevealCells(x, y, graphics, font, bruh);
            }
            Victory();
            pictureBox1.Refresh();
        }

        private void Victory()
        {
            int q = 0;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (moreField[i, j] == -1)
                        q++;
                }
            }
            if (q == 122)
            {
                timer1.Stop();
                for (int i = 0; i < cols; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        if (field[i, j])
                            graphics.DrawIcon(flag, i * resolution + 3, j * resolution + 2);
                    }
                }
                textBox1.Text = Convert.ToString(0);
            }
        }



        private void RevealCells(int x, int y, Graphics graphics, System.Drawing.Font font, Brush brush)
        {
            // √раницы игры
            if (x < 0 || x>= cols || y < 0 || y >= rows || moreField[x, y] == -1)
                return;
            Brush bruh1 = new SolidBrush(Color.Green);
            Brush bruh2 = new SolidBrush(Color.Red);
            Brush bruh3 = new SolidBrush(Color.Coral);
            if (moreField[x, y] > 0)
            {
                graphics.FillRectangle(Brushes.White, x * resolution, y * resolution, resolution - 3, resolution - 3);
                if(moreField[x, y] == 1)
                    graphics.DrawString(moreField[x, y].ToString(), font, brush, x * resolution + 3, y * resolution);
                if (moreField[x, y] == 2)
                    graphics.DrawString(moreField[x, y].ToString(), font, bruh1, x * resolution + 3, y * resolution);
                if (moreField[x, y] == 3)
                    graphics.DrawString(moreField[x, y].ToString(), font, bruh2, x * resolution + 3, y * resolution);
                if (moreField[x, y] == 4)
                    graphics.DrawString(moreField[x, y].ToString(), font, bruh3, x * resolution + 3, y * resolution);
                moreField[x, y] = -1; // ќтмечаем €чейку как раскрытую
                return;
            }
            if (field[x, y])
            {
                graphics.FillRectangle(Brushes.Crimson, x * resolution, y * resolution, resolution - 3, resolution - 3);
                for (int i = 0; i < cols; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        if (field[i, j])
                        {
                            graphics.FillRectangle(Brushes.OrangeRed, i * resolution, j * resolution, resolution - 3, resolution - 3);
                            graphics.DrawIcon(icon, i * resolution, j * resolution - 6);
                            moreField[i, j] = -1;
                        }
                    }
                }
                timer1.Stop();
                return;
            }


            graphics.FillRectangle(Brushes.White, x * resolution, y * resolution, resolution - 3, resolution - 3);
            moreField[x, y] = -1; // ќтмечаем €чейку как раскрытую

            // –екурсивно раскрываем соседние €чейки
            RevealCells(x - 1, y, graphics, font, brush);
            RevealCells(x + 1, y, graphics, font, brush);
            RevealCells(x, y - 1, graphics, font, brush);
            RevealCells(x, y + 1, graphics, font, brush);
            RevealCells(x - 1, y - 1, graphics, font, brush);
            RevealCells(x + 1, y - 1, graphics, font, brush);
            RevealCells(x - 1, y + 1, graphics, font, brush);
            RevealCells(x + 1, y + 1, graphics, font, brush);
        }
    }
}
