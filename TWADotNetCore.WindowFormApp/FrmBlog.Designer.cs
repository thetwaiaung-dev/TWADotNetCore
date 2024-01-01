namespace TWADotNetCore.WindowFormApp
{
    partial class FrmBlog
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
            this.label1 = new System.Windows.Forms.Label();
            this.blogTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.blogAuthor = new System.Windows.Forms.TextBox();
            this.blogContent = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Blog Title";
            // 
            // blogTitle
            // 
            this.blogTitle.Location = new System.Drawing.Point(229, 54);
            this.blogTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blogTitle.Name = "blogTitle";
            this.blogTitle.Size = new System.Drawing.Size(319, 26);
            this.blogTitle.TabIndex = 1;
            this.blogTitle.TextChanged += new System.EventHandler(this.blogTitle_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blog Author";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 218);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Blog Content";
            // 
            // blogAuthor
            // 
            this.blogAuthor.Location = new System.Drawing.Point(229, 136);
            this.blogAuthor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blogAuthor.Name = "blogAuthor";
            this.blogAuthor.Size = new System.Drawing.Size(319, 26);
            this.blogAuthor.TabIndex = 4;
            this.blogAuthor.TextChanged += new System.EventHandler(this.blogAuthor_TextChanged);
            // 
            // blogContent
            // 
            this.blogContent.Location = new System.Drawing.Point(229, 218);
            this.blogContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blogContent.Name = "blogContent";
            this.blogContent.Size = new System.Drawing.Size(319, 26);
            this.blogContent.TabIndex = 5;
            this.blogContent.TextChanged += new System.EventHandler(this.blogContent_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(242, 297);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 34);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmBlog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 661);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.blogContent);
            this.Controls.Add(this.blogAuthor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blogTitle);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmBlog";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmBlog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox blogTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox blogAuthor;
        private System.Windows.Forms.TextBox blogContent;
        private System.Windows.Forms.Button btnSave;
    }
}

