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
using System.Windows.Threading;
using CustomControl;
using GameOfLife.Classes;
using GameOfLife.Classes.Helpers;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int x = 100; // Not actually constants
        const int y = 100; // Have to be calculated separately

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private Generation _gen = new Generation(x,y);

        Cell[,] cells = new Cell[x,y];

        private bool fieldSet = false;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000 - sliderSpeed.Value);
            dispatcherTimer.Tick += Calculate;
        }

        private void ChangeCells(List<Tuple<int, int>> coordinatesToChange)
        {
            foreach (var coordinatePair in coordinatesToChange)
            {
                cells[coordinatePair.Item1, coordinatePair.Item2].State = !cells[coordinatePair.Item1, coordinatePair.Item2].State;
            }
        }

        private async void Calculate(object sender, EventArgs e)
        {
            ChangeCells(await Task.Factory.StartNew(_gen.Evolve));
        }

        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(sliderSpeed.Value);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!fieldSet)
            {
                mainWindowGrid.ColumnDefinitions[0].Width = new GridLength(ActualHeight);

                for (int i = 0; i < x; i++)
                {
                    gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    gameGrid.RowDefinitions.Add(new RowDefinition());
                    for (int j = 0; j < y; j++)
                    {
                        cells[i, j] = new Cell();
                        gameGrid.Children.Add(cells[i, j]);
                        Grid.SetColumn(cells[i, j], i);
                        Grid.SetRow(cells[i, j], j);
                    }
                }
                fieldSet = true;
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }
    }
}
