using System;
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
using Newtonsoft;
using System.Net;
using Newtonsoft.Json;

namespace StarWarsCharacters
{
    public partial class MainWindow : Window
    {
        private AllInfo _info;

        public MainWindow()
        {
            _info = new AllInfo();

            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string url = "https://swapi.dev/api/people/";
            string json = new WebClient().DownloadString(url);

            _info = JsonConvert.DeserializeObject<AllInfo>(json);

            FillStackPanel();
        }

        private void FillStackPanel()
        {
            foreach (var character in _info.characters)
            {
                Grid grid = CreateGrid();

                Button bttn = CreateButton(character);

                bttn.Click += Bttn_Click;

                grid.Children.Add(bttn);
            }
        }

        private Grid CreateGrid()
        {
            Grid grid = new Grid
            {
                Height = 100,
                Width = 200,
                Background = Brushes.Lavender
            };

            charWP.Children.Add(grid);

            return grid;
        }

        private Button CreateButton(Character character)
        {
            Button bttn = new Button
            {
                Content = character.Name,
                Height = 80,
                Width = 150,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.MintCream,
                FontSize = 16,
                FontFamily = new FontFamily("Verdana")
            };

            return bttn;
        }

        private void Bttn_Click(object sender, RoutedEventArgs e)
        {
            charInfo.Items.Clear();

            foreach (var character in _info.characters)
            {
                if ((sender as Button).Content as string == character.Name)
                {
                    charInfo.Items.Add(character);
                }
            }
        }
    }
}
