namespace MetadataBulidUtility
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_sensornode = new System.Windows.Forms.Button();
            this.button_actnode = new System.Windows.Forms.Button();
            this.richTextBox_base64 = new System.Windows.Forms.RichTextBox();
            this.button_base64 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "디폴트 양액기노드";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(170, 22);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(725, 461);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // button_sensornode
            // 
            this.button_sensornode.Location = new System.Drawing.Point(12, 40);
            this.button_sensornode.Name = "button_sensornode";
            this.button_sensornode.Size = new System.Drawing.Size(152, 39);
            this.button_sensornode.TabIndex = 3;
            this.button_sensornode.Text = "디폴트 센서노드";
            this.button_sensornode.UseVisualStyleBackColor = true;
            this.button_sensornode.Click += new System.EventHandler(this.button_sensornode_Click);
            // 
            // button_actnode
            // 
            this.button_actnode.Location = new System.Drawing.Point(12, 94);
            this.button_actnode.Name = "button_actnode";
            this.button_actnode.Size = new System.Drawing.Size(152, 39);
            this.button_actnode.TabIndex = 4;
            this.button_actnode.Text = "디폴트 구동기노드";
            this.button_actnode.UseVisualStyleBackColor = true;
            this.button_actnode.Click += new System.EventHandler(this.button_actnode_Click);
            // 
            // richTextBox_base64
            // 
            this.richTextBox_base64.Location = new System.Drawing.Point(170, 533);
            this.richTextBox_base64.Name = "richTextBox_base64";
            this.richTextBox_base64.ReadOnly = true;
            this.richTextBox_base64.Size = new System.Drawing.Size(725, 106);
            this.richTextBox_base64.TabIndex = 5;
            this.richTextBox_base64.Text = "";
            // 
            // button_base64
            // 
            this.button_base64.Location = new System.Drawing.Point(170, 489);
            this.button_base64.Name = "button_base64";
            this.button_base64.Size = new System.Drawing.Size(152, 39);
            this.button_base64.TabIndex = 6;
            this.button_base64.Text = "Base64 인코딩";
            this.button_base64.UseVisualStyleBackColor = true;
            this.button_base64.Click += new System.EventHandler(this.button_base64_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 637);
            this.Controls.Add(this.button_base64);
            this.Controls.Add(this.richTextBox_base64);
            this.Controls.Add(this.button_actnode);
            this.Controls.Add(this.button_sensornode);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "메타데이타 빌드";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_sensornode;
        private System.Windows.Forms.Button button_actnode;
        private System.Windows.Forms.RichTextBox richTextBox_base64;
        private System.Windows.Forms.Button button_base64;
    }
}

