namespace Server.Forms
{
  using System;
  using System.Net;
  using System.Windows.Forms;

  using BusinessLogic;
  using BusinessLogic.Config;

  public partial class MainForm : Form
  {
    #region Fields

    private NetworkManager _networkManager;

    #endregion

    #region Constructors

    public MainForm()
    {
      InitializeComponent();
    }

    #endregion

    #region Methods

    private void _startButton_Click(object sender, EventArgs e)
    {
      try
      {
        SetConfig();
        _startButton.Enabled = false;
        Config config = ServerConfig.GetConfig();
        _networkManager = new NetworkManager(config.Address, Convert.ToInt32(config.Timeout));
        _networkManager.Start();
        _stopButton.Enabled = true;
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private void _stopButton_Click(object sender, EventArgs e)
    {
      try {
        _startButton.Enabled = true;
        ((Button)sender).Enabled = false;
        _networkManager.Stop();
        _stopButton.Enabled = false;
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    #endregion

    private void _ipTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void _portTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void _timeoutTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void SetConfig()
    {
      try
      {
        ServerConfig.SetConfig(
          new IPEndPoint(IPAddress.Parse(_ipTextBox.Text), Convert.ToInt32(_portTextBox.Text)),
          Convert.ToUInt32(_timeoutTextBox.Text));
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      Config config = ServerConfig.GetConfig();
      _ipTextBox.Text = config.Address.Address.ToString();
      _portTextBox.Text = config.Address.Port.ToString();
      _timeoutTextBox.Text = config.Timeout.ToString();
    }
  }
}
