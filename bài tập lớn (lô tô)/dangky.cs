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

namespace bài_tập_lớn__lô_tô_
{
    public partial class dangky: Form
    {
        public static string file = "user.txt";
        private Dictionary<string, (string matkhau, int diem)> dulieu = new Dictionary<string, (string matkhau, int diem)>();
        public dangky()
        {
            InitializeComponent();
            load();
        }
        private void load()
        {
            if (File.Exists(file))
            {
                foreach (string line in File.ReadAllLines(file))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        string username = parts[0];
                        string password = parts[1];
                        int diem = int.Parse(parts[2]);

                        dulieu[username] = (password, diem);
                    }
                }
            }
        }

        private void dangky_Load(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {

            string tendangnhap = textBox1.Text.Trim();
            string matkhau = textBox2.Text.Trim();
            
            if(tendangnhap=="" || matkhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo" 
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!dulieu.ContainsKey(tendangnhap))
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dulieu[tendangnhap].matkhau != matkhau)
            {
                MessageBox.Show("Mật khẩu không đúng!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tendangnhap = textBox1.Text.Trim();
            string matkhau = textBox2.Text.Trim();
            
            if (tendangnhap == "" || matkhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dulieu.ContainsKey(tendangnhap))
            {
                MessageBox.Show("Tài khoản đã tồn tại, hãy chọn tên khác!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dulieu[tendangnhap] = (matkhau, 0);
            File.AppendAllText(file,$"{tendangnhap},{matkhau},0 \n");
            MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void dangky_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text=="")
            {
                Application.Exit();
                
            }
        }
    }
}
