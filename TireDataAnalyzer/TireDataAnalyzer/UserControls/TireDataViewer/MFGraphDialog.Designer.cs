namespace TireDataAnalyzer.UserControls
{
    partial class MFGraphDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SATB = new System.Windows.Forms.TextBox();
            this.SRTB = new System.Windows.Forms.TextBox();
            this.FZTB = new System.Windows.Forms.TextBox();
            this.IATB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TTB = new System.Windows.Forms.TextBox();
            this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.NextButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.SelectedIndexTB = new System.Windows.Forms.TextBox();
            this.seriesEditorHeader1 = new TireDataAnalyzer.UserControls.SeriesEditorHeader();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(726, 567);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 36;
            this.ApplyButton.Text = "適用";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(645, 567);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 35;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(564, 567);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 34;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "SA";
            // 
            // SATB
            // 
            this.SATB.Location = new System.Drawing.Point(11, 106);
            this.SATB.Name = "SATB";
            this.SATB.Size = new System.Drawing.Size(127, 19);
            this.SATB.TabIndex = 38;
            this.SATB.Enter += new System.EventHandler(this.TB_Enter);
            this.SATB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.SATB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SATB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // SRTB
            // 
            this.SRTB.Location = new System.Drawing.Point(144, 106);
            this.SRTB.Name = "SRTB";
            this.SRTB.Size = new System.Drawing.Size(127, 19);
            this.SRTB.TabIndex = 39;
            this.SRTB.Enter += new System.EventHandler(this.TB_Enter);
            this.SRTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.SRTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SRTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // FZTB
            // 
            this.FZTB.Location = new System.Drawing.Point(277, 106);
            this.FZTB.Name = "FZTB";
            this.FZTB.Size = new System.Drawing.Size(127, 19);
            this.FZTB.TabIndex = 40;
            this.FZTB.Enter += new System.EventHandler(this.TB_Enter);
            this.FZTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.FZTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.FZTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // IATB
            // 
            this.IATB.Location = new System.Drawing.Point(410, 106);
            this.IATB.Name = "IATB";
            this.IATB.Size = new System.Drawing.Size(127, 19);
            this.IATB.TabIndex = 41;
            this.IATB.Enter += new System.EventHandler(this.TB_Enter);
            this.IATB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.IATB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.IATB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "SR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "FZ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(408, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "IA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(541, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "P";
            // 
            // PTB
            // 
            this.PTB.Location = new System.Drawing.Point(543, 106);
            this.PTB.Name = "PTB";
            this.PTB.Size = new System.Drawing.Size(127, 19);
            this.PTB.TabIndex = 45;
            this.PTB.Enter += new System.EventHandler(this.TB_Enter);
            this.PTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.PTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.PTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(674, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 48;
            this.label6.Text = "T";
            // 
            // TTB
            // 
            this.TTB.Location = new System.Drawing.Point(676, 106);
            this.TTB.Name = "TTB";
            this.TTB.Size = new System.Drawing.Size(127, 19);
            this.TTB.TabIndex = 47;
            this.TTB.Enter += new System.EventHandler(this.TB_Enter);
            this.TTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.TTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.TTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // MainChart
            // 
            chartArea1.Name = "ChartArea1";
            this.MainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.MainChart.Legends.Add(legend1);
            this.MainChart.Location = new System.Drawing.Point(11, 161);
            this.MainChart.Name = "MainChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.MainChart.Series.Add(series1);
            this.MainChart.Size = new System.Drawing.Size(790, 400);
            this.MainChart.TabIndex = 49;
            this.MainChart.Text = "chart1";
            // 
            // NextButton
            // 
            this.NextButton.Image = global::TireDataAnalyzer.Properties.Resources.arrow_Next_16xLG;
            this.NextButton.Location = new System.Drawing.Point(723, 131);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(23, 23);
            this.NextButton.TabIndex = 51;
            this.NextButton.Text = " ";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextPreviouse_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Image = global::TireDataAnalyzer.Properties.Resources.arrow_previous_16xLG;
            this.PreviousButton.Location = new System.Drawing.Point(694, 131);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(23, 23);
            this.PreviousButton.TabIndex = 50;
            this.PreviousButton.Text = " ";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.NextPreviouse_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(752, 131);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(51, 23);
            this.SelectButton.TabIndex = 52;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // SelectedIndexTB
            // 
            this.SelectedIndexTB.Location = new System.Drawing.Point(629, 133);
            this.SelectedIndexTB.Name = "SelectedIndexTB";
            this.SelectedIndexTB.Size = new System.Drawing.Size(59, 19);
            this.SelectedIndexTB.TabIndex = 53;
            this.SelectedIndexTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nint_KeyDown);
            this.SelectedIndexTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsNInt_Validating);
            this.SelectedIndexTB.Validated += new System.EventHandler(this.IsNInt_Validated);
            // 
            // seriesEditorHeader1
            // 
            this.seriesEditorHeader1.Location = new System.Drawing.Point(12, 12);
            this.seriesEditorHeader1.Name = "seriesEditorHeader1";
            this.seriesEditorHeader1.Size = new System.Drawing.Size(795, 20);
            this.seriesEditorHeader1.TabIndex = 0;
            // 
            // MFGraphDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 602);
            this.Controls.Add(this.SelectedIndexTB);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.MainChart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IATB);
            this.Controls.Add(this.FZTB);
            this.Controls.Add(this.SRTB);
            this.Controls.Add(this.SATB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.seriesEditorHeader1);
            this.Name = "MFGraphDialog";
            this.Text = "MFGraphDialog";
            this.Load += new System.EventHandler(this.MFGraphDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeriesEditorHeader seriesEditorHeader1;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IATB;
        private System.Windows.Forms.TextBox FZTB;
        private System.Windows.Forms.TextBox SRTB;
        private System.Windows.Forms.TextBox SATB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.TextBox SelectedIndexTB;
        private System.Windows.Forms.Button SelectButton;
    }
}