namespace TireDataAnalyzer.UserControls
{
    partial class TireDataSelectorElement
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.MinTrackBar = new System.Windows.Forms.TrackBar();
            this.MaxTrackBar = new System.Windows.Forms.TrackBar();
            this.maxTB = new System.Windows.Forms.TextBox();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.minTB = new System.Windows.Forms.TextBox();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.MinTrackBar);
            this.groupBox.Controls.Add(this.MaxTrackBar);
            this.groupBox.Controls.Add(this.maxTB);
            this.groupBox.Controls.Add(this.labelMax);
            this.groupBox.Controls.Add(this.labelMin);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.minTB);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(360, 110);
            this.groupBox.TabIndex = 40;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "SelectVelocity(km/h)";
            // 
            // MinTrackBar
            // 
            this.MinTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.MinTrackBar.Location = new System.Drawing.Point(6, 64);
            this.MinTrackBar.Name = "MinTrackBar";
            this.MinTrackBar.Size = new System.Drawing.Size(348, 45);
            this.MinTrackBar.TabIndex = 25;
            this.MinTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.MinTrackBar.ValueChanged += new System.EventHandler(this.MinTrackBar_ValueChanged);
            // 
            // MaxTrackBar
            // 
            this.MaxTrackBar.Location = new System.Drawing.Point(6, 43);
            this.MaxTrackBar.Name = "MaxTrackBar";
            this.MaxTrackBar.Size = new System.Drawing.Size(348, 45);
            this.MaxTrackBar.TabIndex = 24;
            this.MaxTrackBar.ValueChanged += new System.EventHandler(this.MaxTrackBar_ValueChanged);
            // 
            // maxTB
            // 
            this.maxTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxTB.Location = new System.Drawing.Point(195, 18);
            this.maxTB.Name = "maxTB";
            this.maxTB.Size = new System.Drawing.Size(100, 19);
            this.maxTB.TabIndex = 23;
            this.maxTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.maxTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.maxTB.Leave += new System.EventHandler(this.TB_Leave);
            // 
            // labelMax
            // 
            this.labelMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(301, 21);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(53, 12);
            this.labelMax.TabIndex = 22;
            this.labelMax.Text = "00000000";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(6, 21);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(53, 12);
            this.labelMin.TabIndex = 21;
            this.labelMin.Text = "00000000";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "-";
            // 
            // minTB
            // 
            this.minTB.Location = new System.Drawing.Point(65, 18);
            this.minTB.Name = "minTB";
            this.minTB.Size = new System.Drawing.Size(100, 19);
            this.minTB.TabIndex = 18;
            this.minTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.minTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.minTB.Leave += new System.EventHandler(this.TB_Leave);
            // 
            // TireDataSelectorElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "TireDataSelectorElement";
            this.Size = new System.Drawing.Size(360, 110);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox minTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.TextBox maxTB;
        private System.Windows.Forms.TrackBar MaxTrackBar;
        private System.Windows.Forms.TrackBar MinTrackBar;
        private System.Windows.Forms.GroupBox groupBox;
    }
}
