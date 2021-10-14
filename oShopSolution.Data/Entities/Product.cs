﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Data.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public DateTime CreateDate { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public List<Cart> Carts { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
		public List<ProductImg> ProductImgs { get; set; }




	}
}
