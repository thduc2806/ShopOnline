using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Data.Entities
{
	public class Order
	{
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }

        public List<OrderDetail> OrderDetails { get; set; }
		public AppUser AppUser { get; set; }
	}
}
