using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _30._12._2018_final_calısma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sehirler txt dosyasındaki sehirler ve ilçeleri treewiev1 e ekleyen kod. ilceler alt dugum
            treeView1.Nodes.Clear();
            StreamReader sr = new StreamReader("sehirler.txt",Encoding.Default);
            while (!sr.EndOfStream)
            {
                string satir= sr.ReadLine();
                string[] dizi = satir.Split(',');
                TreeNode dugum = treeView1.Nodes.Add(dizi[0]);
                for (int i = 1; i < dizi.Length; i++)
                {
                    dugum.Nodes.Add(dizi[i]);
                }
            }
            sr.Close();
            treeView1.ExpandAll();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //treeView1 deki şehirler ve ilceleri tek satırda deneme txt dosyasına yazan kod
            StreamWriter sw = new StreamWriter("deneme.txt");
            foreach (TreeNode item in treeView1.Nodes)
            {
                sw.Write(item.Text + ";");
                foreach (TreeNode item2 in item.Nodes)
                {
                    sw.Write(item2.Text + ",");
                }
                sw.WriteLine();
            }
            sw.Close();
            MessageBox.Show("Yazma İşlemi Tamamlandı");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Verilen adresteki dizinleri hepsini treView1 e eklesın. alt klasorler alt dugum olsun
            DirectoryInfo dr = new DirectoryInfo(@"C:\Users\reela\Documents\Visual Studio 2013\Projects");
            DirectoryInfo[] dizinler = dr.GetDirectories();
            foreach (DirectoryInfo item in dizinler)
            {
                TreeNode dugum = treeView1.Nodes.Add(item.Name);
                FileInfo[] dosyalar = item.GetFiles();
                foreach (FileInfo item2 in dosyalar)
                {
                    dugum.Nodes.Add(item2.Name);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //treeView1 e çarpım tablosu eklemek
            for (int i = 1; i < 11; i++)
            {
                treeView1.Nodes.Add(i.ToString());
                for (int j = 1; j < 11; j++)
                {
                    treeView1.Nodes[i - 1].Nodes.Add((i * j).ToString());
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //formu dolasıp checkboxları treeview1 e ekleyen 
            //secılı olanın altına secılı olmayana secılı degıl yazan kod
            foreach(var item in this.Controls)
            {
                if(item is CheckBox)
                {
                    CheckBox cb = (CheckBox)item;
                    TreeNode tn = new TreeNode(cb.Text);
                    if (cb.Checked)
                        tn.Nodes.Add("secili");
                    else
                        tn.Nodes.Add("secili değil");
                    treeView1.Nodes.Add(tn);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //listboxdaki elemanları chexbox olarak forma ekleyen checked yazanları cekıd ozellıgı aktıf olsun
            int x = 450;
            int y = 20;
            foreach (string item in listBox1.Items)
            {             
                CheckBox cb = new CheckBox();
                cb.Location = new Point(x, y);
                cb.Text = item.ToString();
                this.Controls.Add(cb);
                y += 25;
                if (item.Contains("UnChecked"))               
                    cb.Checked = false;              
                else               
                    cb.Checked = true;                          
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //resimlerim klasorundeki jpg uzantılı resımlerı RichtextBoxa ekleyen kod
            string[] dosyalar = Directory.GetFiles(@"C:\Users\reela\Pictures\Camera Roll");
            foreach (string item in dosyalar)
            {
                int y = item.IndexOf('.');
                string uzantı = item.Substring(y++);
                if (uzantı == ".jpg")
                {
                    Image resim = Image.FromFile(item);
                    Clipboard.SetImage(resim);
                    richTextBox1.Paste();
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //verilen adresteki dizin adlarını listbox1 e ekleyen kod
            DirectoryInfo d = new DirectoryInfo(@"C:\windows");
            FileInfo[] dizi = d.GetFiles();
            foreach (FileInfo f in dizi)
                listBox1.Items.Add(f);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = new CheckBox();
            cb.Checked = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // deneme txt ıcındekı metnı satır satır buyuuk harfe cevıren kod
            StreamReader sr = new StreamReader("deneme2.txt");
            StreamWriter sw = new StreamWriter("temp.txt");
            while(!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                sw.Write(satir.ToUpper());
                sw.WriteLine();
            }
            sr.Close();
            sw.Close();
            File.Delete("deneme2.txt");
            File.Move("temp.txt", "deneme2.txt");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //listbox1 ın ıcıne tek sıraya random 6 adet sayı ekleyen ve bırbırınden farklı olan
            // sıralı sekılde dızılmıs sayıları ekleyen kod 
            string sonuc = "";
            List<int> liste = new List<int>();
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                int sayi = r.Next(50);
                if (!liste.Contains(sayi))
                    liste.Add(sayi);
            }
            liste.Sort();
            foreach(var item in liste)
                sonuc += " " + item.ToString();
            listBox1.Items.Add(sonuc);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //treeview1 de iller dugum ve ilceler alt dugum seklındedır il ve ilce olacak sekılde  
            //  listbox1 e ekleyen kod ...  istanbul kartal -  istanbul avcılar - istanbul taksim gibi
            foreach(TreeNode dugum in treeView1.Nodes)
            {
                foreach (TreeNode altdugum in dugum.Nodes)
                    listBox1.Items.Add(dugum.Text +"  "+ altdugum.Text);               
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //form load oldugunda  form ıcıeresınde yer alan tum textboxları sildiren kod
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tb = (TextBox)item;
                    tb.Clear();
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //denmee txt adlı dosyanın ıcındekı metınde * gecen satırları silip 
            //her satırın basına satır sayısını yazan kod
            int sayac = 1;
            StreamReader sr = new StreamReader("deneme.txt",Encoding.Default);
            StreamWriter sw = new StreamWriter("temp.txt");
            while(!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                if (!satir.Contains('*'))
                {
                    sw.WriteLine(sayac +"-"+ satir);
                    sayac++;
                }
            }
            sr.Close();
            sw.Close();
            File.Delete("deneme.txt");
            File.Move("temp.txt", "deneme.txt");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //secılen dosyanın ıcındekı metnı richtextbox1 e ekleyen kod
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text|*.txt";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(ofd.FileName);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Butona basıldığında richtextbox1 de yazılan metni kaydeden kodu yazınız?
            //kaydeden bir program hazırlayınız?
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "zengin metin|*.rtf|tümdosyalar|*.*";
            sd.FilterIndex = 1;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(sd.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //listbox1 olup listbox2 de olmayan elemanları buyuk harflerle Listbox3 e yazan kod
            foreach (string item in listBox1.Items)
            {
                if (!listBox2.Items.Contains(item))
                {
                    listBox3.Items.Add(item.ToUpper());
                }
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
           // Form üzerinde yer alan tüm textboxları bularak text özelliklerini sırasıyla 1,2,3...
            //şeklinde değiştiren kodu yazınız?
            int sayac = 1;
            foreach (Object item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox t = (TextBox)item;
                    t.Text = sayac.ToString();
                    sayac++;
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //RRichtextboxda yer alan metni treview e ekle ilk kelıme dugum dıgerlerı alt dugum
            string[] s = richTextBox1.Lines;
            foreach (string item in s)
            {
                string[] dizi = item.Split(' ');


                TreeNode t = treeView1.Nodes.Add(dizi[0].ToString());
                for (int i = 1; i < dizi.Length; i++)
                {
                    t.Nodes.Add(dizi[i]);
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //tersmetin icindekı metni kendi üzerinde satır satır tersıne yazan kod
            StreamReader sr = new StreamReader("tersmetin.txt", Encoding.Default);
            StreamWriter sw = new StreamWriter("yedek.txt");
            while(!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                for (int i = satir.Length-1; i>=0; i--)
                {
                    sw.Write(satir[i]);
                }
                sw.WriteLine();
            }
            sr.Close();
            sw.Close();
            File.Delete("tersmetin.txt");
            File.Move("yedek.txt", "tersmetin.txt");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            // deneme txt dosyasınd her satırda kac kelıme oldugunu satır sonun ayazan kod
            // kelimeleri boşluga gore ayır
            StreamReader sr = new StreamReader("deneme.txt", Encoding.Default);
            StreamWriter sw = new StreamWriter("yeni.txt", true);
            while (!sr.EndOfStream)
            {
                string satır = sr.ReadLine();
                string[] dizi = satır.Split(' ');
                sw.WriteLine(satır + "," + dizi.Length + " kelime");
            }
            sr.Close();
            sw.Close();
            File.Delete("deneme.txt");
            File.Move("yeni.txt", "deneme.txt");
        }
    }
}
