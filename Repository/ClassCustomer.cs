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
        private string _ad

        public ClassCustomer()
        {
            
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
    }
}
