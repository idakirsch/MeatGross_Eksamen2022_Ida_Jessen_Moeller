using System;
using IO;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

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

		public async Task GetApiRates()
		{
            try
            {
				while (true)
				{
                    string strUrl = $"https://openexchangerates.org/api/latest.json?base=DKK&app_id=4b9528bdaf254e829c2f52f4cdaf4ad2";
                    string apiResponse = await CCWA.GetURLContentsAsync(strUrl);
                    apiRates = JsonConvert.DeserializeObject<ClassApiRates>(apiResponse);

                    // Wait 10 minuttes before calling again
                    await Task.Delay(600000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButton.OK, MessageBoxImage.Error);

                /*
                //↓↓ DETTE ER TEST DATA, FJERN SENERE ↓↓
                string dummyResponse = @"
                {disclaimer: ""https://openexchangerates.org/terms/"",
                license: ""https://openexchangerates.org/license/"",
                timestamp: 1449877801,base: ""USD"",rates: {
                EUR: 3.672538,USD: 66.809999,RUB: 125.716501,KWD: 484.902502,BHD: 1.788575,OMR: 135.295998,JOD: 9.750101,GBP: 1.390866,
                KYD: 3.672538,CHF: 66.809999,CAD: 125.716501,AUD: 484.902502,AZN: 1.788575,BRL: 135.295998,HKD: 9.750101,DKK: 1.390866}}";
                apiRates = JsonConvert.DeserializeObject<ClassApiRates>(dummyResponse);
                */
            }

            // Update all valutaRates in listCountry and the database
			foreach (var country in listCountry)
            {
                if (apiRates.rates.ContainsKey(country.countryCode))
                {
                    country.valutaRate = apiRates.rates[country.countryCode];
                    CMGDB.UpdateCountryInDB(country);
                }
            }

            // Update all valutaRates in listCustomer as well (they have seperate ClassCountry's)
            foreach (var customer in listCustomer)
                if (apiRates.rates.ContainsKey(customer.country.countryCode))
                    customer.country.valutaRate = apiRates.rates[customer.country.countryCode];
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
			// Save order to database
			CMGDB.SaveOrderInDB(order);

			// Remove the stock from selected meat with the amount that was ordered
			var temp = listMeat[listMeat.IndexOf(order.orderMeat)];
            temp.stock -= order.orderWeight;
			// Update the reduction of meat in the database
            CMGDB.UpdateMeatInDB(temp);
            // Reflect changes of meat in the GUI
            order.orderMeat = temp;
            order.weight = "";
            // Notify the user that the sale worked (Could be removed)
            MessageBox.Show("Salget er succefuldt oprettet.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void SaveNewMeatPrice(int meatIndex)
		{
            ClassMeat orgMeat = listMeat[meatIndex];
            ClassMeat newMeat = editListMeat[meatIndex];

			if (newMeat.price > 0 && newMeat.stock > 0)
			{
				// Reflect changes in the program
				orgMeat.price = newMeat.price;
                orgMeat.stock += newMeat.stock;

                // Reset the updated meat for visual feedback
                newMeat.price = 0;
                newMeat.stock = 0;

                // Update the meat in the database
                CMGDB.UpdateMeatInDB(orgMeat);

                // Update the list by remaking it
                listMeat = new List<ClassMeat>(listMeat);
            }
            else
			{
                MessageBox.Show("Begge input skal være større end 0.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
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