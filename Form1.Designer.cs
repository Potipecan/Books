
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
            this.SearchMemberGroupBox = new System.Windows.Forms.GroupBox();
            this.BookSearchGroupBox = new System.Windows.Forms.GroupBox();
            this.SearchBookComboBox = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SearchBookLabel = new System.Windows.Forms.Label();
            this.SearchBookButton = new System.Windows.Forms.Button();
            this.SearchBookTextBox = new System.Windows.Forms.TextBox();
            this.SerachMemberLabel = new System.Windows.Forms.Label();
            this.SearchMemberTextBox = new System.Windows.Forms.TextBox();
            this.SearchMember2Label = new System.Windows.Forms.Label();
            this.SearchMemberButton = new System.Windows.Forms.Button();
            this.AddBookButton = new System.Windows.Forms.Button();
            this.AddMemberButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.SearchMemberGroupBox.SuspendLayout();
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
            this.MainTab.Controls.Add(this.SearchMemberGroupBox);
            this.MainTab.Controls.Add(this.BookSearchGroupBox);
            this.MainTab.Location = new System.Drawing.Point(4, 29);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(1055, 670);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Osnovno";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // SearchMemberGroupBox
            // 
            this.SearchMemberGroupBox.Controls.Add(this.AddMemberButton);
            this.SearchMemberGroupBox.Controls.Add(this.SearchMemberButton);
            this.SearchMemberGroupBox.Controls.Add(this.SearchMember2Label);
            this.SearchMemberGroupBox.Controls.Add(this.SearchMemberTextBox);
            this.SearchMemberGroupBox.Controls.Add(this.SerachMemberLabel);
            this.SearchMemberGroupBox.Location = new System.Drawing.Point(599, 73);
            this.SearchMemberGroupBox.Name = "SearchMemberGroupBox";
            this.SearchMemberGroupBox.Size = new System.Drawing.Size(371, 442);
            this.SearchMemberGroupBox.TabIndex = 1;
            this.SearchMemberGroupBox.TabStop = false;
            this.SearchMemberGroupBox.Text = "Iskanje člana";
            // 
            // BookSearchGroupBox
            // 
            this.BookSearchGroupBox.Controls.Add(this.AddBookButton);
            this.BookSearchGroupBox.Controls.Add(this.SearchBookTextBox);
            this.BookSearchGroupBox.Controls.Add(this.SearchBookButton);
            this.BookSearchGroupBox.Controls.Add(this.SearchBookLabel);
            this.BookSearchGroupBox.Controls.Add(this.SearchBookComboBox);
            this.BookSearchGroupBox.Location = new System.Drawing.Point(77, 73);
            this.BookSearchGroupBox.Name = "BookSearchGroupBox";
            this.BookSearchGroupBox.Size = new System.Drawing.Size(368, 442);
            this.BookSearchGroupBox.TabIndex = 0;
            this.BookSearchGroupBox.TabStop = false;
            this.BookSearchGroupBox.Text = "Iskanje gradiva";
            // 
            // SearchBookComboBox
            // 
            this.SearchBookComboBox.FormattingEnabled = true;
            this.SearchBookComboBox.Items.AddRange(new object[] {
            "Avtor",
            "Inventarna številka",
            "naslov"});
            this.SearchBookComboBox.Location = new System.Drawing.Point(75, 103);
            this.SearchBookComboBox.Name = "SearchBookComboBox";
            this.SearchBookComboBox.Size = new System.Drawing.Size(188, 28);
            this.SearchBookComboBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1055, 670);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SearchBookLabel
            // 
            this.SearchBookLabel.AutoSize = true;
            this.SearchBookLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SearchBookLabel.Location = new System.Drawing.Point(102, 47);
            this.SearchBookLabel.Name = "SearchBookLabel";
            this.SearchBookLabel.Size = new System.Drawing.Size(148, 26);
            this.SearchBookLabel.TabIndex = 1;
            this.SearchBookLabel.Text = "Poišči gradivo";
            // 
            // SearchBookButton
            // 
            this.SearchBookButton.Location = new System.Drawing.Point(117, 244);
            this.SearchBookButton.Name = "SearchBookButton";
            this.SearchBookButton.Size = new System.Drawing.Size(117, 36);
            this.SearchBookButton.TabIndex = 2;
            this.SearchBookButton.Text = "Poišči";
            this.SearchBookButton.UseVisualStyleBackColor = true;
            // 
            // SearchBookTextBox
            // 
            this.SearchBookTextBox.Location = new System.Drawing.Point(42, 164);
            this.SearchBookTextBox.Name = "SearchBookTextBox";
            this.SearchBookTextBox.Size = new System.Drawing.Size(266, 26);
            this.SearchBookTextBox.TabIndex = 3;
            // 
            // SerachMemberLabel
            // 
            this.SerachMemberLabel.AutoSize = true;
            this.SerachMemberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SerachMemberLabel.Location = new System.Drawing.Point(126, 47);
            this.SerachMemberLabel.Name = "SerachMemberLabel";
            this.SerachMemberLabel.Size = new System.Drawing.Size(129, 26);
            this.SerachMemberLabel.TabIndex = 0;
            this.SerachMemberLabel.Text = "Poišči člana";
            // 
            // SearchMemberTextBox
            // 
            this.SearchMemberTextBox.Location = new System.Drawing.Point(60, 164);
            this.SearchMemberTextBox.Name = "SearchMemberTextBox";
            this.SearchMemberTextBox.Size = new System.Drawing.Size(258, 26);
            this.SearchMemberTextBox.TabIndex = 1;
            // 
            // SearchMember2Label
            // 
            this.SearchMember2Label.AutoSize = true;
            this.SearchMember2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SearchMember2Label.Location = new System.Drawing.Point(102, 109);
            this.SearchMember2Label.Name = "SearchMember2Label";
            this.SearchMember2Label.Size = new System.Drawing.Size(186, 22);
            this.SearchMember2Label.TabIndex = 2;
            this.SearchMember2Label.Text = "Vnesite Priimek in ime";
            // 
            // SearchMemberButton
            // 
            this.SearchMemberButton.Location = new System.Drawing.Point(131, 244);
            this.SearchMemberButton.Name = "SearchMemberButton";
            this.SearchMemberButton.Size = new System.Drawing.Size(114, 36);
            this.SearchMemberButton.TabIndex = 3;
            this.SearchMemberButton.Text = "Poišči";
            this.SearchMemberButton.UseVisualStyleBackColor = true;
            // 
            // AddBookButton
            // 
            this.AddBookButton.Location = new System.Drawing.Point(107, 341);
            this.AddBookButton.Name = "AddBookButton";
            this.AddBookButton.Size = new System.Drawing.Size(143, 52);
            this.AddBookButton.TabIndex = 4;
            this.AddBookButton.Text = "Dodaj gradivo";
            this.AddBookButton.UseVisualStyleBackColor = true;
            // 
            // AddMemberButton
            // 
            this.AddMemberButton.Location = new System.Drawing.Point(123, 332);
            this.AddMemberButton.Name = "AddMemberButton";
            this.AddMemberButton.Size = new System.Drawing.Size(132, 52);
            this.AddMemberButton.TabIndex = 5;
            this.AddMemberButton.Text = "Dodaj člana";
            this.AddMemberButton.UseVisualStyleBackColor = true;
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
            this.SearchMemberGroupBox.ResumeLayout(false);
            this.SearchMemberGroupBox.PerformLayout();
            this.BookSearchGroupBox.ResumeLayout(false);
            this.BookSearchGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox SearchMemberGroupBox;
        private System.Windows.Forms.GroupBox BookSearchGroupBox;
        private System.Windows.Forms.ComboBox SearchBookComboBox;
        private System.Windows.Forms.TextBox SearchBookTextBox;
        private System.Windows.Forms.Button SearchBookButton;
        private System.Windows.Forms.Label SearchBookLabel;
        private System.Windows.Forms.Button SearchMemberButton;
        private System.Windows.Forms.Label SearchMember2Label;
        private System.Windows.Forms.TextBox SearchMemberTextBox;
        private System.Windows.Forms.Label SerachMemberLabel;
        private System.Windows.Forms.Button AddMemberButton;
        private System.Windows.Forms.Button AddBookButton;
    }
}

