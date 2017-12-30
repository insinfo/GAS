namespace GAS2
{
    partial class FormGDI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.viewPort = new GAS2.ViewPort();
            this.BtnAddRectangle = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnSelectTool = new System.Windows.Forms.Button();
            this.BtnAddCircle = new System.Windows.Forms.Button();
            this.BtnAddTriangle = new System.Windows.Forms.Button();
            this.BtnAddText = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1035, 583);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(53, 63);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 517);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.viewPort);
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(764, 491);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pagina 1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 491);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // viewPort
            // 
            this.viewPort.BackColor = System.Drawing.Color.DarkGray;
            this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPort.ForeColor = System.Drawing.Color.White;
            this.viewPort.Location = new System.Drawing.Point(0, 0);
            this.viewPort.Margin = new System.Windows.Forms.Padding(0);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(764, 491);
            this.viewPort.TabIndex = 0;
            // 
            // BtnAddRectangle
            // 
            this.BtnAddRectangle.BackColor = System.Drawing.Color.Gray;
            this.BtnAddRectangle.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnAddRectangle.FlatAppearance.BorderSize = 0;
            this.BtnAddRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddRectangle.ForeColor = System.Drawing.Color.White;
            this.BtnAddRectangle.Location = new System.Drawing.Point(3, 156);
            this.BtnAddRectangle.Name = "BtnAddRectangle";
            this.BtnAddRectangle.Size = new System.Drawing.Size(47, 45);
            this.BtnAddRectangle.TabIndex = 1;
            this.BtnAddRectangle.Text = "Re";
            this.BtnAddRectangle.UseVisualStyleBackColor = false;
            this.BtnAddRectangle.Click += new System.EventHandler(this.BtnAddRectangle_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Orange;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.BtnAddText, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.BtnAddTriangle, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.BtnAddCircle, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.BtnSelectTool, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnAddRectangle, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(54, 520);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // BtnSelectTool
            // 
            this.BtnSelectTool.BackColor = System.Drawing.Color.Gray;
            this.BtnSelectTool.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnSelectTool.FlatAppearance.BorderSize = 0;
            this.BtnSelectTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSelectTool.ForeColor = System.Drawing.Color.White;
            this.BtnSelectTool.Location = new System.Drawing.Point(3, 3);
            this.BtnSelectTool.Name = "BtnSelectTool";
            this.BtnSelectTool.Size = new System.Drawing.Size(47, 45);
            this.BtnSelectTool.TabIndex = 2;
            this.BtnSelectTool.Text = "S";
            this.BtnSelectTool.UseVisualStyleBackColor = false;
            // 
            // BtnAddCircle
            // 
            this.BtnAddCircle.BackColor = System.Drawing.Color.Gray;
            this.BtnAddCircle.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnAddCircle.FlatAppearance.BorderSize = 0;
            this.BtnAddCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddCircle.ForeColor = System.Drawing.Color.White;
            this.BtnAddCircle.Location = new System.Drawing.Point(3, 54);
            this.BtnAddCircle.Name = "BtnAddCircle";
            this.BtnAddCircle.Size = new System.Drawing.Size(47, 45);
            this.BtnAddCircle.TabIndex = 3;
            this.BtnAddCircle.Text = "C";
            this.BtnAddCircle.UseVisualStyleBackColor = false;
            // 
            // BtnAddTriangle
            // 
            this.BtnAddTriangle.BackColor = System.Drawing.Color.Gray;
            this.BtnAddTriangle.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnAddTriangle.FlatAppearance.BorderSize = 0;
            this.BtnAddTriangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddTriangle.ForeColor = System.Drawing.Color.White;
            this.BtnAddTriangle.Location = new System.Drawing.Point(3, 105);
            this.BtnAddTriangle.Name = "BtnAddTriangle";
            this.BtnAddTriangle.Size = new System.Drawing.Size(47, 45);
            this.BtnAddTriangle.TabIndex = 4;
            this.BtnAddTriangle.Text = "Tri";
            this.BtnAddTriangle.UseVisualStyleBackColor = false;
            // 
            // BtnAddText
            // 
            this.BtnAddText.BackColor = System.Drawing.Color.Gray;
            this.BtnAddText.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnAddText.FlatAppearance.BorderSize = 0;
            this.BtnAddText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddText.ForeColor = System.Drawing.Color.White;
            this.BtnAddText.Location = new System.Drawing.Point(3, 207);
            this.BtnAddText.Name = "BtnAddText";
            this.BtnAddText.Size = new System.Drawing.Size(47, 45);
            this.BtnAddText.TabIndex = 5;
            this.BtnAddText.Text = "T";
            this.BtnAddText.UseVisualStyleBackColor = false;
            this.BtnAddText.Click += new System.EventHandler(this.BtnAddText_Click);
            // 
            // Teste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1035, 583);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Teste";
            this.Text = "Teste";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ViewPort viewPort;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BtnAddRectangle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BtnAddText;
        private System.Windows.Forms.Button BtnAddTriangle;
        private System.Windows.Forms.Button BtnAddCircle;
        private System.Windows.Forms.Button BtnSelectTool;
    }
}