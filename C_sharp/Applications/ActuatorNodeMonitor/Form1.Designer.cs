namespace ActuatorNodeMonitorApp
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
			if( disposing && (components != null) )
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gb_retractable = new System.Windows.Forms.GroupBox();
            this.gb_setConfig = new System.Windows.Forms.GroupBox();
            this.lb_closeTime = new System.Windows.Forms.Label();
            this.lb_openTime = new System.Windows.Forms.Label();
            this.tb_closeTime = new System.Windows.Forms.TextBox();
            this.tb_openTime = new System.Windows.Forms.TextBox();
            this.bt_setConfig = new System.Windows.Forms.Button();
            this.bt_retStop = new System.Windows.Forms.Button();
            this.tb_retTime = new System.Windows.Forms.TextBox();
            this.tb_retPosition = new System.Windows.Forms.TextBox();
            this.rb_retPositionControl = new System.Windows.Forms.RadioButton();
            this.rb_retTimeControl = new System.Windows.Forms.RadioButton();
            this.rb_retbasicControl = new System.Windows.Forms.RadioButton();
            this.bt_retOff = new System.Windows.Forms.Button();
            this.bt_retOn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_comlist = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_device = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_info = new System.Windows.Forms.Label();
            this.button_scan = new System.Windows.Forms.Button();
            this.rb_switchBasicControl = new System.Windows.Forms.RadioButton();
            this.rb_switchTimeControl = new System.Windows.Forms.RadioButton();
            this.rb_switchRatioControl = new System.Windows.Forms.RadioButton();
            this.tb_switchTime = new System.Windows.Forms.TextBox();
            this.tb_switchRatio = new System.Windows.Forms.TextBox();
            this.gb_switch = new System.Windows.Forms.GroupBox();
            this.bt_switchOff = new System.Windows.Forms.Button();
            this.bt_switchOn = new System.Windows.Forms.Button();
            this.comopenbtn = new System.Windows.Forms.Button();
            this.gb_retractable.SuspendLayout();
            this.gb_setConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gb_switch.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 205);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(807, 322);
            this.flowLayoutPanel1.TabIndex = 44;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(20, 545);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(805, 343);
            this.flowLayoutPanel2.TabIndex = 45;
            // 
            // gb_retractable
            // 
            this.gb_retractable.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gb_retractable.Controls.Add(this.gb_setConfig);
            this.gb_retractable.Controls.Add(this.bt_retStop);
            this.gb_retractable.Controls.Add(this.tb_retTime);
            this.gb_retractable.Controls.Add(this.tb_retPosition);
            this.gb_retractable.Controls.Add(this.rb_retPositionControl);
            this.gb_retractable.Controls.Add(this.rb_retTimeControl);
            this.gb_retractable.Controls.Add(this.rb_retbasicControl);
            this.gb_retractable.Controls.Add(this.bt_retOff);
            this.gb_retractable.Controls.Add(this.bt_retOn);
            this.gb_retractable.Location = new System.Drawing.Point(831, 545);
            this.gb_retractable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_retractable.Name = "gb_retractable";
            this.gb_retractable.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_retractable.Size = new System.Drawing.Size(292, 343);
            this.gb_retractable.TabIndex = 47;
            this.gb_retractable.TabStop = false;
            this.gb_retractable.Text = "개폐기제어";
            this.gb_retractable.Visible = false;
            // 
            // gb_setConfig
            // 
            this.gb_setConfig.Controls.Add(this.lb_closeTime);
            this.gb_setConfig.Controls.Add(this.lb_openTime);
            this.gb_setConfig.Controls.Add(this.tb_closeTime);
            this.gb_setConfig.Controls.Add(this.tb_openTime);
            this.gb_setConfig.Controls.Add(this.bt_setConfig);
            this.gb_setConfig.Location = new System.Drawing.Point(7, 169);
            this.gb_setConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_setConfig.Name = "gb_setConfig";
            this.gb_setConfig.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_setConfig.Size = new System.Drawing.Size(272, 89);
            this.gb_setConfig.TabIndex = 15;
            this.gb_setConfig.TabStop = false;
            this.gb_setConfig.Text = "구동기설정";
            // 
            // lb_closeTime
            // 
            this.lb_closeTime.AutoSize = true;
            this.lb_closeTime.Location = new System.Drawing.Point(114, 25);
            this.lb_closeTime.Name = "lb_closeTime";
            this.lb_closeTime.Size = new System.Drawing.Size(55, 15);
            this.lb_closeTime.TabIndex = 14;
            this.lb_closeTime.Text = "닫힘시간";
            // 
            // lb_openTime
            // 
            this.lb_openTime.AutoSize = true;
            this.lb_openTime.Location = new System.Drawing.Point(20, 25);
            this.lb_openTime.Name = "lb_openTime";
            this.lb_openTime.Size = new System.Drawing.Size(55, 15);
            this.lb_openTime.TabIndex = 13;
            this.lb_openTime.Text = "열림시간";
            // 
            // tb_closeTime
            // 
            this.tb_closeTime.Location = new System.Drawing.Point(105, 48);
            this.tb_closeTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_closeTime.Name = "tb_closeTime";
            this.tb_closeTime.Size = new System.Drawing.Size(76, 23);
            this.tb_closeTime.TabIndex = 12;
            // 
            // tb_openTime
            // 
            this.tb_openTime.Location = new System.Drawing.Point(11, 48);
            this.tb_openTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_openTime.Name = "tb_openTime";
            this.tb_openTime.Size = new System.Drawing.Size(76, 23);
            this.tb_openTime.TabIndex = 11;
            // 
            // bt_setConfig
            // 
            this.bt_setConfig.BackColor = System.Drawing.Color.Cyan;
            this.bt_setConfig.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_read_more_black_18dp;
            this.bt_setConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_setConfig.Location = new System.Drawing.Point(187, 25);
            this.bt_setConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_setConfig.Name = "bt_setConfig";
            this.bt_setConfig.Size = new System.Drawing.Size(78, 46);
            this.bt_setConfig.TabIndex = 10;
            this.bt_setConfig.Text = "설정";
            this.bt_setConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_setConfig.UseVisualStyleBackColor = false;
            this.bt_setConfig.Click += new System.EventHandler(this.button6_Click);
            // 
            // bt_retStop
            // 
            this.bt_retStop.BackColor = System.Drawing.Color.Cyan;
            this.bt_retStop.Location = new System.Drawing.Point(105, 266);
            this.bt_retStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_retStop.Name = "bt_retStop";
            this.bt_retStop.Size = new System.Drawing.Size(75, 45);
            this.bt_retStop.TabIndex = 8;
            this.bt_retStop.Text = "정지";
            this.bt_retStop.UseVisualStyleBackColor = false;
            this.bt_retStop.Click += new System.EventHandler(this.button4_Click);
            // 
            // tb_retTime
            // 
            this.tb_retTime.Location = new System.Drawing.Point(136, 77);
            this.tb_retTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_retTime.Name = "tb_retTime";
            this.tb_retTime.Size = new System.Drawing.Size(100, 23);
            this.tb_retTime.TabIndex = 7;
            this.tb_retTime.TextChanged += new System.EventHandler(this.tb_retTime_TextChanged);
            // 
            // tb_retPosition
            // 
            this.tb_retPosition.Location = new System.Drawing.Point(136, 117);
            this.tb_retPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_retPosition.Name = "tb_retPosition";
            this.tb_retPosition.Size = new System.Drawing.Size(100, 23);
            this.tb_retPosition.TabIndex = 6;
            // 
            // rb_retPositionControl
            // 
            this.rb_retPositionControl.AutoSize = true;
            this.rb_retPositionControl.Location = new System.Drawing.Point(17, 121);
            this.rb_retPositionControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_retPositionControl.Name = "rb_retPositionControl";
            this.rb_retPositionControl.Size = new System.Drawing.Size(87, 19);
            this.rb_retPositionControl.TabIndex = 5;
            this.rb_retPositionControl.TabStop = true;
            this.rb_retPositionControl.Text = "폭 제어 (%)";
            this.rb_retPositionControl.UseVisualStyleBackColor = true;
            // 
            // rb_retTimeControl
            // 
            this.rb_retTimeControl.AutoSize = true;
            this.rb_retTimeControl.Location = new System.Drawing.Point(17, 81);
            this.rb_retTimeControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_retTimeControl.Name = "rb_retTimeControl";
            this.rb_retTimeControl.Size = new System.Drawing.Size(97, 19);
            this.rb_retTimeControl.TabIndex = 4;
            this.rb_retTimeControl.TabStop = true;
            this.rb_retTimeControl.Text = "시간제어 (초)";
            this.rb_retTimeControl.UseVisualStyleBackColor = true;
            // 
            // rb_retbasicControl
            // 
            this.rb_retbasicControl.AutoSize = true;
            this.rb_retbasicControl.Location = new System.Drawing.Point(17, 40);
            this.rb_retbasicControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_retbasicControl.Name = "rb_retbasicControl";
            this.rb_retbasicControl.Size = new System.Drawing.Size(77, 19);
            this.rb_retbasicControl.TabIndex = 3;
            this.rb_retbasicControl.TabStop = true;
            this.rb_retbasicControl.Text = "일반 제어";
            this.rb_retbasicControl.UseVisualStyleBackColor = true;
            // 
            // bt_retOff
            // 
            this.bt_retOff.BackColor = System.Drawing.Color.Cyan;
            this.bt_retOff.Location = new System.Drawing.Point(195, 266);
            this.bt_retOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_retOff.Name = "bt_retOff";
            this.bt_retOff.Size = new System.Drawing.Size(75, 45);
            this.bt_retOff.TabIndex = 2;
            this.bt_retOff.Text = "닫기";
            this.bt_retOff.UseVisualStyleBackColor = false;
            this.bt_retOff.Click += new System.EventHandler(this.button5_Click);
            // 
            // bt_retOn
            // 
            this.bt_retOn.BackColor = System.Drawing.Color.Cyan;
            this.bt_retOn.Location = new System.Drawing.Point(14, 266);
            this.bt_retOn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_retOn.Name = "bt_retOn";
            this.bt_retOn.Size = new System.Drawing.Size(75, 45);
            this.bt_retOn.TabIndex = 1;
            this.bt_retOn.Text = "열기";
            this.bt_retOn.UseVisualStyleBackColor = false;
            this.bt_retOn.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "포트번호:";
            // 
            // comboBox_comlist
            // 
            this.comboBox_comlist.FormattingEnabled = true;
            this.comboBox_comlist.Location = new System.Drawing.Point(19, 49);
            this.comboBox_comlist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_comlist.Name = "comboBox_comlist";
            this.comboBox_comlist.Size = new System.Drawing.Size(134, 23);
            this.comboBox_comlist.TabIndex = 49;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_device);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.label_info);
            this.groupBox1.Controls.Add(this.button_scan);
            this.groupBox1.Location = new System.Drawing.Point(169, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(954, 175);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "기본정보";
            // 
            // button_device
            // 
            this.button_device.BackColor = System.Drawing.Color.Cyan;
            this.button_device.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_saved_search_black_18dp;
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
            this.richTextBox1.Location = new System.Drawing.Point(264, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(684, 154);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(6, 72);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(16, 15);
            this.label_info.TabIndex = 16;
            this.label_info.Text = "...";
            // 
            // button_scan
            // 
            this.button_scan.BackColor = System.Drawing.Color.Cyan;
            this.button_scan.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_youtube_searched_for_black_18dp;
            this.button_scan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_scan.Location = new System.Drawing.Point(6, 28);
            this.button_scan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(184, 40);
            this.button_scan.TabIndex = 15;
            this.button_scan.Text = "구동기노드 검색     ";
            this.button_scan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_scan.UseVisualStyleBackColor = false;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // rb_switchBasicControl
            // 
            this.rb_switchBasicControl.AutoSize = true;
            this.rb_switchBasicControl.Location = new System.Drawing.Point(29, 36);
            this.rb_switchBasicControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_switchBasicControl.Name = "rb_switchBasicControl";
            this.rb_switchBasicControl.Size = new System.Drawing.Size(73, 19);
            this.rb_switchBasicControl.TabIndex = 3;
            this.rb_switchBasicControl.TabStop = true;
            this.rb_switchBasicControl.Text = "일반제어";
            this.rb_switchBasicControl.UseVisualStyleBackColor = true;
            // 
            // rb_switchTimeControl
            // 
            this.rb_switchTimeControl.AutoSize = true;
            this.rb_switchTimeControl.Location = new System.Drawing.Point(29, 75);
            this.rb_switchTimeControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_switchTimeControl.Name = "rb_switchTimeControl";
            this.rb_switchTimeControl.Size = new System.Drawing.Size(97, 19);
            this.rb_switchTimeControl.TabIndex = 4;
            this.rb_switchTimeControl.TabStop = true;
            this.rb_switchTimeControl.Text = "시간제어 (초)";
            this.rb_switchTimeControl.UseVisualStyleBackColor = true;
            // 
            // rb_switchRatioControl
            // 
            this.rb_switchRatioControl.AutoSize = true;
            this.rb_switchRatioControl.Location = new System.Drawing.Point(29, 114);
            this.rb_switchRatioControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb_switchRatioControl.Name = "rb_switchRatioControl";
            this.rb_switchRatioControl.Size = new System.Drawing.Size(73, 19);
            this.rb_switchRatioControl.TabIndex = 5;
            this.rb_switchRatioControl.TabStop = true;
            this.rb_switchRatioControl.Text = "강도제어";
            this.rb_switchRatioControl.UseVisualStyleBackColor = true;
            // 
            // tb_switchTime
            // 
            this.tb_switchTime.Location = new System.Drawing.Point(136, 68);
            this.tb_switchTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_switchTime.Name = "tb_switchTime";
            this.tb_switchTime.Size = new System.Drawing.Size(100, 23);
            this.tb_switchTime.TabIndex = 6;
            // 
            // tb_switchRatio
            // 
            this.tb_switchRatio.Location = new System.Drawing.Point(136, 107);
            this.tb_switchRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_switchRatio.Name = "tb_switchRatio";
            this.tb_switchRatio.Size = new System.Drawing.Size(100, 23);
            this.tb_switchRatio.TabIndex = 7;
            // 
            // gb_switch
            // 
            this.gb_switch.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gb_switch.Controls.Add(this.tb_switchRatio);
            this.gb_switch.Controls.Add(this.tb_switchTime);
            this.gb_switch.Controls.Add(this.rb_switchRatioControl);
            this.gb_switch.Controls.Add(this.rb_switchTimeControl);
            this.gb_switch.Controls.Add(this.rb_switchBasicControl);
            this.gb_switch.Controls.Add(this.bt_switchOff);
            this.gb_switch.Controls.Add(this.bt_switchOn);
            this.gb_switch.Location = new System.Drawing.Point(831, 196);
            this.gb_switch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_switch.Name = "gb_switch";
            this.gb_switch.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gb_switch.Size = new System.Drawing.Size(292, 331);
            this.gb_switch.TabIndex = 46;
            this.gb_switch.TabStop = false;
            this.gb_switch.Text = "스위치제어";
            this.gb_switch.Visible = false;
            // 
            // bt_switchOff
            // 
            this.bt_switchOff.BackColor = System.Drawing.Color.Cyan;
            this.bt_switchOff.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_power_off_black_18dp;
            this.bt_switchOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_switchOff.Location = new System.Drawing.Point(136, 163);
            this.bt_switchOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_switchOff.Name = "bt_switchOff";
            this.bt_switchOff.Size = new System.Drawing.Size(100, 38);
            this.bt_switchOff.TabIndex = 2;
            this.bt_switchOff.Text = "Off";
            this.bt_switchOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_switchOff.UseVisualStyleBackColor = false;
            this.bt_switchOff.Click += new System.EventHandler(this.button2_Click);
            // 
            // bt_switchOn
            // 
            this.bt_switchOn.BackColor = System.Drawing.Color.Cyan;
            this.bt_switchOn.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_playlist_play_black_18dp;
            this.bt_switchOn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_switchOn.Location = new System.Drawing.Point(29, 163);
            this.bt_switchOn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_switchOn.Name = "bt_switchOn";
            this.bt_switchOn.Size = new System.Drawing.Size(101, 38);
            this.bt_switchOn.TabIndex = 1;
            this.bt_switchOn.Text = "On";
            this.bt_switchOn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_switchOn.UseVisualStyleBackColor = false;
            this.bt_switchOn.Click += new System.EventHandler(this.button1_Click);
            // 
            // comopenbtn
            // 
            this.comopenbtn.BackColor = System.Drawing.Color.Cyan;
            this.comopenbtn.Image = global::ActuatorNodeMonitorApp.Properties.Resources.baseline_open_in_browser_black_18dp;
            this.comopenbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comopenbtn.Location = new System.Drawing.Point(19, 85);
            this.comopenbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comopenbtn.Name = "comopenbtn";
            this.comopenbtn.Size = new System.Drawing.Size(134, 41);
            this.comopenbtn.TabIndex = 48;
            this.comopenbtn.Text = "통신포트 열기";
            this.comopenbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.comopenbtn.UseVisualStyleBackColor = false;
            this.comopenbtn.Click += new System.EventHandler(this.comopenbtn_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1140, 903);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_comlist);
            this.Controls.Add(this.comopenbtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_retractable);
            this.Controls.Add(this.gb_switch);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "구동기 모니터 C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fclose);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb_retractable.ResumeLayout(false);
            this.gb_retractable.PerformLayout();
            this.gb_setConfig.ResumeLayout(false);
            this.gb_setConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_switch.ResumeLayout(false);
            this.gb_switch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.GroupBox gb_retractable;
		private System.Windows.Forms.Label lb_closeTime;
		private System.Windows.Forms.Label lb_openTime;
		private System.Windows.Forms.TextBox tb_closeTime;
		private System.Windows.Forms.TextBox tb_openTime;
		private System.Windows.Forms.Button bt_setConfig;
		private System.Windows.Forms.Button bt_retStop;
		private System.Windows.Forms.TextBox tb_retTime;
		private System.Windows.Forms.TextBox tb_retPosition;
		private System.Windows.Forms.RadioButton rb_retTimeControl;
		private System.Windows.Forms.RadioButton rb_retbasicControl;
		private System.Windows.Forms.Button bt_retOff;
		private System.Windows.Forms.Button bt_retOn;
		private System.Windows.Forms.RadioButton rb_retPositionControl;
		private System.Windows.Forms.GroupBox gb_setConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_comlist;
        private System.Windows.Forms.Button comopenbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_device;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.Button bt_switchOn;
        private System.Windows.Forms.Button bt_switchOff;
        private System.Windows.Forms.RadioButton rb_switchBasicControl;
        private System.Windows.Forms.RadioButton rb_switchTimeControl;
        private System.Windows.Forms.RadioButton rb_switchRatioControl;
        private System.Windows.Forms.TextBox tb_switchTime;
        private System.Windows.Forms.TextBox tb_switchRatio;
        private System.Windows.Forms.GroupBox gb_switch;
	}
}

