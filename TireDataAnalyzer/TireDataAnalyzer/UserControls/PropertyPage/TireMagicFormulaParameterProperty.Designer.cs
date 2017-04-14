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
            this.CorneringPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView0 = new System.Windows.Forms.DataGridView();
            this.ToSolver0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ParameterName0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.multiTireDataViewer0 = new TireDataAnalyzer.UserControls.MultiTireDataViewer();
            this.GraphTabControl = new System.Windows.Forms.CustomTabControl();
            this.CorneringPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.GraphTabControl.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(878, 632);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // dataGridView0
            // 
            this.dataGridView0.AllowUserToAddRows = false;
            this.dataGridView0.AllowUserToDeleteRows = false;
            this.dataGridView0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView0.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ToSolver0,
            this.ParameterName0,
            this.Value0});
            this.dataGridView0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView0.Location = new System.Drawing.Point(0, 0);
            this.dataGridView0.Name = "dataGridView0";
            this.dataGridView0.RowTemplate.Height = 21;
            this.dataGridView0.Size = new System.Drawing.Size(292, 632);
            this.dataGridView0.TabIndex = 0;
            this.dataGridView0.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValidated);
            this.dataGridView0.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            this.dataGridView0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
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
            this.ParameterName0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Value0
            // 
            this.Value0.HeaderText = "Value";
            this.Value0.Name = "Value0";
            this.Value0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.splitContainer2.Panel1.Controls.Add(this.multiTireDataViewer0);
            this.splitContainer2.Size = new System.Drawing.Size(582, 632);
            this.splitContainer2.SplitterDistance = 440;
            this.splitContainer2.TabIndex = 0;
            // 
            // multiTireDataViewer0
            // 
            this.multiTireDataViewer0.AutoScaleX = true;
            this.multiTireDataViewer0.AutoScaleY = true;
            this.multiTireDataViewer0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiTireDataViewer0.Location = new System.Drawing.Point(0, 0);
            this.multiTireDataViewer0.Name = "multiTireDataViewer0";
            this.multiTireDataViewer0.Size = new System.Drawing.Size(582, 440);
            this.multiTireDataViewer0.TabIndex = 0;
            // 
            // GraphTabControl
            // 
            this.GraphTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.GraphTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphTabControl.Controls.Add(this.CorneringPage);
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
            // TireMagicFormulaParameterProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GraphTabControl);
            this.Name = "TireMagicFormulaParameterProperty";
            this.Load += new System.EventHandler(this.TireMagicFormulaParameterProperty_Load);
            this.Controls.SetChildIndex(this.GraphTabControl, 0);
            this.CorneringPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView0)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.GraphTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage CorneringPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView0;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MultiTireDataViewer multiTireDataViewer0;
        private System.Windows.Forms.CustomTabControl GraphTabControl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToSolver0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterName0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value0;
    }
}
