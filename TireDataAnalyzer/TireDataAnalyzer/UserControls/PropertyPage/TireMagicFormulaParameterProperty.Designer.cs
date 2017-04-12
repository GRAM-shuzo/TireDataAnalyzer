namespace TireDataAnalyzer.UserControls.PropertyPage
{
    partial class TireMagicFormulaParameterProperty
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
            this.GraphTabControl = new System.Windows.Forms.CustomTabControl();
            this.DriveAccelPage = new System.Windows.Forms.TabPage();
            this.TransientPage = new System.Windows.Forms.TabPage();
            this.CorneringPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ToSolver0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ParameterName0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.multiTireDataViewer1 = new TireDataAnalyzer.UserControls.MultiTireDataViewer();
            this.GraphTabControl.SuspendLayout();
            this.CorneringPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GraphTabControl
            // 
            this.GraphTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.GraphTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphTabControl.Controls.Add(this.CorneringPage);
            this.GraphTabControl.Controls.Add(this.DriveAccelPage);
            this.GraphTabControl.Controls.Add(this.TransientPage);
            this.GraphTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Angled;
            // 
            // 
            // 
            this.GraphTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.GraphTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.GraphTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.GraphTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.GraphTabControl.DisplayStyleProvider.FocusTrack = false;
            this.GraphTabControl.DisplayStyleProvider.HotTrack = true;
            this.GraphTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GraphTabControl.DisplayStyleProvider.Opacity = 1F;
            this.GraphTabControl.DisplayStyleProvider.Overlap = 7;
            this.GraphTabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(10, 3);
            this.GraphTabControl.DisplayStyleProvider.Radius = 10;
            this.GraphTabControl.DisplayStyleProvider.ShowTabCloser = false;
            this.GraphTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.GraphTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.GraphTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.GraphTabControl.HotTrack = true;
            this.GraphTabControl.Location = new System.Drawing.Point(3, 3);
            this.GraphTabControl.Name = "GraphTabControl";
            this.GraphTabControl.SelectedIndex = 0;
            this.GraphTabControl.Size = new System.Drawing.Size(892, 665);
            this.GraphTabControl.TabIndex = 28;
            // 
            // DriveAccelPage
            // 
            this.DriveAccelPage.BackColor = System.Drawing.Color.White;
            this.DriveAccelPage.Location = new System.Drawing.Point(4, 4);
            this.DriveAccelPage.Name = "DriveAccelPage";
            this.DriveAccelPage.Padding = new System.Windows.Forms.Padding(3);
            this.DriveAccelPage.Size = new System.Drawing.Size(884, 638);
            this.DriveAccelPage.TabIndex = 1;
            this.DriveAccelPage.Text = "加減速/CombinedSlip";
            // 
            // TransientPage
            // 
            this.TransientPage.BackColor = System.Drawing.Color.White;
            this.TransientPage.Location = new System.Drawing.Point(4, 4);
            this.TransientPage.Name = "TransientPage";
            this.TransientPage.Padding = new System.Windows.Forms.Padding(3);
            this.TransientPage.Size = new System.Drawing.Size(884, 638);
            this.TransientPage.TabIndex = 3;
            this.TransientPage.Text = "過渡特性";
            // 
            // CorneringPage
            // 
            this.CorneringPage.BackColor = System.Drawing.Color.White;
            this.CorneringPage.Controls.Add(this.splitContainer1);
            this.CorneringPage.Location = new System.Drawing.Point(4, 4);
            this.CorneringPage.Name = "CorneringPage";
            this.CorneringPage.Padding = new System.Windows.Forms.Padding(3);
            this.CorneringPage.Size = new System.Drawing.Size(884, 638);
            this.CorneringPage.TabIndex = 0;
            this.CorneringPage.Text = "PureSlip - コーナリング";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(878, 632);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ToSolver0,
            this.ParameterName0,
            this.Value0});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(292, 632);
            this.dataGridView1.TabIndex = 0;
            // 
            // ToSolver0
            // 
            this.ToSolver0.HeaderText = "To Solver";
            this.ToSolver0.MinimumWidth = 60;
            this.ToSolver0.Name = "ToSolver0";
            this.ToSolver0.Width = 60;
            // 
            // ParameterName0
            // 
            this.ParameterName0.HeaderText = "ParameterName";
            this.ParameterName0.Name = "ParameterName0";
            this.ParameterName0.ReadOnly = true;
            // 
            // Value0
            // 
            this.Value0.HeaderText = "Value";
            this.Value0.Name = "Value0";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.multiTireDataViewer1);
            this.splitContainer2.Size = new System.Drawing.Size(582, 632);
            this.splitContainer2.SplitterDistance = 440;
            this.splitContainer2.TabIndex = 0;
            // 
            // multiTireDataViewer1
            // 
            this.multiTireDataViewer1.AutoScaleX = true;
            this.multiTireDataViewer1.AutoScaleY = true;
            this.multiTireDataViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiTireDataViewer1.Location = new System.Drawing.Point(0, 0);
            this.multiTireDataViewer1.Name = "multiTireDataViewer1";
            this.multiTireDataViewer1.Size = new System.Drawing.Size(582, 440);
            this.multiTireDataViewer1.TabIndex = 0;
            // 
            // TireMagicFormulaParameterProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GraphTabControl);
            this.Name = "TireMagicFormulaParameterProperty";
            this.Controls.SetChildIndex(this.GraphTabControl, 0);
            this.GraphTabControl.ResumeLayout(false);
            this.CorneringPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomTabControl GraphTabControl;
        private System.Windows.Forms.TabPage DriveAccelPage;
        private System.Windows.Forms.TabPage TransientPage;
        private System.Windows.Forms.TabPage CorneringPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToSolver0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterName0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value0;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MultiTireDataViewer multiTireDataViewer1;
    }
}
