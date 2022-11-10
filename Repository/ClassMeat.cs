using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassMeat : ClassNotify
    {
        private int _id;
        private string _typeOfMeat;
        private int _stock;
        private string _strStock;
        private double _price;
        private string _strPrice;
        private DateTime _priceTimeStamp;
        private string _strTimeStamp;

        /// <summary>
        /// A: Default Constructor
        /// </summary>
        public ClassMeat()
        {
            id = 0;
            typeOfMeat = "";
            strStock = "";
            strPrice = "";
            priceTimeStamp = DateTime.Now;
            strTimeStamp = "";
        }

        /// <summary>
        /// A: Constructor to make a dupplicate
        /// </summary>
        public ClassMeat(ClassMeat inMeat)
        {
            id = inMeat.id;
            typeOfMeat = inMeat.typeOfMeat;
            strStock = inMeat.strStock;
            strPrice = inMeat.strPrice;
            stock = inMeat.stock;
            price = inMeat.price;
            priceTimeStamp = inMeat.priceTimeStamp;
        }

        public string strTimeStamp
        {
            get { return _strTimeStamp; }
            set
            {
                if (_strTimeStamp != value)
                {
                    _strTimeStamp = value;
                }
                Notify("strTimeStamp");
            }
        }
        public DateTime priceTimeStamp
        {
            get { return _priceTimeStamp; }
            set
            {
                if (_priceTimeStamp != value)
                {
                    _priceTimeStamp = value;
                    strTimeStamp = value.ToString("g"); // A: g = General short date time
                }
                Notify("priceTimeStamp");
            }
        }
        public double price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
                Notify("price");
            }
        }
        public string strPrice
        {
            get { return _strPrice; }
            set
            {
                if (_strPrice != value)
                {
                    // A: If empty, set to 0
                    if (value == "")
                    {
                        _strPrice = value;
                        price = 0;
                    }
                    else if (double.TryParse(value, out double x))
                    {
                        // A: Only accept input if it is a positive number 
                        if (x >= 0)
                        {
                            _strPrice = value;
                            price = x;
                        }
                    }
                }
                Notify("strPrice");
            }
        }
        public int stock
        {
            get { return _stock; }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                }
                Notify("stock");
            }
        }
        public string strStock
        {
            get { return _strStock; }
            set
            {
                if (_strStock != value)
                {
                    // A: If empty, set to 0
                    if (value == "")
                    {
                        _strStock = value;
                        stock = 0;
                    }
                    else if (int.TryParse(value, out int x))
                    {
                        // A: Only accept input if it is a positive number 
                        if (x >= 0)
                        {
                            _strStock = value;
                            stock = x;
                        }
                    }
                }
                Notify("strStock");
            }
        }
        public string typeOfMeat
        {
            get { return _typeOfMeat; }
            set
            {
                if (_typeOfMeat != value)
                {
                    _typeOfMeat = value;
                }
                Notify("typeOfMeat");
            }
        }
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                Notify("id");
            }
        }
    }
}
