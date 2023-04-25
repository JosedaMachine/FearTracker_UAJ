namespace Gravedad
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.calcWeight = new System.Windows.Forms.Button();
            this.weightResult = new System.Windows.Forms.Label();
            this.massEntry = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // calcWeight
            // 
            this.calcWeight.Location = new System.Drawing.Point(372, 233);
            this.calcWeight.Name = "calcWeight";
            this.calcWeight.Size = new System.Drawing.Size(75, 23);
            this.calcWeight.TabIndex = 0;
            this.calcWeight.Text = "Calculate Weight";
            this.calcWeight.UseVisualStyleBackColor = true;
            this.calcWeight.Click += new System.EventHandler(this.calcWeight_Click);
            // 
            // weightResult
            // 
            this.weightResult.AutoSize = true;
            this.weightResult.Location = new System.Drawing.Point(391, 259);
            this.weightResult.Name = "weightResult";
            this.weightResult.Size = new System.Drawing.Size(44, 13);
            this.weightResult.TabIndex = 1;
            this.weightResult.Text = "Weight:";
            // 
            // massEntry
            // 
            this.massEntry.Location = new System.Drawing.Point(359, 207);
            this.massEntry.Name = "massEntry";
            this.massEntry.Size = new System.Drawing.Size(100, 20);
            this.massEntry.TabIndex = 2;
            this.massEntry.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter Mass";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.massEntry);
            this.Controls.Add(this.weightResult);
            this.Controls.Add(this.calcWeight);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calcWeight;
        private System.Windows.Forms.Label weightResult;
        private System.Windows.Forms.TextBox massEntry;
        private System.Windows.Forms.Label label2;
    }
}

