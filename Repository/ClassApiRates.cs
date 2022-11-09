using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassApiRates : ClassNotify
    {
        private long _timeStamp;
        private Dictionary<string, double> _rates;
        private string _newTimeStamp;

        public ClassApiRates()
        {
            timeStamp = 0;
            rates = new Dictionary<string, double>();
            newTimeStamp = "";
        }

        public string newTimeStamp
        {
            get { return _newTimeStamp; }
            set
            {
                if (_newTimeStamp != value)
                {
                    _newTimeStamp = value;
                }
                Notify("newTimeStamp");
            }
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
        public long timeStamp
        {
            get { return _timeStamp; }
            set
            {
                if (_timeStamp != value)
                {
                    _timeStamp = value;
                }
                Notify("timeStamp");
            }
        }
    }
}