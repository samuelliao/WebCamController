namespace CameraController
{
    partial class MainForm
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
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.cameraPropertyValueAuto = new System.Windows.Forms.CheckBox();
            this.cameraPropertyValueValue = new System.Windows.Forms.NumericUpDown();
            this.cameraPropertyValueTypeSelection = new System.Windows.Forms.ComboBox();
            this.cameraPropertyRangeValue = new System.Windows.Forms.Label();
            this.cameraPropertyRangeTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cameraPropertyTitle = new System.Windows.Forms.Label();
            this.cameraPropertyValue = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDisplay.Location = new System.Drawing.Point(26, 22);
            this.pictureBoxDisplay.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(1335, 394);
            this.pictureBoxDisplay.TabIndex = 13;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.Visible = false;
            // 
            // cameraPropertyValueAuto
            // 
            this.cameraPropertyValueAuto.AutoSize = true;
            this.cameraPropertyValueAuto.Enabled = false;
            this.cameraPropertyValueAuto.Location = new System.Drawing.Point(1280, 494);
            this.cameraPropertyValueAuto.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cameraPropertyValueAuto.Name = "cameraPropertyValueAuto";
            this.cameraPropertyValueAuto.Size = new System.Drawing.Size(96, 28);
            this.cameraPropertyValueAuto.TabIndex = 48;
            this.cameraPropertyValueAuto.Text = "Auto?";
            this.cameraPropertyValueAuto.UseVisualStyleBackColor = true;
            this.cameraPropertyValueAuto.CheckedChanged += new System.EventHandler(this.cameraPropertyValueAuto_CheckedChanged);
            // 
            // cameraPropertyValueValue
            // 
            this.cameraPropertyValueValue.Enabled = false;
            this.cameraPropertyValueValue.Location = new System.Drawing.Point(1134, 492);
            this.cameraPropertyValueValue.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cameraPropertyValueValue.Name = "cameraPropertyValueValue";
            this.cameraPropertyValueValue.Size = new System.Drawing.Size(132, 36);
            this.cameraPropertyValueValue.TabIndex = 47;
            this.cameraPropertyValueValue.ValueChanged += new System.EventHandler(this.cameraPropertyValueValue_ValueChanged);
            this.cameraPropertyValueValue.EnabledChanged += new System.EventHandler(this.cameraPropertyValueValue_EnabledChanged);
            // 
            // cameraPropertyValueTypeSelection
            // 
            this.cameraPropertyValueTypeSelection.Enabled = false;
            this.cameraPropertyValueTypeSelection.FormattingEnabled = true;
            this.cameraPropertyValueTypeSelection.Items.AddRange(new object[] {
            "Value",
            "Percentage"});
            this.cameraPropertyValueTypeSelection.Location = new System.Drawing.Point(1002, 491);
            this.cameraPropertyValueTypeSelection.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cameraPropertyValueTypeSelection.Name = "cameraPropertyValueTypeSelection";
            this.cameraPropertyValueTypeSelection.Size = new System.Drawing.Size(115, 32);
            this.cameraPropertyValueTypeSelection.TabIndex = 46;
            this.cameraPropertyValueTypeSelection.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValueTypeSelection_SelectedIndexChanged);
            // 
            // cameraPropertyRangeValue
            // 
            this.cameraPropertyRangeValue.AutoSize = true;
            this.cameraPropertyRangeValue.Location = new System.Drawing.Point(757, 496);
            this.cameraPropertyRangeValue.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.cameraPropertyRangeValue.Name = "cameraPropertyRangeValue";
            this.cameraPropertyRangeValue.Size = new System.Drawing.Size(84, 24);
            this.cameraPropertyRangeValue.TabIndex = 45;
            this.cameraPropertyRangeValue.Text = "<value>";
            // 
            // cameraPropertyRangeTitle
            // 
            this.cameraPropertyRangeTitle.AutoSize = true;
            this.cameraPropertyRangeTitle.Location = new System.Drawing.Point(660, 496);
            this.cameraPropertyRangeTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.cameraPropertyRangeTitle.Name = "cameraPropertyRangeTitle";
            this.cameraPropertyRangeTitle.Size = new System.Drawing.Size(67, 24);
            this.cameraPropertyRangeTitle.TabIndex = 44;
            this.cameraPropertyRangeTitle.Text = "Range";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(916, 496);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 24);
            this.label1.TabIndex = 43;
            this.label1.Text = "Value";
            // 
            // cameraPropertyTitle
            // 
            this.cameraPropertyTitle.AutoSize = true;
            this.cameraPropertyTitle.Location = new System.Drawing.Point(38, 496);
            this.cameraPropertyTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.cameraPropertyTitle.Name = "cameraPropertyTitle";
            this.cameraPropertyTitle.Size = new System.Drawing.Size(88, 24);
            this.cameraPropertyTitle.TabIndex = 42;
            this.cameraPropertyTitle.Text = "Property";
            // 
            // cameraPropertyValue
            // 
            this.cameraPropertyValue.Enabled = false;
            this.cameraPropertyValue.FormattingEnabled = true;
            this.cameraPropertyValue.Location = new System.Drawing.Point(151, 491);
            this.cameraPropertyValue.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cameraPropertyValue.Name = "cameraPropertyValue";
            this.cameraPropertyValue.Size = new System.Drawing.Size(333, 32);
            this.cameraPropertyValue.TabIndex = 41;
            this.cameraPropertyValue.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValue_SelectedIndexChanged);
            this.cameraPropertyValue.EnabledChanged += new System.EventHandler(this.cameraPropertyValue_EnabledChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(983, 428);
            this.btnSave.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(189, 42);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Save current";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(413, 428);
            this.comboBoxCameras.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(327, 32);
            this.comboBoxCameras.TabIndex = 39;
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(1185, 428);
            this.btnConfig.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(189, 42);
            this.btnConfig.TabIndex = 38;
            this.btnConfig.Text = "Configuration";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Location = new System.Drawing.Point(214, 428);
            this.btnStop.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(163, 42);
            this.btnStop.TabIndex = 37;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(38, 428);
            this.btnStart.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(163, 42);
            this.btnStart.TabIndex = 36;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 590);
            this.Controls.Add(this.cameraPropertyValueAuto);
            this.Controls.Add(this.cameraPropertyValueValue);
            this.Controls.Add(this.cameraPropertyValueTypeSelection);
            this.Controls.Add(this.cameraPropertyRangeValue);
            this.Controls.Add(this.cameraPropertyRangeTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cameraPropertyTitle);
            this.Controls.Add(this.cameraPropertyValue);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBoxDisplay);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "WebCam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.CheckBox cameraPropertyValueAuto;
        private System.Windows.Forms.NumericUpDown cameraPropertyValueValue;
        private System.Windows.Forms.ComboBox cameraPropertyValueTypeSelection;
        private System.Windows.Forms.Label cameraPropertyRangeValue;
        private System.Windows.Forms.Label cameraPropertyRangeTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label cameraPropertyTitle;
        private System.Windows.Forms.ComboBox cameraPropertyValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}

