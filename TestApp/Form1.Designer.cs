namespace TestApp
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.txtCreateKeyDescription = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtCreateKeyExpireTime = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtCreateKeyDoors = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtCreateKeyPassword = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtCreateKeyUsername = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtCreateKeyEndpointId = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtCreateKeyVisionlineIP = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtCreateKeyJoinerId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCreateKeyJoinerId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.txtCreateKeyDescription);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.txtCreateKeyExpireTime);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.txtCreateKeyDoors);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.txtCreateKeyPassword);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txtCreateKeyUsername);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.txtCreateKeyEndpointId);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.txtCreateKeyVisionlineIP);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 341);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Key";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 23);
            this.button2.TabIndex = 54;
            this.button2.Text = "Get Cards";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 23);
            this.button1.TabIndex = 53;
            this.button1.Text = "Get Doors";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(6, 216);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(225, 23);
            this.button11.TabIndex = 52;
            this.button11.Text = "CreateMobileAccessKeyFromParameters";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // txtCreateKeyDescription
            // 
            this.txtCreateKeyDescription.Location = new System.Drawing.Point(108, 157);
            this.txtCreateKeyDescription.Name = "txtCreateKeyDescription";
            this.txtCreateKeyDescription.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyDescription.TabIndex = 40;
            this.txtCreateKeyDescription.Text = "Hello";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 158);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 39;
            this.label31.Text = "Description:";
            // 
            // txtCreateKeyExpireTime
            // 
            this.txtCreateKeyExpireTime.Location = new System.Drawing.Point(108, 136);
            this.txtCreateKeyExpireTime.Name = "txtCreateKeyExpireTime";
            this.txtCreateKeyExpireTime.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyExpireTime.TabIndex = 38;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 137);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(62, 13);
            this.label29.TabIndex = 37;
            this.label29.Text = "ExpireTime:";
            // 
            // txtCreateKeyDoors
            // 
            this.txtCreateKeyDoors.Location = new System.Drawing.Point(108, 113);
            this.txtCreateKeyDoors.Name = "txtCreateKeyDoors";
            this.txtCreateKeyDoors.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyDoors.TabIndex = 36;
            this.txtCreateKeyDoors.Text = "101;";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 114);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 13);
            this.label28.TabIndex = 35;
            this.label28.Text = "Doors:";
            // 
            // txtCreateKeyPassword
            // 
            this.txtCreateKeyPassword.Location = new System.Drawing.Point(108, 91);
            this.txtCreateKeyPassword.Name = "txtCreateKeyPassword";
            this.txtCreateKeyPassword.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyPassword.TabIndex = 34;
            this.txtCreateKeyPassword.Text = "sym";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 92);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(56, 13);
            this.label27.TabIndex = 33;
            this.label27.Text = "Password:";
            // 
            // txtCreateKeyUsername
            // 
            this.txtCreateKeyUsername.Location = new System.Drawing.Point(108, 70);
            this.txtCreateKeyUsername.Name = "txtCreateKeyUsername";
            this.txtCreateKeyUsername.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyUsername.TabIndex = 32;
            this.txtCreateKeyUsername.Text = "sym";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 71);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 13);
            this.label26.TabIndex = 31;
            this.label26.Text = "Username:";
            // 
            // txtCreateKeyEndpointId
            // 
            this.txtCreateKeyEndpointId.Location = new System.Drawing.Point(107, 48);
            this.txtCreateKeyEndpointId.Name = "txtCreateKeyEndpointId";
            this.txtCreateKeyEndpointId.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyEndpointId.TabIndex = 30;
            this.txtCreateKeyEndpointId.Text = "guestFolder123";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 49);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(61, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "EndpointId:";
            // 
            // txtCreateKeyVisionlineIP
            // 
            this.txtCreateKeyVisionlineIP.Location = new System.Drawing.Point(107, 26);
            this.txtCreateKeyVisionlineIP.Name = "txtCreateKeyVisionlineIP";
            this.txtCreateKeyVisionlineIP.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyVisionlineIP.TabIndex = 28;
            this.txtCreateKeyVisionlineIP.Text = "https://127.0.0.1";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(7, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(64, 13);
            this.label24.TabIndex = 27;
            this.label24.Text = "VisionlineIP:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 243);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 23);
            this.button3.TabIndex = 55;
            this.button3.Text = "CreateMobileAccessKeyJoinerFromParameters";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtCreateKeyJoinerId
            // 
            this.txtCreateKeyJoinerId.Location = new System.Drawing.Point(107, 181);
            this.txtCreateKeyJoinerId.Name = "txtCreateKeyJoinerId";
            this.txtCreateKeyJoinerId.Size = new System.Drawing.Size(125, 20);
            this.txtCreateKeyJoinerId.TabIndex = 57;
            this.txtCreateKeyJoinerId.Text = "Hello";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "JoinerId:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 535);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox txtCreateKeyDescription;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtCreateKeyExpireTime;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtCreateKeyDoors;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtCreateKeyPassword;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtCreateKeyUsername;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtCreateKeyEndpointId;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtCreateKeyVisionlineIP;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtCreateKeyJoinerId;
        private System.Windows.Forms.Label label1;
    }
}

