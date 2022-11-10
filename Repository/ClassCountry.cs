using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCountry : ClassNotify
    {
        private int _Id;
        private string _countryName;
        private string _countryCode;
        private string _valutaName;
        private double _valutaRate;
        private DateTime _updateTime;

        /// <summary>
        /// A: Default Constructor
        /// </summary>
        public ClassCountry()
        {
            Id = 0;
            countryName = "";
            countryCode = "DKK";
            valutaName = "";
            valutaRate = 1D;
            updateTime = DateTime.Now;
        }

        /// <summary>
        /// A: Constructor to make a dupplicate
        /// </summary>
        public ClassCountry(ClassCountry inCountry)
        {
            Id = inCountry.Id;
            countryName = inCountry.countryName;
            countryCode = inCountry.countryCode;
            valutaName = inCountry.valutaName;
            valutaRate = inCountry.valutaRate;
            updateTime = inCountry.updateTime;
        }

        public double valutaRate
        {
            get { return _valutaRate; }
            set
            {
                if (_valutaRate != value)
                {
                    _valutaRate = value;
                }
                Notify("valutaRate");
            }
        }
        public string valutaName
        {
            get { return _valutaName; }
            set
            {
                if (_valutaName != value)
                {
                    _valutaName = value;
                }
                Notify("valutaName");
            }
        }
        public string countryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode != value)
                {
                    _countryCode = value;
                }
                Notify("countryCode");
            }
        }
        public string countryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName != value)
                {
                    _countryName = value;
                }
                Notify("countryName");
            }
        }
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
                Notify("Id");
            }
        }

        public DateTime updateTime
        {
            get { return _updateTime; }
            set
            {
                if (_updateTime != value)
                {
                    _updateTime = value;
                }
                Notify("updateTime");
            }
        }

    }
}
