namespace MipsSimulator
{
    partial class MipsMainScreen
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
            this.executionLogs = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // executionLogs
            // 
            this.executionLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.executionLogs.Enabled = false;
            this.executionLogs.Location = new System.Drawing.Point(12, 190);
            this.executionLogs.Multiline = true;
            this.executionLogs.Name = "executionLogs";
            this.executionLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.executionLogs.Size = new System.Drawing.Size(392, 248);
            this.executionLogs.TabIndex = 0;
            // 
            // MipsMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.executionLogs);
            this.MaximizeBox = false;
            this.Name = "MipsMainScreen";
            this.Text = "Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox executionLogs;
    }
}