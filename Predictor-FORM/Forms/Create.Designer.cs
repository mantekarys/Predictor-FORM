namespace Predictor_FORM
{
    partial class Create
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CreateMatch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Magneto", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Match name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(178, 180);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 22);
            this.textBox1.TabIndex = 1;
            // 
            // CreateMatch
            // 
            this.CreateMatch.Location = new System.Drawing.Point(272, 219);
            this.CreateMatch.Name = "CreateMatch";
            this.CreateMatch.Size = new System.Drawing.Size(96, 23);
            this.CreateMatch.TabIndex = 2;
            this.CreateMatch.Text = "Create match";
            this.CreateMatch.UseVisualStyleBackColor = true;
            this.CreateMatch.Click += new System.EventHandler(this.CreateMatch_Click);
            // 
            // Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 453);
            this.Controls.Add(this.CreateMatch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Create";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Create_FormClosed);
            this.Load += new System.EventHandler(this.Create_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button CreateMatch;
    }
}