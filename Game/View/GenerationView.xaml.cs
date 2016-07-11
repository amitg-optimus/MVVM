using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using Game.Models;
using Game.ViewModels;
using Game.Utilities;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GenerationView : Window
    {      
        private GenerationViewModel generationViewModel;

        //TODO: can be configured
        private int UNIVERSIZE = 5;

        public GenerationView()
        {
            InitializeComponent();

            generationViewModel = new GenerationViewModel(UNIVERSIZE);
            
            BuildGridUI(generationViewModel);

            DataContext = generationViewModel;
        }

        /// <summary>
        /// TODO:GUI build from code behind to make it dynamic in future
        /// Generate the GUI for the universe grid, each cell contains a texy block
        /// </summary>
        /// <param name="generationViewModel"></param>
        private void BuildGridUI(GenerationViewModel generationViewModel)
        { 
            for (int row = 0; row < generationViewModel.UniverseSize; row++)
            {
                UniverseGrid.RowDefinitions.Add(new RowDefinition());

                for (int column = 0; column < generationViewModel.UniverseSize; column++)
                {
                    if (row == 0)
                        UniverseGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    
                  
                    TextBlock cellTextBlock = CreateCellTextBlock(generationViewModel.GetCell(row, column));
                    
                  
                    Grid.SetRow(cellTextBlock, row);
                    Grid.SetColumn(cellTextBlock, column);
                    
                    UniverseGrid.Children.Add(cellTextBlock);
                }
            }           
        }
        
        private TextBlock CreateCellTextBlock(Cell cell)
        {
            TextBlock cellTextBlock = new TextBlock();
            cellTextBlock.DataContext = cell;
            cellTextBlock.InputBindings.Add(CreateMouseClickInputBinding(cell));
            cellTextBlock.SetBinding(TextBlock.BackgroundProperty, CreateCellAliveBinding());

            return cellTextBlock;
        }

      
        private InputBinding CreateMouseClickInputBinding(Cell cell)
        {
            InputBinding cellTextBlockInputBinding = new InputBinding(
                generationViewModel.ToggleCellLifeCommand,
                new MouseGesture(MouseAction.LeftClick)
            );
            cellTextBlockInputBinding.CommandParameter = string.Format("{0},{1}", cell.Row, cell.Column);

            return cellTextBlockInputBinding;
        }

        private Binding CreateCellAliveBinding()
        {
            return new Binding
            {
                Path = new PropertyPath("Status"),
                Mode = BindingMode.TwoWay,
                Converter = new LifeToColourConverter(
                    aliveColour: Brushes.Black,
                    deadColour: Brushes.White
                )
            };
        }

        public int UniverseSize { get; set; }
    }
}
