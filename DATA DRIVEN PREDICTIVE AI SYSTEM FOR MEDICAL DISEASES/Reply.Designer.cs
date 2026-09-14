namespace DATA_DRIVEN_PREDICTIVE_AI_SYSTEM_FOR_MEDICAL_DISEASES
{
    partial class Reply
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtReply = new System.Windows.Forms.TextBox();
            this.lblReply = new System.Windows.Forms.Label();
            this.btnReply = new System.Windows.Forms.Button();
            this.linkAttachment = new System.Windows.Forms.LinkLabel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(142, 53);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(305, 22);
            this.txtName.TabIndex = 0;
            this.txtName.TabStop = false;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(553, 51);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(305, 22);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.TabStop = false;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(142, 94);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(716, 181);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(44, 53);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 20);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(485, 53);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(62, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "E Mail";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(44, 94);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(84, 20);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "Message";
            // 
            // txtReply
            // 
            this.txtReply.Location = new System.Drawing.Point(142, 315);
            this.txtReply.Multiline = true;
            this.txtReply.Name = "txtReply";
            this.txtReply.Size = new System.Drawing.Size(716, 225);
            this.txtReply.TabIndex = 6;
            // 
            // lblReply
            // 
            this.lblReply.AutoSize = true;
            this.lblReply.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReply.Location = new System.Drawing.Point(44, 315);
            this.lblReply.Name = "lblReply";
            this.lblReply.Size = new System.Drawing.Size(56, 20);
            this.lblReply.TabIndex = 7;
            this.lblReply.Text = "Reply";
            // 
            // btnReply
            // 
            this.btnReply.Location = new System.Drawing.Point(660, 546);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(198, 39);
            this.btnReply.TabIndex = 8;
            this.btnReply.Text = "Reply";
            this.btnReply.UseVisualStyleBackColor = true;
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // linkAttachment
            // 
            this.linkAttachment.AutoSize = true;
            this.linkAttachment.Location = new System.Drawing.Point(139, 287);
            this.linkAttachment.Name = "linkAttachment";
            this.linkAttachment.Size = new System.Drawing.Size(68, 16);
            this.linkAttachment.TabIndex = 9;
            this.linkAttachment.TabStop = true;
            this.linkAttachment.Text = "linkLabel1";
            this.linkAttachment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAttachment_LinkClicked);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(141, 546);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(138, 39);
            this.btnUpload.TabIndex = 10;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(345, 557);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 16);
            this.lblFileName.TabIndex = 11;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(510, 546);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(144, 39);
            this.btnRecord.TabIndex = 12;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(783, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Reply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(905, 636);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.linkAttachment);
            this.Controls.Add(this.btnReply);
            this.Controls.Add(this.lblReply);
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtName);
            this.Name = "Reply";
            this.Text = "Reply";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Reply_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtReply;
        private System.Windows.Forms.Label lblReply;
        private System.Windows.Forms.Button btnReply;
        private System.Windows.Forms.LinkLabel linkAttachment;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnExit;
    }
}

