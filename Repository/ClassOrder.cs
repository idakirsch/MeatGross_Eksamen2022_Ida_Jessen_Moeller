using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassOrder : ClassNotify
    {
        private ClassMeat _orderMeat;
        private ClassCustomer _orderCustomer;
        private int _orderWeight;
        private double _orderPriceDKK;
        private double _orderPriceValuta;
        private string _priceDKK;
        private string _priceValuta;

        public ClassOrder()
        {
            orderMeat = new ClassMeat();
            orderCustomer = new ClassCustomer();
            orderWeight = 0;
            orderPriceDKK = 0D;
            orderPriceValuta = 0D;
            priceDKK = "";
            priceValuta = "";
        }

        public string priceValuta
        {
            get { return _priceValuta; }
            set
            {
                if (_priceValuta != value)
                {
                    _priceValuta = value;
                }
                Notify("priceValuta");
            }
        }
        public string priceDKK
        {
            get { return _priceDKK; }
            set
            {
                if (_priceDKK != value)
                {
                    _priceDKK = value;
                }
                Notify("priceDKK");
            }
        }
        public double orderPriceValuta
        {
            get { return _orderPriceValuta; }
            set
            {
                if (_orderPriceValuta != value)
                {
                    _orderPriceValuta = value;
                }
                Notify("orderPriceValuta");
            }
        }
        public double orderPriceDKK
        {
            get { return _orderPriceDKK; }
            set
            {
                if (_orderPriceDKK != value)
                {
                    _orderPriceDKK = value;
                }
                Notify("orderPriceDKK");
            }
        }
        public int orderWeight
        {
            get { return _orderWeight; }
            set
            {
                if (_orderWeight != value)
                {
                    _orderWeight = value;
                }
                Notify("orderWeight");
            }
        }
        public ClassCustomer orderCustomer
        {
            get { return _orderCustomer; }
            set
            {
                if (_orderCustomer != value)
                {
                    _orderCustomer = value;
                }
                Notify("orderCustomer");
            }
        }
        public ClassMeat orderMeat
        {
            get { return _orderMeat; }
            set
            {
                if (_orderMeat != value)
                {
                    _orderMeat = value;
                }
                Notify("orderMeat");
            }
        }

        private void CalculateAllPrices()
        {

        }

    }
}
