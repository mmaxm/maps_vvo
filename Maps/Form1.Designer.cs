namespace Maps
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                polyOverlay.Dispose();
                polyOverlay = null;
                if (z != null)
                {
                    z.Dispose();
                    z = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gmaper = new GMap.NET.WindowsForms.GMapControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.cmdTest = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.rtxtDotDescription = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbListCoursesLA = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btnShowAPControlPoints = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.TextResult = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.nudMapZoom = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurPosition = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nudAzimut = new System.Windows.Forms.NumericUpDown();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAzimut)).BeginInit();
            this.SuspendLayout();
            // 
            // gmaper
            // 
            this.gmaper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmaper.Bearing = 0F;
            this.gmaper.CanDragMap = true;
            this.gmaper.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmaper.GrayScaleMode = false;
            this.gmaper.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmaper.LevelsKeepInMemmory = 5;
            this.gmaper.Location = new System.Drawing.Point(362, 31);
            this.gmaper.MarkersEnabled = true;
            this.gmaper.MaxZoom = 20;
            this.gmaper.MinZoom = 5;
            this.gmaper.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmaper.Name = "gmaper";
            this.gmaper.NegativeMode = false;
            this.gmaper.PolygonsEnabled = true;
            this.gmaper.RetryLoadTile = 0;
            this.gmaper.RoutesEnabled = true;
            this.gmaper.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmaper.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmaper.ShowTileGridLines = false;
            this.gmaper.Size = new System.Drawing.Size(742, 456);
            this.gmaper.TabIndex = 2;
            this.gmaper.Zoom = 12D;
            this.gmaper.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmaper_OnMarkerClick);
            this.gmaper.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmaper_OnMapZoomChanged);
            this.gmaper.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gmaper_MouseDoubleClick);
            this.gmaper.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gmaper_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btnFileOpen);
            this.panel1.Controls.Add(this.btnFileSave);
            this.panel1.Controls.Add(this.cmdTest);
            this.panel1.Controls.Add(this.cmdAdd);
            this.panel1.Controls.Add(this.rtxtDotDescription);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbListCoursesLA);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.btnShowAPControlPoints);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.TextResult);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(-5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 484);
            this.panel1.TabIndex = 34;
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(259, 50);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(75, 23);
            this.btnFileOpen.TabIndex = 64;
            this.btnFileOpen.Text = "Открыть";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // btnFileSave
            // 
            this.btnFileSave.Location = new System.Drawing.Point(187, 50);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(69, 23);
            this.btnFileSave.TabIndex = 63;
            this.btnFileSave.Text = "Сохранить";
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(339, 50);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(21, 23);
            this.cmdTest.TabIndex = 62;
            this.cmdTest.Text = "Test";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(5, 50);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(65, 23);
            this.cmdAdd.TabIndex = 61;
            this.cmdAdd.Text = "Добавить";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // rtxtDotDescription
            // 
            this.rtxtDotDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDotDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rtxtDotDescription.Location = new System.Drawing.Point(4, 185);
            this.rtxtDotDescription.Name = "rtxtDotDescription";
            this.rtxtDotDescription.Size = new System.Drawing.Size(357, 147);
            this.rtxtDotDescription.TabIndex = 60;
            this.rtxtDotDescription.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(4, 76);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(356, 108);
            this.listBox1.TabIndex = 59;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Порог ВПП";
            // 
            // cmbListCoursesLA
            // 
            this.cmbListCoursesLA.FormattingEnabled = true;
            this.cmbListCoursesLA.Location = new System.Drawing.Point(76, 4);
            this.cmbListCoursesLA.Name = "cmbListCoursesLA";
            this.cmbListCoursesLA.Size = new System.Drawing.Size(112, 21);
            this.cmbListCoursesLA.TabIndex = 57;
            this.cmbListCoursesLA.SelectedIndexChanged += new System.EventHandler(this.cmbListCoursesLA_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(303, 20);
            this.textBox1.TabIndex = 56;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(47, 23);
            this.button5.TabIndex = 55;
            this.button5.Text = "Найти";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnShowAPControlPoints
            // 
            this.btnShowAPControlPoints.Location = new System.Drawing.Point(194, 3);
            this.btnShowAPControlPoints.Name = "btnShowAPControlPoints";
            this.btnShowAPControlPoints.Size = new System.Drawing.Size(118, 23);
            this.btnShowAPControlPoints.TabIndex = 54;
            this.btnShowAPControlPoints.Text = "Контрольные точки";
            this.btnShowAPControlPoints.UseVisualStyleBackColor = true;
            this.btnShowAPControlPoints.Click += new System.EventHandler(this.btnShowAPControlPoints_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(71, 50);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(62, 23);
            this.btnClear.TabIndex = 53;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // TextResult
            // 
            this.TextResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.TextResult.Location = new System.Drawing.Point(4, 355);
            this.TextResult.Name = "TextResult";
            this.TextResult.Size = new System.Drawing.Size(357, 126);
            this.TextResult.TabIndex = 46;
            this.TextResult.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Журнал";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "Отчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(339, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(21, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "Отключить не пересекаемые плоскости";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(530, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 22);
            this.button4.TabIndex = 51;
            this.button4.Text = "Выбрать";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(362, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 50;
            // 
            // nudMapZoom
            // 
            this.nudMapZoom.Location = new System.Drawing.Point(681, 5);
            this.nudMapZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMapZoom.Name = "nudMapZoom";
            this.nudMapZoom.Size = new System.Drawing.Size(49, 20);
            this.nudMapZoom.TabIndex = 35;
            this.nudMapZoom.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudMapZoom.ValueChanged += new System.EventHandler(this.nudMapZoom_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "К увеличения";
            // 
            // lblCurPosition
            // 
            this.lblCurPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurPosition.Location = new System.Drawing.Point(738, 6);
            this.lblCurPosition.Name = "lblCurPosition";
            this.lblCurPosition.Size = new System.Drawing.Size(142, 19);
            this.lblCurPosition.TabIndex = 37;
            this.lblCurPosition.Text = "label4";
            // 
            // nudAzimut
            // 
            this.nudAzimut.Location = new System.Drawing.Point(893, 5);
            this.nudAzimut.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudAzimut.Name = "nudAzimut";
            this.nudAzimut.Size = new System.Drawing.Size(49, 20);
            this.nudAzimut.TabIndex = 52;
            this.nudAzimut.ValueChanged += new System.EventHandler(this.nudAzimut_ValueChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "csv|*.csv|All files|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "csv|*.csv|All files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 487);
            this.Controls.Add(this.nudAzimut);
            this.Controls.Add(this.lblCurPosition);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudMapZoom);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gmaper);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Аэропорт Владивосток";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAzimut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public GMap.NET.WindowsForms.GMapControl gmaper;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RichTextBox TextResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown nudMapZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurPosition;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnShowAPControlPoints;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbListCoursesLA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox rtxtDotDescription;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.NumericUpDown nudAzimut;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

