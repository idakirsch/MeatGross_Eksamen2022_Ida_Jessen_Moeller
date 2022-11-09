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
        private double _price;
        private DateTime _priceTimeStamp;
        private string _strTimeStamp;

        public ClassMeat()
        {
            id = 0;
            typeOfMeat = "";
            stock = 0;
            price = 0D;
            priceTimeStamp = DateTime.Now;
            strTimeStamp = "";
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
