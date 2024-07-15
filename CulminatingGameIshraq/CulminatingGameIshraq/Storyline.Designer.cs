namespace CulminatingGameIshraq
{
    partial class Storyline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Storyline));
            this.lblStoryLineDescription = new System.Windows.Forms.Label();
            this.lblStoryLineTitle = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStoryLineDescription
            // 
            this.lblStoryLineDescription.AutoSize = true;
            this.lblStoryLineDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoryLineDescription.Location = new System.Drawing.Point(154, 137);
            this.lblStoryLineDescription.Name = "lblStoryLineDescription";
            this.lblStoryLineDescription.Size = new System.Drawing.Size(576, 232);
            this.lblStoryLineDescription.TabIndex = 0;
            this.lblStoryLineDescription.Text = resources.GetString("lblStoryLineDescription.Text");
            this.lblStoryLineDescription.Click += new System.EventHandler(this.lblStoryLineDescription_Click);
            // 
            // lblStoryLineTitle
            // 
            this.lblStoryLineTitle.AutoSize = true;
            this.lblStoryLineTitle.Font = new System.Drawing.Font("MV Boli", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoryLineTitle.Location = new System.Drawing.Point(150, 64);
            this.lblStoryLineTitle.Name = "lblStoryLineTitle";
            this.lblStoryLineTitle.Size = new System.Drawing.Size(569, 52);
            this.lblStoryLineTitle.TabIndex = 1;
            this.lblStoryLineTitle.Text = "Astronaut Mayhem Storyline";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(16, 7);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // Storyline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lblStoryLineTitle);
            this.Controls.Add(this.lblStoryLineDescription);
            this.Name = "Storyline";
            this.Text = "Storyline";
            this.Load += new System.EventHandler(this.Storyline_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStoryLineDescription;
        private System.Windows.Forms.Label lblStoryLineTitle;
        private System.Windows.Forms.Button btnReturn;
    }
}