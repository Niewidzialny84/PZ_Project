
namespace DesktopApp
{
    partial class EndForm
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
            this.menuButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.timeLeft = new System.Windows.Forms.Label();
            this.totalResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuButton
            // 
            this.menuButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuButton.Location = new System.Drawing.Point(306, 398);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(202, 40);
            this.menuButton.TabIndex = 0;
            this.menuButton.Text = "Main Menu";
            this.menuButton.UseVisualStyleBackColor = true;
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.messageLabel.Location = new System.Drawing.Point(162, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(500, 100);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "You Answered All Questions ";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeLeft
            // 
            this.timeLeft.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeLeft.Location = new System.Drawing.Point(162, 139);
            this.timeLeft.Name = "timeLeft";
            this.timeLeft.Size = new System.Drawing.Size(500, 100);
            this.timeLeft.TabIndex = 3;
            this.timeLeft.Text = "Time Left:";
            this.timeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalResult
            // 
            this.totalResult.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalResult.Location = new System.Drawing.Point(162, 239);
            this.totalResult.Name = "totalResult";
            this.totalResult.Size = new System.Drawing.Size(500, 100);
            this.totalResult.TabIndex = 6;
            this.totalResult.Text = "Total Result:";
            this.totalResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.totalResult);
            this.Controls.Add(this.timeLeft);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.menuButton);
            this.Name = "EndForm";
            this.Text = "EndForm";
            this.Load += new System.EventHandler(this.EndForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label timeLeft;
        private System.Windows.Forms.Label totalResult;
    }
}