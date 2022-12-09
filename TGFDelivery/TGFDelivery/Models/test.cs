using System;
using System.Collections.Generic;
using System.Text;

namespace TGFDelivery.Models
{
    public class test: ViewModelBase
    {
        string _id;
        public string id
        {
            set { _id = value; OnPropertyChanged(); }
            get { return _id; }
        }
        List<string> _tests { get; set; }
        public List<string> tests {
            set { _tests = value;OnPropertyChanged(); }
            get { return _tests; }
        }


    }
}
