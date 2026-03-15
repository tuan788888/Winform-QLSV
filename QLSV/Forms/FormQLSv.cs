using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.Forms
{
    public partial class FormQLSV : Form
    {
        public FormQLSV()
        {
            InitializeComponent();
            LoadData();
        }

        private void FormQLSv_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var students = from s in db.tbl_sinhviens
                                   select new
                                   {
                                       s.svma,
                                       s.svten,
                                       s.svngaysinh,
                                       s.svgioitinh,
                                       s.svquequan,
                                       s.lqlma
                                   };
                    dataGridView1.DataSource = students.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["svma"].Value?.ToString();
                textBox2.Text = row.Cells["svten"].Value?.ToString();
                textBox3.Text = row.Cells["svngaysinh"].Value?.ToString();
                textBox4.Text = row.Cells["svgioitinh"].Value?.ToString();
                textBox5.Text = row.Cells["svquequan"].Value?.ToString();
                textBox6.Text = row.Cells["lqlma"].Value?.ToString();
            }
        }

        // ============ THÊM ============
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    tbl_sinhvien sv = new tbl_sinhvien();
                    sv.svten = textBox2.Text;
                    sv.svngaysinh = DateTime.Parse(textBox3.Text);
                    sv.svgioitinh = textBox4.Text;
                    sv.svquequan = textBox5.Text;
                    sv.lqlma = int.Parse(textBox6.Text);

                    db.tbl_sinhviens.InsertOnSubmit(sv);
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

        // ============ SỬA ============
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    int ma = int.Parse(textBox1.Text);
                    tbl_sinhvien sv = db.tbl_sinhviens.FirstOrDefault(s => s.svma == ma);

                    if (sv != null)
                    {
                        sv.svten = textBox2.Text;
                        sv.svngaysinh = DateTime.Parse(textBox3.Text);
                        sv.svgioitinh = textBox4.Text;
                        sv.svquequan = textBox5.Text;
                        sv.lqlma = int.Parse(textBox6.Text);

                        db.SubmitChanges();
                        MessageBox.Show("Sửa thành công!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ============ XÓA ============
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc muốn xóa sinh viên này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    using (DataBaseDataContext db = new DataBaseDataContext())
                    {
                        int ma = int.Parse(textBox1.Text);
                        tbl_sinhvien sv = db.tbl_sinhviens.FirstOrDefault(s => s.svma == ma);

                        if (sv != null)
                        {
                            db.tbl_sinhviens.DeleteOnSubmit(sv);
                            db.SubmitChanges();
                            MessageBox.Show("Xóa thành công!");
                            LoadData();
                            ClearForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ============ TẢI LẠI ============
        private void button4_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        // ============ TÌM KIẾM ============
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = textBox7.Text.Trim().ToLower();

                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    var result = from s in db.tbl_sinhviens
                                 where s.svten.ToLower().Contains(keyword)
                                    || s.svma.ToString().Contains(keyword)
                                 select new
                                 {
                                     s.svma,
                                     s.svten,
                                     s.svngaysinh,
                                     s.svgioitinh,
                                     s.svquequan,
                                     s.lqlma
                                 };

                    dataGridView1.DataSource = result.ToList();

                    if (!result.Any())
                        MessageBox.Show("Không tìm thấy kết quả!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ============ QUAY LẠI ============
        private void button6_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            textBox7.Text = "";
        }

        // ====== CÁC HÀM THỪA GIỮ LẠI ĐỂ KHÔNG LỖI DESIGNER ======
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
    }
}