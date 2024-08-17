using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using System.IO;

namespace Staj1
{
    public partial class Anaform : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi,kulid;
  
        public Anaform( string sqlSorgu,string kulidm)
        {
            InitializeComponent();
            baglanticümlecigi = sqlSorgu;
            kulid = kulidm;

        }
        public OleDbConnection baglanti = new OleDbConnection();
        string kullaniciAdiniz;
        public void kulBilgisi()
        {
            try
            {
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'"+kulid.ToString()+"'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu,baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read()) {
                    kullaniciAdiniz = oku["kuladi"].ToString() + " " + oku["kulsoyisim"].ToString();
                    tematool.LookAndFeel.SkinName = oku["kultema"].ToString();
                }
                oku.Close();
                baglanti.Close();

            }
            catch {
                baglanti.Close();
            
            }

        }

        private void Anaform_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            kulBilgisi();
            this.Text = "Anaform(" + kullaniciAdiniz + ")";
        }
        public void yedekleme()
        {
            try
            {
                string yedekdurumu, dizimdurumu;
                string tarih = DateTime.Now.ToString("dd.MM.yyyy");
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'" + kulid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    yedekdurumu = oku["yedekdurumu"].ToString();
                    dizimdurumu = oku["yedekyolu"].ToString();
                    if (yedekdurumu == "Alınsın")
                    {
                        string mevcutdatayeri = Environment.CurrentDirectory + @"/Data.mdb";
                        File.Copy(mevcutdatayeri, dizimdurumu + @"/" + tarih + " " + "backup.mdb", true);
                        XtraMessageBox.Show("Veri tabanı backup'u alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Dispose();
                        Application.Exit();
                    }
                    else
                    {
                        Dispose();
                        Application.Exit();
                    }
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }

        }
        public void tema()
        {
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update kullanicilar set kultema=@kultema where kulid like  '"+kulid.ToString()+"'", baglanti);
                komut.Parameters.AddWithValue("kultema",tematool.LookAndFeel.SkinName);
                komut.ExecuteNonQuery();
                baglanti.Close();
                
            }
            catch
            {
                baglanti.Close();
            }
        }
        public void aktifaraclistesi()
        {
            string sorgu = "SELECT* FROM araclar WHERE aktiflik like '1' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Plaka",Type.GetType("System.String"));
            dt.Columns.Add("Araç Markası", Type.GetType("System.String"));
            dt.Columns.Add("Araç Modeli", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yılı", Type.GetType("System.String"));
            dt.Columns.Add("Araç Rengi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yakıt Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Vites Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Şase No", Type.GetType("System.String"));
            dt.Columns.Add("Açıklama", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while(oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["aracplaka"].ToString();
                dr[1] = oku["aracmarka"].ToString();
                dr[2] = oku["aracmodel"].ToString();
                dr[3] = oku["aracyılı"].ToString();
                dr[4] = oku["aracrengi"].ToString();
                dr[5] = oku["aractipi"].ToString();
                dr[6] = oku["kayıttarihi"].ToString();
                dr[7] = oku["yakıt"].ToString();
                dr[8] = oku["vites"].ToString();
                dr[9] = oku["şaseno"].ToString();
                dr[10] = oku["açıklama"].ToString();
                dr[11] = oku["id"].ToString();
                dr[12] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView1.Columns["İD"].Visible = false;
            gridView1.Columns["aktiflik"].Visible = false;



        }
        public void pasifaraclistesi()
        {
            string sorgu = "SELECT* FROM araclar WHERE aktiflik like '0' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Plaka", Type.GetType("System.String"));
            dt.Columns.Add("Araç Markası", Type.GetType("System.String"));
            dt.Columns.Add("Araç Modeli", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yılı", Type.GetType("System.String"));
            dt.Columns.Add("Araç Rengi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yakıt Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Vites Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Şase No", Type.GetType("System.String"));
            dt.Columns.Add("Açıklama", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["aracplaka"].ToString();
                dr[1] = oku["aracmarka"].ToString();
                dr[2] = oku["aracmodel"].ToString();
                dr[3] = oku["aracyılı"].ToString();
                dr[4] = oku["aracrengi"].ToString();
                dr[5] = oku["aractipi"].ToString();
                dr[6] = oku["kayıttarihi"].ToString();
                dr[7] = oku["yakıt"].ToString();
                dr[8] = oku["vites"].ToString();
                dr[9] = oku["şaseno"].ToString();
                dr[10] = oku["açıklama"].ToString();
                dr[11] = oku["id"].ToString();
                dr[12] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView1.Columns["İD"].Visible = false;
            gridView1.Columns["aktiflik"].Visible = false;



        }
        public void aktifpersonellistesi()
        {
            string sorgu = "SELECT* FROM personel WHERE aktiflik like '1' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("T.C NO", Type.GetType("System.String"));
            dt.Columns.Add("Personel Ad", Type.GetType("System.String"));
            dt.Columns.Add("Personel Soyad", Type.GetType("System.String"));
            dt.Columns.Add("Unvan", Type.GetType("System.String"));
            dt.Columns.Add("Personel Kod", Type.GetType("System.String"));
            dt.Columns.Add("Cinsiyet", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Yeri", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Personel Telefon No", Type.GetType("System.String"));
            dt.Columns.Add("Personel Mail", Type.GetType("System.String"));
            dt.Columns.Add("İl", Type.GetType("System.String"));
            dt.Columns.Add("İlçe", Type.GetType("System.String"));
            dt.Columns.Add("Adres", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["tc"].ToString();
                dr[1] = oku["ad"].ToString();
                dr[2] = oku["soyad"].ToString();
                dr[3] = oku["unvan"].ToString();
                dr[4] = oku["personelkod"].ToString();
                dr[5] = oku["cinsiyet"].ToString();
                dr[6] = oku["dogumyeri"].ToString();
                dr[7] = oku["dogumtarihi"].ToString();
                dr[8] = oku["telno"].ToString();
                dr[9] = oku["mail"].ToString();
                dr[10] = oku["il"].ToString();
                dr[11] = oku["ilce"].ToString();
                dr[12] = oku["adres"].ToString();
                dr[13] = oku["id"].ToString();
                dr[14] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl2.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView2.Columns["İD"].Visible = false;
            gridView2.Columns["aktiflik"].Visible = false;

        }
        public void pasifpersonellistesi()
        {
            string sorgu = "SELECT* FROM personel WHERE aktiflik like '0' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("T.C NO", Type.GetType("System.String"));
            dt.Columns.Add("Personel Ad", Type.GetType("System.String"));
            dt.Columns.Add("Personel Soyad", Type.GetType("System.String"));
            dt.Columns.Add("Unvan", Type.GetType("System.String"));
            dt.Columns.Add("Personel Kod", Type.GetType("System.String"));
            dt.Columns.Add("Cinsiyet", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Yeri", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Personel Telefon No", Type.GetType("System.String"));
            dt.Columns.Add("Personel Mail", Type.GetType("System.String"));
            dt.Columns.Add("İl", Type.GetType("System.String"));
            dt.Columns.Add("İlçe", Type.GetType("System.String"));
            dt.Columns.Add("Adres", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["tc"].ToString();
                dr[1] = oku["ad"].ToString();
                dr[2] = oku["soyad"].ToString();
                dr[3] = oku["unvan"].ToString();
                dr[4] = oku["personelkod"].ToString();
                dr[5] = oku["cinsiyet"].ToString();
                dr[6] = oku["dogumyeri"].ToString();
                dr[7] = oku["dogumtarihi"].ToString();
                dr[8] = oku["telno"].ToString();
                dr[9] = oku["mail"].ToString();
                dr[10] = oku["il"].ToString();
                dr[11] = oku["ilce"].ToString();
                dr[12] = oku["adres"].ToString();
                dr[13] = oku["id"].ToString();
                dr[14] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl2.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView2.Columns["İD"].Visible = false;
            gridView2.Columns["aktiflik"].Visible = false;

        }

        private void Anaform_FormClosing(object sender, FormClosingEventArgs e)
        {
            
               yedekleme();
               tema();
               

        }

       

        private void navButton3_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            arackayıt ac = new arackayıt(baglanticümlecigi,kulid,"1",null);
            ac.ShowDialog();
           
        }

        private void backstageViewTabItem1_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            aktifaraclistesi();
        }

        private void navButton2_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            aktifaraclistesi();
        }

        private void navButton4_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            pasifaraclistesi();
        }
       
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right && gridView1.SelectedRowsCount==1)
            {
                if (Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "aktiflik")) == "1")
                {
                    barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else if (Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "aktiflik")) == "0")
                {
                    barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                AracMenu.ShowPopup(MousePosition);
            }

        }  

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            arackayıt ac = new arackayıt(baglanticümlecigi, kulid, "2", Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE araclar SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "1");            
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Kayıt aktif edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                   
                }
                else
                {
                    XtraMessageBox.Show("Kayıt aktif edilemedi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                pasifaraclistesi();
            }
            catch
            {

            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE araclar SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "0");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Kayıt pasif edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Kayıt pasif edilemedi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                aktifaraclistesi();
            }
            catch
            {

            }
        }

        private void navButton5_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            raporform aç = new raporform("1","1");
            aç.ShowDialog();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            aracyakitfisi ac = new aracyakitfisi(baglanticümlecigi, Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            raporform aç = new raporform("2", Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            aç.ShowDialog();
        }

        private void navButton1_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            personelKayit ac = new personelKayit(baglanticümlecigi, kulid, "1", null);
            ac.ShowDialog();
        }

        private void navButton6_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            aktifpersonellistesi();
        }

        private void navButton7_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            pasifpersonellistesi();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            personelKayit ac = new personelKayit(baglanticümlecigi, kulid, "2", Convert.ToString(gridView2.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kullaniciprofili aç = new kullaniciprofili(baglanticümlecigi, kulid);
            aç.ShowDialog();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            profilguncelleme aç = new profilguncelleme(baglanticümlecigi, kulid);
            aç.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                string datapath = Environment.CurrentDirectory + @"/Data.mdb";
                string dosyaadı = DateTime.Now.ToString("dd.MM.yyyy");
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string backuppath = fbd.SelectedPath.ToString();
                    File.Copy(datapath, backuppath + @"/" + dosyaadı + " " + "backup.mdb", true);
                    XtraMessageBox.Show("Veri tabanı backup'u alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch
            {
                
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult sonuc = new DialogResult();
            sonuc = XtraMessageBox.Show("Program kapatılacaktır çıkmak istediğinizden emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (sonuc == DialogResult.No)
            {

            }
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && gridView2.SelectedRowsCount == 1)
            {
                if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "aktiflik")) == "1")
                {
                    barButtonItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "aktiflik")) == "0")
                {
                    barButtonItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                personelMenu.ShowPopup(MousePosition);
            }

        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            personelKayit ac = new personelKayit(baglanticümlecigi, kulid, "2", Convert.ToString(gridView2.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "1");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Kayıt aktif edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Kayıt aktif edilemedi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                pasifpersonellistesi();
            }
            catch
            {

            }
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "0");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Kayıt pasif edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Kayıt pasif edilemedi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                aktifpersonellistesi();
            }
            catch
            {

            }
        }

        private void navButton8_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            raporform aç = new raporform("3", null);
            aç.ShowDialog();
        }

    
     
       
    }
}