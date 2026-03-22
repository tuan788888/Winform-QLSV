using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV.Forms
{
    public partial class FormQLL : Form
    {
        public FormQLL()
        {
            InitializeComponent();
        }

        private void FormQLL_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var classs = from cl  in db.tbl_lopquanlies
                                   select new
                                   {
                                       cl.lqlma,
                                       cl.lqlten,
                                       cl.lqlkhoahoc
                                      
                                   };
                    dataGridView1.DataSource = classs.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            
        }
        //thêm//
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    tbl_lopquanly  cl  = new tbl_lopquanly();
                    cl.lqlma = Convert.ToInt32(textBox1.Text);
                    cl.lqlkhoahoc = textBox3.Text;
                    cl.lqlten = textBox2.Text;
                  

                    db.tbl_lopquanlies.InsertOnSubmit(cl);
                    db.SubmitChanges();
                    MessageBox.Show("Thêm thành công!");
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FunctionQLSV f = new FunctionQLSV();
            f.Show();
            this.Hide();
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
           
            string keyword = textBox4.Text.Trim().ToLower(); // textBox4 = ô tìm kiếm

            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var result = db.tbl_lopquanlies
                                   .Where(x => x.lqlten.ToLower().Contains(keyword))
                                   .ToList();

                    if (result.Count == 0)
                        MessageBox.Show("Không tìm thấy lớp nào!");

                    dataGridView1.DataSource = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ============ CLICK VÀO DATAGRIDVIEW ĐỂ CHỌN ============
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["lqlma"].Value.ToString();
                textBox2.Text = row.Cells["lqlten"].Value.ToString();
                textBox3.Text = row.Cells["lqlkhoahoc"].Value.ToString();
            }
        }
        
       
            private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!");
                return;
            }

            if (!int.TryParse(textBox1.Text, out int ma))
            {
                MessageBox.Show("Mã lớp không hợp lệ!");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa lớp này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var cl = db.tbl_lopquanlies.FirstOrDefault(x => x.lqlma == ma);

                    if (cl == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp cần xóa!");
                        return;
                    }

                    db.tbl_lopquanlies.DeleteOnSubmit(cl);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa!");
                return;
            }

            if (!int.TryParse(textBox1.Text, out int ma))
            {
                MessageBox.Show("Mã lớp không hợp lệ!");
                return;
            }

            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var cl = db.tbl_lopquanlies.FirstOrDefault(x => x.lqlma == ma);

                    if (cl == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp cần sửa!");
                        return;
                    }

                    cl.lqlten = textBox2.Text;
                    cl.lqlkhoahoc = textBox3.Text;

                    db.SubmitChanges();
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

       
           
        
    }
    }

