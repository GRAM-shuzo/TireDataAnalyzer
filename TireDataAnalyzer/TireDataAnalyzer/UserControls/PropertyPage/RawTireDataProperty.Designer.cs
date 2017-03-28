namespace TireDataAnalyzer.UserControls.PropertyPage
{
    partial class RawTireDataProperty
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
            this.label1 = new System.Windows.Forms.Label();
            this.TransientTableTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CorneringTableAddButton = new System.Windows.Forms.Button();
            this.DriveBrakeTableAddButton = new System.Windows.Forms.Button();
            this.TransientTableAddButton = new System.Windows.Forms.Button();
            this.TransientTableBrowseButton = new System.Windows.Forms.Button();
            this.DriveBrakeTableBrowseButton = new System.Windows.Forms.Button();
            this.CorneringTableBrowseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DriveBrakeTableTextBox = new System.Windows.Forms.TextBox();
            this.CorneringTableTextBox = new System.Windows.Forms.TextBox();
            this.TireNameTextBox = new System.Windows.Forms.TextBox();
            this.NumPoint = new System.Windows.Forms.ComboBox();
            this.TransientPage = new System.Windows.Forms.TabPage();
            this.TransientDataViewer = new TireDataAnalyzer.UserControls.TireDataViewer();
            this.label22 = new System.Windows.Forms.Label();
            this.TransientTableNewDataCount = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.TransientTableDataCount = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.DriveAccelPage = new System.Windows.Forms.TabPage();
            this.DriveBrakeDataViewer = new TireDataAnalyzer.UserControls.TireDataViewer();
            this.label8 = new System.Windows.Forms.Label();
            this.DriveBrakeTableNewDataCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DriveBrakeTableDataCount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.CorneringPage = new System.Windows.Forms.TabPage();
            this.CorneringDataViewer = new TireDataAnalyzer.UserControls.TireDataViewer();
            this.label11 = new System.Windows.Forms.Label();
            this.CorneringTableNewDataCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.CorneringTableDataCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GraphTabControl = new System.Windows.Forms.CustomTabControl();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.TransientPage.SuspendLayout();
            this.DriveAccelPage.SuspendLayout();
            this.CorneringPage.SuspendLayout();
            this.GraphTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "タイヤデータ名";
            // 
            // TransientTableTextBox
            // 
            this.TransientTableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TransientTableTextBox.Location = new System.Drawing.Point(132, 92);
            this.TransientTableTextBox.Name = "TransientTableTextBox";
            this.TransientTableTextBox.Size = new System.Drawing.Size(602, 19);
            this.TransientTableTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "PureSlip - コーナリング";
            // 
            // CorneringTableAddButton
            // 
            this.CorneringTableAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CorneringTableAddButton.Location = new System.Drawing.Point(822, 32);
            this.CorneringTableAddButton.Name = "CorneringTableAddButton";
            this.CorneringTableAddButton.Size = new System.Drawing.Size(75, 23);
            this.CorneringTableAddButton.TabIndex = 8;
            this.CorneringTableAddButton.Text = "追加";
            this.CorneringTableAddButton.UseVisualStyleBackColor = true;
            this.CorneringTableAddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DriveBrakeTableAddButton
            // 
            this.DriveBrakeTableAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DriveBrakeTableAddButton.Location = new System.Drawing.Point(822, 61);
            this.DriveBrakeTableAddButton.Name = "DriveBrakeTableAddButton";
            this.DriveBrakeTableAddButton.Size = new System.Drawing.Size(75, 23);
            this.DriveBrakeTableAddButton.TabIndex = 9;
            this.DriveBrakeTableAddButton.Text = "追加";
            this.DriveBrakeTableAddButton.UseVisualStyleBackColor = true;
            this.DriveBrakeTableAddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // TransientTableAddButton
            // 
            this.TransientTableAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TransientTableAddButton.Location = new System.Drawing.Point(822, 90);
            this.TransientTableAddButton.Name = "TransientTableAddButton";
            this.TransientTableAddButton.Size = new System.Drawing.Size(75, 23);
            this.TransientTableAddButton.TabIndex = 11;
            this.TransientTableAddButton.Text = "追加";
            this.TransientTableAddButton.UseVisualStyleBackColor = true;
            this.TransientTableAddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // TransientTableBrowseButton
            // 
            this.TransientTableBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TransientTableBrowseButton.Location = new System.Drawing.Point(741, 90);
            this.TransientTableBrowseButton.Name = "TransientTableBrowseButton";
            this.TransientTableBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.TransientTableBrowseButton.TabIndex = 18;
            this.TransientTableBrowseButton.Text = "参照";
            this.TransientTableBrowseButton.UseVisualStyleBackColor = true;
            this.TransientTableBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // DriveBrakeTableBrowseButton
            // 
            this.DriveBrakeTableBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DriveBrakeTableBrowseButton.Location = new System.Drawing.Point(741, 61);
            this.DriveBrakeTableBrowseButton.Name = "DriveBrakeTableBrowseButton";
            this.DriveBrakeTableBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.DriveBrakeTableBrowseButton.TabIndex = 16;
            this.DriveBrakeTableBrowseButton.Text = "参照";
            this.DriveBrakeTableBrowseButton.UseVisualStyleBackColor = true;
            this.DriveBrakeTableBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // CorneringTableBrowseButton
            // 
            this.CorneringTableBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CorneringTableBrowseButton.Location = new System.Drawing.Point(741, 32);
            this.CorneringTableBrowseButton.Name = "CorneringTableBrowseButton";
            this.CorneringTableBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.CorneringTableBrowseButton.TabIndex = 15;
            this.CorneringTableBrowseButton.Text = "参照";
            this.CorneringTableBrowseButton.UseVisualStyleBackColor = true;
            this.CorneringTableBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "加減速/CombinedSlip";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "過渡特性（ステップ応答）";
            // 
            // DriveBrakeTableTextBox
            // 
            this.DriveBrakeTableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DriveBrakeTableTextBox.Location = new System.Drawing.Point(132, 63);
            this.DriveBrakeTableTextBox.Name = "DriveBrakeTableTextBox";
            this.DriveBrakeTableTextBox.Size = new System.Drawing.Size(602, 19);
            this.DriveBrakeTableTextBox.TabIndex = 24;
            // 
            // CorneringTableTextBox
            // 
            this.CorneringTableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorneringTableTextBox.Location = new System.Drawing.Point(132, 34);
            this.CorneringTableTextBox.Name = "CorneringTableTextBox";
            this.CorneringTableTextBox.Size = new System.Drawing.Size(602, 19);
            this.CorneringTableTextBox.TabIndex = 25;
            // 
            // TireNameTextBox
            // 
            this.TireNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TireNameTextBox.Location = new System.Drawing.Point(132, 5);
            this.TireNameTextBox.Name = "TireNameTextBox";
            this.TireNameTextBox.Size = new System.Drawing.Size(602, 19);
            this.TireNameTextBox.TabIndex = 26;
            // 
            // NumPoint
            // 
            this.NumPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumPoint.FormattingEnabled = true;
            this.NumPoint.Items.AddRange(new object[] {
            "すべて描画",
            "100000点",
            "50000点",
            "10000点",
            "5000点",
            "1000点"});
            this.NumPoint.Location = new System.Drawing.Point(776, 119);
            this.NumPoint.Name = "NumPoint";
            this.NumPoint.Size = new System.Drawing.Size(121, 20);
            this.NumPoint.TabIndex = 29;
            this.NumPoint.SelectedValueChanged += new System.EventHandler(this.NumPoint_SelectedValueChanged);
            // 
            // TransientPage
            // 
            this.TransientPage.BackColor = System.Drawing.Color.White;
            this.TransientPage.Controls.Add(this.TransientDataViewer);
            this.TransientPage.Controls.Add(this.label22);
            this.TransientPage.Controls.Add(this.TransientTableNewDataCount);
            this.TransientPage.Controls.Add(this.label24);
            this.TransientPage.Controls.Add(this.TransientTableDataCount);
            this.TransientPage.Controls.Add(this.label26);
            this.TransientPage.Controls.Add(this.label27);
            this.TransientPage.Location = new System.Drawing.Point(4, 4);
            this.TransientPage.Name = "TransientPage";
            this.TransientPage.Padding = new System.Windows.Forms.Padding(3);
            this.TransientPage.Size = new System.Drawing.Size(884, 496);
            this.TransientPage.TabIndex = 3;
            this.TransientPage.Text = "過渡特性";
            // 
            // TransientDataViewer
            // 
            this.TransientDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TransientDataViewer.Axis = TireDataAnalyzer.UserControls.TireDataViewer.EnumAxis.RawTireData;
            this.TransientDataViewer.Location = new System.Drawing.Point(6, 30);
            this.TransientDataViewer.Name = "TransientDataViewer";
            this.TransientDataViewer.Size = new System.Drawing.Size(843, 354);
            this.TransientDataViewer.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(146, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 5;
            this.label22.Text = "件";
            // 
            // TransientTableNewDataCount
            // 
            this.TransientTableNewDataCount.AutoSize = true;
            this.TransientTableNewDataCount.Location = new System.Drawing.Point(99, 15);
            this.TransientTableNewDataCount.Name = "TransientTableNewDataCount";
            this.TransientTableNewDataCount.Size = new System.Drawing.Size(41, 12);
            this.TransientTableNewDataCount.TabIndex = 4;
            this.TransientTableNewDataCount.Text = "000000";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(146, 3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 3;
            this.label24.Text = "件";
            // 
            // TransientTableDataCount
            // 
            this.TransientTableDataCount.AutoSize = true;
            this.TransientTableDataCount.Location = new System.Drawing.Point(99, 3);
            this.TransientTableDataCount.Name = "TransientTableDataCount";
            this.TransientTableDataCount.Size = new System.Drawing.Size(41, 12);
            this.TransientTableDataCount.TabIndex = 2;
            this.TransientTableDataCount.Text = "000000";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 15);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(87, 12);
            this.label26.TabIndex = 1;
            this.label26.Text = "新規追加データ：";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(90, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "追加済みデータ： ";
            // 
            // DriveAccelPage
            // 
            this.DriveAccelPage.BackColor = System.Drawing.Color.White;
            this.DriveAccelPage.Controls.Add(this.DriveBrakeDataViewer);
            this.DriveAccelPage.Controls.Add(this.label8);
            this.DriveAccelPage.Controls.Add(this.DriveBrakeTableNewDataCount);
            this.DriveAccelPage.Controls.Add(this.label12);
            this.DriveAccelPage.Controls.Add(this.DriveBrakeTableDataCount);
            this.DriveAccelPage.Controls.Add(this.label14);
            this.DriveAccelPage.Controls.Add(this.label15);
            this.DriveAccelPage.Location = new System.Drawing.Point(4, 4);
            this.DriveAccelPage.Name = "DriveAccelPage";
            this.DriveAccelPage.Padding = new System.Windows.Forms.Padding(3);
            this.DriveAccelPage.Size = new System.Drawing.Size(884, 496);
            this.DriveAccelPage.TabIndex = 1;
            this.DriveAccelPage.Text = "加減速/CombinedSlip";
            // 
            // DriveBrakeDataViewer
            // 
            this.DriveBrakeDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DriveBrakeDataViewer.Axis = TireDataAnalyzer.UserControls.TireDataViewer.EnumAxis.RawTireData;
            this.DriveBrakeDataViewer.Location = new System.Drawing.Point(6, 30);
            this.DriveBrakeDataViewer.Name = "DriveBrakeDataViewer";
            this.DriveBrakeDataViewer.Size = new System.Drawing.Size(843, 354);
            this.DriveBrakeDataViewer.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "件";
            // 
            // DriveBrakeTableNewDataCount
            // 
            this.DriveBrakeTableNewDataCount.AutoSize = true;
            this.DriveBrakeTableNewDataCount.Location = new System.Drawing.Point(99, 15);
            this.DriveBrakeTableNewDataCount.Name = "DriveBrakeTableNewDataCount";
            this.DriveBrakeTableNewDataCount.Size = new System.Drawing.Size(41, 12);
            this.DriveBrakeTableNewDataCount.TabIndex = 4;
            this.DriveBrakeTableNewDataCount.Text = "000000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(146, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "件";
            // 
            // DriveBrakeTableDataCount
            // 
            this.DriveBrakeTableDataCount.AutoSize = true;
            this.DriveBrakeTableDataCount.Location = new System.Drawing.Point(99, 3);
            this.DriveBrakeTableDataCount.Name = "DriveBrakeTableDataCount";
            this.DriveBrakeTableDataCount.Size = new System.Drawing.Size(41, 12);
            this.DriveBrakeTableDataCount.TabIndex = 2;
            this.DriveBrakeTableDataCount.Text = "000000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "新規追加データ：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "追加済みデータ： ";
            // 
            // CorneringPage
            // 
            this.CorneringPage.BackColor = System.Drawing.Color.White;
            this.CorneringPage.Controls.Add(this.CorneringDataViewer);
            this.CorneringPage.Controls.Add(this.label11);
            this.CorneringPage.Controls.Add(this.CorneringTableNewDataCount);
            this.CorneringPage.Controls.Add(this.label9);
            this.CorneringPage.Controls.Add(this.CorneringTableDataCount);
            this.CorneringPage.Controls.Add(this.label7);
            this.CorneringPage.Controls.Add(this.label6);
            this.CorneringPage.Location = new System.Drawing.Point(4, 4);
            this.CorneringPage.Name = "CorneringPage";
            this.CorneringPage.Padding = new System.Windows.Forms.Padding(3);
            this.CorneringPage.Size = new System.Drawing.Size(884, 496);
            this.CorneringPage.TabIndex = 0;
            this.CorneringPage.Text = "PureSlip - コーナリング";
            // 
            // CorneringDataViewer
            // 
            this.CorneringDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorneringDataViewer.Axis = TireDataAnalyzer.UserControls.TireDataViewer.EnumAxis.RawTireData;
            this.CorneringDataViewer.Location = new System.Drawing.Point(6, 30);
            this.CorneringDataViewer.Name = "CorneringDataViewer";
            this.CorneringDataViewer.Size = new System.Drawing.Size(872, 460);
            this.CorneringDataViewer.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(146, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "件";
            // 
            // CorneringTableNewDataCount
            // 
            this.CorneringTableNewDataCount.AutoSize = true;
            this.CorneringTableNewDataCount.Location = new System.Drawing.Point(99, 15);
            this.CorneringTableNewDataCount.Name = "CorneringTableNewDataCount";
            this.CorneringTableNewDataCount.Size = new System.Drawing.Size(41, 12);
            this.CorneringTableNewDataCount.TabIndex = 4;
            this.CorneringTableNewDataCount.Text = "000000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "件";
            // 
            // CorneringTableDataCount
            // 
            this.CorneringTableDataCount.AutoSize = true;
            this.CorneringTableDataCount.Location = new System.Drawing.Point(99, 3);
            this.CorneringTableDataCount.Name = "CorneringTableDataCount";
            this.CorneringTableDataCount.Size = new System.Drawing.Size(41, 12);
            this.CorneringTableDataCount.TabIndex = 2;
            this.CorneringTableDataCount.Text = "000000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "新規追加データ：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "追加済みデータ： ";
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
            this.GraphTabControl.Location = new System.Drawing.Point(5, 145);
            this.GraphTabControl.Name = "GraphTabControl";
            this.GraphTabControl.SelectedIndex = 0;
            this.GraphTabControl.Size = new System.Drawing.Size(892, 523);
            this.GraphTabControl.TabIndex = 27;
            this.GraphTabControl.SelectedIndexChanged += new System.EventHandler(this.GraphTabControl_SelectedIndexChanged);
            // 
            // RawTireDataProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.NumPoint);
            this.Controls.Add(this.GraphTabControl);
            this.Controls.Add(this.TireNameTextBox);
            this.Controls.Add(this.CorneringTableTextBox);
            this.Controls.Add(this.DriveBrakeTableTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TransientTableBrowseButton);
            this.Controls.Add(this.DriveBrakeTableBrowseButton);
            this.Controls.Add(this.CorneringTableBrowseButton);
            this.Controls.Add(this.TransientTableAddButton);
            this.Controls.Add(this.DriveBrakeTableAddButton);
            this.Controls.Add(this.CorneringTableAddButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TransientTableTextBox);
            this.Controls.Add(this.label1);
            this.Name = "RawTireDataProperty";
            this.Load += new System.EventHandler(this.RawTireDataProperty_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.TransientTableTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.CorneringTableAddButton, 0);
            this.Controls.SetChildIndex(this.DriveBrakeTableAddButton, 0);
            this.Controls.SetChildIndex(this.TransientTableAddButton, 0);
            this.Controls.SetChildIndex(this.CorneringTableBrowseButton, 0);
            this.Controls.SetChildIndex(this.DriveBrakeTableBrowseButton, 0);
            this.Controls.SetChildIndex(this.TransientTableBrowseButton, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.DriveBrakeTableTextBox, 0);
            this.Controls.SetChildIndex(this.CorneringTableTextBox, 0);
            this.Controls.SetChildIndex(this.TireNameTextBox, 0);
            this.Controls.SetChildIndex(this.GraphTabControl, 0);
            this.Controls.SetChildIndex(this.NumPoint, 0);
            this.TransientPage.ResumeLayout(false);
            this.TransientPage.PerformLayout();
            this.DriveAccelPage.ResumeLayout(false);
            this.DriveAccelPage.PerformLayout();
            this.CorneringPage.ResumeLayout(false);
            this.CorneringPage.PerformLayout();
            this.GraphTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TransientTableTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CorneringTableAddButton;
        private System.Windows.Forms.Button DriveBrakeTableAddButton;
        private System.Windows.Forms.Button TransientTableAddButton;
        private System.Windows.Forms.Button TransientTableBrowseButton;
        private System.Windows.Forms.Button DriveBrakeTableBrowseButton;
        private System.Windows.Forms.Button CorneringTableBrowseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DriveBrakeTableTextBox;
        private System.Windows.Forms.TextBox CorneringTableTextBox;
        private System.Windows.Forms.TextBox TireNameTextBox;
        private System.Windows.Forms.ComboBox NumPoint;
        private System.Windows.Forms.TabPage TransientPage;
        private TireDataViewer TransientDataViewer;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label TransientTableNewDataCount;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label TransientTableDataCount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabPage DriveAccelPage;
        private TireDataViewer DriveBrakeDataViewer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label DriveBrakeTableNewDataCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label DriveBrakeTableDataCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage CorneringPage;
        private TireDataViewer CorneringDataViewer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label CorneringTableNewDataCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label CorneringTableDataCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CustomTabControl GraphTabControl;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}
