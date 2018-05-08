using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 拍排块游戏
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int N = 4;
        Button [,] btns = new Button[N,N];

        private void Form1_Load(object sender, EventArgs e)
        {
            //生成按钮
            generateBtn();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //打乱
            Shuffle();
        }
        void Shuffle(){
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(btns[a, b], btns[c, d]);

            }
        }
        void generateBtn()
        {
            int x0 = 100, y0 = 20, w = 45, d = 50;
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = num;

                    btn.Click +=  new EventHandler(btn_Click);

                    btns[r,c] = btn;
                    this.Controls.Add(btn);
                }
            }
            btns[N - 1, N - 1].Visible = false;
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button; //当前点中的按钮

            Button blank = FindHiddenButton(); //空白按钮



            //判断是否与空白块相邻，如果是，则交换

            if (IsNeighbor(btn, blank))
            {

                Swap(btn, blank);

                blank.Focus();

            }



            //判断是否完成了

            if (ResultIsOk())
            {

                MessageBox.Show("ok");

            }
        }
        //交换两个按钮

        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;

        }
        //查找要隐藏的按钮

        Button FindHiddenButton()
        {

            for (int r = 0; r < N; r++)

                for (int c = 0; c < N; c++)
                {

                    if (!btns[r, c].Visible)
                    {

                        return btns[r, c];

                    }

                }

            return null;

        }



        //判断是否相邻

        bool IsNeighbor(Button btnA, Button btnB)
        {

            int a = (int)btnA.Tag; //Tag中记录是行列位置

            int b = (int)btnB.Tag;

            int r1 = a / N, c1 = a % N;

            int r2 = b / N, c2 = b % N;



            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //左右相邻

                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))

                return true;

            return false;

        }



        //检查是否完成

        bool ResultIsOk()
        {

            for (int r = 0; r < N; r++)

                for (int c = 0; c < N; c++)
                {

                    if (btns[r, c].Text != (r * N + c + 1).ToString())
                    {

                        return false;

                    }

                }

            return true;

        }
    }
}
