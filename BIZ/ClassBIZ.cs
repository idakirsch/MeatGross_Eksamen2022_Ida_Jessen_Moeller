using System;
using IO;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;

namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
		private ClassCallWebAPI CCWA = new ClassCallWebAPI();
		private ClassMeatGrossDB CMGDB = new ClassMeatGrossDB();

        private List<ClassCustomer> _listCustomer;
        private List<ClassCountry> _listCountry;
        private List<ClassMeat> _listMeat;
		private List<ClassMeat> _editListMeat;
		private ClassApiRates _apiRates;
        private ClassCustomer _selectedCustomer;
        private ClassCustomer _editOrNewCustomer;
        private ClassOrder _order;
        private bool _isEnabled;

		public ClassBIZ()
		{
			listCustomer = SetUpListCustomer();
			listCountry = SetUpListCountry();
			listMeat = SetUpListMeat();
			editListMeat = new List<ClassMeat>();
			apiRates = new ClassApiRates();
            order = new ClassOrder();
            selectedCustomer = new ClassCustomer();
			editOrNewCustomer = new ClassCustomer();
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
        public List<ClassMeat> editListMeat
        {
            get { return _editListMeat; }
            set
            {
                if (_editListMeat != value)
                {
                    _editListMeat = value;
                }
                Notify("editListMeat");
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
					order.orderCustomer = value;
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

// Metoder der bruges til at hente data fra databasen og API
        public void UpdateListCustomer()
		{

		}

		public async Task GetApiRates()
		{
            try
            {
				while (true)
				{
                    string strUrl = $"https://openexchangerates.org/api/latest.json?app_id=4b9528bdaf254e829c2f52f4cdaf4ad2";
                    string apiResponse = await CCWA.GetURLContentsAsync(strUrl);
                    apiRates = JsonConvert.DeserializeObject<ClassApiRates>(apiResponse);
                    await Task.Delay(600000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

		public void SaveNewCustomer()
        {
            // Insert the customer in the database
            editOrNewCustomer.id = CMGDB.SaveCustomerInDB(editOrNewCustomer);

            // Add a copy of the edited customer to the list
            listCustomer.Add(new ClassCustomer(editOrNewCustomer));
            // Make sure the newly added customer is selected
            selectedCustomer = listCustomer.Last();
            // Update the list by remaking it (doesn't work otherwise)
            listCustomer = new List<ClassCustomer>(listCustomer);
        }

		public void UpdateCustomer()
		{
            // Update the customer in the database
			CMGDB.UpdateCustomerInDB(editOrNewCustomer);

            // Find currently selected customer in the list (doesn't work otherwise??) 
            int index = listCustomer.IndexOf(selectedCustomer);
            // Replace it with a copy of the newly edited customer
            listCustomer[index] = new ClassCustomer(editOrNewCustomer);
            // Make sure the updated customer is still the selected customer
            selectedCustomer = listCustomer[index];
            // Update the list by remaking it (doesn't work otherwise)
            listCustomer = new List<ClassCustomer>(listCustomer);
        }

		public void SaveSaleInDB()
		{
			CMGDB.SaveOrderInDB(order);
		}

		public void SaveNewMeatPrice(int meatIndex)
		{
            // Update the meat in the database
			CMGDB.UpdateMeatInDB(editListMeat[meatIndex]);
            // Replace the origional meat with a copy of the newly edited meat
			listMeat[meatIndex] = new ClassMeat(editListMeat[meatIndex]);
            // Update the list by remaking it (doesn't work otherwise)
            listMeat = new List<ClassMeat>(listMeat);
        }

        private List<ClassCustomer> SetUpListCustomer()
        {
            return CMGDB.GetAllCustomersFromDB();
        }

        private List<ClassCountry> SetUpListCountry()
		{
			return CMGDB.GetAllCountriesFromDB();
        }

		private List<ClassMeat> SetUpListMeat()
		{
			return CMGDB.GetAllMeatFromDB();
		}
    }
}