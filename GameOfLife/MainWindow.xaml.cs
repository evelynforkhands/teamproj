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
        private int x;
        private int y;
        
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private Generation _gen;

        Cell[,] cells;

        private bool fieldSet = false;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500 - sliderSpeed.Value);
            dispatcherTimer.Tick += Calculate;
        }

        private void ChangeCells(List<Tuple<int, int>> coordinatesToChange)
        {
            foreach (var coordinatePair in coordinatesToChange)
            {
                if (cells[coordinatePair.Item1, coordinatePair.Item2].State == 1)
                    cells[coordinatePair.Item1, coordinatePair.Item2].State = 0;
                else cells[coordinatePair.Item1, coordinatePair.Item2].State = 1;
            }
        }

        private void SetNewCell(int i, int j, Location location)
        {
            cells[i, j] = new Cell(i, j);
            cells[i, j].Click += Cell_Click;
            _gen.Reachability[i,j] = location;
            gameGrid.Children.Add(cells[i, j]);
            Grid.SetColumn(cells[i, j], i);
            Grid.SetRow(cells[i, j], j);
        }

        private async void Calculate(object sender, EventArgs e)
        {
            ChangeCells(await Task.Factory.StartNew(_gen.Evolve));
        }

        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Dispatcher.Invoke(() => dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500 - sliderSpeed.Value));
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!fieldSet)
            {
                x = Factory.x = (int)gameGrid.ActualWidth / 12 - 1;
                y = Factory.y = (int)gameGrid.ActualHeight / 12 - 1;
                
                
                _gen = Factory.Instance.GetGeneration();
                cells = new Cell[x + 1, y + 1];

                for (int i = 0; i < x + 1; i++)
                {
                    gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int j = 0; j < y + 1; j++)
                {
                    gameGrid.RowDefinitions.Add(new RowDefinition());
                }

                SetNewCell(0, 0, Location.TopLeft);// setting the topLeft cell
                SetNewCell(0, y, Location.TopRight);//setting the topRight cell

                for (int j_topBottomRow = 1; j_topBottomRow < y; j_topBottomRow++)// setting the top & bottom row
                {
                    SetNewCell(0, j_topBottomRow, Location.Top);
                    SetNewCell(x, j_topBottomRow, Location.Bottom);
                }

                for (int i_center = 1; i_center < x ; i_center++)// cetting center, left & right columns
                {
                    SetNewCell(i_center, 0, Location.Left);
                    SetNewCell(i_center, y, Location.Right);

                    for (int j_center = 1; j_center < y ; j_center++)// setting the Center cells
                    {
                        SetNewCell(i_center, j_center, Location.Center);
                    }
                }

                SetNewCell(x, 0, Location.BottomLeft);// setting the bottomLeft cell
                SetNewCell(x, y, Location.BottomRight);//  setting the bottomRight cell

                fieldSet = true;
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Cell currentCell = sender as Cell;
            if (_gen.Field[currentCell.XCoordinate, currentCell.YCoordinate] == 0)
            {
                _gen.Field[currentCell.XCoordinate, currentCell.YCoordinate] = 1;
            }
            else
            {
                _gen.Field[currentCell.XCoordinate, currentCell.YCoordinate] = 0;
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
        }
    }
}
