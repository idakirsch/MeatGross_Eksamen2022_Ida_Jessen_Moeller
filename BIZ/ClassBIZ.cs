using System;
using IO;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
		private ClassCallWebAPI CCWA = new ClassCallWebAPI();
		private ClassMeatGrossDB CMGDB = new ClassMeatGrossDB();

        private List<ClassCustomer> _listCustomer;
        private List<ClassCountry> _listCountry;
        private List<ClassMeat> _listMeat;
        private ClassApiRates _apiRates;
        private ClassCustomer _selectedCustomer;
        private ClassCustomer _editOrNewCustomer;
        private ClassOrder _order;
        private bool _isEnabled;

		public ClassBIZ()
		{
			listCustomer = new List<ClassCustomer>();
			listCountry = new List<ClassCountry>();
			listMeat = new List<ClassMeat>();
			apiRates = new ClassApiRates();
			selectedCustomer = new ClassCustomer();
			editOrNewCustomer = new ClassCustomer();
			order = new ClassOrder();
			isEnabled = true;
		}

		public List<ClassCustomer> listCustomer
		{
			get { return _listCustomer; }
			set
			{
				if (_listCustomer != value)
				{
					_listCustomer = value;
				}
				Notify("listCustomer");
			}
		}
		public List<ClassCountry> listCountry
		{
			get { return _listCountry; }
			set
			{
				if (_listCountry != value)
				{
					_listCountry = value;
				}
				Notify("listCountry");
			}
		}
		public List<ClassMeat> listMeat
		{
			get { return _listMeat; }
			set
			{
				if (_listMeat != value)
				{
					_listMeat = value;
				}
				Notify("listMeat");
			}
		}
		public ClassApiRates apiRates
		{
			get { return _apiRates; }
			set
			{
				if (_apiRates != value)
				{
					_apiRates = value;
				}
				Notify("apiRates");
			}
		}
		public ClassCustomer editOrNewCustomer
        {
            get { return _editOrNewCustomer; }
            set
            {
                if (_editOrNewCustomer != value)
                {
                    _editOrNewCustomer = value;
                }
                Notify("editOrNewCustomer");
            }
        }
        public ClassCustomer selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                }
                Notify("selectedCustomer");
            }
        }
        public ClassOrder order
		{
			get { return _order; }
			set
			{
				if (_order != value)
				{
					_order = value;
				}
				Notify("order");
			}
		}
		public bool isEnabled
		{
			get { return _isEnabled; }
			set
			{
				if (_isEnabled != value)
				{
					_isEnabled = value;
				}
				Notify("isEnabled");
			}
		}

		public void UpdateListCustomer()
		{

		}

		public async Task<ClassApiRates> GetApiRates()
		{
			return new ClassApiRates();
		}

		public void SetUpListCustomer()
		{

		}

		public int SaveNewCustomer()
		{
			return 0;
		}

		public void UpdateCustomer()
		{

		}

		public void SaveSaleInDB()
		{

		}

		public void SaveNewMeatPrice(string inMeat, double inPrice, int inWeight)
		{

		}

		private void SetUpListCountry()
		{

		}
	}
}
