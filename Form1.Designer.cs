
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
            this.BookTab = new System.Windows.Forms.TabPage();
            this.SearchBookLabel = new System.Windows.Forms.Label();
            this.SearchBookButton = new System.Windows.Forms.Button();
            this.SearchBookTextBox = new System.Windows.Forms.TextBox();
            this.SerachMemberLabel = new System.Windows.Forms.Label();
            this.SearchMemberTextBox = new System.Windows.Forms.TextBox();
            this.SearchMember2Label = new System.Windows.Forms.Label();
            this.SearchMemberButton = new System.Windows.Forms.Button();
            this.AddBookButton = new System.Windows.Forms.Button();
            this.AddMemberButton = new System.Windows.Forms.Button();
            this.AddDataGroupBox = new System.Windows.Forms.GroupBox();
            this.OneBookGroupBox = new System.Windows.Forms.GroupBox();
            this.AuthorOneBookTextBox = new System.Windows.Forms.TextBox();
            this.IdNumberOneBookTextBox = new System.Windows.Forms.TextBox();
            this.OneBookLabel = new System.Windows.Forms.Label();
            this.AuthorOneBookLabel = new System.Windows.Forms.Label();
            this.IdNumberOneBookLabel = new System.Windows.Forms.Label();
            this.TypeOneBookLabel = new System.Windows.Forms.Label();
            this.PublisherOneBookLabel = new System.Windows.Forms.Label();
            this.AquireOneBookLabel = new System.Windows.Forms.Label();
            this.StatusOneBookPictureBox = new System.Windows.Forms.PictureBox();
            this.ChangeOneBookButton = new System.Windows.Forms.Button();
            this.PublisherOneBookComboBox = new System.Windows.Forms.ComboBox();
            this.TypeOneBookComboBox = new System.Windows.Forms.ComboBox();
            this.AcquireOneBookGroupBox = new System.Windows.Forms.ComboBox();
            this.PublisherAddButton = new System.Windows.Forms.Button();
            this.TypeAddButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.SearchMemberGroupBox.SuspendLayout();
            this.BookSearchGroupBox.SuspendLayout();
            this.BookTab.SuspendLayout();
            this.OneBookGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusOneBookPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MainTab);
            this.tabControl1.Controls.Add(this.BookTab);
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
            // BookTab
            // 
            this.BookTab.Controls.Add(this.OneBookGroupBox);
            this.BookTab.Controls.Add(this.AddDataGroupBox);
            this.BookTab.Location = new System.Drawing.Point(4, 29);
            this.BookTab.Name = "BookTab";
            this.BookTab.Padding = new System.Windows.Forms.Padding(3);
            this.BookTab.Size = new System.Drawing.Size(1055, 670);
            this.BookTab.TabIndex = 1;
            this.BookTab.Text = "Gradivo";
            this.BookTab.UseVisualStyleBackColor = true;
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
            // AddDataGroupBox
            // 
            this.AddDataGroupBox.Location = new System.Drawing.Point(526, 30);
            this.AddDataGroupBox.Name = "AddDataGroupBox";
            this.AddDataGroupBox.Size = new System.Drawing.Size(467, 177);
            this.AddDataGroupBox.TabIndex = 0;
            this.AddDataGroupBox.TabStop = false;
            this.AddDataGroupBox.Text = "Dodajanje podatkov";
            // 
            // OneBookGroupBox
            // 
            this.OneBookGroupBox.Controls.Add(this.TypeAddButton);
            this.OneBookGroupBox.Controls.Add(this.PublisherAddButton);
            this.OneBookGroupBox.Controls.Add(this.AcquireOneBookGroupBox);
            this.OneBookGroupBox.Controls.Add(this.TypeOneBookComboBox);
            this.OneBookGroupBox.Controls.Add(this.PublisherOneBookComboBox);
            this.OneBookGroupBox.Controls.Add(this.ChangeOneBookButton);
            this.OneBookGroupBox.Controls.Add(this.StatusOneBookPictureBox);
            this.OneBookGroupBox.Controls.Add(this.AquireOneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.PublisherOneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.TypeOneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.AuthorOneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.IdNumberOneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.OneBookLabel);
            this.OneBookGroupBox.Controls.Add(this.IdNumberOneBookTextBox);
            this.OneBookGroupBox.Controls.Add(this.AuthorOneBookTextBox);
            this.OneBookGroupBox.Location = new System.Drawing.Point(71, 30);
            this.OneBookGroupBox.Name = "OneBookGroupBox";
            this.OneBookGroupBox.Size = new System.Drawing.Size(439, 574);
            this.OneBookGroupBox.TabIndex = 0;
            this.OneBookGroupBox.TabStop = false;
            this.OneBookGroupBox.Text = "Knjiga";
            // 
            // AuthorOneBookTextBox
            // 
            this.AuthorOneBookTextBox.Location = new System.Drawing.Point(47, 113);
            this.AuthorOneBookTextBox.Name = "AuthorOneBookTextBox";
            this.AuthorOneBookTextBox.Size = new System.Drawing.Size(255, 26);
            this.AuthorOneBookTextBox.TabIndex = 0;
            // 
            // IdNumberOneBookTextBox
            // 
            this.IdNumberOneBookTextBox.Location = new System.Drawing.Point(47, 357);
            this.IdNumberOneBookTextBox.Name = "IdNumberOneBookTextBox";
            this.IdNumberOneBookTextBox.Size = new System.Drawing.Size(255, 26);
            this.IdNumberOneBookTextBox.TabIndex = 3;
            // 
            // OneBookLabel
            // 
            this.OneBookLabel.AutoSize = true;
            this.OneBookLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OneBookLabel.Location = new System.Drawing.Point(96, 30);
            this.OneBookLabel.Name = "OneBookLabel";
            this.OneBookLabel.Size = new System.Drawing.Size(142, 25);
            this.OneBookLabel.TabIndex = 5;
            this.OneBookLabel.Text = "Podatki o knjigi";
            // 
            // AuthorOneBookLabel
            // 
            this.AuthorOneBookLabel.AutoSize = true;
            this.AuthorOneBookLabel.Location = new System.Drawing.Point(155, 78);
            this.AuthorOneBookLabel.Name = "AuthorOneBookLabel";
            this.AuthorOneBookLabel.Size = new System.Drawing.Size(46, 20);
            this.AuthorOneBookLabel.TabIndex = 6;
            this.AuthorOneBookLabel.Text = "Avtor";
            // 
            // IdNumberOneBookLabel
            // 
            this.IdNumberOneBookLabel.AutoSize = true;
            this.IdNumberOneBookLabel.Location = new System.Drawing.Point(97, 323);
            this.IdNumberOneBookLabel.Name = "IdNumberOneBookLabel";
            this.IdNumberOneBookLabel.Size = new System.Drawing.Size(141, 20);
            this.IdNumberOneBookLabel.TabIndex = 7;
            this.IdNumberOneBookLabel.Text = "Inventarna številka";
            // 
            // TypeOneBookLabel
            // 
            this.TypeOneBookLabel.AutoSize = true;
            this.TypeOneBookLabel.Location = new System.Drawing.Point(109, 240);
            this.TypeOneBookLabel.Name = "TypeOneBookLabel";
            this.TypeOneBookLabel.Size = new System.Drawing.Size(116, 20);
            this.TypeOneBookLabel.TabIndex = 8;
            this.TypeOneBookLabel.Text = "Področje / Žanr";
            // 
            // PublisherOneBookLabel
            // 
            this.PublisherOneBookLabel.AutoSize = true;
            this.PublisherOneBookLabel.Location = new System.Drawing.Point(147, 157);
            this.PublisherOneBookLabel.Name = "PublisherOneBookLabel";
            this.PublisherOneBookLabel.Size = new System.Drawing.Size(66, 20);
            this.PublisherOneBookLabel.TabIndex = 9;
            this.PublisherOneBookLabel.Text = "Založba";
            // 
            // AquireOneBookLabel
            // 
            this.AquireOneBookLabel.AutoSize = true;
            this.AquireOneBookLabel.Location = new System.Drawing.Point(135, 406);
            this.AquireOneBookLabel.Name = "AquireOneBookLabel";
            this.AquireOneBookLabel.Size = new System.Drawing.Size(78, 20);
            this.AquireOneBookLabel.TabIndex = 10;
            this.AquireOneBookLabel.Text = "Pridobitev";
            // 
            // StatusOneBookPictureBox
            // 
            this.StatusOneBookPictureBox.BackColor = System.Drawing.Color.Green;
            this.StatusOneBookPictureBox.Location = new System.Drawing.Point(265, 30);
            this.StatusOneBookPictureBox.Name = "StatusOneBookPictureBox";
            this.StatusOneBookPictureBox.Size = new System.Drawing.Size(37, 30);
            this.StatusOneBookPictureBox.TabIndex = 1;
            this.StatusOneBookPictureBox.TabStop = false;
            // 
            // ChangeOneBookButton
            // 
            this.ChangeOneBookButton.Location = new System.Drawing.Point(120, 506);
            this.ChangeOneBookButton.Name = "ChangeOneBookButton";
            this.ChangeOneBookButton.Size = new System.Drawing.Size(105, 36);
            this.ChangeOneBookButton.TabIndex = 11;
            this.ChangeOneBookButton.Text = "Spremeni";
            this.ChangeOneBookButton.UseVisualStyleBackColor = true;
            // 
            // PublisherOneBookComboBox
            // 
            this.PublisherOneBookComboBox.FormattingEnabled = true;
            this.PublisherOneBookComboBox.Location = new System.Drawing.Point(47, 193);
            this.PublisherOneBookComboBox.Name = "PublisherOneBookComboBox";
            this.PublisherOneBookComboBox.Size = new System.Drawing.Size(255, 28);
            this.PublisherOneBookComboBox.TabIndex = 12;
            // 
            // TypeOneBookComboBox
            // 
            this.TypeOneBookComboBox.FormattingEnabled = true;
            this.TypeOneBookComboBox.Location = new System.Drawing.Point(47, 276);
            this.TypeOneBookComboBox.Name = "TypeOneBookComboBox";
            this.TypeOneBookComboBox.Size = new System.Drawing.Size(255, 28);
            this.TypeOneBookComboBox.TabIndex = 13;
            // 
            // AcquireOneBookGroupBox
            // 
            this.AcquireOneBookGroupBox.FormattingEnabled = true;
            this.AcquireOneBookGroupBox.Location = new System.Drawing.Point(47, 440);
            this.AcquireOneBookGroupBox.Name = "AcquireOneBookGroupBox";
            this.AcquireOneBookGroupBox.Size = new System.Drawing.Size(255, 28);
            this.AcquireOneBookGroupBox.TabIndex = 14;
            // 
            // PublisherAddButton
            // 
            this.PublisherAddButton.Location = new System.Drawing.Point(331, 195);
            this.PublisherAddButton.Name = "PublisherAddButton";
            this.PublisherAddButton.Size = new System.Drawing.Size(84, 34);
            this.PublisherAddButton.TabIndex = 0;
            this.PublisherAddButton.Text = "Dodaj";
            this.PublisherAddButton.UseVisualStyleBackColor = true;
            // 
            // TypeAddButton
            // 
            this.TypeAddButton.Location = new System.Drawing.Point(331, 276);
            this.TypeAddButton.Name = "TypeAddButton";
            this.TypeAddButton.Size = new System.Drawing.Size(84, 34);
            this.TypeAddButton.TabIndex = 15;
            this.TypeAddButton.Text = "Dodaj";
            this.TypeAddButton.UseVisualStyleBackColor = true;
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
            this.BookTab.ResumeLayout(false);
            this.OneBookGroupBox.ResumeLayout(false);
            this.OneBookGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusOneBookPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.TabPage BookTab;
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
        private System.Windows.Forms.GroupBox OneBookGroupBox;
        private System.Windows.Forms.Label OneBookLabel;
        private System.Windows.Forms.TextBox IdNumberOneBookTextBox;
        private System.Windows.Forms.TextBox AuthorOneBookTextBox;
        private System.Windows.Forms.GroupBox AddDataGroupBox;
        private System.Windows.Forms.Label PublisherOneBookLabel;
        private System.Windows.Forms.Label TypeOneBookLabel;
        private System.Windows.Forms.Label IdNumberOneBookLabel;
        private System.Windows.Forms.Label AuthorOneBookLabel;
        private System.Windows.Forms.PictureBox StatusOneBookPictureBox;
        private System.Windows.Forms.Label AquireOneBookLabel;
        private System.Windows.Forms.Button ChangeOneBookButton;
        private System.Windows.Forms.ComboBox AcquireOneBookGroupBox;
        private System.Windows.Forms.ComboBox TypeOneBookComboBox;
        private System.Windows.Forms.ComboBox PublisherOneBookComboBox;
        private System.Windows.Forms.Button TypeAddButton;
        private System.Windows.Forms.Button PublisherAddButton;
    }
}

