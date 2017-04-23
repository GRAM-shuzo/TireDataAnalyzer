namespace TireDataAnalyzer.UserControls.FittingWizard
{
    partial class SolverSettingPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MethodDescription = new System.Windows.Forms.Label();
            this.NLoptRB = new System.Windows.Forms.RadioButton();
            this.LMMethodRB = new System.Windows.Forms.RadioButton();
            this.NoFittingRB = new System.Windows.Forms.RadioButton();
            this.NloptAlgorithmCB = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelfAligningTorqueCB = new System.Windows.Forms.CheckBox();
            this.CombinedFYCB = new System.Windows.Forms.CheckBox();
            this.CombinedFXCB = new System.Windows.Forms.CheckBox();
            this.PureFYCB = new System.Windows.Forms.CheckBox();
            this.PureFXCB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MaxDataTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.XtolTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxEvalTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.MethodDescription);
            this.groupBox1.Controls.Add(this.NLoptRB);
            this.groupBox1.Controls.Add(this.LMMethodRB);
            this.groupBox1.Controls.Add(this.NoFittingRB);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(893, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solver";
            // 
            // MethodDescription
            // 
            this.MethodDescription.AutoSize = true;
            this.MethodDescription.Location = new System.Drawing.Point(6, 112);
            this.MethodDescription.Name = "MethodDescription";
            this.MethodDescription.Size = new System.Drawing.Size(486, 12);
            this.MethodDescription.TabIndex = 4;
            this.MethodDescription.Text = "最適化ライブラリのNLOptを使用します。最適化ソルバは以下から下のコンボボックスから選択してください";
            // 
            // NLoptRB
            // 
            this.NLoptRB.AutoSize = true;
            this.NLoptRB.Location = new System.Drawing.Point(7, 63);
            this.NLoptRB.Name = "NLoptRB";
            this.NLoptRB.Size = new System.Drawing.Size(53, 16);
            this.NLoptRB.TabIndex = 2;
            this.NLoptRB.Text = "NLopt";
            this.NLoptRB.UseVisualStyleBackColor = true;
            this.NLoptRB.CheckedChanged += new System.EventHandler(this.FittingSolver_CheckedChanged);
            // 
            // LMMethodRB
            // 
            this.LMMethodRB.AutoSize = true;
            this.LMMethodRB.Location = new System.Drawing.Point(7, 41);
            this.LMMethodRB.Name = "LMMethodRB";
            this.LMMethodRB.Size = new System.Drawing.Size(237, 16);
            this.LMMethodRB.TabIndex = 1;
            this.LMMethodRB.Text = "Levenberg-Marquardt  Method with SUMT";
            this.LMMethodRB.UseVisualStyleBackColor = true;
            this.LMMethodRB.CheckedChanged += new System.EventHandler(this.FittingSolver_CheckedChanged);
            // 
            // NoFittingRB
            // 
            this.NoFittingRB.AutoSize = true;
            this.NoFittingRB.Checked = true;
            this.NoFittingRB.Location = new System.Drawing.Point(7, 19);
            this.NoFittingRB.Name = "NoFittingRB";
            this.NoFittingRB.Size = new System.Drawing.Size(147, 16);
            this.NoFittingRB.TabIndex = 0;
            this.NoFittingRB.TabStop = true;
            this.NoFittingRB.Text = "Not Using Fitting Solver";
            this.NoFittingRB.UseVisualStyleBackColor = true;
            this.NoFittingRB.CheckedChanged += new System.EventHandler(this.FittingSolver_CheckedChanged);
            // 
            // NloptAlgorithmCB
            // 
            this.NloptAlgorithmCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NloptAlgorithmCB.FormattingEnabled = true;
            this.NloptAlgorithmCB.Location = new System.Drawing.Point(660, 18);
            this.NloptAlgorithmCB.Name = "NloptAlgorithmCB";
            this.NloptAlgorithmCB.Size = new System.Drawing.Size(227, 20);
            this.NloptAlgorithmCB.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SelfAligningTorqueCB);
            this.groupBox2.Controls.Add(this.CombinedFYCB);
            this.groupBox2.Controls.Add(this.CombinedFXCB);
            this.groupBox2.Controls.Add(this.PureFYCB);
            this.groupBox2.Controls.Add(this.PureFXCB);
            this.groupBox2.Location = new System.Drawing.Point(4, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(893, 182);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Formula requiring fitting";
            // 
            // SelfAligningTorqueCB
            // 
            this.SelfAligningTorqueCB.AutoSize = true;
            this.SelfAligningTorqueCB.Enabled = false;
            this.SelfAligningTorqueCB.Location = new System.Drawing.Point(6, 106);
            this.SelfAligningTorqueCB.Name = "SelfAligningTorqueCB";
            this.SelfAligningTorqueCB.Size = new System.Drawing.Size(128, 16);
            this.SelfAligningTorqueCB.TabIndex = 4;
            this.SelfAligningTorqueCB.Text = "Self Aligning Torque";
            this.SelfAligningTorqueCB.UseVisualStyleBackColor = true;
            this.SelfAligningTorqueCB.CheckedChanged += new System.EventHandler(this.FormulaCheckedChanged);
            // 
            // CombinedFYCB
            // 
            this.CombinedFYCB.AutoSize = true;
            this.CombinedFYCB.Location = new System.Drawing.Point(6, 84);
            this.CombinedFYCB.Name = "CombinedFYCB";
            this.CombinedFYCB.Size = new System.Drawing.Size(88, 16);
            this.CombinedFYCB.TabIndex = 3;
            this.CombinedFYCB.Text = "CombinedFY";
            this.CombinedFYCB.UseVisualStyleBackColor = true;
            this.CombinedFYCB.CheckedChanged += new System.EventHandler(this.FormulaCheckedChanged);
            // 
            // CombinedFXCB
            // 
            this.CombinedFXCB.AutoSize = true;
            this.CombinedFXCB.Location = new System.Drawing.Point(6, 62);
            this.CombinedFXCB.Name = "CombinedFXCB";
            this.CombinedFXCB.Size = new System.Drawing.Size(88, 16);
            this.CombinedFXCB.TabIndex = 2;
            this.CombinedFXCB.Text = "CombinedFX";
            this.CombinedFXCB.UseVisualStyleBackColor = true;
            this.CombinedFXCB.CheckedChanged += new System.EventHandler(this.FormulaCheckedChanged);
            // 
            // PureFYCB
            // 
            this.PureFYCB.AutoSize = true;
            this.PureFYCB.Location = new System.Drawing.Point(6, 40);
            this.PureFYCB.Name = "PureFYCB";
            this.PureFYCB.Size = new System.Drawing.Size(61, 16);
            this.PureFYCB.TabIndex = 1;
            this.PureFYCB.Text = "PureFY";
            this.PureFYCB.UseVisualStyleBackColor = true;
            this.PureFYCB.CheckedChanged += new System.EventHandler(this.FormulaCheckedChanged);
            // 
            // PureFXCB
            // 
            this.PureFXCB.AutoSize = true;
            this.PureFXCB.Location = new System.Drawing.Point(6, 18);
            this.PureFXCB.Name = "PureFXCB";
            this.PureFXCB.Size = new System.Drawing.Size(61, 16);
            this.PureFXCB.TabIndex = 0;
            this.PureFXCB.Text = "PureFX";
            this.PureFXCB.UseVisualStyleBackColor = true;
            this.PureFXCB.CheckedChanged += new System.EventHandler(this.FormulaCheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.MaxDataTB);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.XtolTB);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.MaxEvalTB);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.NloptAlgorithmCB);
            this.groupBox3.Location = new System.Drawing.Point(4, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(893, 342);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SolverSetting";
            // 
            // MaxDataTB
            // 
            this.MaxDataTB.Location = new System.Drawing.Point(95, 68);
            this.MaxDataTB.Name = "MaxDataTB";
            this.MaxDataTB.Size = new System.Drawing.Size(100, 19);
            this.MaxDataTB.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "MaxDataUsage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(587, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "NLoptSolver";
            // 
            // XtolTB
            // 
            this.XtolTB.Location = new System.Drawing.Point(95, 43);
            this.XtolTB.Name = "XtolTB";
            this.XtolTB.Size = new System.Drawing.Size(100, 19);
            this.XtolTB.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Xtol";
            // 
            // MaxEvalTB
            // 
            this.MaxEvalTB.Location = new System.Drawing.Point(95, 18);
            this.MaxEvalTB.Name = "MaxEvalTB";
            this.MaxEvalTB.Size = new System.Drawing.Size(100, 19);
            this.MaxEvalTB.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Max Evaluation";
            // 
            // SolverSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SolverSettingPage";
            this.Controls.SetChildIndex(this.CancelButton, 0);
            this.Controls.SetChildIndex(this.NextButton, 0);
            this.Controls.SetChildIndex(this.PreviousButton, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton NoFittingRB;
        private System.Windows.Forms.Label MethodDescription;
        private System.Windows.Forms.ComboBox NloptAlgorithmCB;
        private System.Windows.Forms.RadioButton NLoptRB;
        private System.Windows.Forms.RadioButton LMMethodRB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox SelfAligningTorqueCB;
        private System.Windows.Forms.CheckBox CombinedFYCB;
        private System.Windows.Forms.CheckBox CombinedFXCB;
        private System.Windows.Forms.CheckBox PureFYCB;
        private System.Windows.Forms.CheckBox PureFXCB;
        private System.Windows.Forms.TextBox XtolTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MaxEvalTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MaxDataTB;
    }
}
