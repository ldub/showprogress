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
using System.Threading;

namespace ShowProgress {
    /// <summary>
    /// Interaction logic for UpDownButtonControl.xaml
    /// </summary>
    public partial class UpDownButtonControl : UserControl {
        private int numberInside;

        public UpDownButtonControl() {
            InitializeComponent();
            numberInside = 0;
            UpdateDisplay();
            theTextBox.TextChanged += new TextChangedEventHandler(OnTextChanged);
            theTextBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(theTextBox_KeyDown), true);
        }

        public UpDownButtonControl(int number) {
            InitializeComponent();
            numberInside = number;
            UpdateDisplay();
            theTextBox.TextChanged += new TextChangedEventHandler(OnTextChanged);
            theTextBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(theTextBox_KeyDown), true);
        }

        public int NumberInside {
            get {
                return numberInside;
            }
            set {
                numberInside = value;
                UpdateDisplay();
            }
        }

        public void theTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up || e.Key == Key.Right) {
                numberInside++;
                UpdateDisplay();
            } else if (e.Key == Key.Down || e.Key == Key.Left) {
                numberInside--;
                UpdateDisplay();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            if (e.Key == Key.Up || e.Key == Key.Right) {
                numberInside++;
                UpdateDisplay();
            } else if (e.Key == Key.Down || e.Key == Key.Left) {
                numberInside--;
                UpdateDisplay();
            }
        }

        void UpdateDisplay() {
            theTextBox.Text = numberInside.ToString();
        }

        void OnTextChanged(object sender, TextChangedEventArgs e) {
            e.Handled = true;
            for (int i = 0; i < theTextBox.Text.Length; i++) {
                if (!char.IsDigit(theTextBox.Text[i])) {
                    theTextBox.Text = theTextBox.Text.Remove(i, 1);
                }
            }

            if (theTextBox.Text.Length > 0) {
                numberInside = Convert.ToInt32(theTextBox.Text);
            } else {
                numberInside = 0;
            };
        }

        private void UpButton_Click(object sender, RoutedEventArgs e) {
            numberInside++;
            //while (UpButton.IsPressed) {
            //    NumberInside++;
            //    Thread.Sleep(100);
            //}
            UpdateDisplay();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e) {
            numberInside--;
            UpdateDisplay();
        }
    }
}
