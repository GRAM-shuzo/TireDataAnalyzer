namespace CustomTrackBar
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.doubleTrackBar1 = new DoubleTrackBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // doubleTrackBar1
            // 
            this.doubleTrackBar1.Location = new System.Drawing.Point(41, 129);
            this.doubleTrackBar1.Max = 100D;
            this.doubleTrackBar1.Min = -100D;
            this.doubleTrackBar1.Name = "doubleTrackBar1";
            this.doubleTrackBar1.NumberTicks = 21;
            this.doubleTrackBar1.Size = new System.Drawing.Size(257, 45);
            this.doubleTrackBar1.TabIndex = 0;
            this.doubleTrackBar1.Text = "doubleTrackBar1";
            this.doubleTrackBar1.valueL = -100D;
            this.doubleTrackBar1.valueR = 100D;
            this.doubleTrackBar1.ValueChanged += new System.EventHandler(this.doubleTrackBar1_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 277);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doubleTrackBar1);
            this.Name = "Form1";
            this.Text = "CustomTrackBar Enabled";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTrackBar.DoubleTrackBar doubleTrackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

