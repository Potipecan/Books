
namespace Books.SubWindows
{
    partial class LoginForm
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
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginPasswordTB = new System.Windows.Forms.TextBox();
            this.PasswordLoginLabel = new System.Windows.Forms.Label();
            this.LoginEmailTB = new System.Windows.Forms.TextBox();
            this.DeleteDBButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LoginGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.LoginButton);
            this.LoginGroupBox.Controls.Add(this.label1);
            this.LoginGroupBox.Controls.Add(this.LoginPasswordTB);
            this.LoginGroupBox.Controls.Add(this.PasswordLoginLabel);
            this.LoginGroupBox.Controls.Add(this.LoginEmailTB);
            this.LoginGroupBox.Location = new System.Drawing.Point(252, 64);
            this.LoginGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginGroupBox.Size = new System.Drawing.Size(296, 322);
            this.LoginGroupBox.TabIndex = 4;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Prijava";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(100, 198);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(91, 37);
            this.LoginButton.TabIndex = 17;
            this.LoginButton.Text = "Prijavi se";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Vnesite uporabniško ime";
            // 
            // LoginPasswordTB
            // 
            this.LoginPasswordTB.Location = new System.Drawing.Point(1, 138);
            this.LoginPasswordTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginPasswordTB.Name = "LoginPasswordTB";
            this.LoginPasswordTB.PasswordChar = '*';
            this.LoginPasswordTB.Size = new System.Drawing.Size(289, 22);
            this.LoginPasswordTB.TabIndex = 15;
            // 
            // PasswordLoginLabel
            // 
            this.PasswordLoginLabel.AutoSize = true;
            this.PasswordLoginLabel.Location = new System.Drawing.Point(106, 109);
            this.PasswordLoginLabel.Name = "PasswordLoginLabel";
            this.PasswordLoginLabel.Size = new System.Drawing.Size(93, 17);
            this.PasswordLoginLabel.TabIndex = 14;
            this.PasswordLoginLabel.Text = "Vnesite geslo";
            // 
            // LoginEmailTB
            // 
            this.LoginEmailTB.Location = new System.Drawing.Point(1, 67);
            this.LoginEmailTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginEmailTB.Name = "LoginEmailTB";
            this.LoginEmailTB.Size = new System.Drawing.Size(289, 22);
            this.LoginEmailTB.TabIndex = 13;
            // 
            // DeleteDBButton
            // 
            this.DeleteDBButton.Location = new System.Drawing.Point(646, 262);
            this.DeleteDBButton.Name = "DeleteDBButton";
            this.DeleteDBButton.Size = new System.Drawing.Size(88, 37);
            this.DeleteDBButton.TabIndex = 5;
            this.DeleteDBButton.Text = "Izbriši DB";
            this.DeleteDBButton.UseVisualStyleBackColor = true;
            this.DeleteDBButton.Click += new System.EventHandler(this.DeleteDBButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DeleteDBButton);
            this.Controls.Add(this.LoginGroupBox);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginPasswordTB;
        private System.Windows.Forms.Label PasswordLoginLabel;
        private System.Windows.Forms.TextBox LoginEmailTB;
        private System.Windows.Forms.Button DeleteDBButton;
        private System.Windows.Forms.Button button1;
    }
}