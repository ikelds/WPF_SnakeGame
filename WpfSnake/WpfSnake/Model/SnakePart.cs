using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Model
{
    public class SnakePart : Figure
    {
        //public bool isHead { get; set; }

        private bool isHead;

        public bool IsHead
        {
            get { return isHead; }
            set
            {
                isHead = value;
                OnPropertyChanged();
            }

        }


        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName]string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}
    }
}
