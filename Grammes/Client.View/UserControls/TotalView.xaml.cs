﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.View.UserControls
{
  /// <summary>
  /// Interaction logic for TotalView.xaml
  /// </summary>
  public partial class TotalView : UserControl
  {
    public TotalView()
    {
      InitializeComponent();
    }

    private void OnChange(object sender, SelectionChangedEventArgs e)
    {
      //ComboBox cb = sender as ComboBox;
      //if (cb != null && presenter != null)
      //  presenter.Content = cb.SelectedIndex;
    }
  }
}
