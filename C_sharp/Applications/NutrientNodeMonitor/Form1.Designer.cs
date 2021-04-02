namespace NutrientNodeMonitor
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_comlist = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_device = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_info = new System.Windows.Forms.Label();
            this.button_scan = new System.Windows.Forms.Button();
            this.groupBox_sensor = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_interval = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel_sensor = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox_control = new System.Windows.Forms.GroupBox();
            this.button_control = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_control = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_on_sec = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_end = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_start = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_on = new System.Windows.Forms.Button();
            this.richTextBox_controlstatus = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_ph = new System.Windows.Forms.TextBox();
            this.radioButton_param = new System.Windows.Forms.RadioButton();
            this.radioButton_area = new System.Windows.Forms.RadioButton();
            this.textBox_ec = new System.Windows.Forms.TextBox();
            this.radioButton_one = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label_nodestatus = new System.Windows.Forms.Label();
            this.comopenbtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox_sensor.SuspendLayout();
            this.groupBox_control.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 34;
            this.label2.Text = "포트번호:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBox_comlist
            // 
            this.comboBox_comlist.FormattingEnabled = true;
            this.comboBox_comlist.Location = new System.Drawing.Point(24, 49);
            this.comboBox_comlist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_comlist.Name = "comboBox_comlist";
            this.comboBox_comlist.Size = new System.Drawing.Size(134, 25);
            this.comboBox_comlist.TabIndex = 32;
            this.comboBox_comlist.SelectedIndexChanged += new System.EventHandler(this.comboBox_comlist_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_device);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.label_info);
            this.groupBox1.Controls.Add(this.button_scan);
            this.groupBox1.Location = new System.Drawing.Point(174, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(954, 196);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "기본정보";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button_device
            // 
            this.button_device.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(252)))), ((int)(((byte)(190)))));
            this.button_device.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_saved_search_black_18dp;
            this.button_device.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_device.Location = new System.Drawing.Point(6, 103);
            this.button_device.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_device.Name = "button_device";
            this.button_device.Size = new System.Drawing.Size(184, 40);
            this.button_device.TabIndex = 18;
            this.button_device.Text = "장치(센서) 검색    ";
            this.button_device.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_device.UseVisualStyleBackColor = false;
            this.button_device.Click += new System.EventHandler(this.button_device_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(264, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(684, 161);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(6, 72);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(17, 17);
            this.label_info.TabIndex = 16;
            this.label_info.Text = "...";
            // 
            // button_scan
            // 
            this.button_scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(252)))), ((int)(((byte)(190)))));
            this.button_scan.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_youtube_searched_for_black_18dp;
            this.button_scan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_scan.Location = new System.Drawing.Point(6, 28);
            this.button_scan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(184, 40);
            this.button_scan.TabIndex = 15;
            this.button_scan.Text = "양액기 검색     ";
            this.button_scan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_scan.UseVisualStyleBackColor = false;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // groupBox_sensor
            // 
            this.groupBox_sensor.Controls.Add(this.label1);
            this.groupBox_sensor.Controls.Add(this.comboBox_interval);
            this.groupBox_sensor.Controls.Add(this.flowLayoutPanel_sensor);
            this.groupBox_sensor.Enabled = false;
            this.groupBox_sensor.Location = new System.Drawing.Point(24, 216);
            this.groupBox_sensor.Name = "groupBox_sensor";
            this.groupBox_sensor.Size = new System.Drawing.Size(1104, 198);
            this.groupBox_sensor.TabIndex = 36;
            this.groupBox_sensor.TabStop = false;
            this.groupBox_sensor.Text = "센서";
            this.groupBox_sensor.Enter += new System.EventHandler(this.groupBox_sensor_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "센서읽기 주기(초)";
            // 
            // comboBox_interval
            // 
            this.comboBox_interval.FormattingEnabled = true;
            this.comboBox_interval.Location = new System.Drawing.Point(9, 52);
            this.comboBox_interval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_interval.Name = "comboBox_interval";
            this.comboBox_interval.Size = new System.Drawing.Size(109, 25);
            this.comboBox_interval.TabIndex = 37;
            this.comboBox_interval.SelectedIndexChanged += new System.EventHandler(this.comboBox_interval_SelectedIndexChanged);
            // 
            // flowLayoutPanel_sensor
            // 
            this.flowLayoutPanel_sensor.AutoScroll = true;
            this.flowLayoutPanel_sensor.Location = new System.Drawing.Point(150, 13);
            this.flowLayoutPanel_sensor.Name = "flowLayoutPanel_sensor";
            this.flowLayoutPanel_sensor.Size = new System.Drawing.Size(948, 179);
            this.flowLayoutPanel_sensor.TabIndex = 0;
            // 
            // groupBox_control
            // 
            this.groupBox_control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(131)))), ((int)(((byte)(80)))));
            this.groupBox_control.Controls.Add(this.button_control);
            this.groupBox_control.Controls.Add(this.label9);
            this.groupBox_control.Controls.Add(this.comboBox_control);
            this.groupBox_control.Controls.Add(this.button2);
            this.groupBox_control.Controls.Add(this.label8);
            this.groupBox_control.Controls.Add(this.textBox_on_sec);
            this.groupBox_control.Controls.Add(this.label7);
            this.groupBox_control.Controls.Add(this.label6);
            this.groupBox_control.Controls.Add(this.label5);
            this.groupBox_control.Controls.Add(this.comboBox_end);
            this.groupBox_control.Controls.Add(this.label4);
            this.groupBox_control.Controls.Add(this.comboBox_start);
            this.groupBox_control.Controls.Add(this.label3);
            this.groupBox_control.Controls.Add(this.button1);
            this.groupBox_control.Controls.Add(this.button_on);
            this.groupBox_control.Controls.Add(this.richTextBox_controlstatus);
            this.groupBox_control.Controls.Add(this.groupBox2);
            this.groupBox_control.Controls.Add(this.groupBox3);
            this.groupBox_control.Enabled = false;
            this.groupBox_control.Location = new System.Drawing.Point(24, 420);
            this.groupBox_control.Name = "groupBox_control";
            this.groupBox_control.Size = new System.Drawing.Size(1104, 276);
            this.groupBox_control.TabIndex = 37;
            this.groupBox_control.TabStop = false;
            this.groupBox_control.Text = "양액기제어 ";
            // 
            // button_control
            // 
            this.button_control.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_read_more_black_18dp;
            this.button_control.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_control.Location = new System.Drawing.Point(22, 78);
            this.button_control.Name = "button_control";
            this.button_control.Size = new System.Drawing.Size(151, 38);
            this.button_control.TabIndex = 56;
            this.button_control.Text = "제어권변경";
            this.button_control.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_control.UseVisualStyleBackColor = true;
            this.button_control.Click += new System.EventHandler(this.button_control_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 17);
            this.label9.TabIndex = 55;
            this.label9.Text = "제어권:";
            // 
            // comboBox_control
            // 
            this.comboBox_control.FormattingEnabled = true;
            this.comboBox_control.Location = new System.Drawing.Point(72, 51);
            this.comboBox_control.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_control.Name = "comboBox_control";
            this.comboBox_control.Size = new System.Drawing.Size(99, 25);
            this.comboBox_control.TabIndex = 54;
            // 
            // button2
            // 
            this.button2.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_replay_black_18dp;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(935, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 44);
            this.button2.TabIndex = 52;
            this.button2.Text = "제어상태 읽기";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(517, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 46;
            this.label8.Text = "pH설정값: ";
            // 
            // textBox_on_sec
            // 
            this.textBox_on_sec.Location = new System.Drawing.Point(426, 157);
            this.textBox_on_sec.Name = "textBox_on_sec";
            this.textBox_on_sec.Size = new System.Drawing.Size(74, 25);
            this.textBox_on_sec.TabIndex = 43;
            this.textBox_on_sec.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(352, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 44;
            this.label7.Text = "EC설정값: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 42;
            this.label6.Text = "관수시간: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 41;
            this.label5.Text = "종료구역: ";
            // 
            // comboBox_end
            // 
            this.comboBox_end.FormattingEnabled = true;
            this.comboBox_end.Location = new System.Drawing.Point(591, 122);
            this.comboBox_end.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_end.Name = "comboBox_end";
            this.comboBox_end.Size = new System.Drawing.Size(74, 25);
            this.comboBox_end.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "시작구역: ";
            // 
            // comboBox_start
            // 
            this.comboBox_start.FormattingEnabled = true;
            this.comboBox_start.Location = new System.Drawing.Point(426, 122);
            this.comboBox_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_start.Name = "comboBox_start";
            this.comboBox_start.Size = new System.Drawing.Size(74, 25);
            this.comboBox_start.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "양액기 제어상태 ";
            // 
            // button1
            // 
            this.button1.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_power_off_black_18dp;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(295, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 44);
            this.button1.TabIndex = 20;
            this.button1.Text = "멈춤(Off)";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_on
            // 
            this.button_on.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_playlist_play_black_18dp;
            this.button_on.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_on.Location = new System.Drawing.Point(466, 23);
            this.button_on.Name = "button_on";
            this.button_on.Size = new System.Drawing.Size(125, 44);
            this.button_on.TabIndex = 19;
            this.button_on.Text = "관수(On)";
            this.button_on.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_on.UseVisualStyleBackColor = true;
            this.button_on.Click += new System.EventHandler(this.button_on_Click);
            // 
            // richTextBox_controlstatus
            // 
            this.richTextBox_controlstatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox_controlstatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_controlstatus.Location = new System.Drawing.Point(724, 78);
            this.richTextBox_controlstatus.Name = "richTextBox_controlstatus";
            this.richTextBox_controlstatus.ReadOnly = true;
            this.richTextBox_controlstatus.Size = new System.Drawing.Size(348, 175);
            this.richTextBox_controlstatus.TabIndex = 18;
            this.richTextBox_controlstatus.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_ph);
            this.groupBox2.Controls.Add(this.radioButton_param);
            this.groupBox2.Controls.Add(this.radioButton_area);
            this.groupBox2.Controls.Add(this.textBox_ec);
            this.groupBox2.Controls.Add(this.radioButton_one);
            this.groupBox2.Location = new System.Drawing.Point(210, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 175);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "관수방식";
            // 
            // textBox_ph
            // 
            this.textBox_ph.Location = new System.Drawing.Point(381, 117);
            this.textBox_ph.Name = "textBox_ph";
            this.textBox_ph.Size = new System.Drawing.Size(74, 25);
            this.textBox_ph.TabIndex = 47;
            // 
            // radioButton_param
            // 
            this.radioButton_param.AutoSize = true;
            this.radioButton_param.Location = new System.Drawing.Point(27, 114);
            this.radioButton_param.Name = "radioButton_param";
            this.radioButton_param.Size = new System.Drawing.Size(83, 21);
            this.radioButton_param.TabIndex = 50;
            this.radioButton_param.TabStop = true;
            this.radioButton_param.Text = "설정관수 ";
            this.radioButton_param.UseVisualStyleBackColor = true;
            this.radioButton_param.CheckedChanged += new System.EventHandler(this.radioButton_param_CheckedChanged);
            // 
            // radioButton_area
            // 
            this.radioButton_area.AutoSize = true;
            this.radioButton_area.Location = new System.Drawing.Point(27, 79);
            this.radioButton_area.Name = "radioButton_area";
            this.radioButton_area.Size = new System.Drawing.Size(83, 21);
            this.radioButton_area.TabIndex = 49;
            this.radioButton_area.TabStop = true;
            this.radioButton_area.Text = "구역관수 ";
            this.radioButton_area.UseVisualStyleBackColor = true;
            this.radioButton_area.CheckedChanged += new System.EventHandler(this.radioButton_area_CheckedChanged);
            // 
            // textBox_ec
            // 
            this.textBox_ec.Location = new System.Drawing.Point(216, 117);
            this.textBox_ec.Name = "textBox_ec";
            this.textBox_ec.Size = new System.Drawing.Size(74, 25);
            this.textBox_ec.TabIndex = 45;
            // 
            // radioButton_one
            // 
            this.radioButton_one.AutoSize = true;
            this.radioButton_one.Location = new System.Drawing.Point(27, 44);
            this.radioButton_one.Name = "radioButton_one";
            this.radioButton_one.Size = new System.Drawing.Size(77, 21);
            this.radioButton_one.TabIndex = 48;
            this.radioButton_one.TabStop = true;
            this.radioButton_one.Text = "1회관수 ";
            this.radioButton_one.UseVisualStyleBackColor = true;
            this.radioButton_one.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label_nodestatus);
            this.groupBox3.Location = new System.Drawing.Point(9, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(195, 228);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "노드제어";
            // 
            // button3
            // 
            this.button3.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_replay_black_18dp;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(12, 97);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 36);
            this.button3.TabIndex = 53;
            this.button3.Text = "노드상태 읽기";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_nodestatus
            // 
            this.label_nodestatus.AutoSize = true;
            this.label_nodestatus.ForeColor = System.Drawing.Color.Lime;
            this.label_nodestatus.Location = new System.Drawing.Point(10, 136);
            this.label_nodestatus.Name = "label_nodestatus";
            this.label_nodestatus.Size = new System.Drawing.Size(68, 17);
            this.label_nodestatus.TabIndex = 0;
            this.label_nodestatus.Text = "노드상태: ";
            // 
            // comopenbtn
            // 
            this.comopenbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(252)))), ((int)(((byte)(190)))));
            this.comopenbtn.Image = global::NutrientNodeMonitor.Properties.Resources.baseline_open_in_browser_black_18dp;
            this.comopenbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comopenbtn.Location = new System.Drawing.Point(24, 85);
            this.comopenbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comopenbtn.Name = "comopenbtn";
            this.comopenbtn.Size = new System.Drawing.Size(134, 41);
            this.comopenbtn.TabIndex = 31;
            this.comopenbtn.Text = "통신포트 열기";
            this.comopenbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.comopenbtn.UseVisualStyleBackColor = false;
            this.comopenbtn.Click += new System.EventHandler(this.comopenbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1140, 708);
            this.Controls.Add(this.groupBox_control);
            this.Controls.Add(this.groupBox_sensor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_comlist);
            this.Controls.Add(this.comopenbtn);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "양액기 모니터 C#";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_sensor.ResumeLayout(false);
            this.groupBox_sensor.PerformLayout();
            this.groupBox_control.ResumeLayout(false);
            this.groupBox_control.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_comlist;
        private System.Windows.Forms.Button comopenbtn;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_device;
        private System.Windows.Forms.GroupBox groupBox_sensor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_sensor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_interval;
        private System.Windows.Forms.GroupBox groupBox_control;
        private System.Windows.Forms.Label label_nodestatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_on;
        private System.Windows.Forms.RichTextBox richTextBox_controlstatus;
        private System.Windows.Forms.TextBox textBox_on_sec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_end;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_start;
        private System.Windows.Forms.TextBox textBox_ph;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_ec;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_param;
        private System.Windows.Forms.RadioButton radioButton_area;
        private System.Windows.Forms.RadioButton radioButton_one;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_control;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_control;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

