using System.ComponentModel.DataAnnotations;

namespace DEVinSalesTest.Models
{
    public class ShippingCompany
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
