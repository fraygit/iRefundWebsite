using iRefundUmbraco7.Custom.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRefundUmbraco7.Custom.Data.Model
{
    public class RefundApplication : MongoEntity
    {
        public string IRDNumber {get; set;}
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string DayTimePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public string HowYouHeard { get; set; }
        public string HaveNZDriverLicense { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string CardVersion { get; set; }
        public DateTime ApplicationDate {get; set;}
    }
}
