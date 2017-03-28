namespace TireDataAnalyzer.UserControls
{
    partial class SeriesEditor
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
            this.NameTB = new System.Windows.Forms.TextBox();
            this.SourceCB = new System.Windows.Forms.ComboBox();
            this.PlotTypeCB = new System.Windows.Forms.ComboBox();
            this.ColorCB = new System.Windows.Forms.ComboBox();
            this.ShowCB = new System.Windows.Forms.CheckBox();
            this.ArgumentsButton = new System.Windows.Forms.Button();
            this.SizeTB = new System.Windows.Forms.TextBox();
            this.EP_NumericalInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.MFSourceCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(0, 0);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(150, 19);
            this.NameTB.TabIndex = 0;
            this.NameTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nint_KeyDown);
            this.NameTB.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // SourceCB
            // 
            this.SourceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceCB.FormattingEnabled = true;
            this.SourceCB.Location = new System.Drawing.Point(156, 0);
            this.SourceCB.Name = "SourceCB";
            this.SourceCB.Size = new System.Drawing.Size(130, 20);
            this.SourceCB.TabIndex = 1;
            this.SourceCB.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            this.SourceCB.Click += new System.EventHandler(this.SourceCB_Click);
            // 
            // PlotTypeCB
            // 
            this.PlotTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlotTypeCB.FormattingEnabled = true;
            this.PlotTypeCB.Location = new System.Drawing.Point(292, 0);
            this.PlotTypeCB.Name = "PlotTypeCB";
            this.PlotTypeCB.Size = new System.Drawing.Size(130, 20);
            this.PlotTypeCB.TabIndex = 2;
            this.PlotTypeCB.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ColorCB
            // 
            this.ColorCB.BackColor = System.Drawing.SystemColors.Window;
            this.ColorCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColorCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColorCB.FormattingEnabled = true;
            this.ColorCB.Location = new System.Drawing.Point(428, 0);
            this.ColorCB.Name = "ColorCB";
            this.ColorCB.Size = new System.Drawing.Size(130, 20);
            this.ColorCB.TabIndex = 3;
            this.ColorCB.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ShowCB
            // 
            this.ShowCB.AutoSize = true;
            this.ShowCB.Location = new System.Drawing.Point(660, 3);
            this.ShowCB.Name = "ShowCB";
            this.ShowCB.Size = new System.Drawing.Size(15, 14);
            this.ShowCB.TabIndex = 4;
            this.ShowCB.UseVisualStyleBackColor = true;
            this.ShowCB.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ArgumentsButton
            // 
            this.ArgumentsButton.Location = new System.Drawing.Point(681, -1);
            this.ArgumentsButton.Name = "ArgumentsButton";
            this.ArgumentsButton.Size = new System.Drawing.Size(76, 20);
            this.ArgumentsButton.TabIndex = 5;
            this.ArgumentsButton.Text = "Arguments";
            this.ArgumentsButton.UseVisualStyleBackColor = true;
            this.ArgumentsButton.Visible = false;
            this.ArgumentsButton.Click += new System.EventHandler(this.OtherButton_Click);
            // 
            // SizeTB
            // 
            this.SizeTB.Location = new System.Drawing.Point(564, 0);
            this.SizeTB.Name = "SizeTB";
            this.SizeTB.Size = new System.Drawing.Size(90, 19);
            this.SizeTB.TabIndex = 6;
            this.SizeTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nint_KeyDown);
            this.SizeTB.Validating += new System.ComponentModel.CancelEventHandler(this.IsNInt_Validating);
            this.SizeTB.Validated += new System.EventHandler(this.IsNInt_Validated);
            // 
            // EP_NumericalInput
            // 
            this.EP_NumericalInput.ContainerControl = this;
            // 
            // MFSourceCB
            // 
            this.MFSourceCB.FormattingEnabled = true;
            this.MFSourceCB.Location = new System.Drawing.Point(681, 0);
            this.MFSourceCB.Name = "MFSourceCB";
            this.MFSourceCB.Size = new System.Drawing.Size(111, 20);
            this.MFSourceCB.TabIndex = 7;
            this.MFSourceCB.SelectedIndexChanged += new System.EventHandler(this.MFSourceCB_SelectedIndexChanged);
            this.MFSourceCB.SelectedValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // SeriesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.MFSourceCB);
            this.Controls.Add(this.SizeTB);
            this.Controls.Add(this.ArgumentsButton);
            this.Controls.Add(this.ShowCB);
            this.Controls.Add(this.ColorCB);
            this.Controls.Add(this.PlotTypeCB);
            this.Controls.Add(this.SourceCB);
            this.Controls.Add(this.NameTB);
            this.Name = "SeriesEditor";
            this.Size = new System.Drawing.Size(795, 20);
            ((System.ComponentModel.ISupportInitialize)(this.EP_NumericalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.ComboBox SourceCB;
        private System.Windows.Forms.ComboBox PlotTypeCB;
        private System.Windows.Forms.ComboBox ColorCB;
        private System.Windows.Forms.CheckBox ShowCB;
        private System.Windows.Forms.Button ArgumentsButton;
        private System.Windows.Forms.TextBox SizeTB;
        private System.Windows.Forms.ErrorProvider EP_NumericalInput;
        private System.Windows.Forms.ComboBox MFSourceCB;
    }
}
