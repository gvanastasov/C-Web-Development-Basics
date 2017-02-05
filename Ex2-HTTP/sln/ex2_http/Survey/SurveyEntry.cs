using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Survey
{
    class SurveyEntry
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public bool? HasLaptop { get; set; }
        public bool? HasSmartPhone { get; set; }
        public bool? HasMobilePhone { get; set; }
        public bool? HasCar { get; set; }
        public bool? HasBike { get; set; }

        public override string ToString()
        {
            return
                string.Format(@"First Name, {1}
                    Last Name, {2}
                    BirthDate, {3}
                    Gender, {4}         
                    Status, {5}        
                    Comment, {6}          
                    laptop, {7}          
                    smartPhone, {8}         
                    mobilePhone, {9}          
                    car, {10}           
                    bike, {11}{0}", Environment.NewLine, FirstName, LastName, BirthDate, Gender, Status, Comment, HasLaptop, HasMobilePhone, HasMobilePhone, HasCar, HasBike);
        }
    }
}
