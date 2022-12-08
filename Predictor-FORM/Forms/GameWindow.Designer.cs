namespace Predictor_FORM.Forms
{
    partial class GameWindow
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
            this.GameConsole = new System.Windows.Forms.TextBox();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GameConsole
            // 
            this.GameConsole.Enabled = false;
            this.GameConsole.Location = new System.Drawing.Point(750, 22);
            this.GameConsole.Multiline = true;
            this.GameConsole.Name = "GameConsole";
            this.GameConsole.Size = new System.Drawing.Size(130, 41);
            this.GameConsole.TabIndex = 0;
            this.GameConsole.TextChanged += new System.EventHandler(this.NewInput);
            this.GameConsole.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameConsole_KeyDown);
            // 
            // MessageBox
            // 
            this.MessageBox.Enabled = false;
            this.MessageBox.Location = new System.Drawing.Point(750, 102);
            this.MessageBox.Multiline = true;
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ReadOnly = true;
            this.MessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessageBox.Size = new System.Drawing.Size(130, 324);
            this.MessageBox.TabIndex = 1;
            // 
            // GameWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(882, 703);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.GameConsole);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(900, 750);
            this.MinimumSize = new System.Drawing.Size(900, 750);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            this.Load += new System.EventHandler(this.Map_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameWindow_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameWindow_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GameConsole;
        private System.Windows.Forms.TextBox MessageBox;
    }
}