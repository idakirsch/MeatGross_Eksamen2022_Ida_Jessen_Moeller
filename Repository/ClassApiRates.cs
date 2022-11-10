using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassApiRates : ClassNotify
    {
        private Dictionary<string, double> _rates;

        public ClassApiRates()
        {
            rates = new Dictionary<string, double>();
        }

        public Dictionary<string, double> rates
        {
            get { return _rates; }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                }
                Notify("rates");
            }
        }
    }
}