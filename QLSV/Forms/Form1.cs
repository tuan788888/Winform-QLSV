using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV.Forms;
namespace QLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //   FunctionQLSV  f = new FunctionQLSV(); 
        //    f.Show();
        //     this.Hide();   

        //}
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra để trống
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    // Tên cột phải khớp với database: tktaikhoan, tkmatkhau
                    var taikhoan = db.tbl_taikhoans
                                    .FirstOrDefault(tk => tk.tktkhoan == username
                                                       && tk.tkmatkhau == password);
                    if (taikhoan != null)
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FunctionQLSV f = new FunctionQLSV();
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Lỗi dữ liệu: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
