
namespace Server.Forms
{
  partial class MainForm
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
      if (disposing && (components != null)) {
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
      this._webIpTextBox = new System.Windows.Forms.TextBox();
      this._webPortTextBox = new System.Windows.Forms.TextBox();
      this._timeoutTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this._startButton = new System.Windows.Forms.Button();
      this._stopButton = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this._dbCatalogTextBox = new System.Windows.Forms.TextBox();
      this._dbSourceTextBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this._tcpPortTextBox = new System.Windows.Forms.TextBox();
      this._tcpIpTextBox = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // _webIpTextBox
      // 
      this._webIpTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._webIpTextBox.Location = new System.Drawing.Point(52, 29);
      this._webIpTextBox.Name = "_webIpTextBox";
      this._webIpTextBox.Size = new System.Drawing.Size(128, 23);
      this._webIpTextBox.TabIndex = 0;
      this._webIpTextBox.TextChanged += new System.EventHandler(this._webIpTextBox_TextChanged);
      // 
      // _webPortTextBox
      // 
      this._webPortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._webPortTextBox.Location = new System.Drawing.Point(52, 58);
      this._webPortTextBox.Name = "_webPortTextBox";
      this._webPortTextBox.Size = new System.Drawing.Size(128, 23);
      this._webPortTextBox.TabIndex = 2;
      this._webPortTextBox.TextChanged += new System.EventHandler(this._webPortTextBox_TextChanged);
      // 
      // _timeoutTextBox
      // 
      this._timeoutTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._timeoutTextBox.Location = new System.Drawing.Point(142, 87);
      this._timeoutTextBox.Name = "_timeoutTextBox";
      this._timeoutTextBox.Size = new System.Drawing.Size(174, 23);
      this._timeoutTextBox.TabIndex = 4;
      this._timeoutTextBox.TextChanged += new System.EventHandler(this._timeoutTextBox_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(12, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(20, 17);
      this.label1.TabIndex = 3;
      this.label1.Text = "IP";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.Location = new System.Drawing.Point(12, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 17);
      this.label2.TabIndex = 4;
      this.label2.Text = "Port";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label3.Location = new System.Drawing.Point(59, 92);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 17);
      this.label3.TabIndex = 5;
      this.label3.Text = "Timeout";
      // 
      // _startButton
      // 
      this._startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._startButton.Location = new System.Drawing.Point(12, 215);
      this._startButton.Name = "_startButton";
      this._startButton.Size = new System.Drawing.Size(120, 34);
      this._startButton.TabIndex = 7;
      this._startButton.Text = "Start";
      this._startButton.UseVisualStyleBackColor = true;
      this._startButton.Click += new System.EventHandler(this._startButton_Click);
      // 
      // _stopButton
      // 
      this._stopButton.Enabled = false;
      this._stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._stopButton.Location = new System.Drawing.Point(252, 215);
      this._stopButton.Name = "_stopButton";
      this._stopButton.Size = new System.Drawing.Size(120, 34);
      this._stopButton.TabIndex = 8;
      this._stopButton.Text = "Stop";
      this._stopButton.UseVisualStyleBackColor = true;
      this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label4.Location = new System.Drawing.Point(59, 150);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(77, 17);
      this.label4.TabIndex = 11;
      this.label4.Text = "DB catalog";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label5.Location = new System.Drawing.Point(59, 119);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(74, 17);
      this.label5.TabIndex = 10;
      this.label5.Text = "DB source";
      // 
      // _dbCatalogTextBox
      // 
      this._dbCatalogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._dbCatalogTextBox.Location = new System.Drawing.Point(142, 145);
      this._dbCatalogTextBox.Name = "_dbCatalogTextBox";
      this._dbCatalogTextBox.Size = new System.Drawing.Size(174, 23);
      this._dbCatalogTextBox.TabIndex = 6;
      this._dbCatalogTextBox.TextChanged += new System.EventHandler(this._dbCatalogTextBox_TextChanged);
      // 
      // _dbSourceTextBox
      // 
      this._dbSourceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._dbSourceTextBox.Location = new System.Drawing.Point(142, 116);
      this._dbSourceTextBox.Name = "_dbSourceTextBox";
      this._dbSourceTextBox.Size = new System.Drawing.Size(174, 23);
      this._dbSourceTextBox.TabIndex = 5;
      this._dbSourceTextBox.TextChanged += new System.EventHandler(this._dbSourceTextBox_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label6.Location = new System.Drawing.Point(204, 61);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(34, 17);
      this.label6.TabIndex = 15;
      this.label6.Text = "Port";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label7.Location = new System.Drawing.Point(204, 32);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(20, 17);
      this.label7.TabIndex = 14;
      this.label7.Text = "IP";
      // 
      // _tcpPortTextBox
      // 
      this._tcpPortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._tcpPortTextBox.Location = new System.Drawing.Point(244, 58);
      this._tcpPortTextBox.Name = "_tcpPortTextBox";
      this._tcpPortTextBox.Size = new System.Drawing.Size(128, 23);
      this._tcpPortTextBox.TabIndex = 3;
      this._tcpPortTextBox.TextChanged += new System.EventHandler(this._tcpPortTextBox_TextChanged);
      // 
      // _tcpIpTextBox
      // 
      this._tcpIpTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._tcpIpTextBox.Location = new System.Drawing.Point(244, 29);
      this._tcpIpTextBox.Name = "_tcpIpTextBox";
      this._tcpIpTextBox.Size = new System.Drawing.Size(128, 23);
      this._tcpIpTextBox.TabIndex = 1;
      this._tcpIpTextBox.TextChanged += new System.EventHandler(this._tcpIpTextBox_TextChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label8.Location = new System.Drawing.Point(12, 9);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(82, 17);
      this.label8.TabIndex = 16;
      this.label8.Text = "Web socket";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label9.Location = new System.Drawing.Point(204, 9);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(35, 17);
      this.label9.TabIndex = 17;
      this.label9.Text = "TCP";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 261);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label7);
      this.Controls.Add(this._tcpPortTextBox);
      this.Controls.Add(this._tcpIpTextBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label5);
      this.Controls.Add(this._dbCatalogTextBox);
      this.Controls.Add(this._dbSourceTextBox);
      this.Controls.Add(this._stopButton);
      this.Controls.Add(this._startButton);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this._timeoutTextBox);
      this.Controls.Add(this._webPortTextBox);
      this.Controls.Add(this._webIpTextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.ShowIcon = false;
      this.Text = "Grammes Server";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox _webIpTextBox;
    private System.Windows.Forms.TextBox _webPortTextBox;
    private System.Windows.Forms.TextBox _timeoutTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button _startButton;
    private System.Windows.Forms.Button _stopButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox _dbCatalogTextBox;
    private System.Windows.Forms.TextBox _dbSourceTextBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox _tcpPortTextBox;
    private System.Windows.Forms.TextBox _tcpIpTextBox;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
  }
}

