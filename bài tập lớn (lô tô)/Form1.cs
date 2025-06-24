using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace bài_tập_lớn__lô_tô_
{
    public partial class Form1 : Form
    {
        static int diem;
        static Random r = new Random();
        static HashSet<int> so_random = new HashSet<int>();
        public static string file = "user.txt";
        public Form1()
        {
            dangky d = new dangky();
            d.ShowDialog();
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void capnhatlabel4(string text)
        {
            label4.Text = text;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            label2.Text = "";
            so_random.Clear();
            tableLayoutPanel1.Controls.Clear();

            int rows = tableLayoutPanel1.RowCount;
            int cols = tableLayoutPanel1.ColumnCount;

          
            List<(int, int)> danhsachmang = new List<(int, int)>();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                   danhsachmang.Add((i, j));

          
            danhsachmang = danhsachmang.OrderBy(x => r.Next()).ToList();

           
            int[] rowCounts = new int[rows];
            int[] colCounts = new int[cols];
            
            // HashSet để tránh số trùng
            HashSet<int> sodadung = new HashSet<int>();

            // Duyệt danh sách và đặt đúng 5 Label 
            foreach (var (row, col) in danhsachmang)
            {
                if (rowCounts[row] < 5 && colCounts[col] < 5)
                {
                    int minValue = col * 10 + 1;
                    int maxValue = minValue + 8;
                    int value;
                    do
                    {
                        value = r.Next(minValue, maxValue + 1);
                    } while (sodadung.Contains(value));

                    sodadung.Add(value); // Đánh dấu số đã sử dụng

                    Label lb = new Label
                    {
                        Text = value.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Margin = new Padding(1),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        Tag = false

                    };
                    lb.Click += Label_Click;

                    tableLayoutPanel1.Controls.Add(lb, col, row);

                    rowCounts[row]++;
                    colCounts[col]++;

                   
                    if (rowCounts[row] == 5 && colCounts[col] == 5)
                        continue;
                }
            }
            
           
        }
        
        private void Label_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (lb != null)
            {
                int value;
                if (int.TryParse(lb.Text, out value))
                {
                    if (so_random.Contains(value))
                    {
                        bool isBlack = (bool)(lb.Tag?? false); // Lấy trạng thái hiện tại

                     
                        lb.BackColor = isBlack ? Color.White : Color.Black;
                        lb.ForeColor = isBlack ? Color.Black : Color.White;

                        lb.Tag = !isBlack; // Cập nhật trạng thái
                    }
                    else
                    {
                        MessageBox.Show($"Số {value} chưa được gọi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        static int So_rand(int min, int max)
        {
            int a;
            do
            {
                a = r.Next(min, max);
            } while (so_random.Contains(a));
            so_random.Add(a);
            return a;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            label2.Text = So_rand(1, 91).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] mau = { "LightCoral", "YellowGreen", "Gold", "SteelBlue","Blue","Purple","Crimson" };
            string mauto = mau[r.Next(0, mau.Length)];
            tableLayoutPanel1.BackColor = Color.FromName(mauto);
            label2.Text = "";
            so_random.Clear();
            tableLayoutPanel1.Controls.Clear();

            int rows = tableLayoutPanel1.RowCount;
            int cols = tableLayoutPanel1.ColumnCount;

           
            List<(int, int)> danhsachmang = new List<(int, int)>();

            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    danhsachmang.Add((row, col));

            
            danhsachmang = danhsachmang.OrderBy(x => r.Next()).ToList();

            
            int[] rowCounts = new int[rows];
            int[] colCounts = new int[cols];

            
            HashSet<int> sodadung = new HashSet<int>();

            
            foreach (var (row, col) in danhsachmang)
            {
                if (rowCounts[row] < 5 && colCounts[col] < 5)
                {
                    int minValue = col * 10 + 1;
                    int maxValue = minValue + 8;
                    int value;
                    do
                    {
                        value = r.Next(minValue, maxValue + 1);
                    } while (sodadung.Contains(value));

                    sodadung.Add(value); 

                    Label lb = new Label
                    {
                        Text = value.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Margin = new Padding(1),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        Tag = false

                    };
                    lb.Click += Label_Click;

                    tableLayoutPanel1.Controls.Add(lb, col, row);

                    rowCounts[row]++;
                    colCounts[col]++;

                    
                    if (rowCounts[row] == 5 && colCounts[col] == 5)
                        continue;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rows = tableLayoutPanel1.RowCount;
            int cols = tableLayoutPanel1.ColumnCount;

            
            for (int row = 0; row < rows; row++)
            {
                int count = 0; 

                for (int col = 0; col < cols; col++)
                {
                    Control control = tableLayoutPanel1.GetControlFromPosition(col, row);

                    if (control is Label lb)
                    {
                        int value;
                        if (int.TryParse(lb.Text, out value) && so_random.Contains(value))
                        {
                            count++;
                        }
                    }
                }

                
                if (count == 5)
                {
                    MessageBox.Show("Bạn đã thắng!", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    diem = int.Parse(label4.Text);
                    diem += 10;
                    label4.Text = diem.ToString();
                    return; 
                }
            }
        }

        private void Điển(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Xác nhận đóng", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
        }
            

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}