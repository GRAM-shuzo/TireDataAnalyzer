namespace TireDataAnalyzer.UserControls.PropertyPage
{
    partial class DataSelectorProperty
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NumPoint = new System.Windows.Forms.ComboBox();
            this.TransientPage = new System.Windows.Forms.TabPage();
            this.TransientDataSelector = new TireDataAnalyzer.UserControls.PropertyPage.TireDataSelectorWithViewer();
            this.DriveBrakePage = new System.Windows.Forms.TabPage();
            this.DriveBrakeDataSelector = new TireDataAnalyzer.UserControls.PropertyPage.TireDataSelectorWithViewer();
            this.CorneringPage = new System.Windows.Forms.TabPage();
            this.CorneringDataSelector = new TireDataAnalyzer.UserControls.PropertyPage.TireDataSelectorWithViewer();
            this.MainTabControl = new System.Windows.Forms.CustomTabControl();
            this.GradationCB = new System.Windows.Forms.ComboBox();
            this.TableColumnCB = new System.Windows.Forms.ComboBox();
            this.MaxTB = new System.Windows.Forms.TextBox();
            this.MinTB = new System.Windows.Forms.TextBox();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.TransientPage.SuspendLayout();
            this.DriveBrakePage.SuspendLayout();
            this.CorneringPage.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(4, 4);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(893, 19);
            this.NameTextBox.TabIndex = 30;
            // 
            // NumPoint
            // 
            this.NumPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumPoint.FormattingEnabled = true;
            this.NumPoint.Items.AddRange(new object[] {
            "すべて描画",
            "100000点",
            "50000点",
            "10000点",
            "5000点",
            "1000点"});
            this.NumPoint.Location = new System.Drawing.Point(776, 33);
            this.NumPoint.Name = "NumPoint";
            this.NumPoint.Size = new System.Drawing.Size(121, 20);
            this.NumPoint.TabIndex = 30;
            this.NumPoint.SelectedIndexChanged += new System.EventHandler(this.NumPoint_SelectedValueChanged);
            // 
            // TransientPage
            // 
            this.TransientPage.BackColor = System.Drawing.SystemColors.Control;
            this.TransientPage.Controls.Add(this.TransientDataSelector);
            this.TransientPage.Location = new System.Drawing.Point(4, 4);
            this.TransientPage.Name = "TransientPage";
            this.TransientPage.Size = new System.Drawing.Size(889, 586);
            this.TransientPage.TabIndex = 5;
            this.TransientPage.Text = "過渡特性";
            // 
            // TransientDataSelector
            // 
            this.TransientDataSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransientDataSelector.Location = new System.Drawing.Point(0, 0);
            this.TransientDataSelector.Name = "TransientDataSelector";
            this.TransientDataSelector.Size = new System.Drawing.Size(889, 586);
            this.TransientDataSelector.TabIndex = 0;
            // 
            // DriveBrakePage
            // 
            this.DriveBrakePage.BackColor = System.Drawing.SystemColors.Control;
            this.DriveBrakePage.Controls.Add(this.DriveBrakeDataSelector);
            this.DriveBrakePage.Location = new System.Drawing.Point(4, 4);
            this.DriveBrakePage.Name = "DriveBrakePage";
            this.DriveBrakePage.Size = new System.Drawing.Size(889, 586);
            this.DriveBrakePage.TabIndex = 4;
            this.DriveBrakePage.Text = "加減速/CombinedSlip";
            // 
            // DriveBrakeDataSelector
            // 
            this.DriveBrakeDataSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DriveBrakeDataSelector.Location = new System.Drawing.Point(0, 0);
            this.DriveBrakeDataSelector.Name = "DriveBrakeDataSelector";
            this.DriveBrakeDataSelector.Size = new System.Drawing.Size(889, 586);
            this.DriveBrakeDataSelector.TabIndex = 0;
            // 
            // CorneringPage
            // 
            this.CorneringPage.BackColor = System.Drawing.SystemColors.Control;
            this.CorneringPage.Controls.Add(this.CorneringDataSelector);
            this.CorneringPage.Location = new System.Drawing.Point(4, 4);
            this.CorneringPage.Name = "CorneringPage";
            this.CorneringPage.Size = new System.Drawing.Size(889, 586);
            this.CorneringPage.TabIndex = 6;
            this.CorneringPage.Text = "PureSlip - コーナリング";
            // 
            // CorneringDataSelector
            // 
            this.CorneringDataSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CorneringDataSelector.Location = new System.Drawing.Point(0, 0);
            this.CorneringDataSelector.Name = "CorneringDataSelector";
            this.CorneringDataSelector.Size = new System.Drawing.Size(889, 586);
            this.CorneringDataSelector.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.CorneringPage);
            this.MainTabControl.Controls.Add(this.DriveBrakePage);
            this.MainTabControl.Controls.Add(this.TransientPage);
            this.MainTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Angled;
            // 
            // 
            // 
            this.MainTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.MainTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.MainTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.MainTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.MainTabControl.DisplayStyleProvider.FocusTrack = false;
            this.MainTabControl.DisplayStyleProvider.HotTrack = true;
            this.MainTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MainTabControl.DisplayStyleProvider.Opacity = 1F;
            this.MainTabControl.DisplayStyleProvider.Overlap = 7;
            this.MainTabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(10, 3);
            this.MainTabControl.DisplayStyleProvider.Radius = 10;
            this.MainTabControl.DisplayStyleProvider.ShowTabCloser = false;
            this.MainTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.MainTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.MainTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.MainTabControl.HotTrack = true;
            this.MainTabControl.Location = new System.Drawing.Point(0, 55);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(897, 613);
            this.MainTabControl.TabIndex = 33;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.MainTabControl_SelectedIndexChanged);
            // 
            // GradationCB
            // 
            this.GradationCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GradationCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GradationCB.FormattingEnabled = true;
            this.GradationCB.Location = new System.Drawing.Point(310, 33);
            this.GradationCB.Name = "GradationCB";
            this.GradationCB.Size = new System.Drawing.Size(121, 20);
            this.GradationCB.TabIndex = 34;
            // 
            // TableColumnCB
            // 
            this.TableColumnCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TableColumnCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableColumnCB.FormattingEnabled = true;
            this.TableColumnCB.Location = new System.Drawing.Point(649, 33);
            this.TableColumnCB.Name = "TableColumnCB";
            this.TableColumnCB.Size = new System.Drawing.Size(121, 20);
            this.TableColumnCB.TabIndex = 35;
            // 
            // MaxTB
            // 
            this.MaxTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxTB.Location = new System.Drawing.Point(543, 33);
            this.MaxTB.Name = "MaxTB";
            this.MaxTB.Size = new System.Drawing.Size(100, 19);
            this.MaxTB.TabIndex = 36;
            // 
            // MinTB
            // 
            this.MinTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinTB.Location = new System.Drawing.Point(437, 33);
            this.MinTB.Name = "MinTB";
            this.MinTB.Size = new System.Drawing.Size(100, 19);
            this.MinTB.TabIndex = 37;
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // DataSelectorProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.MinTB);
            this.Controls.Add(this.MaxTB);
            this.Controls.Add(this.TableColumnCB);
            this.Controls.Add(this.GradationCB);
            this.Controls.Add(this.NumPoint);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.NameTextBox);
            this.Name = "DataSelectorProperty";
            this.Load += new System.EventHandler(this.DataSelectorProperty_Load);
            this.Controls.SetChildIndex(this.NameTextBox, 0);
            this.Controls.SetChildIndex(this.MainTabControl, 0);
            this.Controls.SetChildIndex(this.NumPoint, 0);
            this.Controls.SetChildIndex(this.GradationCB, 0);
            this.Controls.SetChildIndex(this.TableColumnCB, 0);
            this.Controls.SetChildIndex(this.MaxTB, 0);
            this.Controls.SetChildIndex(this.MinTB, 0);
            this.TransientPage.ResumeLayout(false);
            this.DriveBrakePage.ResumeLayout(false);
            this.CorneringPage.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TabPage TransientPage;
        private TireDataSelectorWithViewer TransientDataSelector;
        private System.Windows.Forms.TabPage DriveBrakePage;
        private TireDataSelectorWithViewer DriveBrakeDataSelector;
        private System.Windows.Forms.TabPage CorneringPage;
        private TireDataSelectorWithViewer CorneringDataSelector;
        private System.Windows.Forms.CustomTabControl MainTabControl;
        private System.Windows.Forms.ComboBox GradationCB;
        private System.Windows.Forms.ComboBox TableColumnCB;
        private System.Windows.Forms.TextBox MaxTB;
        private System.Windows.Forms.TextBox MinTB;
        private System.Windows.Forms.ComboBox NumPoint;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
    }
}
