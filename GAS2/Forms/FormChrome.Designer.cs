namespace GAS2
{
    partial class FormChrome
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
            this.canvasUML1 = new GAS2.UML.CanvasUML();
            this.SuspendLayout();
            // 
            // canvasUML1
            // 
            this.canvasUML1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasUML1.Location = new System.Drawing.Point(0, 0);
            this.canvasUML1.Name = "canvasUML1";
            this.canvasUML1.Size = new System.Drawing.Size(896, 507);
            this.canvasUML1.TabIndex = 0;
            // 
            // FormChrome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 507);
            this.Controls.Add(this.canvasUML1);
            this.Name = "FormChrome";
            this.Text = "FormChrome";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormChrome_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormChrome_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormChrome_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private UML.CanvasUML canvasUML1;
    }
}