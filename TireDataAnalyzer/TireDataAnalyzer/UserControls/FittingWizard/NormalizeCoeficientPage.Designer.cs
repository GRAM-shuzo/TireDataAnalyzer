namespace TireDataAnalyzer.UserControls.FittingWizard
{
    partial class NormalizeCoeficientPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SA_C0 = new System.Windows.Forms.TextBox();
            this.SA_C1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SA_Min = new System.Windows.Forms.Label();
            this.SA_Max = new System.Windows.Forms.Label();
            this.SA_Mean = new System.Windows.Forms.Label();
            this.SR_Mean = new System.Windows.Forms.Label();
            this.SR_Max = new System.Windows.Forms.Label();
            this.SR_Min = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SR_C1 = new System.Windows.Forms.TextBox();
            this.SR_C0 = new System.Windows.Forms.TextBox();
            this.IA_Mean = new System.Windows.Forms.Label();
            this.IA_Max = new System.Windows.Forms.Label();
            this.IA_Min = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.IA_C1 = new System.Windows.Forms.TextBox();
            this.IA_C0 = new System.Windows.Forms.TextBox();
            this.FZ_Mean = new System.Windows.Forms.Label();
            this.FZ_Max = new System.Windows.Forms.Label();
            this.FZ_Min = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.FZ_C1 = new System.Windows.Forms.TextBox();
            this.FZ_C0 = new System.Windows.Forms.TextBox();
            this.P_Mean = new System.Windows.Forms.Label();
            this.P_Max = new System.Windows.Forms.Label();
            this.P_Min = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.P_C1 = new System.Windows.Forms.TextBox();
            this.P_C0 = new System.Windows.Forms.TextBox();
            this.T_Mean = new System.Windows.Forms.Label();
            this.T_Max = new System.Windows.Forms.Label();
            this.T_Min = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.T_C1 = new System.Windows.Forms.TextBox();
            this.T_C0 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.T_MeanN = new System.Windows.Forms.Label();
            this.T_MaxN = new System.Windows.Forms.Label();
            this.T_MinN = new System.Windows.Forms.Label();
            this.P_MeanN = new System.Windows.Forms.Label();
            this.P_MaxN = new System.Windows.Forms.Label();
            this.P_MinN = new System.Windows.Forms.Label();
            this.FZ_MeanN = new System.Windows.Forms.Label();
            this.FZ_MaxN = new System.Windows.Forms.Label();
            this.FZ_MinN = new System.Windows.Forms.Label();
            this.IA_MeanN = new System.Windows.Forms.Label();
            this.IA_MaxN = new System.Windows.Forms.Label();
            this.IA_MinN = new System.Windows.Forms.Label();
            this.SR_MeanN = new System.Windows.Forms.Label();
            this.SR_MaxN = new System.Windows.Forms.Label();
            this.SR_MinN = new System.Windows.Forms.Label();
            this.SA_MeanN = new System.Windows.Forms.Label();
            this.SA_MaxN = new System.Windows.Forms.Label();
            this.SA_MinN = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 552);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(893, 116);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advise";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "各入力変数の正規化係数を与えます。\n入力変数 X（ X∈{SA, FZ...} ）は (X-C0)/C1に正規化（無次元化）されます。\n計算が収束しない場合" +
    "各入力変数が-1～1に収まるように調整することで安定する可能性があります。\nパラメータが大きすぎたり小さすぎる場合これらの係数を変更します。";
            // 
            // SA_C0
            // 
            this.SA_C0.Location = new System.Drawing.Point(153, 46);
            this.SA_C0.Name = "SA_C0";
            this.SA_C0.Size = new System.Drawing.Size(100, 19);
            this.SA_C0.TabIndex = 12;
            this.SA_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SA_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // SA_C1
            // 
            this.SA_C1.Location = new System.Drawing.Point(259, 46);
            this.SA_C1.Name = "SA_C1";
            this.SA_C1.Size = new System.Drawing.Size(100, 19);
            this.SA_C1.TabIndex = 13;
            this.SA_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SA_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Slip Angle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "C0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "C1";
            // 
            // SA_Min
            // 
            this.SA_Min.AutoSize = true;
            this.SA_Min.Location = new System.Drawing.Point(365, 49);
            this.SA_Min.Name = "SA_Min";
            this.SA_Min.Size = new System.Drawing.Size(53, 12);
            this.SA_Min.TabIndex = 17;
            this.SA_Min.Text = "00000000";
            // 
            // SA_Max
            // 
            this.SA_Max.AutoSize = true;
            this.SA_Max.Location = new System.Drawing.Point(424, 49);
            this.SA_Max.Name = "SA_Max";
            this.SA_Max.Size = new System.Drawing.Size(53, 12);
            this.SA_Max.TabIndex = 18;
            this.SA_Max.Text = "00000000";
            // 
            // SA_Mean
            // 
            this.SA_Mean.AutoSize = true;
            this.SA_Mean.Location = new System.Drawing.Point(483, 49);
            this.SA_Mean.Name = "SA_Mean";
            this.SA_Mean.Size = new System.Drawing.Size(53, 12);
            this.SA_Mean.TabIndex = 19;
            this.SA_Mean.Text = "00000000";
            // 
            // SR_Mean
            // 
            this.SR_Mean.AutoSize = true;
            this.SR_Mean.Location = new System.Drawing.Point(483, 74);
            this.SR_Mean.Name = "SR_Mean";
            this.SR_Mean.Size = new System.Drawing.Size(53, 12);
            this.SR_Mean.TabIndex = 25;
            this.SR_Mean.Text = "00000000";
            // 
            // SR_Max
            // 
            this.SR_Max.AutoSize = true;
            this.SR_Max.Location = new System.Drawing.Point(424, 74);
            this.SR_Max.Name = "SR_Max";
            this.SR_Max.Size = new System.Drawing.Size(53, 12);
            this.SR_Max.TabIndex = 24;
            this.SR_Max.Text = "00000000";
            // 
            // SR_Min
            // 
            this.SR_Min.AutoSize = true;
            this.SR_Min.Location = new System.Drawing.Point(365, 74);
            this.SR_Min.Name = "SR_Min";
            this.SR_Min.Size = new System.Drawing.Size(53, 12);
            this.SR_Min.TabIndex = 23;
            this.SR_Min.Text = "00000000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "Slip Ratio";
            // 
            // SR_C1
            // 
            this.SR_C1.Location = new System.Drawing.Point(259, 71);
            this.SR_C1.Name = "SR_C1";
            this.SR_C1.Size = new System.Drawing.Size(100, 19);
            this.SR_C1.TabIndex = 21;
            this.SR_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SR_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // SR_C0
            // 
            this.SR_C0.Location = new System.Drawing.Point(153, 71);
            this.SR_C0.Name = "SR_C0";
            this.SR_C0.Size = new System.Drawing.Size(100, 19);
            this.SR_C0.TabIndex = 20;
            this.SR_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.SR_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // IA_Mean
            // 
            this.IA_Mean.AutoSize = true;
            this.IA_Mean.Location = new System.Drawing.Point(483, 99);
            this.IA_Mean.Name = "IA_Mean";
            this.IA_Mean.Size = new System.Drawing.Size(53, 12);
            this.IA_Mean.TabIndex = 31;
            this.IA_Mean.Text = "00000000";
            // 
            // IA_Max
            // 
            this.IA_Max.AutoSize = true;
            this.IA_Max.Location = new System.Drawing.Point(424, 99);
            this.IA_Max.Name = "IA_Max";
            this.IA_Max.Size = new System.Drawing.Size(53, 12);
            this.IA_Max.TabIndex = 30;
            this.IA_Max.Text = "00000000";
            // 
            // IA_Min
            // 
            this.IA_Min.AutoSize = true;
            this.IA_Min.Location = new System.Drawing.Point(365, 99);
            this.IA_Min.Name = "IA_Min";
            this.IA_Min.Size = new System.Drawing.Size(53, 12);
            this.IA_Min.TabIndex = 29;
            this.IA_Min.Text = "00000000";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "Inclination Angle";
            // 
            // IA_C1
            // 
            this.IA_C1.Location = new System.Drawing.Point(259, 96);
            this.IA_C1.Name = "IA_C1";
            this.IA_C1.Size = new System.Drawing.Size(100, 19);
            this.IA_C1.TabIndex = 27;
            this.IA_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.IA_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // IA_C0
            // 
            this.IA_C0.Location = new System.Drawing.Point(153, 96);
            this.IA_C0.Name = "IA_C0";
            this.IA_C0.Size = new System.Drawing.Size(100, 19);
            this.IA_C0.TabIndex = 26;
            this.IA_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.IA_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // FZ_Mean
            // 
            this.FZ_Mean.AutoSize = true;
            this.FZ_Mean.Location = new System.Drawing.Point(483, 124);
            this.FZ_Mean.Name = "FZ_Mean";
            this.FZ_Mean.Size = new System.Drawing.Size(53, 12);
            this.FZ_Mean.TabIndex = 37;
            this.FZ_Mean.Text = "00000000";
            // 
            // FZ_Max
            // 
            this.FZ_Max.AutoSize = true;
            this.FZ_Max.Location = new System.Drawing.Point(424, 124);
            this.FZ_Max.Name = "FZ_Max";
            this.FZ_Max.Size = new System.Drawing.Size(53, 12);
            this.FZ_Max.TabIndex = 36;
            this.FZ_Max.Text = "00000000";
            // 
            // FZ_Min
            // 
            this.FZ_Min.AutoSize = true;
            this.FZ_Min.Location = new System.Drawing.Point(365, 124);
            this.FZ_Min.Name = "FZ_Min";
            this.FZ_Min.Size = new System.Drawing.Size(53, 12);
            this.FZ_Min.TabIndex = 35;
            this.FZ_Min.Text = "00000000";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 12);
            this.label19.TabIndex = 34;
            this.label19.Text = "Normal Load";
            // 
            // FZ_C1
            // 
            this.FZ_C1.Location = new System.Drawing.Point(259, 121);
            this.FZ_C1.Name = "FZ_C1";
            this.FZ_C1.Size = new System.Drawing.Size(100, 19);
            this.FZ_C1.TabIndex = 33;
            this.FZ_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.FZ_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // FZ_C0
            // 
            this.FZ_C0.Location = new System.Drawing.Point(153, 121);
            this.FZ_C0.Name = "FZ_C0";
            this.FZ_C0.Size = new System.Drawing.Size(100, 19);
            this.FZ_C0.TabIndex = 32;
            this.FZ_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.FZ_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // P_Mean
            // 
            this.P_Mean.AutoSize = true;
            this.P_Mean.Location = new System.Drawing.Point(483, 149);
            this.P_Mean.Name = "P_Mean";
            this.P_Mean.Size = new System.Drawing.Size(53, 12);
            this.P_Mean.TabIndex = 43;
            this.P_Mean.Text = "00000000";
            // 
            // P_Max
            // 
            this.P_Max.AutoSize = true;
            this.P_Max.Location = new System.Drawing.Point(424, 149);
            this.P_Max.Name = "P_Max";
            this.P_Max.Size = new System.Drawing.Size(53, 12);
            this.P_Max.TabIndex = 42;
            this.P_Max.Text = "00000000";
            // 
            // P_Min
            // 
            this.P_Min.AutoSize = true;
            this.P_Min.Location = new System.Drawing.Point(365, 149);
            this.P_Min.Name = "P_Min";
            this.P_Min.Size = new System.Drawing.Size(53, 12);
            this.P_Min.TabIndex = 41;
            this.P_Min.Text = "00000000";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 149);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 12);
            this.label23.TabIndex = 40;
            this.label23.Text = "Pressure";
            // 
            // P_C1
            // 
            this.P_C1.Location = new System.Drawing.Point(259, 146);
            this.P_C1.Name = "P_C1";
            this.P_C1.Size = new System.Drawing.Size(100, 19);
            this.P_C1.TabIndex = 39;
            this.P_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.P_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // P_C0
            // 
            this.P_C0.Location = new System.Drawing.Point(153, 146);
            this.P_C0.Name = "P_C0";
            this.P_C0.Size = new System.Drawing.Size(100, 19);
            this.P_C0.TabIndex = 38;
            this.P_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.P_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // T_Mean
            // 
            this.T_Mean.AutoSize = true;
            this.T_Mean.Location = new System.Drawing.Point(483, 174);
            this.T_Mean.Name = "T_Mean";
            this.T_Mean.Size = new System.Drawing.Size(53, 12);
            this.T_Mean.TabIndex = 49;
            this.T_Mean.Text = "00000000";
            // 
            // T_Max
            // 
            this.T_Max.AutoSize = true;
            this.T_Max.Location = new System.Drawing.Point(424, 174);
            this.T_Max.Name = "T_Max";
            this.T_Max.Size = new System.Drawing.Size(53, 12);
            this.T_Max.TabIndex = 48;
            this.T_Max.Text = "00000000";
            // 
            // T_Min
            // 
            this.T_Min.AutoSize = true;
            this.T_Min.Location = new System.Drawing.Point(365, 174);
            this.T_Min.Name = "T_Min";
            this.T_Min.Size = new System.Drawing.Size(53, 12);
            this.T_Min.TabIndex = 47;
            this.T_Min.Text = "00000000";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(11, 174);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(136, 12);
            this.label27.TabIndex = 46;
            this.label27.Text = "Tire Surface Temperature";
            // 
            // T_C1
            // 
            this.T_C1.Location = new System.Drawing.Point(259, 171);
            this.T_C1.Name = "T_C1";
            this.T_C1.Size = new System.Drawing.Size(100, 19);
            this.T_C1.TabIndex = 45;
            this.T_C1.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.T_C1.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // T_C0
            // 
            this.T_C0.Location = new System.Drawing.Point(153, 171);
            this.T_C0.Name = "T_C0";
            this.T_C0.Size = new System.Drawing.Size(100, 19);
            this.T_C0.TabIndex = 44;
            this.T_C0.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.T_C0.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(382, 31);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(23, 12);
            this.label28.TabIndex = 50;
            this.label28.Text = "Min";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(440, 31);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(26, 12);
            this.label29.TabIndex = 51;
            this.label29.Text = "Max";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(493, 31);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(32, 12);
            this.label30.TabIndex = 52;
            this.label30.Text = "Mean";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(683, 31);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 12);
            this.label31.TabIndex = 73;
            this.label31.Text = "Mean Norm";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(625, 31);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(57, 12);
            this.label32.TabIndex = 72;
            this.label32.Text = "Max Norm";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(565, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(54, 12);
            this.label33.TabIndex = 71;
            this.label33.Text = "Min Norm";
            // 
            // T_MeanN
            // 
            this.T_MeanN.AutoSize = true;
            this.T_MeanN.Location = new System.Drawing.Point(683, 174);
            this.T_MeanN.Name = "T_MeanN";
            this.T_MeanN.Size = new System.Drawing.Size(53, 12);
            this.T_MeanN.TabIndex = 70;
            this.T_MeanN.Text = "00000000";
            // 
            // T_MaxN
            // 
            this.T_MaxN.AutoSize = true;
            this.T_MaxN.Location = new System.Drawing.Point(624, 174);
            this.T_MaxN.Name = "T_MaxN";
            this.T_MaxN.Size = new System.Drawing.Size(53, 12);
            this.T_MaxN.TabIndex = 69;
            this.T_MaxN.Text = "00000000";
            // 
            // T_MinN
            // 
            this.T_MinN.AutoSize = true;
            this.T_MinN.Location = new System.Drawing.Point(565, 174);
            this.T_MinN.Name = "T_MinN";
            this.T_MinN.Size = new System.Drawing.Size(53, 12);
            this.T_MinN.TabIndex = 68;
            this.T_MinN.Text = "00000000";
            // 
            // P_MeanN
            // 
            this.P_MeanN.AutoSize = true;
            this.P_MeanN.Location = new System.Drawing.Point(683, 149);
            this.P_MeanN.Name = "P_MeanN";
            this.P_MeanN.Size = new System.Drawing.Size(53, 12);
            this.P_MeanN.TabIndex = 67;
            this.P_MeanN.Text = "00000000";
            // 
            // P_MaxN
            // 
            this.P_MaxN.AutoSize = true;
            this.P_MaxN.Location = new System.Drawing.Point(624, 149);
            this.P_MaxN.Name = "P_MaxN";
            this.P_MaxN.Size = new System.Drawing.Size(53, 12);
            this.P_MaxN.TabIndex = 66;
            this.P_MaxN.Text = "00000000";
            // 
            // P_MinN
            // 
            this.P_MinN.AutoSize = true;
            this.P_MinN.Location = new System.Drawing.Point(565, 149);
            this.P_MinN.Name = "P_MinN";
            this.P_MinN.Size = new System.Drawing.Size(53, 12);
            this.P_MinN.TabIndex = 65;
            this.P_MinN.Text = "00000000";
            // 
            // FZ_MeanN
            // 
            this.FZ_MeanN.AutoSize = true;
            this.FZ_MeanN.Location = new System.Drawing.Point(683, 124);
            this.FZ_MeanN.Name = "FZ_MeanN";
            this.FZ_MeanN.Size = new System.Drawing.Size(53, 12);
            this.FZ_MeanN.TabIndex = 64;
            this.FZ_MeanN.Text = "00000000";
            // 
            // FZ_MaxN
            // 
            this.FZ_MaxN.AutoSize = true;
            this.FZ_MaxN.Location = new System.Drawing.Point(624, 124);
            this.FZ_MaxN.Name = "FZ_MaxN";
            this.FZ_MaxN.Size = new System.Drawing.Size(53, 12);
            this.FZ_MaxN.TabIndex = 63;
            this.FZ_MaxN.Text = "00000000";
            // 
            // FZ_MinN
            // 
            this.FZ_MinN.AutoSize = true;
            this.FZ_MinN.Location = new System.Drawing.Point(565, 124);
            this.FZ_MinN.Name = "FZ_MinN";
            this.FZ_MinN.Size = new System.Drawing.Size(53, 12);
            this.FZ_MinN.TabIndex = 62;
            this.FZ_MinN.Text = "00000000";
            // 
            // IA_MeanN
            // 
            this.IA_MeanN.AutoSize = true;
            this.IA_MeanN.Location = new System.Drawing.Point(683, 99);
            this.IA_MeanN.Name = "IA_MeanN";
            this.IA_MeanN.Size = new System.Drawing.Size(53, 12);
            this.IA_MeanN.TabIndex = 61;
            this.IA_MeanN.Text = "00000000";
            // 
            // IA_MaxN
            // 
            this.IA_MaxN.AutoSize = true;
            this.IA_MaxN.Location = new System.Drawing.Point(624, 99);
            this.IA_MaxN.Name = "IA_MaxN";
            this.IA_MaxN.Size = new System.Drawing.Size(53, 12);
            this.IA_MaxN.TabIndex = 60;
            this.IA_MaxN.Text = "00000000";
            // 
            // IA_MinN
            // 
            this.IA_MinN.AutoSize = true;
            this.IA_MinN.Location = new System.Drawing.Point(565, 99);
            this.IA_MinN.Name = "IA_MinN";
            this.IA_MinN.Size = new System.Drawing.Size(53, 12);
            this.IA_MinN.TabIndex = 59;
            this.IA_MinN.Text = "00000000";
            // 
            // SR_MeanN
            // 
            this.SR_MeanN.AutoSize = true;
            this.SR_MeanN.Location = new System.Drawing.Point(683, 74);
            this.SR_MeanN.Name = "SR_MeanN";
            this.SR_MeanN.Size = new System.Drawing.Size(53, 12);
            this.SR_MeanN.TabIndex = 58;
            this.SR_MeanN.Text = "00000000";
            // 
            // SR_MaxN
            // 
            this.SR_MaxN.AutoSize = true;
            this.SR_MaxN.Location = new System.Drawing.Point(624, 74);
            this.SR_MaxN.Name = "SR_MaxN";
            this.SR_MaxN.Size = new System.Drawing.Size(53, 12);
            this.SR_MaxN.TabIndex = 57;
            this.SR_MaxN.Text = "00000000";
            // 
            // SR_MinN
            // 
            this.SR_MinN.AutoSize = true;
            this.SR_MinN.Location = new System.Drawing.Point(565, 74);
            this.SR_MinN.Name = "SR_MinN";
            this.SR_MinN.Size = new System.Drawing.Size(53, 12);
            this.SR_MinN.TabIndex = 56;
            this.SR_MinN.Text = "00000000";
            // 
            // SA_MeanN
            // 
            this.SA_MeanN.AutoSize = true;
            this.SA_MeanN.Location = new System.Drawing.Point(683, 49);
            this.SA_MeanN.Name = "SA_MeanN";
            this.SA_MeanN.Size = new System.Drawing.Size(53, 12);
            this.SA_MeanN.TabIndex = 55;
            this.SA_MeanN.Text = "00000000";
            // 
            // SA_MaxN
            // 
            this.SA_MaxN.AutoSize = true;
            this.SA_MaxN.Location = new System.Drawing.Point(624, 49);
            this.SA_MaxN.Name = "SA_MaxN";
            this.SA_MaxN.Size = new System.Drawing.Size(53, 12);
            this.SA_MaxN.TabIndex = 54;
            this.SA_MaxN.Text = "00000000";
            // 
            // SA_MinN
            // 
            this.SA_MinN.AutoSize = true;
            this.SA_MinN.Location = new System.Drawing.Point(565, 49);
            this.SA_MinN.Name = "SA_MinN";
            this.SA_MinN.Size = new System.Drawing.Size(53, 12);
            this.SA_MinN.TabIndex = 53;
            this.SA_MinN.Text = "00000000";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(542, 49);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(17, 12);
            this.label52.TabIndex = 74;
            this.label52.Text = "→";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(542, 74);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(17, 12);
            this.label53.TabIndex = 75;
            this.label53.Text = "→";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(542, 99);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(17, 12);
            this.label54.TabIndex = 76;
            this.label54.Text = "→";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(542, 124);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(17, 12);
            this.label55.TabIndex = 77;
            this.label55.Text = "→";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(542, 149);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(17, 12);
            this.label56.TabIndex = 78;
            this.label56.Text = "→";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(542, 174);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(17, 12);
            this.label57.TabIndex = 79;
            this.label57.Text = "→";
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // NormalizeCoeficientPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label57);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.label53);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.T_MeanN);
            this.Controls.Add(this.T_MaxN);
            this.Controls.Add(this.T_MinN);
            this.Controls.Add(this.P_MeanN);
            this.Controls.Add(this.P_MaxN);
            this.Controls.Add(this.P_MinN);
            this.Controls.Add(this.FZ_MeanN);
            this.Controls.Add(this.FZ_MaxN);
            this.Controls.Add(this.FZ_MinN);
            this.Controls.Add(this.IA_MeanN);
            this.Controls.Add(this.IA_MaxN);
            this.Controls.Add(this.IA_MinN);
            this.Controls.Add(this.SR_MeanN);
            this.Controls.Add(this.SR_MaxN);
            this.Controls.Add(this.SR_MinN);
            this.Controls.Add(this.SA_MeanN);
            this.Controls.Add(this.SA_MaxN);
            this.Controls.Add(this.SA_MinN);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.T_Mean);
            this.Controls.Add(this.T_Max);
            this.Controls.Add(this.T_Min);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.T_C1);
            this.Controls.Add(this.T_C0);
            this.Controls.Add(this.P_Mean);
            this.Controls.Add(this.P_Max);
            this.Controls.Add(this.P_Min);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.P_C1);
            this.Controls.Add(this.P_C0);
            this.Controls.Add(this.FZ_Mean);
            this.Controls.Add(this.FZ_Max);
            this.Controls.Add(this.FZ_Min);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.FZ_C1);
            this.Controls.Add(this.FZ_C0);
            this.Controls.Add(this.IA_Mean);
            this.Controls.Add(this.IA_Max);
            this.Controls.Add(this.IA_Min);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.IA_C1);
            this.Controls.Add(this.IA_C0);
            this.Controls.Add(this.SR_Mean);
            this.Controls.Add(this.SR_Max);
            this.Controls.Add(this.SR_Min);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.SR_C1);
            this.Controls.Add(this.SR_C0);
            this.Controls.Add(this.SA_Mean);
            this.Controls.Add(this.SA_Max);
            this.Controls.Add(this.SA_Min);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SA_C1);
            this.Controls.Add(this.SA_C0);
            this.Controls.Add(this.groupBox1);
            this.Name = "NormalizeCoeficientPage";
            this.Load += new System.EventHandler(this.NormalizeCoeficientPage_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.SA_C0, 0);
            this.Controls.SetChildIndex(this.SA_C1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.SA_Min, 0);
            this.Controls.SetChildIndex(this.SA_Max, 0);
            this.Controls.SetChildIndex(this.SA_Mean, 0);
            this.Controls.SetChildIndex(this.SR_C0, 0);
            this.Controls.SetChildIndex(this.SR_C1, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.SR_Min, 0);
            this.Controls.SetChildIndex(this.SR_Max, 0);
            this.Controls.SetChildIndex(this.SR_Mean, 0);
            this.Controls.SetChildIndex(this.IA_C0, 0);
            this.Controls.SetChildIndex(this.IA_C1, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.IA_Min, 0);
            this.Controls.SetChildIndex(this.IA_Max, 0);
            this.Controls.SetChildIndex(this.IA_Mean, 0);
            this.Controls.SetChildIndex(this.FZ_C0, 0);
            this.Controls.SetChildIndex(this.FZ_C1, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.FZ_Min, 0);
            this.Controls.SetChildIndex(this.FZ_Max, 0);
            this.Controls.SetChildIndex(this.FZ_Mean, 0);
            this.Controls.SetChildIndex(this.P_C0, 0);
            this.Controls.SetChildIndex(this.P_C1, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.P_Min, 0);
            this.Controls.SetChildIndex(this.P_Max, 0);
            this.Controls.SetChildIndex(this.P_Mean, 0);
            this.Controls.SetChildIndex(this.T_C0, 0);
            this.Controls.SetChildIndex(this.T_C1, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.T_Min, 0);
            this.Controls.SetChildIndex(this.T_Max, 0);
            this.Controls.SetChildIndex(this.T_Mean, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.SA_MinN, 0);
            this.Controls.SetChildIndex(this.SA_MaxN, 0);
            this.Controls.SetChildIndex(this.SA_MeanN, 0);
            this.Controls.SetChildIndex(this.SR_MinN, 0);
            this.Controls.SetChildIndex(this.SR_MaxN, 0);
            this.Controls.SetChildIndex(this.SR_MeanN, 0);
            this.Controls.SetChildIndex(this.IA_MinN, 0);
            this.Controls.SetChildIndex(this.IA_MaxN, 0);
            this.Controls.SetChildIndex(this.IA_MeanN, 0);
            this.Controls.SetChildIndex(this.FZ_MinN, 0);
            this.Controls.SetChildIndex(this.FZ_MaxN, 0);
            this.Controls.SetChildIndex(this.FZ_MeanN, 0);
            this.Controls.SetChildIndex(this.P_MinN, 0);
            this.Controls.SetChildIndex(this.P_MaxN, 0);
            this.Controls.SetChildIndex(this.P_MeanN, 0);
            this.Controls.SetChildIndex(this.T_MinN, 0);
            this.Controls.SetChildIndex(this.T_MaxN, 0);
            this.Controls.SetChildIndex(this.T_MeanN, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.label52, 0);
            this.Controls.SetChildIndex(this.label53, 0);
            this.Controls.SetChildIndex(this.label54, 0);
            this.Controls.SetChildIndex(this.label55, 0);
            this.Controls.SetChildIndex(this.label56, 0);
            this.Controls.SetChildIndex(this.label57, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SA_C0;
        private System.Windows.Forms.TextBox SA_C1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label SA_Min;
        private System.Windows.Forms.Label SA_Max;
        private System.Windows.Forms.Label SA_Mean;
        private System.Windows.Forms.Label SR_Mean;
        private System.Windows.Forms.Label SR_Max;
        private System.Windows.Forms.Label SR_Min;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox SR_C1;
        private System.Windows.Forms.TextBox SR_C0;
        private System.Windows.Forms.Label IA_Mean;
        private System.Windows.Forms.Label IA_Max;
        private System.Windows.Forms.Label IA_Min;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox IA_C1;
        private System.Windows.Forms.TextBox IA_C0;
        private System.Windows.Forms.Label FZ_Mean;
        private System.Windows.Forms.Label FZ_Max;
        private System.Windows.Forms.Label FZ_Min;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox FZ_C1;
        private System.Windows.Forms.TextBox FZ_C0;
        private System.Windows.Forms.Label P_Mean;
        private System.Windows.Forms.Label P_Max;
        private System.Windows.Forms.Label P_Min;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox P_C1;
        private System.Windows.Forms.TextBox P_C0;
        private System.Windows.Forms.Label T_Mean;
        private System.Windows.Forms.Label T_Max;
        private System.Windows.Forms.Label T_Min;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox T_C1;
        private System.Windows.Forms.TextBox T_C0;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label T_MeanN;
        private System.Windows.Forms.Label T_MaxN;
        private System.Windows.Forms.Label T_MinN;
        private System.Windows.Forms.Label P_MeanN;
        private System.Windows.Forms.Label P_MaxN;
        private System.Windows.Forms.Label P_MinN;
        private System.Windows.Forms.Label FZ_MeanN;
        private System.Windows.Forms.Label FZ_MaxN;
        private System.Windows.Forms.Label FZ_MinN;
        private System.Windows.Forms.Label IA_MeanN;
        private System.Windows.Forms.Label IA_MaxN;
        private System.Windows.Forms.Label IA_MinN;
        private System.Windows.Forms.Label SR_MeanN;
        private System.Windows.Forms.Label SR_MaxN;
        private System.Windows.Forms.Label SR_MinN;
        private System.Windows.Forms.Label SA_MeanN;
        private System.Windows.Forms.Label SA_MaxN;
        private System.Windows.Forms.Label SA_MinN;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
    }
}
