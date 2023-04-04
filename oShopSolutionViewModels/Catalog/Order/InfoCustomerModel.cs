using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
    public class InfoCustomerModel
    {
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Ward { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [StringLength(11, ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PostCode { get; set; }

    }
}
