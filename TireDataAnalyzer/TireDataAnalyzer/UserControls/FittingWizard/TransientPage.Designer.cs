namespace TireDataAnalyzer.UserControls.FittingWizard
{
    partial class TransientPage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransientPage));
            this.t_tauY = new System.Windows.Forms.TabPage();
            this.Theta_FySC = new System.Windows.Forms.SplitContainer();
            this.testMaxLabel = new System.Windows.Forms.Label();
            this.testNumTB = new System.Windows.Forms.TextBox();
            this.testNumTrackBar = new System.Windows.Forms.TrackBar();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.simpleTireDataSelector1 = new TireDataAnalyzer.UserControls.SimpleTireDataSelector();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.s4TB = new System.Windows.Forms.TextBox();
            this.s3TB = new System.Windows.Forms.TextBox();
            this.AdviseBox = new System.Windows.Forms.GroupBox();
            this.magicFormula_TexEquation0 = new TireDataAnalyzer.TexEquation.MagicFormula_TexEquation();
            this.AdviseText0 = new System.Windows.Forms.Label();
            this.s2TB = new System.Windows.Forms.TextBox();
            this.s1TB = new System.Windows.Forms.TextBox();
            this.comboBox0 = new System.Windows.Forms.ComboBox();
            this.Theta_FyViewer = new TireDataAnalyzer.UserControls.MultiTireDataViewer();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.t_tauX = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label111 = new System.Windows.Forms.Label();
            this.DontUseT_sigmaCB = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.s4TBX = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.magicFormula_TexEquation1 = new TireDataAnalyzer.TexEquation.MagicFormula_TexEquation();
            this.AdviseText1 = new System.Windows.Forms.Label();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.s3TBX = new System.Windows.Forms.TextBox();
            this.s2TBX = new System.Windows.Forms.TextBox();
            this.s1TBX = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.sr_FxViewer = new TireDataAnalyzer.UserControls.MultiTireDataViewer();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.simpleTireDataSelector2 = new TireDataAnalyzer.UserControls.SimpleTireDataSelector();
            this.t_tauY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Theta_FySC)).BeginInit();
            this.Theta_FySC.Panel1.SuspendLayout();
            this.Theta_FySC.Panel2.SuspendLayout();
            this.Theta_FySC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testNumTrackBar)).BeginInit();
            this.AdviseBox.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.t_tauX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // t_tauY
            // 
            this.t_tauY.Controls.Add(this.Theta_FySC);
            this.t_tauY.Location = new System.Drawing.Point(4, 4);
            this.t_tauY.Name = "t_tauY";
            this.t_tauY.Padding = new System.Windows.Forms.Padding(3);
            this.t_tauY.Size = new System.Drawing.Size(885, 638);
            this.t_tauY.TabIndex = 0;
            this.t_tauY.Text = "t-σY";
            this.t_tauY.UseVisualStyleBackColor = true;
            // 
            // Theta_FySC
            // 
            this.Theta_FySC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Theta_FySC.Location = new System.Drawing.Point(3, 3);
            this.Theta_FySC.Name = "Theta_FySC";
            // 
            // Theta_FySC.Panel1
            // 
            this.Theta_FySC.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Theta_FySC.Panel1.Controls.Add(this.testMaxLabel);
            this.Theta_FySC.Panel1.Controls.Add(this.testNumTB);
            this.Theta_FySC.Panel1.Controls.Add(this.testNumTrackBar);
            this.Theta_FySC.Panel1.Controls.Add(this.checkBox4);
            this.Theta_FySC.Panel1.Controls.Add(this.checkBox3);
            this.Theta_FySC.Panel1.Controls.Add(this.checkBox2);
            this.Theta_FySC.Panel1.Controls.Add(this.checkBox1);
            this.Theta_FySC.Panel1.Controls.Add(this.simpleTireDataSelector1);
            this.Theta_FySC.Panel1.Controls.Add(this.label11);
            this.Theta_FySC.Panel1.Controls.Add(this.label10);
            this.Theta_FySC.Panel1.Controls.Add(this.label9);
            this.Theta_FySC.Panel1.Controls.Add(this.label8);
            this.Theta_FySC.Panel1.Controls.Add(this.label5);
            this.Theta_FySC.Panel1.Controls.Add(this.label4);
            this.Theta_FySC.Panel1.Controls.Add(this.label3);
            this.Theta_FySC.Panel1.Controls.Add(this.label1);
            this.Theta_FySC.Panel1.Controls.Add(this.s4TB);
            this.Theta_FySC.Panel1.Controls.Add(this.s3TB);
            this.Theta_FySC.Panel1.Controls.Add(this.AdviseBox);
            this.Theta_FySC.Panel1.Controls.Add(this.s2TB);
            this.Theta_FySC.Panel1.Controls.Add(this.s1TB);
            this.Theta_FySC.Panel1.Controls.Add(this.comboBox0);
            this.Theta_FySC.Panel1.Text = "CustomTrackBar Enabled";
            // 
            // Theta_FySC.Panel2
            // 
            this.Theta_FySC.Panel2.Controls.Add(this.Theta_FyViewer);
            this.Theta_FySC.Size = new System.Drawing.Size(879, 632);
            this.Theta_FySC.SplitterDistance = 485;
            this.Theta_FySC.TabIndex = 0;
            // 
            // testMaxLabel
            // 
            this.testMaxLabel.AutoSize = true;
            this.testMaxLabel.Location = new System.Drawing.Point(447, 179);
            this.testMaxLabel.Name = "testMaxLabel";
            this.testMaxLabel.Size = new System.Drawing.Size(35, 12);
            this.testMaxLabel.TabIndex = 77;
            this.testMaxLabel.Text = "/1000";
            // 
            // testNumTB
            // 
            this.testNumTB.Location = new System.Drawing.Point(391, 176);
            this.testNumTB.Name = "testNumTB";
            this.testNumTB.Size = new System.Drawing.Size(50, 19);
            this.testNumTB.TabIndex = 76;
            // 
            // testNumTrackBar
            // 
            this.testNumTrackBar.Location = new System.Drawing.Point(11, 176);
            this.testNumTrackBar.Name = "testNumTrackBar";
            this.testNumTrackBar.Size = new System.Drawing.Size(374, 45);
            this.testNumTrackBar.TabIndex = 75;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(103, 107);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 74;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(103, 82);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 73;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(103, 57);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 72;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(103, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 51;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // simpleTireDataSelector1
            // 
            this.simpleTireDataSelector1.AttachedTable = TTCDataUtils.Table.CorneringTable;
            this.simpleTireDataSelector1.Location = new System.Drawing.Point(3, 230);
            this.simpleTireDataSelector1.MFFD = null;
            this.simpleTireDataSelector1.Name = "simpleTireDataSelector1";
            this.simpleTireDataSelector1.NumSearch = 5000;
            this.simpleTireDataSelector1.PureSlipX = false;
            this.simpleTireDataSelector1.SASREnable = true;
            this.simpleTireDataSelector1.Size = new System.Drawing.Size(479, 200);
            this.simpleTireDataSelector1.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(67, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 47;
            this.label11.Text = "s4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 46;
            this.label10.Text = "s3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(67, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "s2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "s1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "σ-FZ*P";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "σ-P";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "σ-FZ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "Const";
            // 
            // s4TB
            // 
            this.s4TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s4TB.Location = new System.Drawing.Point(361, 104);
            this.s4TB.Name = "s4TB";
            this.s4TB.Size = new System.Drawing.Size(121, 19);
            this.s4TB.TabIndex = 36;
            this.s4TB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s4TB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // s3TB
            // 
            this.s3TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s3TB.Location = new System.Drawing.Point(361, 79);
            this.s3TB.Name = "s3TB";
            this.s3TB.Size = new System.Drawing.Size(121, 19);
            this.s3TB.TabIndex = 35;
            this.s3TB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s3TB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // AdviseBox
            // 
            this.AdviseBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdviseBox.Controls.Add(this.magicFormula_TexEquation0);
            this.AdviseBox.Controls.Add(this.AdviseText0);
            this.AdviseBox.Location = new System.Drawing.Point(3, 436);
            this.AdviseBox.Name = "AdviseBox";
            this.AdviseBox.Size = new System.Drawing.Size(479, 193);
            this.AdviseBox.TabIndex = 34;
            this.AdviseBox.TabStop = false;
            this.AdviseBox.Text = "Advise";
            // 
            // magicFormula_TexEquation0
            // 
            this.magicFormula_TexEquation0.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.magicFormula_TexEquation0.Location = new System.Drawing.Point(3, 90);
            this.magicFormula_TexEquation0.Name = "magicFormula_TexEquation0";
            this.magicFormula_TexEquation0.Size = new System.Drawing.Size(473, 100);
            this.magicFormula_TexEquation0.TabIndex = 40;
            this.magicFormula_TexEquation0.Type = TireDataAnalyzer.TexEquation.MagicFormula_TexEquation.MagicFormulaType.FY;
            // 
            // AdviseText0
            // 
            this.AdviseText0.AutoSize = true;
            this.AdviseText0.Location = new System.Drawing.Point(6, 15);
            this.AdviseText0.Name = "AdviseText0";
            this.AdviseText0.Size = new System.Drawing.Size(471, 72);
            this.AdviseText0.TabIndex = 39;
            this.AdviseText0.Text = resources.GetString("AdviseText0.Text");
            // 
            // s2TB
            // 
            this.s2TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s2TB.Location = new System.Drawing.Point(361, 54);
            this.s2TB.Name = "s2TB";
            this.s2TB.Size = new System.Drawing.Size(121, 19);
            this.s2TB.TabIndex = 33;
            this.s2TB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s2TB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // s1TB
            // 
            this.s1TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s1TB.Location = new System.Drawing.Point(361, 29);
            this.s1TB.Name = "s1TB";
            this.s1TB.Size = new System.Drawing.Size(121, 19);
            this.s1TB.TabIndex = 31;
            this.s1TB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s1TB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // comboBox0
            // 
            this.comboBox0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox0.FormattingEnabled = true;
            this.comboBox0.Items.AddRange(new object[] {
            "すべて描画",
            "100000点",
            "50000点",
            "10000点",
            "5000点",
            "1000点"});
            this.comboBox0.Location = new System.Drawing.Point(361, 3);
            this.comboBox0.Name = "comboBox0";
            this.comboBox0.Size = new System.Drawing.Size(121, 20);
            this.comboBox0.TabIndex = 30;
            this.comboBox0.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // Theta_FyViewer
            // 
            this.Theta_FyViewer.AutoScaleX = true;
            this.Theta_FyViewer.AutoScaleY = true;
            this.Theta_FyViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Theta_FyViewer.Location = new System.Drawing.Point(0, 0);
            this.Theta_FyViewer.Name = "Theta_FyViewer";
            this.Theta_FyViewer.numPoints = 2000;
            this.Theta_FyViewer.PropertyEnable = false;
            this.Theta_FyViewer.ScreenCountEnable = false;
            this.Theta_FyViewer.Size = new System.Drawing.Size(390, 632);
            this.Theta_FyViewer.TabIndex = 0;
            // 
            // TabControl
            // 
            this.TabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.t_tauY);
            this.TabControl.Controls.Add(this.t_tauX);
            this.TabControl.Location = new System.Drawing.Point(4, 4);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(893, 664);
            this.TabControl.TabIndex = 3;
            this.TabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl_Selecting);
            this.TabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Selected);
            this.TabControl.TabIndexChanged += new System.EventHandler(this.TabControl_TabIndexChanged);
            // 
            // t_tauX
            // 
            this.t_tauX.Controls.Add(this.splitContainer1);
            this.t_tauX.Location = new System.Drawing.Point(4, 4);
            this.t_tauX.Name = "t_tauX";
            this.t_tauX.Padding = new System.Windows.Forms.Padding(3);
            this.t_tauX.Size = new System.Drawing.Size(885, 638);
            this.t_tauX.TabIndex = 1;
            this.t_tauX.Text = "t-σX";
            this.t_tauX.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.simpleTireDataSelector2);
            this.splitContainer1.Panel1.Controls.Add(this.label111);
            this.splitContainer1.Panel1.Controls.Add(this.DontUseT_sigmaCB);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox14);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.s4TBX);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox13);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox12);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox11);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.label22);
            this.splitContainer1.Panel1.Controls.Add(this.label23);
            this.splitContainer1.Panel1.Controls.Add(this.label24);
            this.splitContainer1.Panel1.Controls.Add(this.s3TBX);
            this.splitContainer1.Panel1.Controls.Add(this.s2TBX);
            this.splitContainer1.Panel1.Controls.Add(this.s1TBX);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Text = "CustomTrackBar Enabled";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sr_FxViewer);
            this.splitContainer1.Size = new System.Drawing.Size(879, 632);
            this.splitContainer1.SplitterDistance = 485;
            this.splitContainer1.TabIndex = 1;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(4, 6);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(58, 12);
            this.label111.TabIndex = 61;
            this.label111.Text = "使用しない";
            // 
            // DontUseT_sigmaCB
            // 
            this.DontUseT_sigmaCB.AutoSize = true;
            this.DontUseT_sigmaCB.Location = new System.Drawing.Point(90, 6);
            this.DontUseT_sigmaCB.Name = "DontUseT_sigmaCB";
            this.DontUseT_sigmaCB.Size = new System.Drawing.Size(15, 14);
            this.DontUseT_sigmaCB.TabIndex = 60;
            this.DontUseT_sigmaCB.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Location = new System.Drawing.Point(90, 107);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(15, 14);
            this.checkBox14.TabIndex = 59;
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "s4X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 12);
            this.label6.TabIndex = 57;
            this.label6.Text = "σ-FZ*P";
            // 
            // s4TBX
            // 
            this.s4TBX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s4TBX.Location = new System.Drawing.Point(362, 104);
            this.s4TBX.Name = "s4TBX";
            this.s4TBX.Size = new System.Drawing.Size(121, 19);
            this.s4TBX.TabIndex = 56;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.magicFormula_TexEquation1);
            this.groupBox1.Controls.Add(this.AdviseText1);
            this.groupBox1.Location = new System.Drawing.Point(3, 436);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 193);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advise";
            // 
            // magicFormula_TexEquation1
            // 
            this.magicFormula_TexEquation1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.magicFormula_TexEquation1.Location = new System.Drawing.Point(3, 90);
            this.magicFormula_TexEquation1.Name = "magicFormula_TexEquation1";
            this.magicFormula_TexEquation1.Size = new System.Drawing.Size(473, 100);
            this.magicFormula_TexEquation1.TabIndex = 40;
            this.magicFormula_TexEquation1.Type = TireDataAnalyzer.TexEquation.MagicFormula_TexEquation.MagicFormulaType.FY;
            // 
            // AdviseText1
            // 
            this.AdviseText1.AutoSize = true;
            this.AdviseText1.Location = new System.Drawing.Point(6, 15);
            this.AdviseText1.Name = "AdviseText1";
            this.AdviseText1.Size = new System.Drawing.Size(23, 12);
            this.AdviseText1.TabIndex = 39;
            this.AdviseText1.Text = "aaa";
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Location = new System.Drawing.Point(90, 82);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(15, 14);
            this.checkBox13.TabIndex = 54;
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(90, 57);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(15, 14);
            this.checkBox12.TabIndex = 53;
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(90, 32);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(15, 14);
            this.checkBox11.TabIndex = 52;
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(61, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 12);
            this.label16.TabIndex = 46;
            this.label16.Text = "s3X";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(61, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 12);
            this.label17.TabIndex = 45;
            this.label17.Text = "s2X";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(60, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 12);
            this.label18.TabIndex = 44;
            this.label18.Text = "s1X";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(4, 82);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(30, 12);
            this.label22.TabIndex = 40;
            this.label22.Text = "σ-P";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(4, 57);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 12);
            this.label23.TabIndex = 39;
            this.label23.Text = "σ-FZ";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 32);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 12);
            this.label24.TabIndex = 38;
            this.label24.Text = "Const";
            // 
            // s3TBX
            // 
            this.s3TBX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s3TBX.Location = new System.Drawing.Point(362, 79);
            this.s3TBX.Name = "s3TBX";
            this.s3TBX.Size = new System.Drawing.Size(121, 19);
            this.s3TBX.TabIndex = 35;
            this.s3TBX.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s3TBX.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // s2TBX
            // 
            this.s2TBX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s2TBX.Location = new System.Drawing.Point(362, 54);
            this.s2TBX.Name = "s2TBX";
            this.s2TBX.Size = new System.Drawing.Size(121, 19);
            this.s2TBX.TabIndex = 33;
            this.s2TBX.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s2TBX.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // s1TBX
            // 
            this.s1TBX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s1TBX.Location = new System.Drawing.Point(361, 29);
            this.s1TBX.Name = "s1TBX";
            this.s1TBX.Size = new System.Drawing.Size(121, 19);
            this.s1TBX.TabIndex = 31;
            this.s1TBX.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.s1TBX.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "すべて描画",
            "100000点",
            "50000点",
            "10000点",
            "5000点",
            "1000点"});
            this.comboBox1.Location = new System.Drawing.Point(361, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // sr_FxViewer
            // 
            this.sr_FxViewer.AutoScaleX = true;
            this.sr_FxViewer.AutoScaleY = true;
            this.sr_FxViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sr_FxViewer.Location = new System.Drawing.Point(0, 0);
            this.sr_FxViewer.Name = "sr_FxViewer";
            this.sr_FxViewer.numPoints = 2000;
            this.sr_FxViewer.PropertyEnable = false;
            this.sr_FxViewer.ScreenCountEnable = false;
            this.sr_FxViewer.Size = new System.Drawing.Size(390, 632);
            this.sr_FxViewer.TabIndex = 0;
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // simpleTireDataSelector2
            // 
            this.simpleTireDataSelector2.AttachedTable = TTCDataUtils.Table.CorneringTable;
            this.simpleTireDataSelector2.Location = new System.Drawing.Point(3, 216);
            this.simpleTireDataSelector2.MFFD = null;
            this.simpleTireDataSelector2.Name = "simpleTireDataSelector2";
            this.simpleTireDataSelector2.NumSearch = 5000;
            this.simpleTireDataSelector2.PureSlipX = false;
            this.simpleTireDataSelector2.SASREnable = true;
            this.simpleTireDataSelector2.Size = new System.Drawing.Size(479, 200);
            this.simpleTireDataSelector2.TabIndex = 62;
            // 
            // TransientPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "TransientPage";
            this.Load += new System.EventHandler(this.PureCorneringPage_Load);
            this.Controls.SetChildIndex(this.CancelButton, 0);
            this.Controls.SetChildIndex(this.NextButton, 0);
            this.Controls.SetChildIndex(this.PreviousButton, 0);
            this.Controls.SetChildIndex(this.TabControl, 0);
            this.t_tauY.ResumeLayout(false);
            this.Theta_FySC.Panel1.ResumeLayout(false);
            this.Theta_FySC.Panel1.PerformLayout();
            this.Theta_FySC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Theta_FySC)).EndInit();
            this.Theta_FySC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.testNumTrackBar)).EndInit();
            this.AdviseBox.ResumeLayout(false);
            this.AdviseBox.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.t_tauX.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage t_tauY;
        private System.Windows.Forms.SplitContainer Theta_FySC;
        private System.Windows.Forms.TextBox s4TB;
        private System.Windows.Forms.TextBox s3TB;
        private System.Windows.Forms.TextBox s2TB;
        private System.Windows.Forms.TextBox s1TB;
        private System.Windows.Forms.ComboBox comboBox0;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage t_tauX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox AdviseBox;
        private System.Windows.Forms.Label AdviseText0;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox s3TBX;
        private System.Windows.Forms.TextBox s2TBX;
        private System.Windows.Forms.TextBox s1TBX;
        private System.Windows.Forms.ComboBox comboBox1;
        private SimpleTireDataSelector simpleTireDataSelector1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox11;
        private TexEquation.MagicFormula_TexEquation magicFormula_TexEquation0;
        private System.Windows.Forms.GroupBox groupBox1;
        private TexEquation.MagicFormula_TexEquation magicFormula_TexEquation1;
        private System.Windows.Forms.Label AdviseText1;
        private System.Windows.Forms.CheckBox checkBox14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox s4TBX;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.CheckBox DontUseT_sigmaCB;
        private System.Windows.Forms.TrackBar testNumTrackBar;
        private System.Windows.Forms.Label testMaxLabel;
        private System.Windows.Forms.TextBox testNumTB;
        private MultiTireDataViewer sr_FxViewer;
        private MultiTireDataViewer Theta_FyViewer;
        private SimpleTireDataSelector simpleTireDataSelector2;
    }
}
