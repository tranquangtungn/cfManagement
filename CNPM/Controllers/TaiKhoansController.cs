using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using CNPM.Models;

namespace CNPM.Controllers
{
    class TaiKhoansController
    {
        private int ma;
        private string quyen;

        public void UpdateRight(int manv, int right)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.TaiKhoans.Where(x => x.MaNV == manv).Select(x => x).FirstOrDefault();

            temp.LoaiTK = right.ToString();
            qlcp.SaveChanges();
        }
        public bool GetAcc(string acc, string pass, ref int id, ref int right)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            pass = HashPass(pass);
            var temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.TenDangNhap == acc).Where(x => x.MatKhau == pass).Where(x => x.DaKhoa == false).FirstOrDefault();
            if (temp != null)
            {
                id = temp.MaNV;
                right = Convert.ToInt32(temp.LoaiTK);
                return true;
            }
            return false;
        }

        public List<string> DsNhanVien()
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.Xoa == false).Select(x => x.TenNV);
            return _temp.ToList();
        }

        public DataTable Details()
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.TaiKhoans.Select(x => x);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Chức vụ");
            dt.Columns.Add("Tên đăng nhập");
            dt.Columns.Add("Email");
            dt.Columns.Add("Tình trạng");
            foreach (var item in _temp)
            {
                dt.Rows.Add(item.MaNV, FindName(item.NhanVien.MaNV), item.NhanVien.ChucVu, item.TenDangNhap, item.Email, item.DaKhoa);
            }
            return dt;
        }

        public void FogotPassWords(string email)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            try
            {
                string _newPass = SendPassWord(email);
                if (_newPass != null)
                {
                    var _temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.Email == email).FirstOrDefault();
                    if (_temp != null)
                        _temp.MatKhau = HashPass(_newPass);
                    quanLyQuanCaPheEntities.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Không thể gửi mật khẩu mới qua email đã nhập. Vui lòng thử lại sau!");
            }
        }

        public void Edit(string name, string user, string email, Boolean flag)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var taiKhoan = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.TenDangNhap == user).FirstOrDefault();

            if (taiKhoan != null)
            {
                taiKhoan.Email = email;
                taiKhoan.DaKhoa = flag;
                FindKey(name, ref ma, ref quyen);
                taiKhoan.MaNV = ma;
            }
            quanLyQuanCaPheEntities.SaveChanges();
        }

        public void Create(string name, string User, string email, Boolean flag)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.TenDangNhap = User;
            taiKhoan.Email = email;
            taiKhoan.DaKhoa = flag;
            taiKhoan.MatKhau = HashPass(NewAcc(email, User));
            FindKey(name, ref ma, ref quyen);
            taiKhoan.MaNV = ma;
            taiKhoan.LoaiTK = quyen;
            quanLyQuanCaPheEntities.TaiKhoans.Add(taiKhoan);
            quanLyQuanCaPheEntities.SaveChanges();
        }

        public void Delete(string user)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = new TaiKhoan { TenDangNhap = user };
            quanLyQuanCaPheEntities.Entry(_temp).State = System.Data.Entity.EntityState.Deleted;
            quanLyQuanCaPheEntities.SaveChanges();

        }

        public void ChangePass(string user, string newpass)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.TenDangNhap == user).Select(x => x).FirstOrDefault();
            if(_temp!=null)
            {
                _temp.MatKhau = HashPass(newpass);
                quanLyQuanCaPheEntities.SaveChanges();
            }
        }

        public bool GetAcc(string acc, string pass)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            pass = HashPass(pass);
            var temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.TenDangNhap == acc).Where(x => x.MatKhau == pass).Where(x => x.DaKhoa == false).FirstOrDefault();
            if (temp != null)
            {
                return true;
            }
            return false;
        }
        #region Function_Phuc_Vu_Phia_Tren
        public int FindID(string name)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.TenNV == name).FirstOrDefault();
            if (_temp != null)
            {
                return _temp.MaNV;
            }
            return -1;
        }
        private void FindKey(string name, ref int ma, ref string right)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.TenNV == name).FirstOrDefault();
            if (_temp != null)
            {
                ma = _temp.MaNV;
                if (_temp.ChucVu == "Quản Lý")
                    right = "0";
                else if (_temp.ChucVu == "Thủ Kho")
                    right = "1";
                else
                    right = "2";
            }
        }
        public void ChangeStatus(string user)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.TenDangNhap == user).FirstOrDefault();
            if (_temp != null)
                _temp.DaKhoa = false;
            quanLyQuanCaPheEntities.SaveChanges();
        }
        private string HashPass(string _temp)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(_temp);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        private string FindName(int id)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            string _name = quanLyQuanCaPheEntities.NhanViens.Where(x => x.MaNV == id).Select(x => x.TenNV).FirstOrDefault();
            return _name.ToString();
        }

        private string FindCV(int flag)
        {
            if (flag == 0)
                return "Quản Lý";
            else if (flag == 1)
                return "Kế Toán";
            else if (flag == 2)
                return "Bán Hàng";
            else
                return "Quản Kho";
        }

        public Boolean CheckMail(string email)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            int _temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.Email == email).Count();
            if (_temp == 0)
                return false;
            else
                return true;
        }

        private string SendPassWord(string email)
        {
            string _newPass = null;
            try
            {
                _newPass = RandomPassWord();
                QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
                TaiKhoan _temp = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.Email == email).FirstOrDefault();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("deverloperUTE@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Cấp lại mật khẩu cho tài khoản";
                mail.Body = "Mật khẩu mới cho tài khoản " + _temp.TenDangNhap + " của bạn là: " + _newPass + ". Không cung cấp mật khẩu này cho ai khác!";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "deverloperUTE@gmail.com";
                ntcd.Password = "nguyenvantrieu";
                smtp.Credentials = ntcd;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mail);
                return _newPass;
            }
            catch
            {
                return null;
            }
        }

        private string NewAcc(string email, string user)
        {
            string _newPass = null;
            try
            {
                _newPass = RandomPassWord();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("deverloperUTE@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Cấp lại mật khẩu cho tài khoản";
                mail.Body = "Mật khẩu mới cho tài khoản " + user + " của bạn là: " + _newPass + ". Không cung cấp mật khẩu này cho ai khác!";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "deverloperUTE@gmail.com";
                ntcd.Password = "nguyenvantrieu";
                smtp.Credentials = ntcd;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mail);
                return _newPass;
            }
            catch
            {
                return null;
            }
        }

        private string RandomPassWord()
        {
            string _newPass = null;
            Random rd = new Random();
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    _newPass += rd.Next(0, 9).ToString();
                }
                else
                {
                    _newPass += Convert.ToString((char)rd.Next(97, 122));
                }
            }
            return _newPass;
        }

        public string GetName(int id)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            string _name = quanLyQuanCaPheEntities.TaiKhoans.Where(x => x.NhanVien.MaNV == id).Select(x => x.TenDangNhap).FirstOrDefault();
            return _name.ToString();
        }
        #endregion
    }
}
