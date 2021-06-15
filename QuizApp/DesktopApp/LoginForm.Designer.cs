
namespace DesktopApp
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.name_lbl = new System.Windows.Forms.Label();
            this.top_lbl = new System.Windows.Forms.Label();
            this.log_box = new System.Windows.Forms.TextBox();
            this.pass_box = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.BackColor = System.Drawing.Color.Transparent;
            this.name_lbl.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.name_lbl.Location = new System.Drawing.Point(291, 9);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(124, 45);
            this.name_lbl.TabIndex = 0;
            this.name_lbl.Text = "Quizer";
            // 
            // top_lbl
            // 
            this.top_lbl.AutoSize = true;
            this.top_lbl.BackColor = System.Drawing.Color.Transparent;
            this.top_lbl.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.top_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.top_lbl.Location = new System.Drawing.Point(230, 54);
            this.top_lbl.Name = "top_lbl";
            this.top_lbl.Size = new System.Drawing.Size(247, 33);
            this.top_lbl.TabIndex = 1;
            this.top_lbl.Text = "Wyzwanie dla umysłu";
            // 
            // log_box
            // 
            this.log_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.log_box.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.log_box.ForeColor = System.Drawing.SystemColors.Window;
            this.log_box.Location = new System.Drawing.Point(258, 114);
            this.log_box.Name = "log_box";
            this.log_box.PlaceholderText = "Użytkownik";
            this.log_box.Size = new System.Drawing.Size(191, 34);
            this.log_box.TabIndex = 4;
            // 
            // pass_box
            // 
            this.pass_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.pass_box.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pass_box.ForeColor = System.Drawing.SystemColors.Window;
            this.pass_box.Location = new System.Drawing.Point(258, 154);
            this.pass_box.Name = "pass_box";
            this.pass_box.PlaceholderText = "Hasło";
            this.pass_box.Size = new System.Drawing.Size(191, 34);
            this.pass_box.TabIndex = 5;
            this.pass_box.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loginButton.ForeColor = System.Drawing.Color.White;
            this.loginButton.Location = new System.Drawing.Point(258, 210);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(191, 43);
            this.loginButton.TabIndex = 6;
            this.loginButton.Text = "Zaloguj";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(311, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Zarejestruj się";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(150)))), ((int)(((byte)(125)))));
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.pass_box);
            this.Controls.Add(this.log_box);
            this.Controls.Add(this.top_lbl);
            this.Controls.Add(this.name_lbl);
            this.Name = "LoginForm";
            this.Text = "Quizer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name_lbl;
        private System.Windows.Forms.Label top_lbl;
        private System.Windows.Forms.TextBox log_box;
        private System.Windows.Forms.TextBox pass_box;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label1;
    }
}

