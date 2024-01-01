using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TWADotNetCore.WindowFormApp.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TWADotNetCore.WindowFormApp
{
    public partial class FrmBlogLst : Form
    {
        private readonly AppDbContext _context;

        public FrmBlogLst()
        {
            AppConfigService service = new AppConfigService();
            _context = new AppDbContext(service.GetDbConnection());
            InitializeComponent();
        }

        public async Task<List<BlogModel>> GetBlogLst(int pageNo, int pageSize)
        {
            List<BlogModel> blogLst = await _context.Blog.AsNoTracking()
                                                    .OrderByDescending(x => x.Blog_Id)
                                                    .Skip((pageNo - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();

            return blogLst;
        }

        public void FrmBlogLst_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmBlogLst_Load_1(object sender, EventArgs e)
        {

        }

        private async void search_Click(object sender, EventArgs e)
        {
            pageNumber.Text = "1";

            List<BlogModel> lst = await GetBlogLst(1, 5);
            this.BindBlogLst(lst);
        }

        private List<BlogModel> BindBlogLst(List<BlogModel> blogLst)
        {
            List<BlogModel> list = blogLst;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = list;

            return list;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void nextBtn_Click(object sender, EventArgs e)
        {
            int pageNo = Convert.ToInt16(pageNumber.Text);
            int pageCount = await GetPageCount(5);
            if (pageNo == pageCount)
            {
                lastBtn.Enabled = false;
                return;
            }
            lastBtn.Enabled = true;
            firstBtn.Enabled = true;
            pageNumber.Text = Convert.ToString(pageNo + 1);

            List<BlogModel> lst = await GetBlogLst(pageNo + 1, 5);
            this.BindBlogLst(lst);

            if ((pageNo + 1) == pageCount)
            {
                lastBtn.Enabled = false;
            }
        }

        private async void previousBtn_Click(object sender, EventArgs e)
        {
            int pageNo = Convert.ToInt16(pageNumber.Text);
            if (pageNo == 1)
            {
                firstBtn.Enabled = false;
                return;
            }

            firstBtn.Enabled = true;
            lastBtn.Enabled = true;
            pageNumber.Text = Convert.ToString(pageNo - 1);

            List<BlogModel> lst = await GetBlogLst(pageNo - 1, 5);
            this.BindBlogLst(lst);

            if ((pageNo - 1) == 1)
            {
                firstBtn.Enabled = false;
            }
        }

        private async void firstBtn_Click(object sender, EventArgs e)
        {
            pageNumber.Text = "1";

            List<BlogModel> lst = await GetBlogLst(1, 5);
            this.BindBlogLst(lst);
            firstBtn.Enabled = false;
            lastBtn.Enabled = true;
        }

        private async void lastBtn_Click(object sender, EventArgs e)
        {
            int pageNo = await GetPageCount(5);
            pageNumber.Text = Convert.ToString(pageNo);

            List<BlogModel> lst = await GetBlogLst(pageNo, 5);
            this.BindBlogLst(lst);
            lastBtn.Enabled = false;
            firstBtn.Enabled = true;
        }

        public async Task<int> GetPageCount(int pageSize)
        {
            int count = await _context.Blog.CountAsync();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;

            return pageCount;
        }
    }
}
