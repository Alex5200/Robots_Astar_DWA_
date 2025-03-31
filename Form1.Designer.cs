namespace IoTRobotWorldUDPServer
{
    partial class Form
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RemoteIPTextBox = new System.Windows.Forms.TextBox();
            this.RemotePortTextBox = new System.Windows.Forms.TextBox();
            this.LocalPortTextBox = new System.Windows.Forms.TextBox();
            this.LocalIPTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UDPMessageTextBox = new System.Windows.Forms.TextBox();
            this.SendUDPMessageButton = new System.Windows.Forms.Button();
            this.ReportListBox = new System.Windows.Forms.ListBox();
            this.StartStopUDPClientButton = new System.Windows.Forms.Button();
            this.UDPRegularSenderTimer = new System.Windows.Forms.Timer(this.components);
            this.RegularUDPSendCheckBox = new System.Windows.Forms.CheckBox();
            this.AppendLFSymbolCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.d_0 = new System.Windows.Forms.TextBox();
            this.d_1 = new System.Windows.Forms.TextBox();
            this.d_3 = new System.Windows.Forms.TextBox();
            this.d_2 = new System.Windows.Forms.TextBox();
            this.d_7 = new System.Windows.Forms.TextBox();
            this.d_6 = new System.Windows.Forms.TextBox();
            this.d_5 = new System.Windows.Forms.TextBox();
            this.d_4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.X_textbox = new System.Windows.Forms.TextBox();
            this.Y_textbox = new System.Windows.Forms.TextBox();
            this.T_textbox = new System.Windows.Forms.TextBox();
            this.AgleRobot = new System.Windows.Forms.TextBox();
            this.TargetX = new System.Windows.Forms.TextBox();
            this.TargetY = new System.Windows.Forms.TextBox();
            this.PointY = new System.Windows.Forms.TextBox();
            this.PointX = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LoadMAP = new System.Windows.Forms.Button();
            this.PortLidar = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 230);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 21);
            this.button1.TabIndex = 27;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remote IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Remote Port";
            // 
            // RemoteIPTextBox
            // 
            this.RemoteIPTextBox.Location = new System.Drawing.Point(72, 6);
            this.RemoteIPTextBox.Name = "RemoteIPTextBox";
            this.RemoteIPTextBox.Size = new System.Drawing.Size(100, 20);
            this.RemoteIPTextBox.TabIndex = 2;
            this.RemoteIPTextBox.Text = "127.0.0.1";
            // 
            // RemotePortTextBox
            // 
            this.RemotePortTextBox.Location = new System.Drawing.Point(72, 38);
            this.RemotePortTextBox.Name = "RemotePortTextBox";
            this.RemotePortTextBox.Size = new System.Drawing.Size(100, 20);
            this.RemotePortTextBox.TabIndex = 3;
            this.RemotePortTextBox.Text = "8888";
            // 
            // LocalPortTextBox
            // 
            this.LocalPortTextBox.Location = new System.Drawing.Point(240, 38);
            this.LocalPortTextBox.Name = "LocalPortTextBox";
            this.LocalPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.LocalPortTextBox.TabIndex = 7;
            this.LocalPortTextBox.Text = "7777";
            this.LocalPortTextBox.TextChanged += new System.EventHandler(this.LocalPortTextBox_TextChanged);
            // 
            // LocalIPTextBox
            // 
            this.LocalIPTextBox.Location = new System.Drawing.Point(240, 6);
            this.LocalIPTextBox.Name = "LocalIPTextBox";
            this.LocalIPTextBox.Size = new System.Drawing.Size(100, 20);
            this.LocalIPTextBox.TabIndex = 6;
            this.LocalIPTextBox.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Local Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Local IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Message";
            // 
            // UDPMessageTextBox
            // 
            this.UDPMessageTextBox.Location = new System.Drawing.Point(72, 136);
            this.UDPMessageTextBox.Name = "UDPMessageTextBox";
            this.UDPMessageTextBox.Size = new System.Drawing.Size(249, 20);
            this.UDPMessageTextBox.TabIndex = 9;
            this.UDPMessageTextBox.Text = "{\"N\":1, \"M\":0, \"F\":50, \"B\":10, \"T\":0}";
            // 
            // SendUDPMessageButton
            // 
            this.SendUDPMessageButton.Location = new System.Drawing.Point(70, 156);
            this.SendUDPMessageButton.Name = "SendUDPMessageButton";
            this.SendUDPMessageButton.Size = new System.Drawing.Size(75, 28);
            this.SendUDPMessageButton.TabIndex = 10;
            this.SendUDPMessageButton.Text = "Send";
            this.SendUDPMessageButton.UseVisualStyleBackColor = true;
            // 
            // ReportListBox
            // 
            this.ReportListBox.FormattingEnabled = true;
            this.ReportListBox.Location = new System.Drawing.Point(16, 190);
            this.ReportListBox.Name = "ReportListBox";
            this.ReportListBox.ScrollAlwaysVisible = true;
            this.ReportListBox.Size = new System.Drawing.Size(377, 82);
            this.ReportListBox.TabIndex = 11;
            // 
            // StartStopUDPClientButton
            // 
            this.StartStopUDPClientButton.Location = new System.Drawing.Point(347, 11);
            this.StartStopUDPClientButton.Name = "StartStopUDPClientButton";
            this.StartStopUDPClientButton.Size = new System.Drawing.Size(54, 42);
            this.StartStopUDPClientButton.TabIndex = 12;
            this.StartStopUDPClientButton.Text = "Start";
            this.StartStopUDPClientButton.UseVisualStyleBackColor = true;
            this.StartStopUDPClientButton.Click += new System.EventHandler(this.StartStopUDPClientButton_Click);
            // 
            // UDPRegularSenderTimer
            // 
            this.UDPRegularSenderTimer.Interval = 1000;
            // 
            // RegularUDPSendCheckBox
            // 
            this.RegularUDPSendCheckBox.AutoSize = true;
            this.RegularUDPSendCheckBox.Location = new System.Drawing.Point(194, 167);
            this.RegularUDPSendCheckBox.Name = "RegularUDPSendCheckBox";
            this.RegularUDPSendCheckBox.Size = new System.Drawing.Size(134, 17);
            this.RegularUDPSendCheckBox.TabIndex = 13;
            this.RegularUDPSendCheckBox.Text = "Send message regular ";
            this.RegularUDPSendCheckBox.UseVisualStyleBackColor = true;
            // 
            // AppendLFSymbolCheckBox
            // 
            this.AppendLFSymbolCheckBox.AutoSize = true;
            this.AppendLFSymbolCheckBox.Checked = true;
            this.AppendLFSymbolCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AppendLFSymbolCheckBox.Location = new System.Drawing.Point(326, 138);
            this.AppendLFSymbolCheckBox.Name = "AppendLFSymbolCheckBox";
            this.AppendLFSymbolCheckBox.Size = new System.Drawing.Size(78, 17);
            this.AppendLFSymbolCheckBox.TabIndex = 14;
            this.AppendLFSymbolCheckBox.Text = "Append LF";
            this.AppendLFSymbolCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = " d1,d2,d3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Filter Input";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 300;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // d_0
            // 
            this.d_0.Location = new System.Drawing.Point(425, 11);
            this.d_0.Margin = new System.Windows.Forms.Padding(2);
            this.d_0.Name = "d_0";
            this.d_0.Size = new System.Drawing.Size(68, 20);
            this.d_0.TabIndex = 18;
            // 
            // d_1
            // 
            this.d_1.Location = new System.Drawing.Point(425, 36);
            this.d_1.Margin = new System.Windows.Forms.Padding(2);
            this.d_1.Name = "d_1";
            this.d_1.Size = new System.Drawing.Size(68, 20);
            this.d_1.TabIndex = 19;
            // 
            // d_3
            // 
            this.d_3.Location = new System.Drawing.Point(425, 90);
            this.d_3.Margin = new System.Windows.Forms.Padding(2);
            this.d_3.Name = "d_3";
            this.d_3.Size = new System.Drawing.Size(68, 20);
            this.d_3.TabIndex = 21;
            // 
            // d_2
            // 
            this.d_2.Location = new System.Drawing.Point(425, 65);
            this.d_2.Margin = new System.Windows.Forms.Padding(2);
            this.d_2.Name = "d_2";
            this.d_2.Size = new System.Drawing.Size(68, 20);
            this.d_2.TabIndex = 20;
            // 
            // d_7
            // 
            this.d_7.Location = new System.Drawing.Point(425, 196);
            this.d_7.Margin = new System.Windows.Forms.Padding(2);
            this.d_7.Name = "d_7";
            this.d_7.Size = new System.Drawing.Size(68, 20);
            this.d_7.TabIndex = 25;
            // 
            // d_6
            // 
            this.d_6.Location = new System.Drawing.Point(425, 171);
            this.d_6.Margin = new System.Windows.Forms.Padding(2);
            this.d_6.Name = "d_6";
            this.d_6.Size = new System.Drawing.Size(68, 20);
            this.d_6.TabIndex = 24;
            // 
            // d_5
            // 
            this.d_5.Location = new System.Drawing.Point(425, 142);
            this.d_5.Margin = new System.Windows.Forms.Padding(2);
            this.d_5.Name = "d_5";
            this.d_5.Size = new System.Drawing.Size(68, 20);
            this.d_5.TabIndex = 23;
            // 
            // d_4
            // 
            this.d_4.Location = new System.Drawing.Point(425, 117);
            this.d_4.Margin = new System.Windows.Forms.Padding(2);
            this.d_4.Name = "d_4";
            this.d_4.Size = new System.Drawing.Size(68, 20);
            this.d_4.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 215);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Статус захвата";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(496, 9);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 45);
            this.button2.TabIndex = 28;
            this.button2.Text = "LoadCSV";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(572, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(462, 240);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer3
            // 
            this.timer3.Interval = 500;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // X_textbox
            // 
            this.X_textbox.Location = new System.Drawing.Point(419, 255);
            this.X_textbox.Margin = new System.Windows.Forms.Padding(2);
            this.X_textbox.Name = "X_textbox";
            this.X_textbox.Size = new System.Drawing.Size(68, 20);
            this.X_textbox.TabIndex = 30;
            // 
            // Y_textbox
            // 
            this.Y_textbox.Location = new System.Drawing.Point(419, 276);
            this.Y_textbox.Margin = new System.Windows.Forms.Padding(2);
            this.Y_textbox.Name = "Y_textbox";
            this.Y_textbox.Size = new System.Drawing.Size(68, 20);
            this.Y_textbox.TabIndex = 31;
            // 
            // T_textbox
            // 
            this.T_textbox.Location = new System.Drawing.Point(489, 276);
            this.T_textbox.Margin = new System.Windows.Forms.Padding(2);
            this.T_textbox.Name = "T_textbox";
            this.T_textbox.Size = new System.Drawing.Size(68, 20);
            this.T_textbox.TabIndex = 32;
            // 
            // AgleRobot
            // 
            this.AgleRobot.Location = new System.Drawing.Point(489, 255);
            this.AgleRobot.Margin = new System.Windows.Forms.Padding(2);
            this.AgleRobot.Name = "AgleRobot";
            this.AgleRobot.Size = new System.Drawing.Size(68, 20);
            this.AgleRobot.TabIndex = 33;
            // 
            // TargetX
            // 
            this.TargetX.Location = new System.Drawing.Point(500, 161);
            this.TargetX.Margin = new System.Windows.Forms.Padding(2);
            this.TargetX.Name = "TargetX";
            this.TargetX.Size = new System.Drawing.Size(68, 20);
            this.TargetX.TabIndex = 34;
            // 
            // TargetY
            // 
            this.TargetY.Location = new System.Drawing.Point(500, 185);
            this.TargetY.Margin = new System.Windows.Forms.Padding(2);
            this.TargetY.Name = "TargetY";
            this.TargetY.Size = new System.Drawing.Size(68, 20);
            this.TargetY.TabIndex = 35;
            // 
            // PointY
            // 
            this.PointY.Location = new System.Drawing.Point(500, 233);
            this.PointY.Margin = new System.Windows.Forms.Padding(2);
            this.PointY.Name = "PointY";
            this.PointY.Size = new System.Drawing.Size(68, 20);
            this.PointY.TabIndex = 37;
            // 
            // PointX
            // 
            this.PointX.Location = new System.Drawing.Point(500, 209);
            this.PointX.Margin = new System.Windows.Forms.Padding(2);
            this.PointX.Name = "PointX";
            this.PointX.Size = new System.Drawing.Size(68, 20);
            this.PointX.TabIndex = 36;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(496, 58);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 45);
            this.button3.TabIndex = 38;
            this.button3.Text = "Search Path";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1038, 11);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(401, 240);
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            // 
            // LoadMAP
            // 
            this.LoadMAP.Location = new System.Drawing.Point(497, 107);
            this.LoadMAP.Margin = new System.Windows.Forms.Padding(2);
            this.LoadMAP.Name = "LoadMAP";
            this.LoadMAP.Size = new System.Drawing.Size(72, 45);
            this.LoadMAP.TabIndex = 40;
            this.LoadMAP.Text = "LoadMAP";
            this.LoadMAP.UseVisualStyleBackColor = true;
            this.LoadMAP.Click += new System.EventHandler(this.LoadMAP_Click);
            // 
            // PortLidar
            // 
            this.PortLidar.Location = new System.Drawing.Point(240, 64);
            this.PortLidar.Name = "PortLidar";
            this.PortLidar.Size = new System.Drawing.Size(100, 20);
            this.PortLidar.TabIndex = 42;
            this.PortLidar.Text = "2368";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Lidar port";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 297);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PortLidar);
            this.Controls.Add(this.LoadMAP);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.PointY);
            this.Controls.Add(this.PointX);
            this.Controls.Add(this.TargetY);
            this.Controls.Add(this.TargetX);
            this.Controls.Add(this.AgleRobot);
            this.Controls.Add(this.T_textbox);
            this.Controls.Add(this.Y_textbox);
            this.Controls.Add(this.X_textbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.d_7);
            this.Controls.Add(this.d_6);
            this.Controls.Add(this.d_5);
            this.Controls.Add(this.d_4);
            this.Controls.Add(this.d_3);
            this.Controls.Add(this.d_2);
            this.Controls.Add(this.d_1);
            this.Controls.Add(this.d_0);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AppendLFSymbolCheckBox);
            this.Controls.Add(this.RegularUDPSendCheckBox);
            this.Controls.Add(this.StartStopUDPClientButton);
            this.Controls.Add(this.ReportListBox);
            this.Controls.Add(this.SendUDPMessageButton);
            this.Controls.Add(this.UDPMessageTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LocalPortTextBox);
            this.Controls.Add(this.LocalIPTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RemotePortTextBox);
            this.Controls.Add(this.RemoteIPTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form";
            this.Text = "IoTRobotWorldUDPServer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RemoteIPTextBox;
        private System.Windows.Forms.TextBox RemotePortTextBox;
        private System.Windows.Forms.TextBox LocalPortTextBox;
        private System.Windows.Forms.TextBox LocalIPTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UDPMessageTextBox;
        private System.Windows.Forms.Button SendUDPMessageButton;
        private System.Windows.Forms.ListBox ReportListBox;
        private System.Windows.Forms.Button StartStopUDPClientButton;
        private System.Windows.Forms.Timer UDPRegularSenderTimer;
        private System.Windows.Forms.CheckBox RegularUDPSendCheckBox;
        private System.Windows.Forms.CheckBox AppendLFSymbolCheckBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox d_0;
        private System.Windows.Forms.TextBox d_3;
        private System.Windows.Forms.TextBox d_2;
        private System.Windows.Forms.TextBox d_7;
        private System.Windows.Forms.TextBox d_6;
        private System.Windows.Forms.TextBox d_5;
        private System.Windows.Forms.TextBox d_4;
        private System.Windows.Forms.TextBox d_1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.TextBox X_textbox;
        private System.Windows.Forms.TextBox Y_textbox;
        private System.Windows.Forms.TextBox T_textbox;
        private System.Windows.Forms.TextBox AgleRobot;
        private System.Windows.Forms.TextBox TargetX;
        private System.Windows.Forms.TextBox TargetY;
        private System.Windows.Forms.TextBox PointY;
        private System.Windows.Forms.TextBox PointX;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button LoadMAP;
        private System.Windows.Forms.TextBox PortLidar;
        private System.Windows.Forms.Label label8;
    }
}

