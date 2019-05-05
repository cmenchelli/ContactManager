using System;

namespace ContactManager.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zip { get; set; }
        public string AddressType { get; set; }

        public int ContactId { get; set; }

        public static Address ToAddress(AddressModel addressModel)
        {
            return new Address
            {
                Id = addressModel.Id,
                AddressLine1 = addressModel.AddressLine1,
                AddressLine2 = addressModel.AddressLine2,
                City = addressModel.City,
                StateCode = addressModel.StateCode,
                Zip = addressModel.Zip,
                AddressType = addressModel.AddressType
            };
        }

        public static AddressModel FromAddress(Address address, int contactId)
        {
            return new AddressModel
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                StateCode = address.StateCode,
                Zip = address.Zip,
                AddressType = address.AddressType,

                ContactId = contactId,
            };
        }
    }
}
