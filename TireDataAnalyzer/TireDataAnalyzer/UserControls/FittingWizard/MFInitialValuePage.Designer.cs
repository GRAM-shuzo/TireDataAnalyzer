namespace TireDataAnalyzer.UserControls.FittingWizard
{
    partial class MFInitialValuePage
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
            this.LoadDefaultRB = new System.Windows.Forms.RadioButton();
            this.LoadFromFileRB = new System.Windows.Forms.RadioButton();
            this.LoadFromOtherRB = new System.Windows.Forms.RadioButton();
            this.FileNameTB = new System.Windows.Forms.TextBox();
            this.MFLoadedCB = new System.Windows.Forms.ComboBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NowValueRB = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadDefaultRB
            // 
            this.LoadDefaultRB.AutoSize = true;
            this.LoadDefaultRB.Checked = true;
            this.LoadDefaultRB.Location = new System.Drawing.Point(3, 3);
            this.LoadDefaultRB.Name = "LoadDefaultRB";
            this.LoadDefaultRB.Size = new System.Drawing.Size(113, 16);
            this.LoadDefaultRB.TabIndex = 4;
            this.LoadDefaultRB.TabStop = true;
            this.LoadDefaultRB.Text = "デフォルト値の使用";
            this.LoadDefaultRB.UseVisualStyleBackColor = true;
            // 
            // LoadFromFileRB
            // 
            this.LoadFromFileRB.AutoSize = true;
            this.LoadFromFileRB.Location = new System.Drawing.Point(3, 28);
            this.LoadFromFileRB.Name = "LoadFromFileRB";
            this.LoadFromFileRB.Size = new System.Drawing.Size(121, 16);
            this.LoadFromFileRB.TabIndex = 5;
            this.LoadFromFileRB.Text = "ファイルから読み込み";
            this.LoadFromFileRB.UseVisualStyleBackColor = true;
            this.LoadFromFileRB.CheckedChanged += new System.EventHandler(this.LoadFromOtherRB_CheckedChanged);
            // 
            // LoadFromOtherRB
            // 
            this.LoadFromOtherRB.AutoSize = true;
            this.LoadFromOtherRB.Location = new System.Drawing.Point(3, 53);
            this.LoadFromOtherRB.Name = "LoadFromOtherRB";
            this.LoadFromOtherRB.Size = new System.Drawing.Size(112, 16);
            this.LoadFromOtherRB.TabIndex = 6;
            this.LoadFromOtherRB.Text = "次の値を読み込み";
            this.LoadFromOtherRB.UseVisualStyleBackColor = true;
            this.LoadFromOtherRB.CheckedChanged += new System.EventHandler(this.LoadFromOtherRB_CheckedChanged);
            // 
            // FileNameTB
            // 
            this.FileNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileNameTB.Location = new System.Drawing.Point(130, 27);
            this.FileNameTB.Name = "FileNameTB";
            this.FileNameTB.Size = new System.Drawing.Size(686, 19);
            this.FileNameTB.TabIndex = 7;
            // 
            // MFLoadedCB
            // 
            this.MFLoadedCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MFLoadedCB.FormattingEnabled = true;
            this.MFLoadedCB.Location = new System.Drawing.Point(130, 52);
            this.MFLoadedCB.Name = "MFLoadedCB";
            this.MFLoadedCB.Size = new System.Drawing.Size(767, 20);
            this.MFLoadedCB.TabIndex = 8;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.Location = new System.Drawing.Point(822, 25);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 9;
            this.BrowseButton.Text = "参照";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 552);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(893, 116);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advise";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "フィッティング開始時の各パラメータの初期値を読み込みます。\n（これらは次に続くページで修正できます）";
            // 
            // NowValueRB
            // 
            this.NowValueRB.AutoSize = true;
            this.NowValueRB.Location = new System.Drawing.Point(3, 75);
            this.NowValueRB.Name = "NowValueRB";
            this.NowValueRB.Size = new System.Drawing.Size(103, 16);
            this.NowValueRB.TabIndex = 11;
            this.NowValueRB.Text = "現在の値の使用";
            this.NowValueRB.UseVisualStyleBackColor = true;
            // 
            // MFInitialValuePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NowValueRB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.MFLoadedCB);
            this.Controls.Add(this.FileNameTB);
            this.Controls.Add(this.LoadFromOtherRB);
            this.Controls.Add(this.LoadFromFileRB);
            this.Controls.Add(this.LoadDefaultRB);
            this.Name = "MFInitialValuePage";
            this.Load += new System.EventHandler(this.MFInitialValuePage_Load);
            this.Controls.SetChildIndex(this.CancelButton, 0);
            this.Controls.SetChildIndex(this.NextButton, 0);
            this.Controls.SetChildIndex(this.PreviousButton, 0);
            this.Controls.SetChildIndex(this.LoadDefaultRB, 0);
            this.Controls.SetChildIndex(this.LoadFromFileRB, 0);
            this.Controls.SetChildIndex(this.LoadFromOtherRB, 0);
            this.Controls.SetChildIndex(this.FileNameTB, 0);
            this.Controls.SetChildIndex(this.MFLoadedCB, 0);
            this.Controls.SetChildIndex(this.BrowseButton, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.NowValueRB, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton LoadDefaultRB;
        private System.Windows.Forms.RadioButton LoadFromFileRB;
        private System.Windows.Forms.RadioButton LoadFromOtherRB;
        private System.Windows.Forms.TextBox FileNameTB;
        private System.Windows.Forms.ComboBox MFLoadedCB;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton NowValueRB;
    }
}
