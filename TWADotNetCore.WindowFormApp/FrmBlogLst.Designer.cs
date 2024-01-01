namespace TWADotNetCore.WindowFormApp
{
    partial class FrmBlogLst
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.search = new System.Windows.Forms.Button();
            this.Blog_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blog_Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blog_Contetn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.previousBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pageNumber = new System.Windows.Forms.Label();
            this.lastBtn = new System.Windows.Forms.Button();
            this.firstBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Blog_Title,
            this.Blog_Author,
            this.Blog_Contetn});
            this.dataGridView1.Location = new System.Drawing.Point(3, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(793, 387);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(12, 22);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(142, 33);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // Blog_Title
            // 
            this.Blog_Title.DataPropertyName = "Blog_Title";
            this.Blog_Title.HeaderText = "Blog Title";
            this.Blog_Title.MinimumWidth = 6;
            this.Blog_Title.Name = "Blog_Title";
            this.Blog_Title.ReadOnly = true;
            this.Blog_Title.Width = 125;
            // 
            // Blog_Author
            // 
            this.Blog_Author.DataPropertyName = "Blog_Author";
            this.Blog_Author.HeaderText = "Blog Author";
            this.Blog_Author.MinimumWidth = 6;
            this.Blog_Author.Name = "Blog_Author";
            this.Blog_Author.ReadOnly = true;
            this.Blog_Author.Width = 125;
            // 
            // Blog_Contetn
            // 
            this.Blog_Contetn.DataPropertyName = "Blog_Content";
            this.Blog_Contetn.HeaderText = "Blog_Content";
            this.Blog_Contetn.MinimumWidth = 6;
            this.Blog_Contetn.Name = "Blog_Contetn";
            this.Blog_Contetn.ReadOnly = true;
            this.Blog_Contetn.Width = 125;
            // 
            // previousBtn
            // 
            this.previousBtn.Location = new System.Drawing.Point(225, 511);
            this.previousBtn.Name = "previousBtn";
            this.previousBtn.Size = new System.Drawing.Size(113, 33);
            this.previousBtn.TabIndex = 2;
            this.previousBtn.Text = "<";
            this.previousBtn.UseVisualStyleBackColor = true;
            this.previousBtn.Click += new System.EventHandler(this.previousBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(472, 510);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(111, 33);
            this.nextBtn.TabIndex = 3;
            this.nextBtn.Text = ">";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Page ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pageNumber
            // 
            this.pageNumber.AutoSize = true;
            this.pageNumber.Location = new System.Drawing.Point(414, 518);
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(14, 16);
            this.pageNumber.TabIndex = 5;
            this.pageNumber.Text = "1";
            this.pageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lastBtn
            // 
            this.lastBtn.Location = new System.Drawing.Point(598, 510);
            this.lastBtn.Name = "lastBtn";
            this.lastBtn.Size = new System.Drawing.Size(64, 33);
            this.lastBtn.TabIndex = 6;
            this.lastBtn.Text = "Last";
            this.lastBtn.UseVisualStyleBackColor = true;
            this.lastBtn.Click += new System.EventHandler(this.lastBtn_Click);
            // 
            // firstBtn
            // 
            this.firstBtn.Location = new System.Drawing.Point(155, 511);
            this.firstBtn.Name = "firstBtn";
            this.firstBtn.Size = new System.Drawing.Size(64, 32);
            this.firstBtn.TabIndex = 7;
            this.firstBtn.Text = "First";
            this.firstBtn.UseVisualStyleBackColor = true;
            this.firstBtn.Click += new System.EventHandler(this.firstBtn_Click);
            // 
            // FrmBlogLst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 612);
            this.Controls.Add(this.firstBtn);
            this.Controls.Add(this.lastBtn);
            this.Controls.Add(this.pageNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.previousBtn);
            this.Controls.Add(this.search);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmBlogLst";
            this.Text = "FrmBlogLst";
            this.Load += new System.EventHandler(this.FrmBlogLst_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blog_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blog_Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blog_Contetn;
        private System.Windows.Forms.Button previousBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label pageNumber;
        private System.Windows.Forms.Button lastBtn;
        private System.Windows.Forms.Button firstBtn;
    }
}