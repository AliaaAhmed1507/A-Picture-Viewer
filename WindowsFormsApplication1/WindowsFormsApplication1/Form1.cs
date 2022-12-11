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
using System.Reflection;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //select pictures from my computer by fileDialoge

            OPFD.Title = "Choose the pictures";
            OPFD.Multiselect = true;

            if (OPFD.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(OPFD.FileNames);
                //listBox1.Items.Add(Path.GetFileName(OPFD.FileName));
            }
        }

      

        private void singlePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show apicture in single mode

            mToolStripMenuItem.Visible = false;
            SlidShowToolStripMenuItem.Visible = false;

            pictureBox1.ImageLocation = listBox1.SelectedItems[0].ToString();

           /* PictureBox pic = new PictureBox();   
            string str = listBox1.SelectedItem.ToString();
            pic.Image = Image.FromFile(str);
            pic.Location = new Point(0, 0);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(516, 315);
            this.panel1.Controls.Add(pic);*/

        }


        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show picture in multi picture mode


            singlePictureToolStripMenuItem.Visible = false;
            SlidShowToolStripMenuItem.Visible = false;

            int x = 10;
            int y = 10;
            int maxHeight = -1;
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                PictureBox pic = new PictureBox();
                int xx = listBox1.SelectedIndices[i];
                string str = listBox1.Items[xx].ToString();
                pic.Image = Image.FromFile(str);

                pic.Location = new Point(x, y);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                x += pic.Width + 10;
                maxHeight = Math.Max(pic.Height, maxHeight);
                if (x > panel1.Width - 100)
                {
                    x = 10;
                    y += maxHeight + 10;
                }
                this.panel1.Controls.Add(pic);
            }
       }
  
      
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            pictureBox1.Image = null;
            SlidShowToolStripMenuItem.Visible = true;
            mToolStripMenuItem.Visible = true;
            singlePictureToolStripMenuItem.Visible = true;
        }

        private void ClearToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            panel1.Controls.Clear();
            pictureBox1.Image = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SlidShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mToolStripMenuItem.Visible = false;
            singlePictureToolStripMenuItem.Visible = false;
           
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(PlayTime);
            timer1.Start();
        }

        int counter = 0, count = 0;
        void PlayTime(object sender, EventArgs e)
        {
             pictureBox1.ImageLocation = listBox1.Items[counter++].ToString();
             toolStripStatusLabel1.Text = Path.GetFileNameWithoutExtension(listBox1.Items[count++].ToString());
             if (counter >= listBox1.Items.Count) { timer1.Stop(); }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.AutoScroll = true;
            panel1.VerticalScroll.Visible = true;

        }


    }
}