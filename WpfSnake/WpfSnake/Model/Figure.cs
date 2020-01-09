using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Model
{
    public class Figure : INotifyPropertyChanged
    {
        private int left;
        private int top;
        private bool visibility;
        private String color;

        public string ElementType { get; set; }

        public int FigureWidth { get; set; } = 20;
        public int FigureHeight { get; set; } = 20;
        public int Left
        {
            get { return left; }
            set
            {
                left = value;
            }
        }
        public int Top
        {
            get { return top; }
            set
            {
                top = value;
            }
        }
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
            }
        }
        public String Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
