
namespace Books
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BookSearchGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SearchBookComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.BookSearchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MainTab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1063, 703);
            this.tabControl1.TabIndex = 0;
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.groupBox2);
            this.MainTab.Controls.Add(this.BookSearchGroupBox);
            this.MainTab.Location = new System.Drawing.Point(4, 29);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(1055, 670);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Osnovno";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 417);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BookSearchGroupBox
            // 
            this.BookSearchGroupBox.Controls.Add(this.SearchBookComboBox);
            this.BookSearchGroupBox.Location = new System.Drawing.Point(77, 73);
            this.BookSearchGroupBox.Name = "BookSearchGroupBox";
            this.BookSearchGroupBox.Size = new System.Drawing.Size(368, 527);
            this.BookSearchGroupBox.TabIndex = 0;
            this.BookSearchGroupBox.TabStop = false;
            this.BookSearchGroupBox.Text = "Iskanje gradiva";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(720, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // SearchBookComboBox
            // 
            this.SearchBookComboBox.FormattingEnabled = true;
            this.SearchBookComboBox.Location = new System.Drawing.Point(75, 103);
            this.SearchBookComboBox.Name = "SearchBookComboBox";
            this.SearchBookComboBox.Size = new System.Drawing.Size(188, 28);
            this.SearchBookComboBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 703);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.BookSearchGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox BookSearchGroupBox;
        private System.Windows.Forms.ComboBox SearchBookComboBox;
    }
}

