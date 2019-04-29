using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;    // you need to add this Namespace to access FolderBrowser
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        string currentDir = ""; 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fb = new FolderBrowserDialog();
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentDir = fb.SelectedPath; // get the selected folder by the user; 
                    
                    // display current directory in the text box
                    textBoxDirectory.Text = currentDir; 

                    // get all image files from the directory
                    // first create directory info
                    var dirInfo = new DirectoryInfo(currentDir);
                    listBoxImages.Items.Clear();
                    // get the files
                    var files = dirInfo.GetFiles().Where(c => (c.Extension.Equals(".jpg") || c.Extension.Equals(".jpeg") || c.Extension.Equals(".bmp") || c.Extension.Equals(".png")));
                    foreach (var image in files)
                    {
                        // add images (file names) to the list box
                        listBoxImages.Items.Add(image.Name); // we could add full path to the list but it won't look good. 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message + " " + ex.Source); 
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // get the selected image
                var selectedImage = listBoxImages.SelectedItems[0].ToString();
                if (!string.IsNullOrEmpty(selectedImage) && !string.IsNullOrEmpty(currentDir))
                { 
                    // make the full path to the image. 
                    var fullPath = Path.Combine(currentDir, selectedImage); 

                    // set an image from file to the PictureBox
                    pictureBoxImagePreview.Image = Image.FromFile(fullPath); 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void pictureBoxImagePreview_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";

            if (sfd.ShowDialog()==DialogResult.OK && sfd.FileName.Length > 0)
            {
                pictureBoxImagePreview.Image.Save(sfd.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
