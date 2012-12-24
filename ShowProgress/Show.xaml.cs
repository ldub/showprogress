using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ShowProgress {
    /// <summary>
    /// Interaction logic for Show.xaml
    /// </summary>
    public partial class Show : UserControl {
        private string showName;

        public Show() {
            InitializeComponent();
            showTextBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(upDownControl.theTextBox_KeyDown), true);
        }

        public Show(string ShowName) {
            InitializeComponent();
            showTextBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(upDownControl.theTextBox_KeyDown), true);
            showName = ShowName;
            UpdateShowDisplay();
        }

        public Show(string ShowName, int epNumber) {
            InitializeComponent();
            showTextBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(upDownControl.theTextBox_KeyDown), true);
            showName = ShowName;
            upDownControl.NumberInside = epNumber;
            UpdateShowDisplay();
        }

        private void UpdateShowDisplay() {
            showTextBox.Text = showName;
        }

        public string ShowName {
            get {
                return showName;
            }
            set {
                showName = value;
            }
        }

        private void showTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            showName = showTextBox.Text;
        }
    }
}
