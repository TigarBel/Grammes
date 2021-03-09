namespace Server.Forms
{
  using System;
  using System.Drawing;
  using System.Net;
  using System.Text.RegularExpressions;
  using System.Windows.Forms;

  using BusinessLogic;
  using BusinessLogic.Config;

  using Common.DataBase;

  using Properties;

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

    private void SwitchButton(bool turn)
    {
      _webIpTextBox.Enabled = !turn;
      _webPortTextBox.Enabled = !turn;
      _tcpIpTextBox.Enabled = !turn;
      _tcpPortTextBox.Enabled = !turn;
      _timeoutTextBox.Enabled = !turn;
      _dbSourceTextBox.Enabled = !turn;
      _dbCatalogTextBox.Enabled = !turn;
      _startButton.Enabled = !turn;
      _stopButton.Enabled = turn;
    }

    private void _startButton_Click(object sender, EventArgs e)
    {
      try
      {
        SwitchButton(true);

        SetConfig();
        Config config = ServerConfig.GetConfig();
        _networkManager = new NetworkManager(
          config.WebAddress,
          config.TcpAddress,
          Convert.ToInt32(config.Timeout),
          new DataBaseManager(_dbSourceTextBox.Text, _dbCatalogTextBox.Text));
        _networkManager.Start();
      }
      catch (Exception exception)
      {
        MessageBox.Show($"{exception.TargetSite.DeclaringType.Name}: {exception.Message}");
        SwitchButton(false);
        _networkManager?.Stop();
      }
    }

    private void _stopButton_Click(object sender, EventArgs e)
    {
      try
      {
        SwitchButton(false);
        _networkManager.Stop();
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private void _timeoutTextBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int timeout = Convert.ToInt32(((TextBox)sender).Text);
        CheckValue(timeout >= 1 && timeout < 900, (TextBox)sender);
      }
      catch
      {
        ((TextBox)sender).BackColor = Color.PaleVioletRed;
        _startButton.Enabled = false;
      }
    }

    private void _dbSourceTextBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        string source = ((TextBox)sender).Text;
        CheckValue(source != "", (TextBox)sender);
      }
      catch
      {
        ((TextBox)sender).BackColor = Color.PaleVioletRed;
        _startButton.Enabled = false;
      }
    }

    private void _dbCatalogTextBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        string catalog = ((TextBox)sender).Text;
        CheckValue(catalog != "", (TextBox)sender);
      }
      catch
      {
        ((TextBox)sender).BackColor = Color.PaleVioletRed;
        _startButton.Enabled = false;
      }
    }

    private void CheckValue(bool rule, TextBox textBox)
    {
      if (rule)
      {
        textBox.BackColor = Color.White;
        _startButton.Enabled = true;
      }
      else
      {
        textBox.BackColor = Color.PaleVioletRed;
        _startButton.Enabled = false;
      }
    }

    private void SetConfig()
    {
      try
      {
        ServerConfig.SetConfig(
          new IPEndPoint(IPAddress.Parse(_webIpTextBox.Text), Convert.ToInt32(_webPortTextBox.Text)),
          new IPEndPoint(IPAddress.Parse(_tcpIpTextBox.Text), Convert.ToInt32(_tcpPortTextBox.Text)),
          Convert.ToUInt32(_timeoutTextBox.Text),
          _dbSourceTextBox.Text,
          _dbCatalogTextBox.Text);
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      try
      {
        Config config = ServerConfig.GetConfig();
        StayParameters(config);
      }
      catch (NullReferenceException exception)
      {
        MessageBox.Show(exception.Message);
        Config config = ServerConfig.GetDefaultConfig();
        StayParameters(config);
        throw;
      }
    }

    private void StayParameters(Config config)
    {
      _webIpTextBox.Text = config.WebAddress.Address.ToString();
      _webPortTextBox.Text = config.WebAddress.Port.ToString();
      _tcpIpTextBox.Text = config.TcpAddress.Address.ToString();
      _tcpPortTextBox.Text = config.TcpAddress.Port.ToString();
      _timeoutTextBox.Text = config.Timeout.ToString();
      _dbSourceTextBox.Text = config.DataSource;
      _dbCatalogTextBox.Text = config.Catalog;
    }

    private void _webIpTextBox_TextChanged(object sender, EventArgs e)
    {
      IpTextChanged(sender);
    }

    private void _tcpIpTextBox_TextChanged(object sender, EventArgs e)
    {
      IpTextChanged(sender);
    }

    private void IpTextChanged(object sender)
    {
      string text = ((TextBox)sender).Text;
      CheckValue(new Regex(Resources.IpAddressUnacceptableSymbols, RegexOptions.IgnoreCase).IsMatch(text ?? string.Empty), (TextBox)sender);
    }

    private void _webPortTextBox_TextChanged(object sender, EventArgs e)
    {
      PortTextChanged(sender, _tcpPortTextBox.Text);
    }

    private void _tcpPortTextBox_TextChanged(object sender, EventArgs e)
    {
      PortTextChanged(sender, _webPortTextBox.Text);
    }

    private void PortTextChanged(object sender, string oppositePort)
    {
      try
      {
        int port = Convert.ToInt32(((TextBox)sender).Text);
        if (string.IsNullOrEmpty(oppositePort))
        {
          CheckValue(port >= 0 && port < 65000, (TextBox)sender);
        }
        else
        {
          int opposite = Convert.ToInt32(oppositePort);
          CheckValue(port >= 0 && port < 65000 && port != opposite, (TextBox)sender);
        }
      }
      catch
      {
        ((TextBox)sender).BackColor = Color.PaleVioletRed;
        _startButton.Enabled = false;
      }
    }

    #endregion
  }
}
