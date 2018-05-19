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

        private Calculation calculation = new Calculation();

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        Cell[,] cells = new Cell[x, y];

        public MainWindow()
        {
            InitializeComponent();
            calculation.CellsToBeChanged += ChangeCells;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(sliderSpeed.Value);
            dispatcherTimer.Tick += Calculate;
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("something happened");
        }


        private void Calculate(object sender, EventArgs e)
        {
            calculation.CalculateNextGeneration();
        }

        private void ChangeCells(List<Tuple<int,int>> coordinates)
        {
            foreach (var coordinateTuple in coordinates)
            {
                cells[coordinateTuple.Item1, coordinateTuple.Item2].State = !cells[coordinateTuple.Item1, coordinateTuple.Item2].State;
            }
        }

        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(sliderSpeed.Value);
        }

        private void Window_Activated(object sender, EventArgs e) // The 'Activated' event usage is bad due to the fact that it can be invoked more than once thus overriding existing cells with dead ones
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
