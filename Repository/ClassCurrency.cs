using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCurrency : ClassNotify
    {
        private string _disclaimer;
        private string _license;
        private long _timestamp;
        private string _Base;
        private Dictionary<string, decimal> _rates;

        public ClassCurrency()
        {
            disclaimer = "";
            license = "";
            timestamp = 0;
            Base = "";
            rates = new Dictionary<string, decimal>();
        }

        public string disclaimer
        {
            get { return _disclaimer; }
            set
            {
                if (_disclaimer != value)
                {
                    _disclaimer = value;
                }
                Notify("disclaimer");
            }
        }
        public string license
        {
            get { return _license; }
            set
            {
                if (_license != value)
                {
                    _license = value;
                }
                Notify("license");
            }
        }
        public long timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                }
                Notify("timestamp");
            }
        }
        public string Base
        {
            get { return _Base; }
            set
            {
                if (_Base != value)
                {
                    _Base = value;
                }
                Notify("Base");
            }
        }
        public Dictionary<string, decimal> rates
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
