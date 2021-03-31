namespace NutrientNodeMonitor
{
    partial class UC_Sensor
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_name = new System.Windows.Forms.Label();
            this.label_value = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(4, 5);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(39, 15);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "label1";
            // 
            // label_value
            // 
            this.label_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_value.AutoSize = true;
            this.label_value.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_value.Location = new System.Drawing.Point(34, 29);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(51, 20);
            this.label_value.TabIndex = 1;
            this.label_value.Text = "label2";
            this.label_value.Click += new System.EventHandler(this.label_value_Click);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(4, 59);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(39, 15);
            this.label_status.TabIndex = 2;
            this.label_status.Text = "label3";
            // 
            // UC_Sensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_value);
            this.Controls.Add(this.label_name);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_Sensor";
            this.Size = new System.Drawing.Size(142, 85);
            this.Click += new System.EventHandler(this.click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_value;
        private System.Windows.Forms.Label label_status;
    }
}
