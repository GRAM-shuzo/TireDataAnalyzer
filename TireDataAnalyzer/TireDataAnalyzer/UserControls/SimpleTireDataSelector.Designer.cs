namespace TireDataAnalyzer.UserControls
{
    partial class SimpleTireDataSelector
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label108 = new System.Windows.Forms.Label();
            this.FzMin = new System.Windows.Forms.Label();
            this.FzMax = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.TMin = new System.Windows.Forms.Label();
            this.TMax = new System.Windows.Forms.Label();
            this.IAMin = new System.Windows.Forms.Label();
            this.IAMax = new System.Windows.Forms.Label();
            this.PMin = new System.Windows.Forms.Label();
            this.PMax = new System.Windows.Forms.Label();
            this.NormalizedCB = new System.Windows.Forms.CheckBox();
            this.FZBar = new CustomTrackBar.DoubleTrackBar();
            this.TBar = new CustomTrackBar.DoubleTrackBar();
            this.IABar = new CustomTrackBar.DoubleTrackBar();
            this.PBar = new CustomTrackBar.DoubleTrackBar();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.FzC = new System.Windows.Forms.Label();
            this.TC = new System.Windows.Forms.Label();
            this.IAC = new System.Windows.Forms.Label();
            this.PC = new System.Windows.Forms.Label();
            this.SRC = new System.Windows.Forms.Label();
            this.SAC = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SRMin = new System.Windows.Forms.Label();
            this.SRMax = new System.Windows.Forms.Label();
            this.SRBar = new CustomTrackBar.DoubleTrackBar();
            this.SAMin = new System.Windows.Forms.Label();
            this.SAMax = new System.Windows.Forms.Label();
            this.SABar = new CustomTrackBar.DoubleTrackBar();
            this.SuspendLayout();
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(3, 8);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(17, 12);
            this.label108.TabIndex = 95;
            this.label108.Text = "Fz";
            // 
            // FzMin
            // 
            this.FzMin.AutoSize = true;
            this.FzMin.Location = new System.Drawing.Point(109, 8);
            this.FzMin.Name = "FzMin";
            this.FzMin.Size = new System.Drawing.Size(47, 12);
            this.FzMin.TabIndex = 94;
            this.FzMin.Text = "0000000";
            // 
            // FzMax
            // 
            this.FzMax.AutoSize = true;
            this.FzMax.Location = new System.Drawing.Point(429, 8);
            this.FzMax.Name = "FzMax";
            this.FzMax.Size = new System.Drawing.Size(47, 12);
            this.FzMax.TabIndex = 93;
            this.FzMax.Text = "0000000";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(3, 98);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(12, 12);
            this.label47.TabIndex = 91;
            this.label47.Text = "T";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(3, 68);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(16, 12);
            this.label46.TabIndex = 90;
            this.label46.Text = "IA";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(3, 38);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(12, 12);
            this.label45.TabIndex = 89;
            this.label45.Text = "P";
            // 
            // TMin
            // 
            this.TMin.AutoSize = true;
            this.TMin.Location = new System.Drawing.Point(109, 98);
            this.TMin.Name = "TMin";
            this.TMin.Size = new System.Drawing.Size(47, 12);
            this.TMin.TabIndex = 88;
            this.TMin.Text = "0000000";
            // 
            // TMax
            // 
            this.TMax.AutoSize = true;
            this.TMax.Location = new System.Drawing.Point(429, 98);
            this.TMax.Name = "TMax";
            this.TMax.Size = new System.Drawing.Size(47, 12);
            this.TMax.TabIndex = 87;
            this.TMax.Text = "0000000";
            // 
            // IAMin
            // 
            this.IAMin.AutoSize = true;
            this.IAMin.Location = new System.Drawing.Point(109, 68);
            this.IAMin.Name = "IAMin";
            this.IAMin.Size = new System.Drawing.Size(47, 12);
            this.IAMin.TabIndex = 85;
            this.IAMin.Text = "0000000";
            // 
            // IAMax
            // 
            this.IAMax.AutoSize = true;
            this.IAMax.Location = new System.Drawing.Point(429, 68);
            this.IAMax.Name = "IAMax";
            this.IAMax.Size = new System.Drawing.Size(47, 12);
            this.IAMax.TabIndex = 84;
            this.IAMax.Text = "0000000";
            // 
            // PMin
            // 
            this.PMin.AutoSize = true;
            this.PMin.Location = new System.Drawing.Point(109, 38);
            this.PMin.Name = "PMin";
            this.PMin.Size = new System.Drawing.Size(47, 12);
            this.PMin.TabIndex = 82;
            this.PMin.Text = "0000000";
            // 
            // PMax
            // 
            this.PMax.AutoSize = true;
            this.PMax.Location = new System.Drawing.Point(429, 38);
            this.PMax.Name = "PMax";
            this.PMax.Size = new System.Drawing.Size(47, 12);
            this.PMax.TabIndex = 81;
            this.PMax.Text = "0000000";
            // 
            // NormalizedCB
            // 
            this.NormalizedCB.AutoSize = true;
            this.NormalizedCB.Location = new System.Drawing.Point(396, 183);
            this.NormalizedCB.Name = "NormalizedCB";
            this.NormalizedCB.Size = new System.Drawing.Size(80, 16);
            this.NormalizedCB.TabIndex = 96;
            this.NormalizedCB.Text = "Normalized";
            this.NormalizedCB.UseVisualStyleBackColor = true;
            this.NormalizedCB.CheckedChanged += new System.EventHandler(this.BarValueChanged);
            // 
            // FZBar
            // 
            this.FZBar.Location = new System.Drawing.Point(162, 3);
            this.FZBar.Max = 100D;
            this.FZBar.Min = -100D;
            this.FZBar.Name = "FZBar";
            this.FZBar.NumberTicks = 0;
            this.FZBar.Size = new System.Drawing.Size(260, 24);
            this.FZBar.TabIndex = 92;
            this.FZBar.Text = "doubleTrackBar19";
            this.FZBar.valueL = -100D;
            this.FZBar.valueR = 100D;
            this.FZBar.ValueChanged += new System.EventHandler(this.BarValueChanged);
            // 
            // TBar
            // 
            this.TBar.Location = new System.Drawing.Point(162, 93);
            this.TBar.Max = 100D;
            this.TBar.Min = -100D;
            this.TBar.Name = "TBar";
            this.TBar.NumberTicks = 0;
            this.TBar.Size = new System.Drawing.Size(260, 24);
            this.TBar.TabIndex = 86;
            this.TBar.Text = "doubleTrackBar3";
            this.TBar.valueL = -100D;
            this.TBar.valueR = 100D;
            this.TBar.ValueChanged += new System.EventHandler(this.BarValueChanged);
            // 
            // IABar
            // 
            this.IABar.Location = new System.Drawing.Point(162, 63);
            this.IABar.Max = 100D;
            this.IABar.Min = -100D;
            this.IABar.Name = "IABar";
            this.IABar.NumberTicks = 0;
            this.IABar.Size = new System.Drawing.Size(260, 24);
            this.IABar.TabIndex = 83;
            this.IABar.Text = "doubleTrackBar2";
            this.IABar.valueL = -100D;
            this.IABar.valueR = 100D;
            this.IABar.ValueChanged += new System.EventHandler(this.BarValueChanged);
            // 
            // PBar
            // 
            this.PBar.Location = new System.Drawing.Point(162, 33);
            this.PBar.Max = 100D;
            this.PBar.Min = -100D;
            this.PBar.Name = "PBar";
            this.PBar.NumberTicks = 0;
            this.PBar.Size = new System.Drawing.Size(260, 24);
            this.PBar.TabIndex = 80;
            this.PBar.Text = "doubleTrackBar1";
            this.PBar.valueL = -100D;
            this.PBar.valueR = 100D;
            this.PBar.ValueChanged += new System.EventHandler(this.BarValueChanged);
            // 
            // FzC
            // 
            this.FzC.AutoSize = true;
            this.FzC.Location = new System.Drawing.Point(26, 8);
            this.FzC.Name = "FzC";
            this.FzC.Size = new System.Drawing.Size(47, 12);
            this.FzC.TabIndex = 100;
            this.FzC.Text = "0000000";
            // 
            // TC
            // 
            this.TC.AutoSize = true;
            this.TC.Location = new System.Drawing.Point(26, 98);
            this.TC.Name = "TC";
            this.TC.Size = new System.Drawing.Size(47, 12);
            this.TC.TabIndex = 99;
            this.TC.Text = "0000000";
            // 
            // IAC
            // 
            this.IAC.AutoSize = true;
            this.IAC.Location = new System.Drawing.Point(26, 68);
            this.IAC.Name = "IAC";
            this.IAC.Size = new System.Drawing.Size(47, 12);
            this.IAC.TabIndex = 98;
            this.IAC.Text = "0000000";
            // 
            // PC
            // 
            this.PC.AutoSize = true;
            this.PC.Location = new System.Drawing.Point(26, 38);
            this.PC.Name = "PC";
            this.PC.Size = new System.Drawing.Size(47, 12);
            this.PC.TabIndex = 97;
            this.PC.Text = "0000000";
            // 
            // SRC
            // 
            this.SRC.AutoSize = true;
            this.SRC.Location = new System.Drawing.Point(26, 158);
            this.SRC.Name = "SRC";
            this.SRC.Size = new System.Drawing.Size(47, 12);
            this.SRC.TabIndex = 110;
            this.SRC.Text = "0000000";
            // 
            // SAC
            // 
            this.SAC.AutoSize = true;
            this.SAC.Location = new System.Drawing.Point(26, 128);
            this.SAC.Name = "SAC";
            this.SAC.Size = new System.Drawing.Size(47, 12);
            this.SAC.TabIndex = 109;
            this.SAC.Text = "0000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 12);
            this.label3.TabIndex = 108;
            this.label3.Text = "SR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 12);
            this.label4.TabIndex = 107;
            this.label4.Text = "SA";
            // 
            // SRMin
            // 
            this.SRMin.AutoSize = true;
            this.SRMin.Location = new System.Drawing.Point(109, 158);
            this.SRMin.Name = "SRMin";
            this.SRMin.Size = new System.Drawing.Size(47, 12);
            this.SRMin.TabIndex = 106;
            this.SRMin.Text = "0000000";
            // 
            // SRMax
            // 
            this.SRMax.AutoSize = true;
            this.SRMax.Location = new System.Drawing.Point(429, 158);
            this.SRMax.Name = "SRMax";
            this.SRMax.Size = new System.Drawing.Size(47, 12);
            this.SRMax.TabIndex = 105;
            this.SRMax.Text = "0000000";
            // 
            // SRBar
            // 
            this.SRBar.Location = new System.Drawing.Point(162, 153);
            this.SRBar.Max = 100D;
            this.SRBar.Min = -100D;
            this.SRBar.Name = "SRBar";
            this.SRBar.NumberTicks = 0;
            this.SRBar.Size = new System.Drawing.Size(260, 24);
            this.SRBar.TabIndex = 104;
            this.SRBar.Text = "doubleTrackBar3";
            this.SRBar.valueL = -100D;
            this.SRBar.valueR = 100D;
            // 
            // SAMin
            // 
            this.SAMin.AutoSize = true;
            this.SAMin.Location = new System.Drawing.Point(109, 128);
            this.SAMin.Name = "SAMin";
            this.SAMin.Size = new System.Drawing.Size(47, 12);
            this.SAMin.TabIndex = 103;
            this.SAMin.Text = "0000000";
            // 
            // SAMax
            // 
            this.SAMax.AutoSize = true;
            this.SAMax.Location = new System.Drawing.Point(429, 128);
            this.SAMax.Name = "SAMax";
            this.SAMax.Size = new System.Drawing.Size(47, 12);
            this.SAMax.TabIndex = 102;
            this.SAMax.Text = "0000000";
            // 
            // SABar
            // 
            this.SABar.Location = new System.Drawing.Point(162, 123);
            this.SABar.Max = 100D;
            this.SABar.Min = -100D;
            this.SABar.Name = "SABar";
            this.SABar.NumberTicks = 0;
            this.SABar.Size = new System.Drawing.Size(260, 24);
            this.SABar.TabIndex = 101;
            this.SABar.Text = "doubleTrackBar2";
            this.SABar.valueL = -100D;
            this.SABar.valueR = 100D;
            // 
            // SimpleTireDataSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SRC);
            this.Controls.Add(this.SAC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SRMin);
            this.Controls.Add(this.SRMax);
            this.Controls.Add(this.SRBar);
            this.Controls.Add(this.SAMin);
            this.Controls.Add(this.SAMax);
            this.Controls.Add(this.SABar);
            this.Controls.Add(this.FzC);
            this.Controls.Add(this.TC);
            this.Controls.Add(this.IAC);
            this.Controls.Add(this.PC);
            this.Controls.Add(this.NormalizedCB);
            this.Controls.Add(this.label108);
            this.Controls.Add(this.FzMin);
            this.Controls.Add(this.FzMax);
            this.Controls.Add(this.FZBar);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.TMin);
            this.Controls.Add(this.TMax);
            this.Controls.Add(this.TBar);
            this.Controls.Add(this.IAMin);
            this.Controls.Add(this.IAMax);
            this.Controls.Add(this.IABar);
            this.Controls.Add(this.PMin);
            this.Controls.Add(this.PMax);
            this.Controls.Add(this.PBar);
            this.Name = "SimpleTireDataSelector";
            this.Size = new System.Drawing.Size(479, 203);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label FzMin;
        private System.Windows.Forms.Label FzMax;
        private CustomTrackBar.DoubleTrackBar FZBar;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label TMin;
        private System.Windows.Forms.Label TMax;
        private CustomTrackBar.DoubleTrackBar TBar;
        private System.Windows.Forms.Label IAMin;
        private System.Windows.Forms.Label IAMax;
        private CustomTrackBar.DoubleTrackBar IABar;
        private System.Windows.Forms.Label PMin;
        private System.Windows.Forms.Label PMax;
        private CustomTrackBar.DoubleTrackBar PBar;
        private System.Windows.Forms.CheckBox NormalizedCB;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Label FzC;
        private System.Windows.Forms.Label TC;
        private System.Windows.Forms.Label IAC;
        private System.Windows.Forms.Label PC;
        private System.Windows.Forms.Label SRC;
        private System.Windows.Forms.Label SAC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label SRMin;
        private System.Windows.Forms.Label SRMax;
        private CustomTrackBar.DoubleTrackBar SRBar;
        private System.Windows.Forms.Label SAMin;
        private System.Windows.Forms.Label SAMax;
        private CustomTrackBar.DoubleTrackBar SABar;
    }
}
