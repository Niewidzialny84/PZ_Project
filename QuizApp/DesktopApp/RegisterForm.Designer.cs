
namespace DesktopApp
{
    partial class RegisterForm
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
            this.reg_label = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.repPassBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reg_label
            // 
            this.reg_label.AutoSize = true;
            this.reg_label.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reg_label.Location = new System.Drawing.Point(306, 74);
            this.reg_label.Name = "reg_label";
            this.reg_label.Size = new System.Drawing.Size(127, 29);
            this.reg_label.TabIndex = 0;
            this.reg_label.Text = "Rejestracja";
            // 
            // loginBox
            // 
            this.loginBox.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loginBox.Location = new System.Drawing.Point(263, 139);
            this.loginBox.Name = "loginBox";
            this.loginBox.PlaceholderText = "nazwa użytkownika";
            this.loginBox.Size = new System.Drawing.Size(204, 37);
            this.loginBox.TabIndex = 1;
            // 
            // passBox
            // 
            this.passBox.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.passBox.Location = new System.Drawing.Point(263, 182);
            this.passBox.Name = "passBox";
            this.passBox.PlaceholderText = "hasło";
            this.passBox.Size = new System.Drawing.Size(204, 37);
            this.passBox.TabIndex = 2;
            // 
            // repPassBox
            // 
            this.repPassBox.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.repPassBox.Location = new System.Drawing.Point(263, 225);
            this.repPassBox.Name = "repPassBox";
            this.repPassBox.PlaceholderText = "powtórz hasło";
            this.repPassBox.Size = new System.Drawing.Size(204, 37);
            this.repPassBox.TabIndex = 3;
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.registerButton.Location = new System.Drawing.Point(263, 268);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(204, 41);
            this.registerButton.TabIndex = 4;
            this.registerButton.Text = "Zarejestruj się";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // returnButton
            // 
            this.returnButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.returnButton.Location = new System.Drawing.Point(3, 3);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(90, 33);
            this.returnButton.TabIndex = 5;
            this.returnButton.Text = "Powrót";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(198)))), ((int)(((byte)(167)))));
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.repPassBox);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.reg_label);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reg_label;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.TextBox repPassBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button returnButton;
    }
}