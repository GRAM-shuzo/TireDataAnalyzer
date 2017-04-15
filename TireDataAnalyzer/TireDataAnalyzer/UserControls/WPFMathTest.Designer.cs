namespace TireDataAnalyzer.UserControls
{
    partial class WPFMathTest
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.magicFormula_TexEquation1 = new TireDataAnalyzer.TexEquation.MagicFormula_TexEquation();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(502, 542);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Validated += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // magicFormula_TexEquation1
            // 
            this.magicFormula_TexEquation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.magicFormula_TexEquation1.Location = new System.Drawing.Point(0, 0);
            this.magicFormula_TexEquation1.Name = "magicFormula_TexEquation1";
            this.magicFormula_TexEquation1.Size = new System.Drawing.Size(764, 603);
            this.magicFormula_TexEquation1.TabIndex = 0;
            this.magicFormula_TexEquation1.Type = TireDataAnalyzer.TexEquation.MagicFormula_TexEquation.MagicFormulaType.FY;
            // 
            // WPFMathTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 603);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.magicFormula_TexEquation1);
            this.Name = "WPFMathTest";
            this.Text = "WPFMathTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TexEquation.MagicFormula_TexEquation magicFormula_TexEquation1;
        private System.Windows.Forms.TextBox textBox1;
    }
}