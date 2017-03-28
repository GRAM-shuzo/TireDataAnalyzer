namespace TireDataAnalyzer.UserControls
{
    partial class TireDataSelector
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.SelectorTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // SelectorTreeView
            // 
            this.SelectorTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectorTreeView.LabelEdit = true;
            this.SelectorTreeView.Location = new System.Drawing.Point(0, 0);
            this.SelectorTreeView.Name = "SelectorTreeView";
            this.SelectorTreeView.Size = new System.Drawing.Size(278, 494);
            this.SelectorTreeView.TabIndex = 0;
            // 
            // TireDataSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SelectorTreeView);
            this.Name = "TireDataSelector";
            this.Size = new System.Drawing.Size(278, 494);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SelectorTreeView;
    }
}
