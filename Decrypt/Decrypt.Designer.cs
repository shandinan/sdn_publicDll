namespace Decrypt
{
    partial class Decrypt
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEx1 = new sdnControls.sdnPanelEx.PanelEx();
            this.txtSqMsg = new sdnControls.sdnTextBoxEx.TextBoxEx();
            this.labelEx3 = new sdnControls.sdnLabelEx.LabelEx();
            this.btnOk = new sdnControls.sdnButtonEx.ButtonEx();
            this.txtSqm = new sdnControls.sdnTextBoxEx.TextBoxEx();
            this.lbsqm = new sdnControls.sdnLabelEx.LabelEx();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.labelEx2 = new sdnControls.sdnLabelEx.LabelEx();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.lbss = new sdnControls.sdnLabelEx.LabelEx();
            this.txtClientId = new sdnControls.sdnTextBoxEx.TextBoxEx();
            this.labelEx1 = new sdnControls.sdnLabelEx.LabelEx();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.BaseColor = System.Drawing.Color.Empty;
            this.panelEx1.BottomWidth = 1;
            this.panelEx1.Controls.Add(this.txtSqMsg);
            this.panelEx1.Controls.Add(this.labelEx3);
            this.panelEx1.Controls.Add(this.btnOk);
            this.panelEx1.Controls.Add(this.txtSqm);
            this.panelEx1.Controls.Add(this.lbsqm);
            this.panelEx1.Controls.Add(this.dtEnd);
            this.panelEx1.Controls.Add(this.labelEx2);
            this.panelEx1.Controls.Add(this.dtStart);
            this.panelEx1.Controls.Add(this.lbss);
            this.panelEx1.Controls.Add(this.txtClientId);
            this.panelEx1.Controls.Add(this.labelEx1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.LeftWidth = 1;
            this.panelEx1.Location = new System.Drawing.Point(3, 24);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.RightWidth = 1;
            this.panelEx1.Size = new System.Drawing.Size(630, 363);
            this.panelEx1.TabIndex = 0;
            this.panelEx1.TopWidth = 1;
            // 
            // txtSqMsg
            // 
            this.txtSqMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqMsg.EmptyTextTip = null;
            this.txtSqMsg.Location = new System.Drawing.Point(485, 82);
            this.txtSqMsg.Multiline = true;
            this.txtSqMsg.Name = "txtSqMsg";
            this.txtSqMsg.Size = new System.Drawing.Size(129, 221);
            this.txtSqMsg.TabIndex = 11;
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new System.Drawing.Point(426, 178);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new System.Drawing.Size(53, 12);
            this.labelEx3.TabIndex = 10;
            this.labelEx3.Text = "授权信息";
            this.labelEx3.TextArrange = sdnControls.sdnLabelEx.LabelEx.ETextArrange.Horizontal;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(539, 330);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "生成授权码";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtSqm
            // 
            this.txtSqm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqm.EmptyTextTip = null;
            this.txtSqm.Location = new System.Drawing.Point(78, 82);
            this.txtSqm.Multiline = true;
            this.txtSqm.Name = "txtSqm";
            this.txtSqm.Size = new System.Drawing.Size(333, 262);
            this.txtSqm.TabIndex = 8;
            // 
            // lbsqm
            // 
            this.lbsqm.AutoSize = true;
            this.lbsqm.Location = new System.Drawing.Point(16, 187);
            this.lbsqm.Name = "lbsqm";
            this.lbsqm.Size = new System.Drawing.Size(41, 12);
            this.lbsqm.TabIndex = 7;
            this.lbsqm.Text = "授权码";
            this.lbsqm.TextArrange = sdnControls.sdnLabelEx.LabelEx.ETextArrange.Horizontal;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(375, 55);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(119, 21);
            this.dtEnd.TabIndex = 5;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new System.Drawing.Point(299, 61);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new System.Drawing.Size(53, 12);
            this.labelEx2.TabIndex = 4;
            this.labelEx2.Text = "结束时间";
            this.labelEx2.TextArrange = sdnControls.sdnLabelEx.LabelEx.ETextArrange.Horizontal;
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(90, 55);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(119, 21);
            this.dtStart.TabIndex = 3;
            // 
            // lbss
            // 
            this.lbss.AutoSize = true;
            this.lbss.Location = new System.Drawing.Point(18, 61);
            this.lbss.Name = "lbss";
            this.lbss.Size = new System.Drawing.Size(53, 12);
            this.lbss.TabIndex = 2;
            this.lbss.Text = "开始时间";
            this.lbss.TextArrange = sdnControls.sdnLabelEx.LabelEx.ETextArrange.Horizontal;
            // 
            // txtClientId
            // 
            this.txtClientId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientId.EmptyTextTip = null;
            this.txtClientId.Location = new System.Drawing.Point(123, 9);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(435, 21);
            this.txtClientId.TabIndex = 1;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new System.Drawing.Point(16, 18);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(101, 12);
            this.labelEx1.TabIndex = 0;
            this.labelEx1.Text = "加密客户端标识：";
            this.labelEx1.TextArrange = sdnControls.sdnLabelEx.LabelEx.ETextArrange.Horizontal;
            // 
            // Decrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 390);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.Name = "Decrypt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "授权码生成工具";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sdnControls.sdnPanelEx.PanelEx panelEx1;
        private sdnControls.sdnButtonEx.ButtonEx btnOk;
        private sdnControls.sdnTextBoxEx.TextBoxEx txtSqm;
        private sdnControls.sdnLabelEx.LabelEx lbsqm;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private sdnControls.sdnLabelEx.LabelEx labelEx2;
        private System.Windows.Forms.DateTimePicker dtStart;
        private sdnControls.sdnLabelEx.LabelEx lbss;
        private sdnControls.sdnTextBoxEx.TextBoxEx txtClientId;
        private sdnControls.sdnLabelEx.LabelEx labelEx1;
        private sdnControls.sdnTextBoxEx.TextBoxEx txtSqMsg;
        private sdnControls.sdnLabelEx.LabelEx labelEx3;
    }
}

