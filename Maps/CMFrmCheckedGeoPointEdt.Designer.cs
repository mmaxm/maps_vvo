namespace Maps
{
    partial class CMFrmCheckedGeoPointEdt
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
            this.lb_longtitude = new System.Windows.Forms.Label();
            this.Lng_SecVal = new System.Windows.Forms.NumericUpDown();
            this.Lng_MinVal = new System.Windows.Forms.NumericUpDown();
            this.Lng_GrVal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.PHeight = new System.Windows.Forms.NumericUpDown();
            this.lb_Latitude = new System.Windows.Forms.Label();
            this.Lat_SecVal = new System.Windows.Forms.NumericUpDown();
            this.Lat_MinVal = new System.Windows.Forms.NumericUpDown();
            this.Lat_GrVal = new System.Windows.Forms.NumericUpDown();
            this.lblText = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.nudAzimut = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDistance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Lng_SecVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lng_MinVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lng_GrVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_SecVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_MinVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_GrVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAzimut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_longtitude
            // 
            this.lb_longtitude.AutoSize = true;
            this.lb_longtitude.Location = new System.Drawing.Point(247, 30);
            this.lb_longtitude.Name = "lb_longtitude";
            this.lb_longtitude.Size = new System.Drawing.Size(50, 13);
            this.lb_longtitude.TabIndex = 62;
            this.lb_longtitude.Text = "Долгота";
            // 
            // Lng_SecVal
            // 
            this.Lng_SecVal.DecimalPlaces = 2;
            this.Lng_SecVal.Location = new System.Drawing.Point(400, 26);
            this.Lng_SecVal.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Lng_SecVal.Name = "Lng_SecVal";
            this.Lng_SecVal.Size = new System.Drawing.Size(52, 20);
            this.Lng_SecVal.TabIndex = 61;
            this.Lng_SecVal.Value = new decimal(new int[] {
            56,
            0,
            0,
            0});
            // 
            // Lng_MinVal
            // 
            this.Lng_MinVal.Location = new System.Drawing.Point(349, 26);
            this.Lng_MinVal.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Lng_MinVal.Name = "Lng_MinVal";
            this.Lng_MinVal.Size = new System.Drawing.Size(48, 20);
            this.Lng_MinVal.TabIndex = 60;
            this.Lng_MinVal.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // Lng_GrVal
            // 
            this.Lng_GrVal.Location = new System.Drawing.Point(299, 26);
            this.Lng_GrVal.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Lng_GrVal.Name = "Lng_GrVal";
            this.Lng_GrVal.Size = new System.Drawing.Size(48, 20);
            this.Lng_GrVal.TabIndex = 59;
            this.Lng_GrVal.Value = new decimal(new int[] {
            132,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "H абс";
            // 
            // PHeight
            // 
            this.PHeight.DecimalPlaces = 2;
            this.PHeight.Location = new System.Drawing.Point(58, 72);
            this.PHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.PHeight.Name = "PHeight";
            this.PHeight.Size = new System.Drawing.Size(78, 20);
            this.PHeight.TabIndex = 57;
            this.PHeight.Value = new decimal(new int[] {
            171,
            0,
            0,
            65536});
            // 
            // lb_Latitude
            // 
            this.lb_Latitude.AutoSize = true;
            this.lb_Latitude.Location = new System.Drawing.Point(38, 30);
            this.lb_Latitude.Name = "lb_Latitude";
            this.lb_Latitude.Size = new System.Drawing.Size(45, 13);
            this.lb_Latitude.TabIndex = 56;
            this.lb_Latitude.Text = "Широта";
            // 
            // Lat_SecVal
            // 
            this.Lat_SecVal.DecimalPlaces = 2;
            this.Lat_SecVal.Location = new System.Drawing.Point(191, 26);
            this.Lat_SecVal.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Lat_SecVal.Name = "Lat_SecVal";
            this.Lat_SecVal.Size = new System.Drawing.Size(52, 20);
            this.Lat_SecVal.TabIndex = 55;
            this.Lat_SecVal.Value = new decimal(new int[] {
            53,
            0,
            0,
            0});
            // 
            // Lat_MinVal
            // 
            this.Lat_MinVal.Location = new System.Drawing.Point(140, 26);
            this.Lat_MinVal.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Lat_MinVal.Name = "Lat_MinVal";
            this.Lat_MinVal.Size = new System.Drawing.Size(48, 20);
            this.Lat_MinVal.TabIndex = 54;
            this.Lat_MinVal.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // Lat_GrVal
            // 
            this.Lat_GrVal.Location = new System.Drawing.Point(90, 26);
            this.Lat_GrVal.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Lat_GrVal.Name = "Lat_GrVal";
            this.Lat_GrVal.Size = new System.Drawing.Size(48, 20);
            this.Lat_GrVal.TabIndex = 53;
            this.Lat_GrVal.Value = new decimal(new int[] {
            43,
            0,
            0,
            0});
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(1, 6);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(83, 13);
            this.lblText.TabIndex = 63;
            this.lblText.Text = "Наименование";
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(90, 3);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(362, 20);
            this.txtText.TabIndex = 64;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(305, 163);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(65, 23);
            this.cmdCancel.TabIndex = 65;
            this.cmdCancel.Text = "Отмена";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(377, 163);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 66;
            this.cmdOk.Text = "Ок";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(6, 28);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(31, 17);
            this.rb1.TabIndex = 67;
            this.rb1.TabStop = true;
            this.rb1.Text = "1";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(6, 51);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(31, 17);
            this.rb2.TabIndex = 68;
            this.rb2.Text = "2";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb2_CheckedChanged);
            // 
            // nudAzimut
            // 
            this.nudAzimut.DecimalPlaces = 2;
            this.nudAzimut.Location = new System.Drawing.Point(90, 49);
            this.nudAzimut.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudAzimut.Name = "nudAzimut";
            this.nudAzimut.Size = new System.Drawing.Size(59, 20);
            this.nudAzimut.TabIndex = 70;
            this.nudAzimut.Leave += new System.EventHandler(this.nudAzimut_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Азимут:";
            // 
            // nudDistance
            // 
            this.nudDistance.DecimalPlaces = 2;
            this.nudDistance.Location = new System.Drawing.Point(227, 49);
            this.nudDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDistance.Name = "nudDistance";
            this.nudDistance.Size = new System.Drawing.Size(86, 20);
            this.nudDistance.TabIndex = 72;
            this.nudDistance.Leave += new System.EventHandler(this.nudDistance_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Дальность:";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(4, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(448, 18);
            this.label6.TabIndex = 78;
            this.label6.Text = "Примечание:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(4, 112);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(448, 48);
            this.txtDescription.TabIndex = 79;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "от КТА";
            // 
            // CMFrmCheckedGeoPointEdt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 186);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudDistance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudAzimut);
            this.Controls.Add(this.rb2);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lb_longtitude);
            this.Controls.Add(this.Lng_SecVal);
            this.Controls.Add(this.Lng_MinVal);
            this.Controls.Add(this.Lng_GrVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PHeight);
            this.Controls.Add(this.lb_Latitude);
            this.Controls.Add(this.Lat_SecVal);
            this.Controls.Add(this.Lat_MinVal);
            this.Controls.Add(this.Lat_GrVal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CMFrmCheckedGeoPointEdt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные по препятствию";
            this.Load += new System.EventHandler(this.CMFrmCheckedGeoPointEdt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Lng_SecVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lng_MinVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lng_GrVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_SecVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_MinVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat_GrVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAzimut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_longtitude;
        private System.Windows.Forms.NumericUpDown Lng_SecVal;
        private System.Windows.Forms.NumericUpDown Lng_MinVal;
        private System.Windows.Forms.NumericUpDown Lng_GrVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown PHeight;
        private System.Windows.Forms.Label lb_Latitude;
        private System.Windows.Forms.NumericUpDown Lat_SecVal;
        private System.Windows.Forms.NumericUpDown Lat_MinVal;
        private System.Windows.Forms.NumericUpDown Lat_GrVal;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.NumericUpDown nudAzimut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDistance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
    }
}