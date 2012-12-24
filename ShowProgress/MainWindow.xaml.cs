// ShowProgress v 1.0
//  - Created by Lev Dubinets
//  - This program remembers which episode of which show you are on for you.
//  - 08/24/2011

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private int numShows;
        private string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\shows.txt";

        public MainWindow() {
            InitializeComponent();
            LoadShows();
            //myWindow.Resources.Add("myResourceKey", myVariable);
        }

        private void LoadShows() {
            if (File.Exists(appDataPath)) {
                numShows = File.ReadLines(appDataPath).Count();
                StreamReader srShows = new StreamReader(appDataPath);
                for (int i = 0; i < numShows; i++) {
                    string currentLine = srShows.ReadLine(); //iterator for going through the shows
                    
                    int separatorLocation = currentLine.IndexOf('|'); //for parsing show name and ep. number
                    
                    string showName = currentLine.Substring(0, separatorLocation);
                    int episodeNum = Convert.ToInt32(currentLine.Substring(separatorLocation + 1));

                    StackPanel splShow = new StackPanel();                                      //The structure is like so:
                    splShow.Name = "splShow" + i.ToString();                                    // [ [ {deleteButton0} (show0) ]
                    splShow.Orientation = Orientation.Horizontal;                               //   [ {deleteButton1} (show1) ]
                    Button deleteButton = new Button();                                         //               ...
                    deleteButton.Name = "delete" + i.ToString();                                //   [ {deleteButtonN} (showN) ] ]
                    deleteButton.Height = 15;                                                   // Where [] are StackPanels, {} are Buttons,
                    deleteButton.Width = 15;                                                    //  and () are Shows.
                    deleteButton.Margin = new System.Windows.Thickness(2.5, 1, 2.5, 1);         // It *was* done like this to facilitate show deletion,
                    deleteButton.Content = "-";                                                 // but it turned out I failed at that and now have implemented
                    deleteButton.Click += new RoutedEventHandler(deleteShow);                   // much better. Kept the structure, it seems good.
                    ShowProgress.Show newShow = new Show(showName, episodeNum);
                    newShow.Name = "show" + i.ToString();
                    newShow.Height = 30;                                                        
                    newShow.Width = 200;                                                        
                    newShow.Margin = new System.Windows.Thickness(5, 5, 5, 5);                  
                    splMain.Children.Add(splShow);                                              
                    splShow.Children.Add(deleteButton);                                         
                    splShow.Children.Add(newShow);                                                                      
                }

                srShows.Close();
            } else {
                File.Create(appDataPath);
            }
        }

        private void SaveShows() {
            if (!File.Exists(appDataPath)) {
                File.Create(appDataPath);
            }

            numShows = splMain.Children.Count;
            StreamWriter swShows = new StreamWriter(appDataPath);
            for (int i = 0; i < numShows; i++) {
                StackPanel splCurrent = (StackPanel)splMain.Children[i];
                ShowProgress.Show showCurrent = (ShowProgress.Show)splCurrent.Children[1];
                //UpDownButtonControl udbcCurrent = (UpDownButtonControl)showCurrent.

                string showName = showCurrent.ShowName;
                int separatorIndex = 0;
                while (showName.Contains('|')) {
                    separatorIndex = showName.IndexOf('|');
                    showName = showName.Remove(separatorIndex, 1);
                }

                int episodeNum = showCurrent.upDownControl.NumberInside;
                swShows.WriteLine(showName + "|" + episodeNum.ToString());
            }
            swShows.Flush();
            swShows.Close();
        }

        public void deleteShow(object sender, RoutedEventArgs e) {
            Button clickedButton = (Button) sender;
            StackPanel fatherSpl = (StackPanel) clickedButton.Parent;
            splMain.Children.Remove(fatherSpl);
        }

        private void addShow_Click(object sender, RoutedEventArgs e) {
            numShows++;
            StackPanel splShow = new StackPanel();                                      //The structure is like so:
            splShow.Name = "splShow" + (numShows - 1).ToString();                             // [ [ {deleteButton0} (show0) ]
            splShow.Orientation = Orientation.Horizontal;                               //   [ {deleteButton1} (show1) ]
            Button deleteButton = new Button();                                         //               ...
            deleteButton.Name = "delete" + (numShows - 1).ToString();                         //   [ {deleteButtonN} (showN) ] ]
            deleteButton.Height = 15;                                                   // Where [] are StackPanels, {} are Buttons,
            deleteButton.Width = 15;                                                    //  and () are Shows.
            deleteButton.Margin = new System.Windows.Thickness(2.5, 1, 2.5, 1);         // It is done like this to facilitate show deletion.
            deleteButton.Content = "-";
            deleteButton.Click += new RoutedEventHandler(deleteShow);
            ShowProgress.Show newShow = new Show(newShowTextBox.Text);
            newShow.Name = "show" + (numShows - 1).ToString();
            newShow.Height = 30;
            newShow.Width = 200;
            newShow.Margin = new System.Windows.Thickness(5, 5, 5, 5);
            splMain.Children.Add(splShow);
            splShow.Children.Add(deleteButton);
            splShow.Children.Add(newShow);

            newShowTextBox.Text = "";
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            SaveShows();
        }

        private void newShowTextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
