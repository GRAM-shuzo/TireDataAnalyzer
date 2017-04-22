namespace TireDataAnalyzer.UserControls
{
    partial class TireDataViewerProperty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.TitleTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.XAxisCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.YAxisCB = new System.Windows.Forms.ComboBox();
            this.MagicFormulaRB = new System.Windows.Forms.RadioButton();
            this.RawDataRB = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.XMaxTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.XMinTB = new System.Windows.Forms.TextBox();
            this.YMinTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.YMaxTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LegendAlignCB = new System.Windows.Forms.ComboBox();
            this.DataSourceList = new System.Windows.Forms.Panel();
            this.ShowTitleCB = new System.Windows.Forms.CheckBox();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.AutoScaleX = new System.Windows.Forms.CheckBox();
            this.AutoScaleY = new System.Windows.Forms.CheckBox();
            this.YTickStyleCB = new System.Windows.Forms.ComboBox();
            this.YGridLineStyleCB = new System.Windows.Forms.ComboBox();
            this.XGridLineStyleCB = new System.Windows.Forms.ComboBox();
            this.XTickStyleCB = new System.Windows.Forms.ComboBox();
            this.TitleDockCB = new System.Windows.Forms.ComboBox();
            this.XGridLabelFormat = new System.Windows.Forms.TextBox();
            this.Format = new System.Windows.Forms.Label();
            this.YGridLabelFormat = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.LegendDockCB = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LegendStyleCB = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.DockToAreaCB = new System.Windows.Forms.CheckBox();
            this.XAdvanced = new System.Windows.Forms.Button();
            this.YAdvanced = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.PointsToRenderTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.seriesEditorHeader1 = new TireDataAnalyzer.UserControls.SeriesEditorHeader();
            this.DataSourceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // TitleTB
            // 
            this.TitleTB.Location = new System.Drawing.Point(57, 12);
            this.TitleTB.Name = "TitleTB";
            this.TitleTB.Size = new System.Drawing.Size(484, 19);
            this.TitleTB.TabIndex = 1;
            this.TitleTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.TitleTB.Leave += new System.EventHandler(this.ChartItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "X Axis";
            // 
            // XAxisCB
            // 
            this.XAxisCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.XAxisCB.FormattingEnabled = true;
            this.XAxisCB.Location = new System.Drawing.Point(57, 37);
            this.XAxisCB.Name = "XAxisCB";
            this.XAxisCB.Size = new System.Drawing.Size(100, 20);
            this.XAxisCB.TabIndex = 3;
            this.XAxisCB.SelectedIndexChanged += new System.EventHandler(this.AxisCB_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y Axis";
            // 
            // YAxisCB
            // 
            this.YAxisCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YAxisCB.FormattingEnabled = true;
            this.YAxisCB.Location = new System.Drawing.Point(57, 89);
            this.YAxisCB.Name = "YAxisCB";
            this.YAxisCB.Size = new System.Drawing.Size(100, 20);
            this.YAxisCB.TabIndex = 5;
            this.YAxisCB.SelectedIndexChanged += new System.EventHandler(this.AxisCB_SelectedIndexChanged);
            // 
            // MagicFormulaRB
            // 
            this.MagicFormulaRB.AutoSize = true;
            this.MagicFormulaRB.Location = new System.Drawing.Point(14, 140);
            this.MagicFormulaRB.Name = "MagicFormulaRB";
            this.MagicFormulaRB.Size = new System.Drawing.Size(94, 16);
            this.MagicFormulaRB.TabIndex = 6;
            this.MagicFormulaRB.Text = "MagicFormula";
            this.MagicFormulaRB.UseVisualStyleBackColor = true;
            this.MagicFormulaRB.CheckedChanged += new System.EventHandler(this.AxisTypeChanged);
            // 
            // RawDataRB
            // 
            this.RawDataRB.AutoSize = true;
            this.RawDataRB.Location = new System.Drawing.Point(114, 140);
            this.RawDataRB.Name = "RawDataRB";
            this.RawDataRB.Size = new System.Drawing.Size(69, 16);
            this.RawDataRB.TabIndex = 7;
            this.RawDataRB.Text = "RawData";
            this.RawDataRB.UseVisualStyleBackColor = true;
            this.RawDataRB.CheckedChanged += new System.EventHandler(this.AxisTypeChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "MaxValue";
            // 
            // XMaxTB
            // 
            this.XMaxTB.Location = new System.Drawing.Point(224, 37);
            this.XMaxTB.Name = "XMaxTB";
            this.XMaxTB.Size = new System.Drawing.Size(90, 19);
            this.XMaxTB.TabIndex = 11;
            this.XMaxTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.XMaxTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.XMaxTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "MinValue";
            // 
            // XMinTB
            // 
            this.XMinTB.Location = new System.Drawing.Point(378, 37);
            this.XMinTB.Name = "XMinTB";
            this.XMinTB.Size = new System.Drawing.Size(90, 19);
            this.XMinTB.TabIndex = 13;
            this.XMinTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.XMinTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.XMinTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // YMinTB
            // 
            this.YMinTB.Location = new System.Drawing.Point(378, 89);
            this.YMinTB.Name = "YMinTB";
            this.YMinTB.Size = new System.Drawing.Size(90, 19);
            this.YMinTB.TabIndex = 21;
            this.YMinTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.YMinTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.YMinTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(320, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "MinValue";
            // 
            // YMaxTB
            // 
            this.YMaxTB.Location = new System.Drawing.Point(224, 89);
            this.YMaxTB.Name = "YMaxTB";
            this.YMaxTB.Size = new System.Drawing.Size(90, 19);
            this.YMaxTB.TabIndex = 19;
            this.YMaxTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.YMaxTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsReal_Validating);
            this.YMaxTB.Validated += new System.EventHandler(this.IsReal_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(163, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "MaxValue";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(648, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "LegendAlign";
            // 
            // LegendAlignCB
            // 
            this.LegendAlignCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LegendAlignCB.FormattingEnabled = true;
            this.LegendAlignCB.Location = new System.Drawing.Point(721, 139);
            this.LegendAlignCB.Name = "LegendAlignCB";
            this.LegendAlignCB.Size = new System.Drawing.Size(81, 20);
            this.LegendAlignCB.TabIndex = 26;
            this.LegendAlignCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // DataSourceList
            // 
            this.DataSourceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataSourceList.AutoScroll = true;
            this.DataSourceList.BackColor = System.Drawing.SystemColors.Control;
            this.DataSourceList.Controls.Add(this.seriesEditorHeader1);
            this.DataSourceList.Location = new System.Drawing.Point(12, 194);
            this.DataSourceList.Name = "DataSourceList";
            this.DataSourceList.Size = new System.Drawing.Size(790, 272);
            this.DataSourceList.TabIndex = 27;
            // 
            // ShowTitleCB
            // 
            this.ShowTitleCB.AutoSize = true;
            this.ShowTitleCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowTitleCB.Location = new System.Drawing.Point(698, 14);
            this.ShowTitleCB.Name = "ShowTitleCB";
            this.ShowTitleCB.Size = new System.Drawing.Size(104, 16);
            this.ShowTitleCB.TabIndex = 28;
            this.ShowTitleCB.Text = "ShowGraphTitle";
            this.ShowTitleCB.UseVisualStyleBackColor = true;
            this.ShowTitleCB.CheckedChanged += new System.EventHandler(this.ShowTitleCB_CheckedChanged);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(727, 472);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 33;
            this.ApplyButton.Text = "適用";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(646, 472);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 32;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(565, 472);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 31;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // AutoScaleX
            // 
            this.AutoScaleX.AutoSize = true;
            this.AutoScaleX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoScaleX.Location = new System.Drawing.Point(639, 39);
            this.AutoScaleX.Name = "AutoScaleX";
            this.AutoScaleX.Size = new System.Drawing.Size(76, 16);
            this.AutoScaleX.TabIndex = 34;
            this.AutoScaleX.Text = "AutoScale";
            this.AutoScaleX.UseVisualStyleBackColor = true;
            this.AutoScaleX.CheckedChanged += new System.EventHandler(this.AutoAxisStateChanged);
            // 
            // AutoScaleY
            // 
            this.AutoScaleY.AutoSize = true;
            this.AutoScaleY.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoScaleY.Location = new System.Drawing.Point(639, 91);
            this.AutoScaleY.Name = "AutoScaleY";
            this.AutoScaleY.Size = new System.Drawing.Size(76, 16);
            this.AutoScaleY.TabIndex = 36;
            this.AutoScaleY.Text = "AutoScale";
            this.AutoScaleY.UseVisualStyleBackColor = true;
            this.AutoScaleY.CheckedChanged += new System.EventHandler(this.AutoAxisStateChanged);
            // 
            // YTickStyleCB
            // 
            this.YTickStyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YTickStyleCB.FormattingEnabled = true;
            this.YTickStyleCB.Location = new System.Drawing.Point(625, 113);
            this.YTickStyleCB.Name = "YTickStyleCB";
            this.YTickStyleCB.Size = new System.Drawing.Size(90, 20);
            this.YTickStyleCB.TabIndex = 40;
            this.YTickStyleCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // YGridLineStyleCB
            // 
            this.YGridLineStyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YGridLineStyleCB.FormattingEnabled = true;
            this.YGridLineStyleCB.Location = new System.Drawing.Point(721, 113);
            this.YGridLineStyleCB.Name = "YGridLineStyleCB";
            this.YGridLineStyleCB.Size = new System.Drawing.Size(81, 20);
            this.YGridLineStyleCB.TabIndex = 42;
            this.YGridLineStyleCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // XGridLineStyleCB
            // 
            this.XGridLineStyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.XGridLineStyleCB.FormattingEnabled = true;
            this.XGridLineStyleCB.Location = new System.Drawing.Point(721, 61);
            this.XGridLineStyleCB.Name = "XGridLineStyleCB";
            this.XGridLineStyleCB.Size = new System.Drawing.Size(81, 20);
            this.XGridLineStyleCB.TabIndex = 45;
            this.XGridLineStyleCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // XTickStyleCB
            // 
            this.XTickStyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.XTickStyleCB.FormattingEnabled = true;
            this.XTickStyleCB.Location = new System.Drawing.Point(625, 61);
            this.XTickStyleCB.Name = "XTickStyleCB";
            this.XTickStyleCB.Size = new System.Drawing.Size(90, 20);
            this.XTickStyleCB.TabIndex = 43;
            this.XTickStyleCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // TitleDockCB
            // 
            this.TitleDockCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TitleDockCB.FormattingEnabled = true;
            this.TitleDockCB.Location = new System.Drawing.Point(547, 12);
            this.TitleDockCB.Name = "TitleDockCB";
            this.TitleDockCB.Size = new System.Drawing.Size(143, 20);
            this.TitleDockCB.TabIndex = 46;
            this.TitleDockCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // XGridLabelFormat
            // 
            this.XGridLabelFormat.Location = new System.Drawing.Point(523, 61);
            this.XGridLabelFormat.Name = "XGridLabelFormat";
            this.XGridLabelFormat.Size = new System.Drawing.Size(90, 19);
            this.XGridLabelFormat.TabIndex = 48;
            this.XGridLabelFormat.Validating += new System.ComponentModel.CancelEventHandler(this.XGridLabelFormat_Validating);
            this.XGridLabelFormat.Validated += new System.EventHandler(this.XGridLabelFormat_Validated);
            // 
            // Format
            // 
            this.Format.AutoSize = true;
            this.Format.Location = new System.Drawing.Point(474, 64);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(41, 12);
            this.Format.TabIndex = 47;
            this.Format.Text = "Format";
            // 
            // YGridLabelFormat
            // 
            this.YGridLabelFormat.Location = new System.Drawing.Point(523, 113);
            this.YGridLabelFormat.Name = "YGridLabelFormat";
            this.YGridLabelFormat.Size = new System.Drawing.Size(90, 19);
            this.YGridLabelFormat.TabIndex = 50;
            this.YGridLabelFormat.Validating += new System.ComponentModel.CancelEventHandler(this.XGridLabelFormat_Validating);
            this.YGridLabelFormat.Validated += new System.EventHandler(this.XGridLabelFormat_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(474, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 49;
            this.label12.Text = "Format";
            // 
            // LegendDockCB
            // 
            this.LegendDockCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LegendDockCB.FormattingEnabled = true;
            this.LegendDockCB.Location = new System.Drawing.Point(547, 139);
            this.LegendDockCB.Name = "LegendDockCB";
            this.LegendDockCB.Size = new System.Drawing.Size(95, 20);
            this.LegendDockCB.TabIndex = 52;
            this.LegendDockCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(474, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 12);
            this.label11.TabIndex = 51;
            this.label11.Text = "LegendDock";
            // 
            // LegendStyleCB
            // 
            this.LegendStyleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LegendStyleCB.FormattingEnabled = true;
            this.LegendStyleCB.Location = new System.Drawing.Point(378, 139);
            this.LegendStyleCB.Name = "LegendStyleCB";
            this.LegendStyleCB.Size = new System.Drawing.Size(90, 20);
            this.LegendStyleCB.TabIndex = 54;
            this.LegendStyleCB.SelectedIndexChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(305, 142);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 12);
            this.label13.TabIndex = 53;
            this.label13.Text = "LegendStyle";
            // 
            // DockToAreaCB
            // 
            this.DockToAreaCB.AutoSize = true;
            this.DockToAreaCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DockToAreaCB.Location = new System.Drawing.Point(212, 141);
            this.DockToAreaCB.Name = "DockToAreaCB";
            this.DockToAreaCB.Size = new System.Drawing.Size(87, 16);
            this.DockToAreaCB.TabIndex = 55;
            this.DockToAreaCB.Text = "DockToArea";
            this.DockToAreaCB.UseVisualStyleBackColor = true;
            this.DockToAreaCB.CheckedChanged += new System.EventHandler(this.ChartItemChanged);
            // 
            // XAdvanced
            // 
            this.XAdvanced.Location = new System.Drawing.Point(721, 35);
            this.XAdvanced.Name = "XAdvanced";
            this.XAdvanced.Size = new System.Drawing.Size(81, 23);
            this.XAdvanced.TabIndex = 56;
            this.XAdvanced.Text = "Advanced";
            this.XAdvanced.UseVisualStyleBackColor = true;
            this.XAdvanced.Click += new System.EventHandler(this.XAdvanced_Click);
            // 
            // YAdvanced
            // 
            this.YAdvanced.Location = new System.Drawing.Point(721, 87);
            this.YAdvanced.Name = "YAdvanced";
            this.YAdvanced.Size = new System.Drawing.Size(81, 23);
            this.YAdvanced.TabIndex = 57;
            this.YAdvanced.Text = "Advanced";
            this.YAdvanced.UseVisualStyleBackColor = true;
            this.YAdvanced.Click += new System.EventHandler(this.YAdvanced_Click);
            // 
            // DownButton
            // 
            this.DownButton.Image = global::TireDataAnalyzer.Properties.Resources.arrow_Down_16xLG;
            this.DownButton.Location = new System.Drawing.Point(692, 165);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(23, 23);
            this.DownButton.TabIndex = 39;
            this.DownButton.Text = " ";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Image = global::TireDataAnalyzer.Properties.Resources.action_add_16xLG;
            this.AddButton.Location = new System.Drawing.Point(750, 165);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(23, 23);
            this.AddButton.TabIndex = 29;
            this.AddButton.Text = " ";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.Image = global::TireDataAnalyzer.Properties.Resources.arrow_Up_16xLG;
            this.UpButton.Location = new System.Drawing.Point(663, 165);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(23, 23);
            this.UpButton.TabIndex = 38;
            this.UpButton.Text = " ";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Image = global::TireDataAnalyzer.Properties.Resources.action_Cancel_16xLG;
            this.DeleteButton.Location = new System.Drawing.Point(779, 165);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(23, 23);
            this.DeleteButton.TabIndex = 30;
            this.DeleteButton.Text = " ";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Image = global::TireDataAnalyzer.Properties.Resources.Copy_16x;
            this.CopyButton.Location = new System.Drawing.Point(721, 165);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(23, 23);
            this.CopyButton.TabIndex = 58;
            this.CopyButton.Text = " ";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // PointsToRenderTB
            // 
            this.PointsToRenderTB.Location = new System.Drawing.Point(557, 167);
            this.PointsToRenderTB.Name = "PointsToRenderTB";
            this.PointsToRenderTB.Size = new System.Drawing.Size(100, 19);
            this.PointsToRenderTB.TabIndex = 59;
            this.PointsToRenderTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown2);
            this.PointsToRenderTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsNatural_Validating);
            this.PointsToRenderTB.Validated += new System.EventHandler(this.IsNatural_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(457, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "Points To Render";
            // 
            // seriesEditorHeader1
            // 
            this.seriesEditorHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.seriesEditorHeader1.Location = new System.Drawing.Point(0, 0);
            this.seriesEditorHeader1.Name = "seriesEditorHeader1";
            this.seriesEditorHeader1.Size = new System.Drawing.Size(790, 20);
            this.seriesEditorHeader1.TabIndex = 0;
            // 
            // TireDataViewerProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 507);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PointsToRenderTB);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.YAdvanced);
            this.Controls.Add(this.XAdvanced);
            this.Controls.Add(this.DockToAreaCB);
            this.Controls.Add(this.LegendStyleCB);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.LegendDockCB);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.YGridLabelFormat);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.XGridLabelFormat);
            this.Controls.Add(this.Format);
            this.Controls.Add(this.TitleDockCB);
            this.Controls.Add(this.XGridLineStyleCB);
            this.Controls.Add(this.XTickStyleCB);
            this.Controls.Add(this.YGridLineStyleCB);
            this.Controls.Add(this.YTickStyleCB);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.AutoScaleY);
            this.Controls.Add(this.AutoScaleX);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ShowTitleCB);
            this.Controls.Add(this.DataSourceList);
            this.Controls.Add(this.LegendAlignCB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.YMinTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.YMaxTB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.XMinTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.XMaxTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RawDataRB);
            this.Controls.Add(this.MagicFormulaRB);
            this.Controls.Add(this.YAxisCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.XAxisCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TitleTB);
            this.Controls.Add(this.label1);
            this.Name = "TireDataViewerProperty";
            this.Text = "TireDataViewerProperty";
            this.DataSourceList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TitleTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox XAxisCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox YAxisCB;
        private System.Windows.Forms.RadioButton MagicFormulaRB;
        private System.Windows.Forms.RadioButton RawDataRB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox XMaxTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox XMinTB;
        private System.Windows.Forms.TextBox YMinTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox YMaxTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox LegendAlignCB;
        private System.Windows.Forms.Panel DataSourceList;
        private System.Windows.Forms.CheckBox ShowTitleCB;
        private SeriesEditorHeader seriesEditorHeader1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
        private System.Windows.Forms.CheckBox AutoScaleY;
        private System.Windows.Forms.CheckBox AutoScaleX;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.ComboBox XGridLineStyleCB;
        private System.Windows.Forms.ComboBox XTickStyleCB;
        private System.Windows.Forms.ComboBox YGridLineStyleCB;
        private System.Windows.Forms.ComboBox YTickStyleCB;
        private System.Windows.Forms.ComboBox TitleDockCB;
        private System.Windows.Forms.TextBox YGridLabelFormat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox XGridLabelFormat;
        private System.Windows.Forms.Label Format;
        private System.Windows.Forms.ComboBox LegendStyleCB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox LegendDockCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox DockToAreaCB;
        private System.Windows.Forms.Button YAdvanced;
        private System.Windows.Forms.Button XAdvanced;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PointsToRenderTB;
    }
}