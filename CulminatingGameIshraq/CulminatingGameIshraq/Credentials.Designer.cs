namespace CulminatingGameIshraq
{
    partial class Credentials
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
            this.lblCredentialsTitle = new System.Windows.Forms.Label();
            this.lblCredentialsDescription = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCredentialsTitle
            // 
            this.lblCredentialsTitle.AutoSize = true;
            this.lblCredentialsTitle.Font = new System.Drawing.Font("MV Boli", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredentialsTitle.Location = new System.Drawing.Point(237, 35);
            this.lblCredentialsTitle.Name = "lblCredentialsTitle";
            this.lblCredentialsTitle.Size = new System.Drawing.Size(338, 79);
            this.lblCredentialsTitle.TabIndex = 0;
            this.lblCredentialsTitle.Text = "Credentials";
            // 
            // lblCredentialsDescription
            // 
            this.lblCredentialsDescription.AutoSize = true;
            this.lblCredentialsDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredentialsDescription.Location = new System.Drawing.Point(245, 151);
            this.lblCredentialsDescription.Name = "lblCredentialsDescription";
            this.lblCredentialsDescription.Size = new System.Drawing.Size(328, 180);
            this.lblCredentialsDescription.TabIndex = 1;
            this.lblCredentialsDescription.Text = " Creator: Ishraq Alam\r\n\r\n Designer: Ishraq Alam\r\n\r\nInspiration: Ishraq Alam\r\n";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(27, 21);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // Credentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lblCredentialsDescription);
            this.Controls.Add(this.lblCredentialsTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Credentials";
            this.Text = "Credentials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCredentialsTitle;
        private System.Windows.Forms.Label lblCredentialsDescription;
        private System.Windows.Forms.Button btnReturn;
    }
}