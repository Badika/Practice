using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.ClinicModels
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public Address PatientAddress { get; set; }
        public DateTime BDay { get; set; }
        public string PhoneNumber { get; set; }


        #region ctors

        public Patient()
        {
            this.FirstName = "test";
            this.LastName = "test";
            this.BDay = DateTime.Now;
            this.PhoneNumber = string.Empty;
        }

        public Patient(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BDay = DateTime.Now;
            this.PhoneNumber = string.Empty;
        }

        public Patient(string FirstName, string LastName, DateTime BDay, string PhoneNumber)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BDay = BDay;
            this.PhoneNumber = PhoneNumber;
        }

        public Patient(string FirstName, string LastName, string BDay, string PhoneNumber)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            try
            {
                this.BDay = Convert.ToDateTime(BDay);
            }
            catch(Exception)
            {
                this.BDay = DateTime.Now;
            }
            this.PhoneNumber = PhoneNumber;
        }
        
        #endregion
    }
}
