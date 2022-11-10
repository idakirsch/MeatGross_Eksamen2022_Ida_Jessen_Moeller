using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCustomer : ClassNotify
    {
        private string _companyName;
        private string _address;
        private string _zipCity;
        private string _phone;
        private string _mail;
        private string _contactName;
        private int _id;
        private ClassCountry _country;

        /// <summary>
        /// A: Default Constructor
        /// </summary>
        public ClassCustomer()
        {
            companyName = "";
            address = "";
            zipCity = "";
            phone = "";
            mail = "";
            contactName = "";
            id = 0;
            country = new ClassCountry();
        }

        /// <summary>
        /// A: Constructor to make a dupplicate
        /// </summary>
        public ClassCustomer(ClassCustomer inCustomer)
        {
            companyName = inCustomer.companyName;
            address = inCustomer.address;
            zipCity = inCustomer.zipCity;
            phone = inCustomer.phone;
            mail = inCustomer.mail;
            contactName = inCustomer.contactName;
            id = inCustomer.id;
            country = inCustomer.country;
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
        public string contactName
        {
            get { return _contactName; }
            set
            {
                if (_contactName != value)
                {
                    _contactName = value;
                }
                Notify("contactName");
            }
        }
        public string mail
        {
            get { return _mail; }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                }
                Notify("mail");
            }
        }
        public string phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                }
                Notify("phone");
            }
        }
        public string zipCity
        {
            get { return _zipCity; }
            set
            {
                if (_zipCity != value)
                {
                    _zipCity = value;
                }
                Notify("zipCity");
            }
        }
        public string address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                }
                Notify("address");
            }
        }
        public string companyName
        {
            get { return _companyName; }
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                }
                Notify("companyName");
            }
        }

        public ClassCountry country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                }
                Notify("country");
            }
        }

        /// <summary>
        /// Checks that all properties have a non-empty value
        /// </summary>
        /// <returns>True or False</returns>
        public bool AreAllFieldsFilled()
        {
            if (companyName.Trim().Length > 0 &&
                address.Trim().Length > 0 &&
                zipCity.Trim().Length > 0 &&
                phone.Trim().Length > 0 &&
                mail.Trim().Length > 0 &&
                contactName.Trim().Length > 0 &&
                country.Id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
