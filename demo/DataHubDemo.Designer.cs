﻿namespace DataHubDemo
{
    partial class DataHubDemo
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
            this.components = new System.ComponentModel.Container();
            this.serverURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.instanceId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.instanceKey = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.connectResult = new System.Windows.Forms.PictureBox();
            this.subResult = new System.Windows.Forms.PictureBox();
            this.subscribe = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.text_sub_or_unsub_qos = new System.Windows.Forms.TextBox();
            this.messageReceive = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.text_pub_topic = new System.Windows.Forms.TextBox();
            this.picture_pub_result = new System.Windows.Forms.PictureBox();
            this.publish = new System.Windows.Forms.Button();
            this.message_pub = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.picture_sendquest_result = new System.Windows.Forms.PictureBox();
            this.sendrequest = new System.Windows.Forms.Button();
            this.unsub_result = new System.Windows.Forms.PictureBox();
            this.unsubscribe = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.subqos = new System.Windows.Forms.Label();
            this.text_sub_qos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_pub_qos = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.picture_connect_status = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.connectResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_pub_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_sendquest_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unsub_result)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_connect_status)).BeginInit();
            this.SuspendLayout();
            // 
            // serverURI
            // 
            this.serverURI.Location = new System.Drawing.Point(88, 27);
            this.serverURI.Name = "serverURI";
            this.serverURI.Size = new System.Drawing.Size(168, 21);
            this.serverURI.TabIndex = 1;
            this.serverURI.Text = "try.iotdatahub.net";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "serverURI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "instanceId";
            // 
            // instanceId
            // 
            this.instanceId.Location = new System.Drawing.Point(88, 54);
            this.instanceId.Name = "instanceId";
            this.instanceId.Size = new System.Drawing.Size(168, 21);
            this.instanceId.TabIndex = 3;
            this.instanceId.Text = "dsd_9FmYSNiqpFmi69Bui0_A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "instanceKey";
            // 
            // instanceKey
            // 
            this.instanceKey.Location = new System.Drawing.Point(88, 81);
            this.instanceKey.Name = "instanceKey";
            this.instanceKey.Size = new System.Drawing.Size(168, 21);
            this.instanceKey.TabIndex = 5;
            this.instanceKey.Text = "238f173d6cc0608a";
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(88, 108);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(97, 21);
            this.connect.TabIndex = 7;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.button_connect);
            // 
            // connectResult
            // 
            this.connectResult.Location = new System.Drawing.Point(191, 108);
            this.connectResult.Name = "connectResult";
            this.connectResult.Size = new System.Drawing.Size(28, 28);
            this.connectResult.TabIndex = 8;
            this.connectResult.TabStop = false;
            // 
            // subResult
            // 
            this.subResult.Location = new System.Drawing.Point(191, 72);
            this.subResult.Name = "subResult";
            this.subResult.Size = new System.Drawing.Size(28, 28);
            this.subResult.TabIndex = 10;
            this.subResult.TabStop = false;
            // 
            // subscribe
            // 
            this.subscribe.Location = new System.Drawing.Point(88, 72);
            this.subscribe.Name = "subscribe";
            this.subscribe.Size = new System.Drawing.Size(97, 21);
            this.subscribe.TabIndex = 9;
            this.subscribe.Text = "subscribe";
            this.subscribe.UseVisualStyleBackColor = true;
            this.subscribe.Click += new System.EventHandler(this.button_subscribe);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "topic";
            // 
            // text_sub_or_unsub_qos
            // 
            this.text_sub_or_unsub_qos.Location = new System.Drawing.Point(88, 15);
            this.text_sub_or_unsub_qos.Name = "text_sub_or_unsub_qos";
            this.text_sub_or_unsub_qos.Size = new System.Drawing.Size(168, 21);
            this.text_sub_or_unsub_qos.TabIndex = 19;
            this.text_sub_or_unsub_qos.Text = "test";
            // 
            // messageReceive
            // 
            this.messageReceive.Location = new System.Drawing.Point(88, 109);
            this.messageReceive.Multiline = true;
            this.messageReceive.Name = "messageReceive";
            this.messageReceive.Size = new System.Drawing.Size(316, 81);
            this.messageReceive.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "topic";
            // 
            // text_pub_topic
            // 
            this.text_pub_topic.Location = new System.Drawing.Point(85, 16);
            this.text_pub_topic.Name = "text_pub_topic";
            this.text_pub_topic.Size = new System.Drawing.Size(168, 21);
            this.text_pub_topic.TabIndex = 25;
            this.text_pub_topic.Text = "test";
            // 
            // picture_pub_result
            // 
            this.picture_pub_result.Location = new System.Drawing.Point(188, 192);
            this.picture_pub_result.Name = "picture_pub_result";
            this.picture_pub_result.Size = new System.Drawing.Size(28, 28);
            this.picture_pub_result.TabIndex = 24;
            this.picture_pub_result.TabStop = false;
            // 
            // publish
            // 
            this.publish.Location = new System.Drawing.Point(85, 192);
            this.publish.Name = "publish";
            this.publish.Size = new System.Drawing.Size(97, 21);
            this.publish.TabIndex = 23;
            this.publish.Text = "publish";
            this.publish.UseVisualStyleBackColor = true;
            this.publish.Click += new System.EventHandler(this.publish_Click);
            // 
            // message_pub
            // 
            this.message_pub.Location = new System.Drawing.Point(85, 90);
            this.message_pub.Multiline = true;
            this.message_pub.Name = "message_pub";
            this.message_pub.Size = new System.Drawing.Size(316, 81);
            this.message_pub.TabIndex = 28;
            this.message_pub.Text = "hello world";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "要发送的消息";
            // 
            // picture_sendquest_result
            // 
            this.picture_sendquest_result.Location = new System.Drawing.Point(373, 192);
            this.picture_sendquest_result.Name = "picture_sendquest_result";
            this.picture_sendquest_result.Size = new System.Drawing.Size(28, 28);
            this.picture_sendquest_result.TabIndex = 31;
            this.picture_sendquest_result.TabStop = false;
            // 
            // sendrequest
            // 
            this.sendrequest.Location = new System.Drawing.Point(270, 192);
            this.sendrequest.Name = "sendrequest";
            this.sendrequest.Size = new System.Drawing.Size(97, 21);
            this.sendrequest.TabIndex = 30;
            this.sendrequest.Text = "sendquest";
            this.sendrequest.UseVisualStyleBackColor = true;
            this.sendrequest.Click += new System.EventHandler(this.sendrequest_Click);
            // 
            // unsub_result
            // 
            this.unsub_result.Location = new System.Drawing.Point(347, 72);
            this.unsub_result.Name = "unsub_result";
            this.unsub_result.Size = new System.Drawing.Size(28, 28);
            this.unsub_result.TabIndex = 33;
            this.unsub_result.TabStop = false;
            // 
            // unsubscribe
            // 
            this.unsubscribe.Location = new System.Drawing.Point(244, 72);
            this.unsubscribe.Name = "unsubscribe";
            this.unsubscribe.Size = new System.Drawing.Size(97, 21);
            this.unsubscribe.TabIndex = 32;
            this.unsubscribe.Text = "unsubscribe";
            this.unsubscribe.UseVisualStyleBackColor = true;
            this.unsubscribe.Click += new System.EventHandler(this.unsubscribe_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.disconnect);
            this.groupBox1.Controls.Add(this.connectResult);
            this.groupBox1.Controls.Add(this.connect);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.instanceKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.instanceId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverURI);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 153);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接与断开连接";
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(259, 108);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(97, 21);
            this.disconnect.TabIndex = 9;
            this.disconnect.Text = "disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.subqos);
            this.groupBox2.Controls.Add(this.text_sub_qos);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.unsub_result);
            this.groupBox2.Controls.Add(this.unsubscribe);
            this.groupBox2.Controls.Add(this.messageReceive);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.text_sub_or_unsub_qos);
            this.groupBox2.Controls.Add(this.subResult);
            this.groupBox2.Controls.Add(this.subscribe);
            this.groupBox2.Location = new System.Drawing.Point(12, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 228);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "订阅与取消订阅";
            // 
            // subqos
            // 
            this.subqos.AutoSize = true;
            this.subqos.Location = new System.Drawing.Point(41, 49);
            this.subqos.Name = "subqos";
            this.subqos.Size = new System.Drawing.Size(23, 12);
            this.subqos.TabIndex = 35;
            this.subqos.Text = "qos";
            // 
            // text_sub_qos
            // 
            this.text_sub_qos.Location = new System.Drawing.Point(88, 45);
            this.text_sub_qos.Name = "text_sub_qos";
            this.text_sub_qos.Size = new System.Drawing.Size(168, 21);
            this.text_sub_qos.TabIndex = 34;
            this.text_sub_qos.Text = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "接收到的消息";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.text_pub_qos);
            this.groupBox3.Controls.Add(this.picture_sendquest_result);
            this.groupBox3.Controls.Add(this.sendrequest);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.message_pub);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.text_pub_topic);
            this.groupBox3.Controls.Add(this.picture_pub_result);
            this.groupBox3.Controls.Add(this.publish);
            this.groupBox3.Location = new System.Drawing.Point(446, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(412, 228);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发布消息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "qos";
            // 
            // text_pub_qos
            // 
            this.text_pub_qos.Location = new System.Drawing.Point(85, 49);
            this.text_pub_qos.Name = "text_pub_qos";
            this.text_pub_qos.Size = new System.Drawing.Size(168, 21);
            this.text_pub_qos.TabIndex = 32;
            this.text_pub_qos.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "SDK当前连接状态";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DataHubDemo.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(713, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(145, 72);
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.picture_connect_status);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(444, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(255, 153);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SDK当前连接状态";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // picture_connect_status
            // 
            this.picture_connect_status.Location = new System.Drawing.Point(109, 47);
            this.picture_connect_status.Name = "picture_connect_status";
            this.picture_connect_status.Size = new System.Drawing.Size(28, 28);
            this.picture_connect_status.TabIndex = 10;
            this.picture_connect_status.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 415);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.connectResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_pub_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_sendquest_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unsub_result)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_connect_status)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox serverURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox instanceId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox instanceKey;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.PictureBox connectResult;
        private System.Windows.Forms.PictureBox subResult;
        private System.Windows.Forms.Button subscribe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_sub_or_unsub_qos;
        private System.Windows.Forms.TextBox messageReceive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_pub_topic;
        private System.Windows.Forms.PictureBox picture_pub_result;
        private System.Windows.Forms.Button publish;
        private System.Windows.Forms.TextBox message_pub;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox picture_sendquest_result;
        private System.Windows.Forms.Button sendrequest;
        private System.Windows.Forms.PictureBox unsub_result;
        private System.Windows.Forms.Button unsubscribe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Label subqos;
        private System.Windows.Forms.TextBox text_sub_qos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_pub_qos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox picture_connect_status;
    }
}
