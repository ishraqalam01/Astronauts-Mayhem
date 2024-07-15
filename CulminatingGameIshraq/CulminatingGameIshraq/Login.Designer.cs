namespace CulminatingGameIshraq
{
    partial class Login
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
            this.txtLoginBox = new System.Windows.Forms.TextBox();
            this.lblEnterName = new System.Windows.Forms.Label();
            this.btnContinueToMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLoginBox
            // 
            this.txtLoginBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginBox.Location = new System.Drawing.Point(117, 190);
            this.txtLoginBox.Name = "txtLoginBox";
            this.txtLoginBox.Size = new System.Drawing.Size(585, 34);
            this.txtLoginBox.TabIndex = 0;
            // 
            // lblEnterName
            // 
            this.lblEnterName.AutoSize = true;
            this.lblEnterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterName.Location = new System.Drawing.Point(223, 111);
            this.lblEnterName.Name = "lblEnterName";
            this.lblEnterName.Size = new System.Drawing.Size(396, 54);
            this.lblEnterName.TabIndex = 1;
            this.lblEnterName.Text = "Enter Your Name:";
            // 
            // btnContinueToMenu
            // 
            this.btnContinueToMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinueToMenu.Location = new System.Drawing.Point(298, 333);
            this.btnContinueToMenu.Name = "btnContinueToMenu";
            this.btnContinueToMenu.Size = new System.Drawing.Size(241, 73);
            this.btnContinueToMenu.TabIndex = 2;
            this.btnContinueToMenu.Text = "Continue";
            this.btnContinueToMenu.UseVisualStyleBackColor = true;
            this.btnContinueToMenu.Click += new System.EventHandler(this.btnContinueToMenu_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(838, 585);
            this.Controls.Add(this.btnContinueToMenu);
            this.Controls.Add(this.lblEnterName);
            this.Controls.Add(this.txtLoginBox);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLoginBox;
        private System.Windows.Forms.Label lblEnterName;
        private System.Windows.Forms.Button btnContinueToMenu;
    }
}