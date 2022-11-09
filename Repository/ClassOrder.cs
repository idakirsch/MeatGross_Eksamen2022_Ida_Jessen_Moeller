
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
        private string _weight;
        private string _priceDKK;
        private string _priceValuta;

        public ClassOrder()
        {
            orderMeat = new ClassMeat();
            orderCustomer = new ClassCustomer();
            orderWeight = 0;
            orderPriceDKK = 0D;
            orderPriceValuta = 0D;
            weight = "";
            priceDKK = "0.00";
            priceValuta = "0.00";
        }

        public string weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    if (value == "")
                    {
                        orderWeight = 0;
                        _weight = value;
                    }
                    else if (int.TryParse(value, out int x))
                    {
                        if (x > orderMeat.stock)
                        {
                            orderWeight = orderMeat.stock;
                            _weight = orderMeat.stock.ToString();
                        }
                        else if (x >= 0)
                        {
                            orderWeight = x;
                            _weight = value;
                        }
                    }
                }
                Notify("weight");
            }
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
                    CalculateAllPrices();
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
                    weight = "";
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
                    weight = "";
                }
                Notify("orderMeat");
            }
        }

        public void CalculateAllPrices()
        {
            orderPriceDKK = orderWeight * orderMeat.price;
            orderPriceValuta = orderPriceDKK * orderCustomer.country.valutaRate;

            priceDKK = orderPriceDKK.ToString("#0.00");
            priceValuta = orderPriceValuta.ToString("#0.00");
        }

    }
}
