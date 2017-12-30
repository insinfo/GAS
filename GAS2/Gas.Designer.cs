namespace GAS2
{
    partial class Gas
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenDesigner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnExecute.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.Location = new System.Drawing.Point(610, 16);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(144, 51);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Generate";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtResults.ForeColor = System.Drawing.Color.White;
            this.txtResults.Location = new System.Drawing.Point(0, 73);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(766, 353);
            this.txtResults.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "GAS pre alpha version";
            // 
            // btnOpenDesigner
            // 
            this.btnOpenDesigner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnOpenDesigner.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOpenDesigner.FlatAppearance.BorderSize = 0;
            this.btnOpenDesigner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOpenDesigner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnOpenDesigner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDesigner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDesigner.ForeColor = System.Drawing.Color.White;
            this.btnOpenDesigner.Location = new System.Drawing.Point(460, 16);
            this.btnOpenDesigner.Name = "btnOpenDesigner";
            this.btnOpenDesigner.Size = new System.Drawing.Size(144, 51);
            this.btnOpenDesigner.TabIndex = 3;
            this.btnOpenDesigner.Text = "Designer";
            this.btnOpenDesigner.UseVisualStyleBackColor = false;
            this.btnOpenDesigner.Click += new System.EventHandler(this.btnOpenDesigner_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(766, 426);
            this.Controls.Add(this.btnOpenDesigner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnExecute);
            this.Name = "Main";
            this.Text = "GAS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenDesigner;
    }
}

