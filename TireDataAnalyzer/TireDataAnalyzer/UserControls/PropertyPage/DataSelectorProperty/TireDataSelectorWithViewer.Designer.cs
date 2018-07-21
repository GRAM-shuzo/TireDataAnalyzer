namespace TireDataAnalyzer.UserControls.PropertyPage
{
    partial class TireDataSelectorWithViewer
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
            this.corneringSplitContainer = new System.Windows.Forms.SplitContainer();
            this.Selector = new TireDataAnalyzer.UserControls.TireDataSelector();
            this.DataViewer = new TireDataAnalyzer.UserControls.MultiTireDataViewer();
            ((System.ComponentModel.ISupportInitialize)(this.corneringSplitContainer)).BeginInit();
            this.corneringSplitContainer.Panel1.SuspendLayout();
            this.corneringSplitContainer.Panel2.SuspendLayout();
            this.corneringSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // corneringSplitContainer
            // 
            this.corneringSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.corneringSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.corneringSplitContainer.Name = "corneringSplitContainer";
            // 
            // corneringSplitContainer.Panel1
            // 
            this.corneringSplitContainer.Panel1.AutoScroll = true;
            this.corneringSplitContainer.Panel1.Controls.Add(this.Selector);
            // 
            // corneringSplitContainer.Panel2
            // 
            this.corneringSplitContainer.Panel2.AutoScroll = true;
            this.corneringSplitContainer.Panel2.Controls.Add(this.DataViewer);
            this.corneringSplitContainer.Size = new System.Drawing.Size(1025, 636);
            this.corneringSplitContainer.SplitterDistance = 278;
            this.corneringSplitContainer.TabIndex = 33;
            // 
            // Selector
            // 
            this.Selector.AutoScroll = true;
            this.Selector.BackColor = System.Drawing.SystemColors.Control;
            this.Selector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Selector.Location = new System.Drawing.Point(0, 0);
            this.Selector.Name = "Selector";
            this.Selector.Size = new System.Drawing.Size(278, 636);
            this.Selector.TabIndex = 1;
            // 
            // DataViewer
            // 
            this.DataViewer.AutoScaleX = true;
            this.DataViewer.AutoScaleY = true;
            this.DataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataViewer.Location = new System.Drawing.Point(0, 0);
            this.DataViewer.Name = "DataViewer";
            this.DataViewer.numPoints = 2000;
            this.DataViewer.PropertyEnable = false;
            this.DataViewer.ScreenCountEnable = true;
            this.DataViewer.Size = new System.Drawing.Size(743, 636);
            this.DataViewer.TabIndex = 0;
            // 
            // TireDataSelectorWithViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.corneringSplitContainer);
            this.Name = "TireDataSelectorWithViewer";
            this.Size = new System.Drawing.Size(1025, 636);
            this.corneringSplitContainer.Panel1.ResumeLayout(false);
            this.corneringSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.corneringSplitContainer)).EndInit();
            this.corneringSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer corneringSplitContainer;
        private TireDataSelector Selector;
        private MultiTireDataViewer DataViewer;
    }
}
