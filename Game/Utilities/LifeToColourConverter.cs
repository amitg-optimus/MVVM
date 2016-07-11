using Game.Models;
using Game.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Game.Utilities
{
    /// <summary>
    /// Enables binding conversion between a cell's living status and a colour for whether
    /// the cell is dead or alive.
    /// </summary>
    public class LifeToColourConverter : IValueConverter
    {
        
        public SolidColorBrush AliveColour { get; set; }
        
        public SolidColorBrush DeadColour { get; set; }

        public LifeToColourConverter(SolidColorBrush aliveColour, SolidColorBrush deadColour)
        {
            AliveColour = aliveColour;
            DeadColour = deadColour;
        }
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (StateOfLife)value == StateOfLife.Alive ? AliveColour : DeadColour;
        }
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
                return ((SolidColorBrush) value) == AliveColour;

            return StateOfLife.Alive;
        }
    }
}
