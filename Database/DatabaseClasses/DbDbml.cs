using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public abstract class BaseORM
    {
        public DateTime create_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public enum ProfileStatus
    {
        NEW,
        ACTIVE,
        BLOCKED,
        INACTIVE
    }
    public class Profile : BaseORM
    {
        [Key]
        public int profileId { get; set; }
        public string firstName { get; set; }    
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public ApplicationUser user { get; set; }
        public string userId { get; set; }
        public bool isSupportUser { get; set; }
        public bool isActive { get; set; }
        public string status { get; set; }
    }
    public enum PanicStatus
    {
        NEW,
        INPROGRESS,
        RESOLVED
    }
    public class PanicAlerts : BaseORM
    {
        [Key]
        public int alertId { get; set; }
        public string alertType { get; set; }
        public string panicStatus { get; set; }
        public bool isActive { get; set; }     
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int profileId { get; set; }
        public int supportId { get; set; }
    }
    public class PanicAlertResolution : BaseORM
    {
        [Key]
        public int alertResolutionId { get; set; }
        public int alertId { get; set; }
        public int supportId { get; set; }
        public string resolutionStatement { get; set; }
    }

    public class Address : BaseORM
    {
        [Key]
        public int addressId { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string province { get; set; }
    }

    public class Vehicle : BaseORM
    {
        [Key]
        public int vehicleId { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string bodyType { get; set; }
        public string registrationNumber { get; set; }
    }

    public class BodyType
    {
       [Key]
       public int typeId { get; set; }
       public string name { get; set; }
    }
}
