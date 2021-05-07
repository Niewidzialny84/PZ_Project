
namespace DesktopApp
{
    partial class MainForm
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.statsButton = new System.Windows.Forms.Button();
            this.rankingButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameLabel.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.usernameLabel.Location = new System.Drawing.Point(265, 28);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(192, 29);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Hello Niewidzialny";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // logoutButton
            // 
            this.logoutButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logoutButton.Location = new System.Drawing.Point(245, 253);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(227, 39);
            this.logoutButton.TabIndex = 1;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // statsButton
            // 
            this.statsButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statsButton.Location = new System.Drawing.Point(245, 118);
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(227, 39);
            this.statsButton.TabIndex = 2;
            this.statsButton.Text = "Stats";
            this.statsButton.UseVisualStyleBackColor = true;
            this.statsButton.Click += new System.EventHandler(this.statsButton_Click);
            // 
            // rankingButton
            // 
            this.rankingButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rankingButton.Location = new System.Drawing.Point(245, 163);
            this.rankingButton.Name = "rankingButton";
            this.rankingButton.Size = new System.Drawing.Size(227, 39);
            this.rankingButton.TabIndex = 3;
            this.rankingButton.Text = "Global Rankings";
            this.rankingButton.UseVisualStyleBackColor = true;
            this.rankingButton.Click += new System.EventHandler(this.rankingButton_Click);
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playButton.Location = new System.Drawing.Point(245, 73);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(227, 39);
            this.playButton.TabIndex = 5;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(198)))), ((int)(((byte)(167)))));
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.rankingButton);
            this.Controls.Add(this.statsButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.usernameLabel);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button statsButton;
        private System.Windows.Forms.Button rankingButton;
        private System.Windows.Forms.Button playButton;
    }
}