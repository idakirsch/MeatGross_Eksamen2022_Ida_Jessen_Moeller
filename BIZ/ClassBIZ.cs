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
    // Fields
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

        /// <summary>
        /// P: Default constructor
        /// </summary>
		public ClassBIZ()
		{
            //Initialiserer properties
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
        // Properties
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

    // Public Metod
        /// <summary>
        /// A: Fetches currency exchangerates from OpenExchangerates.org into 'apiRates' and existing 'valutaRates'
        /// </summary>
        public async Task GetApiRates()
		{
            try
            {
				while (true)
				{
                    string strUrl = $"https://openexchangerates.org/api/latest.json" +
                        $"?app_id=4b9528bdaf254e829c2f52f4cdaf4ad2";
                    string apiResponse = await CCWA.GetURLContentsAsync(strUrl);
                    apiRates = JsonConvert.DeserializeObject<ClassApiRates>(apiResponse);

                    // A: Update all valutaRates in listCountry and the database
                    foreach (var country in listCountry)
                    {
                        if (apiRates.rates.ContainsKey(country.countryCode))
                        {
                            // A: ApiRates cames back as USD, so you have to divide by The rate of DKK first to match the database
                            country.valutaRate = apiRates.rates["DKK"] / apiRates.rates[country.countryCode];
                            CMGDB.UpdateCountryInDB(country);
                        }
                    }
                    // A: Update all valutaRates in listCustomer as well (they have seperate ClassCountry's)
                    foreach (var customer in listCustomer)
                        if (apiRates.rates.ContainsKey(customer.country.countryCode))
                            customer.country.valutaRate = apiRates.rates["DKK"] / apiRates.rates[customer.country.countryCode];

                    // A: Wait 10 minuttes before calling again
                    await Task.Delay(600000);
                }
            }
            catch (Exception ex)
            {
                // A: Message, Window title, Buttons available, Icon
                MessageBox.Show(ex.Message, "API Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// A: Saves Customer in Database
        /// </summary>
		public void SaveNewCustomer()
        {
            // A: Insert the customer in the database
            editOrNewCustomer.id = CMGDB.SaveCustomerInDB(editOrNewCustomer);

            // A: Add a copy of the edited customer to the list
            listCustomer.Add(new ClassCustomer(editOrNewCustomer));
            // A: Make sure the newly added customer is selected
            selectedCustomer = listCustomer.Last();
            // A: Update the list by remaking it (doesn't work otherwise)
            listCustomer = new List<ClassCustomer>(listCustomer);
        }
        /// <summary>
        /// A: Updates Customer in Database with new data
        /// </summary>
		public void UpdateCustomer()
		{
            // A: Update the customer in the database
            CMGDB.UpdateCustomerInDB(editOrNewCustomer);

            // A: Find currently selected customer in the list (doesn't work otherwise??) 
            int index = listCustomer.IndexOf(selectedCustomer);
            // A: Replace it with a copy of the newly edited customer
            listCustomer[index] = new ClassCustomer(editOrNewCustomer);
            // A: Make sure the updated customer is still the selected customer
            selectedCustomer = listCustomer[index];
            // A: Update the list by remaking it (doesn't work otherwise)
            listCustomer = new List<ClassCustomer>(listCustomer);
        }
        /// <summary>
        /// A: Saves order in Database
        /// </summary>
		public void SaveSaleInDB()
		{
            if (order.orderCustomer.id != 0 && order.orderMeat.id != 0)
            {
                if (order.orderWeight > 0)
                {
                    // A: Save order to database
                    CMGDB.SaveOrderInDB(order);

                    // A: Remove the stock from selected meat with the amount that was ordered
                    var temp = listMeat[listMeat.IndexOf(order.orderMeat)];
                    temp.stock -= order.orderWeight;
                    // A: Update the reduction of meat in the database
                    CMGDB.UpdateMeatInDB(temp);
                    // A: Reflect changes of meat in the GUI
                    order.orderMeat = temp;
                    order.weight = "";
                    // A: Notify the user that the sale worked (Could be removed)
                    MessageBox.Show("Salget er succesfuldt oprettet.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // A: Message, Window title, Buttons available, Icon
                    MessageBox.Show("Mængden af kød, der sælges, skal være større end 0.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // A: Message, Window title, Buttons available, Icon
                MessageBox.Show("Der skal vælges en kunde og en type kød.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// A: Updates Meat prices in Database
        /// </summary>
        /// <param name="meatIndex"></param>
        public void SaveNewMeatPrice(int meatIndex)
		{
            ClassMeat orgMeat = listMeat[meatIndex];
            ClassMeat newMeat = editListMeat[meatIndex];

			if (newMeat.price > 0 && newMeat.stock > 0)
			{
                // A: Reflect changes in the program
                orgMeat.price = newMeat.price;
                orgMeat.stock += newMeat.stock;
                // A: Reset the updated meat for visual feedback
                newMeat.strPrice = "";
                newMeat.strStock = "";
                // A: Update the timestamp
                orgMeat.priceTimeStamp = DateTime.Now;

                // A: Update the meat in the database
                CMGDB.UpdateMeatInDB(orgMeat);

                // A: Update the list by remaking it
                listMeat = new List<ClassMeat>(listMeat);
            }
            else
			{
                // A: Message, Window title, Buttons available, Icon
                MessageBox.Show("Begge input skal være større end 0.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }

    // Private Metod
        // Methods to get data from Database
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