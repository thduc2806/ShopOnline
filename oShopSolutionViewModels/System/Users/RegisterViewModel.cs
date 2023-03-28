using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
    public class RegisterViewModel
    {
        public bool IsSuccess { get; set; }
        public string ConfirmEmailToken { get; set; }
        public string UserId { get; set; }
    }
}
