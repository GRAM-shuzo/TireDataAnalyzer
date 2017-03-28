namespace TireDataAnalyzer.UserControls.FittingWizard
{
    partial class ProgressPage
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
            this.CountBar = new System.Windows.Forms.ProgressBar();
            this.StageBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.StageTB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CountTB = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ErrorBar = new System.Windows.Forms.ProgressBar();
            this.ErrorTB = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CountBar
            // 
            this.CountBar.Location = new System.Drawing.Point(0, 196);
            this.CountBar.Maximum = 200;
            this.CountBar.Name = "CountBar";
            this.CountBar.Size = new System.Drawing.Size(894, 23);
            this.CountBar.TabIndex = 3;
            this.CountBar.Value = 5;
            // 
            // StageBar
            // 
            this.StageBar.Location = new System.Drawing.Point(3, 394);
            this.StageBar.Maximum = 7;
            this.StageBar.Name = "StageBar";
            this.StageBar.Size = new System.Drawing.Size(894, 23);
            this.StageBar.TabIndex = 4;
            this.StageBar.Value = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Stage";
            // 
            // StageTB
            // 
            this.StageTB.AutoSize = true;
            this.StageTB.Location = new System.Drawing.Point(44, 379);
            this.StageTB.Name = "StageTB";
            this.StageTB.Size = new System.Drawing.Size(11, 12);
            this.StageTB.TabIndex = 6;
            this.StageTB.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "/  7";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 10;
            // 
            // CountTB
            // 
            this.CountTB.AutoSize = true;
            this.CountTB.Location = new System.Drawing.Point(44, 181);
            this.CountTB.Name = "CountTB";
            this.CountTB.Size = new System.Drawing.Size(11, 12);
            this.CountTB.TabIndex = 9;
            this.CountTB.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Count";
            // 
            // ErrorBar
            // 
            this.ErrorBar.Location = new System.Drawing.Point(0, 251);
            this.ErrorBar.Name = "ErrorBar";
            this.ErrorBar.Size = new System.Drawing.Size(894, 23);
            this.ErrorBar.TabIndex = 11;
            this.ErrorBar.Value = 5;
            // 
            // ErrorTB
            // 
            this.ErrorTB.AutoSize = true;
            this.ErrorTB.Location = new System.Drawing.Point(44, 236);
            this.ErrorTB.Name = "ErrorTB";
            this.ErrorTB.Size = new System.Drawing.Size(11, 12);
            this.ErrorTB.TabIndex = 13;
            this.ErrorTB.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "Error";
            // 
            // ProgressPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ErrorTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ErrorBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CountTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StageTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StageBar);
            this.Controls.Add(this.CountBar);
            this.Name = "ProgressPage";
            this.Controls.SetChildIndex(this.CountBar, 0);
            this.Controls.SetChildIndex(this.StageBar, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.StageTB, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.CountTB, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ErrorBar, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.ErrorTB, 0);
            this.Controls.SetChildIndex(this.CancelButton, 0);
            this.Controls.SetChildIndex(this.NextButton, 0);
            this.Controls.SetChildIndex(this.PreviousButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar CountBar;
        private System.Windows.Forms.ProgressBar StageBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StageTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CountTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar ErrorBar;
        private System.Windows.Forms.Label ErrorTB;
        private System.Windows.Forms.Label label8;
    }
}
