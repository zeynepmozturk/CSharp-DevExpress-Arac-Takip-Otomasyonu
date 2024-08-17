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
namespace Staj1
{
    public partial class personelKayit : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi, kulid, deger, personelid;
        public personelKayit(string baglanticümlecigim, string kulidm, string degerim, string personelidim)
        {
            InitializeComponent();
            baglanticümlecigi = baglanticümlecigim;
            kulid = kulidm;
            deger = degerim;
            personelid = personelidim;
        }
        OleDbConnection baglanti = new OleDbConnection(); 
        private void personelKayit_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            if (deger.ToString() == "1")
            {
                simpleButton1.Text = "Kaydet";
            }
            else if (deger.ToString() == "2")
            {
                simpleButton1.Text = "Güncelle";
                vericek();
            }
            comboBoxEdit1.Properties.Items.Add("Adana");
            comboBoxEdit1.Properties.Items.Add("Erzincan");
            comboBoxEdit1.Properties.Items.Add("Ankara");
            comboBoxEdit1.Properties.Items.Add("Bursa");
            comboBoxEdit1.Properties.Items.Add("Balıkesir");
        }
        public void vericek()
        {
            try
            {
                string sorgu = "SELECT * FROM personel WHERE id like'" + personelid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    textEdit1.Text = oku["tc"].ToString();
                    textEdit2.Text = oku["ad"].ToString();
                    textEdit3.Text = oku["soyad"].ToString();
                    textEdit4.Text = oku["unvan"].ToString();
                    textEdit5.Text = oku["personelkod"].ToString();
                  

                    if (oku["cinsiyet"].ToString() == "Kadın")
                    {
                        radioGroup1.SelectedIndex = 0;
                    }
                    else if (oku["cinsiyet"].ToString() == "Erkek")
                    {
                        radioGroup1.SelectedIndex = 1;
                    }
                    textEdit6.Text = oku["dogumyeri"].ToString();
                    dateEdit1.Text = oku["dogumtarihi"].ToString();
                    textEdit12.Text = oku["telno"].ToString();
                    textEdit11.Text = oku["mail"].ToString();
                    comboBoxEdit1.Text = oku["il"].ToString();
                    textEdit7.Text = oku["ilce"].ToString();
                    memoEdit1.Text = oku["adres"].ToString();



                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }
        }
        public void personelekle()
        {
            try
            {
                if (textEdit1.Text == "" || textEdit2.Text == "" || textEdit3.Text == "")
                {
                    XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez \n  Lütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO personel (tc,ad,soyad,unvan,personelkod,cinsiyet,dogumyeri,dogumtarihi,telno,mail,il,ilce,adres,aktiflik) VALUES (@tc,@ad,@soyad,@unvan,@personelkod,@cinsiyet,@dogumyeri,@dogumtarihi,@telno,@mail,@il,@ilce,@adres,@aktiflik) ", baglanti);
                    komut.Parameters.Add("tc", OleDbType.VarChar).Value = textEdit1.Text;
                    komut.Parameters.Add("ad", OleDbType.VarChar).Value = textEdit2.Text;
                    komut.Parameters.Add("soyad", OleDbType.VarChar).Value = textEdit3.Text;
                    komut.Parameters.Add("unvan", OleDbType.VarChar).Value = textEdit4.Text;
                    komut.Parameters.Add("personelkod", OleDbType.VarChar).Value = textEdit5.Text;
                
                 

                    if (radioGroup1.SelectedIndex.ToString() == "0")
                    {
                        komut.Parameters.Add("cinsiyet", OleDbType.VarChar).Value = "Kadın";
                    }
                    else if (radioGroup1.SelectedIndex.ToString() == "1")
                    {
                        komut.Parameters.Add("cinsiyet", OleDbType.VarChar).Value = "Erkek";
                    }
                 
                  
                    komut.Parameters.Add("dogumyeri", OleDbType.VarChar).Value = textEdit6.Text;
                    komut.Parameters.Add("dogumtarihi", OleDbType.VarChar).Value = dateEdit1.Text;
                    komut.Parameters.Add("telno", OleDbType.VarChar).Value = textEdit12.Text;
                    komut.Parameters.Add("mail", OleDbType.VarChar).Value = textEdit11.Text;
                    komut.Parameters.Add("il", OleDbType.VarChar).Value =comboBoxEdit1.Text;
                    komut.Parameters.Add("ilce", OleDbType.VarChar).Value = textEdit7.Text;
                    komut.Parameters.Add("adres", OleDbType.VarChar).Value = memoEdit1.Text;
                    komut.Parameters.Add("aktiflik", OleDbType.VarChar).Value = "1";


                    if (komut.ExecuteNonQuery() == 1)
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                    else
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarısız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }


                }
            }
            catch
            {
                baglanti.Close();
            }
        }
        public void personelguncelle()
        {

            if (textEdit1.Text == "" || textEdit2.Text == "" || textEdit3.Text == "")
            {
                XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez \n  Lütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
               
                try
                {
                    baglanti.Open();
                    OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET tc=@tc,ad=@ad,soyad=@soyad,  " +
                        "unvan=@unvan,personelkod=@personelkod,cinsiyet=@cinsiyet,dogumyeri=@dogumyeri,dogumtarihi=@dogumtarihi,telno=@telno,mail=@mail,il=@il,ilce=@ilce,adres=@adres " +
                        "WHERE id like'" + personelid.ToString() + "'", baglanti);
                    sorgu.Parameters.AddWithValue("tc", textEdit1.Text);
                    sorgu.Parameters.AddWithValue("ad", textEdit2.Text);
                    sorgu.Parameters.AddWithValue("soyad", textEdit3.Text);
                    sorgu.Parameters.AddWithValue("unvan", textEdit4.Text);
                    sorgu.Parameters.AddWithValue("personelkod", textEdit5.Text);

                    if (radioGroup1.SelectedIndex == 0)
                    {
                        sorgu.Parameters.AddWithValue("cinsiyet", "Kadın");
                    }
                    else if (radioGroup1.SelectedIndex == 1)
                    {
                        sorgu.Parameters.AddWithValue("cinsiyet", "Erkek");
                    }
                    
                    sorgu.Parameters.AddWithValue("dogumyeri", textEdit6.Text);
                    sorgu.Parameters.AddWithValue("dogumtarihi", dateEdit1.Text);
                    sorgu.Parameters.AddWithValue("telno", textEdit12.Text);
                    sorgu.Parameters.AddWithValue("mail", textEdit11.Text);
                    sorgu.Parameters.AddWithValue("il", comboBoxEdit1.Text);
                    sorgu.Parameters.AddWithValue("ilce", textEdit7.Text);
                    sorgu.Parameters.AddWithValue("adres", memoEdit1.Text);

                    if (sorgu.ExecuteNonQuery() == 1)
                    {
                        XtraMessageBox.Show("Güncelleme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Güncelleme işlemi başarısız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    baglanti.Close();
                }
                catch
                {

                }


            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (deger.ToString() == "1")
            {
                personelekle();
            }
            else if (deger.ToString() == "2")
            {
                personelguncelle();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
      
    }
}