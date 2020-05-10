using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using KusaMochiAutoLibrary.Recorders;

namespace KusaMochiAuto.Views
{
    /// <summary>
    /// SettingDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingDialog : UserControl
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void StopKeyHelp_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null) return;

            ToolTip toolTip = element.ToolTip as ToolTip;
            if (toolTip == null) return;

            toolTip.IsOpen = true;
        }

        private void StopKeyHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null) return;

            ToolTip toolTip = element.ToolTip as ToolTip;
            if (toolTip == null) return;

            toolTip.IsOpen = false;
        }
    }
}
