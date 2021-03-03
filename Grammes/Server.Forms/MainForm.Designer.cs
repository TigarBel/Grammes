
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
      this._ipTextBox = new System.Windows.Forms.TextBox();
      this._portTextBox = new System.Windows.Forms.TextBox();
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
      this.SuspendLayout();
      // 
      // _ipTextBox
      // 
      this._ipTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._ipTextBox.Location = new System.Drawing.Point(142, 12);
      this._ipTextBox.Name = "_ipTextBox";
      this._ipTextBox.Size = new System.Drawing.Size(174, 23);
      this._ipTextBox.TabIndex = 0;
      this._ipTextBox.TextChanged += new System.EventHandler(this._ipTextBox_TextChanged);
      // 
      // _portTextBox
      // 
      this._portTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._portTextBox.Location = new System.Drawing.Point(142, 41);
      this._portTextBox.Name = "_portTextBox";
      this._portTextBox.Size = new System.Drawing.Size(174, 23);
      this._portTextBox.TabIndex = 1;
      this._portTextBox.TextChanged += new System.EventHandler(this._portTextBox_TextChanged);
      // 
      // _timeoutTextBox
      // 
      this._timeoutTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._timeoutTextBox.Location = new System.Drawing.Point(142, 70);
      this._timeoutTextBox.Name = "_timeoutTextBox";
      this._timeoutTextBox.Size = new System.Drawing.Size(174, 23);
      this._timeoutTextBox.TabIndex = 2;
      this._timeoutTextBox.TextChanged += new System.EventHandler(this._timeoutTextBox_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(59, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(20, 17);
      this.label1.TabIndex = 3;
      this.label1.Text = "IP";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.Location = new System.Drawing.Point(59, 44);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 17);
      this.label2.TabIndex = 4;
      this.label2.Text = "Port";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label3.Location = new System.Drawing.Point(59, 75);
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
      this._startButton.TabIndex = 6;
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
      this._stopButton.TabIndex = 7;
      this._stopButton.Text = "Stop";
      this._stopButton.UseVisualStyleBackColor = true;
      this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label4.Location = new System.Drawing.Point(59, 133);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(77, 17);
      this.label4.TabIndex = 11;
      this.label4.Text = "DB catalog";
      this.label4.Click += new System.EventHandler(this.label4_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label5.Location = new System.Drawing.Point(59, 102);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(74, 17);
      this.label5.TabIndex = 10;
      this.label5.Text = "DB source";
      // 
      // _dbCatalogTextBox
      // 
      this._dbCatalogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._dbCatalogTextBox.Location = new System.Drawing.Point(142, 128);
      this._dbCatalogTextBox.Name = "_dbCatalogTextBox";
      this._dbCatalogTextBox.Size = new System.Drawing.Size(174, 23);
      this._dbCatalogTextBox.TabIndex = 9;
      this._dbCatalogTextBox.TextChanged += new System.EventHandler(this._dbCatalogTextBox_TextChanged);
      // 
      // _dbSourceTextBox
      // 
      this._dbSourceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._dbSourceTextBox.Location = new System.Drawing.Point(142, 99);
      this._dbSourceTextBox.Name = "_dbSourceTextBox";
      this._dbSourceTextBox.Size = new System.Drawing.Size(174, 23);
      this._dbSourceTextBox.TabIndex = 8;
      this._dbSourceTextBox.TextChanged += new System.EventHandler(this._dbSourceTextBox_TextChanged);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 261);
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
      this.Controls.Add(this._portTextBox);
      this.Controls.Add(this._ipTextBox);
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

    private System.Windows.Forms.TextBox _ipTextBox;
    private System.Windows.Forms.TextBox _portTextBox;
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
  }
}

