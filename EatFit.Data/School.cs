using System;
using System.Collections.Generic;
using System.Text;

namespace EatFit.Data
{
    public class School
    {
        private long school_id;

        public long SchoolId
        {
            get { return school_id; }
            set { school_id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string NameCityState
        {
            get { return name+" ("+city+", "+state+")"; }
        }
    }
}
