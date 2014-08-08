using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Code.Common
{
    public class Colors : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Color = string.Empty;
        public string Color
        {
            get
            {
                return this._Color;
            }

            set
            {
                if (this._Color != value)
                {
                    this._Color = value;
                    this.OnPropertyChanged("Color");
                }
            }
        }
        private Windows.UI.Colors _Colors1 = null;
        public Windows.UI.Colors Colors1
        {
            get
            {
                return this._Colors1;
            }

            set
            {
                if (this._Colors1 != value)
                {
                    this._Colors1 = value;
                    this.OnPropertyChanged("Colors1");
                }
            }
        }

    }
}
