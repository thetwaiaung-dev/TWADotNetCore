using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TWADotNetCore.WindowFormApp.Models;

namespace TWADotNetCore.WindowFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly AppDbContext _context;
        public FrmBlog()
        {
            AppConfigService service = new AppConfigService();
            _context = new AppDbContext(service.GetDbConnection());
            InitializeComponent();
        }

        private void FrmBlog_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void blogTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void blogAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void blogContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BlogModel blog = new BlogModel()
            {
                Blog_Title = blogTitle.Text,
                Blog_Author = blogAuthor.Text,
                Blog_Content = blogContent.Text,
            };

            _context.Blog.Add(blog);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Success" : "Failed";
            MessageBox.Show(message);

            blogTitle.Clear();
            blogAuthor.Clear();
            blogContent.Clear();
            blogTitle.Focus();
        }
    }
}
