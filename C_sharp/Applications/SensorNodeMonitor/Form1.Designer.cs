namespace SensorNodeMonitor
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox_sensor = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_interval = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel_sensor = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_comlist = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_device = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_info = new System.Windows.Forms.Label();
            this.button_scan = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comopenbtn = new System.Windows.Forms.Button();
            this.groupBox_sensor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_sensor
            // 
            this.groupBox_sensor.Controls.Add(this.label1);
            this.groupBox_sensor.Controls.Add(this.comboBox_interval);
            this.groupBox_sensor.Controls.Add(this.flowLayoutPanel_sensor);
            this.groupBox_sensor.Enabled = false;
            this.groupBox_sensor.Location = new System.Drawing.Point(30, 217);
            this.groupBox_sensor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_sensor.Name = "groupBox_sensor";
            this.groupBox_sensor.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_sensor.Size = new System.Drawing.Size(1104, 248);
            this.groupBox_sensor.TabIndex = 41;
            this.groupBox_sensor.TabStop = false;
            this.groupBox_sensor.Text = "센서";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "센서읽기 주기(초)";
            // 
            // comboBox_interval
            // 
            this.comboBox_interval.FormattingEnabled = true;
            this.comboBox_interval.Location = new System.Drawing.Point(9, 65);
            this.comboBox_interval.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBox_interval.Name = "comboBox_interval";
            this.comboBox_interval.Size = new System.Drawing.Size(109, 23);
            this.comboBox_interval.TabIndex = 37;
            this.comboBox_interval.SelectedIndexChanged += new System.EventHandler(this.comboBox_interval_SelectedIndexChanged);
            // 
            // flowLayoutPanel_sensor
            // 
            this.flowLayoutPanel_sensor.AutoScroll = true;
            this.flowLayoutPanel_sensor.Location = new System.Drawing.Point(150, 16);
            this.flowLayoutPanel_sensor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel_sensor.Name = "flowLayoutPanel_sensor";
            this.flowLayoutPanel_sensor.Size = new System.Drawing.Size(948, 224);
            this.flowLayoutPanel_sensor.TabIndex = 0;
            this.flowLayoutPanel_sensor.Click += new System.EventHandler(this.onUCClick);
            this.flowLayoutPanel_sensor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "포트번호:";
            // 
            // comboBox_comlist
            // 
            this.comboBox_comlist.FormattingEnabled = true;
            this.comboBox_comlist.Location = new System.Drawing.Point(29, 61);
            this.comboBox_comlist.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBox_comlist.Name = "comboBox_comlist";
            this.comboBox_comlist.Size = new System.Drawing.Size(134, 23);
            this.comboBox_comlist.TabIndex = 38;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_device);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.label_info);
            this.groupBox1.Controls.Add(this.button_scan);
            this.groupBox1.Location = new System.Drawing.Point(179, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(954, 192);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "기본정보";
            // 
            // button_device
            // 
            this.button_device.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button_device.Image = global::SensorNodeMonitor.Properties.Resources.baseline_saved_search_black_18dp;
            this.button_device.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_device.Location = new System.Drawing.Point(6, 129);
            this.button_device.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_device.Name = "button_device";
            this.button_device.Size = new System.Drawing.Size(184, 50);
            this.button_device.TabIndex = 18;
            this.button_device.Text = "센서장치 검색    ";
            this.button_device.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_device.UseVisualStyleBackColor = false;
            this.button_device.Click += new System.EventHandler(this.button_device_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(264, 19);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(684, 160);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(6, 90);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(16, 15);
            this.label_info.TabIndex = 16;
            this.label_info.Text = "...";
            // 
            // button_scan
            // 
            this.button_scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button_scan.Image = global::SensorNodeMonitor.Properties.Resources.baseline_youtube_searched_for_black_18dp;
            this.button_scan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_scan.Location = new System.Drawing.Point(6, 35);
            this.button_scan.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(184, 50);
            this.button_scan.TabIndex = 15;
            this.button_scan.Text = "센서노드 검색     ";
            this.button_scan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_scan.UseVisualStyleBackColor = false;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // chart1
            // 
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(29, 472);
            this.chart1.Name = "chart1";
            series13.ChartArea = "ChartArea1";
            series13.Legend = "Legend1";
            series13.Name = "Series1";
            series14.ChartArea = "ChartArea1";
            series14.Legend = "Legend1";
            series14.Name = "Series2";
            series15.ChartArea = "ChartArea1";
            series15.Legend = "Legend1";
            series15.Name = "Series3";
            series16.ChartArea = "ChartArea1";
            series16.Legend = "Legend1";
            series16.Name = "Series4";
            this.chart1.Series.Add(series13);
            this.chart1.Series.Add(series14);
            this.chart1.Series.Add(series15);
            this.chart1.Series.Add(series16);
            this.chart1.Size = new System.Drawing.Size(1104, 312);
            this.chart1.TabIndex = 44;
            this.chart1.Text = "chart1";
            // 
            // comopenbtn
            // 
            this.comopenbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.comopenbtn.Image = global::SensorNodeMonitor.Properties.Resources.baseline_open_in_browser_black_18dp;
            this.comopenbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comopenbtn.Location = new System.Drawing.Point(29, 106);
            this.comopenbtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comopenbtn.Name = "comopenbtn";
            this.comopenbtn.Size = new System.Drawing.Size(134, 51);
            this.comopenbtn.TabIndex = 37;
            this.comopenbtn.Text = "통신포트 열기";
            this.comopenbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.comopenbtn.UseVisualStyleBackColor = false;
            this.comopenbtn.Click += new System.EventHandler(this.comopenbtn_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1151, 789);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBox_sensor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox_comlist);
            this.Controls.Add(this.comopenbtn);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "센서노드 모니터 C#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_sensor.ResumeLayout(false);
            this.groupBox_sensor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_sensor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_interval;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_sensor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_comlist;
        private System.Windows.Forms.Button comopenbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_device;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Button button_scan;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;

    }
}

