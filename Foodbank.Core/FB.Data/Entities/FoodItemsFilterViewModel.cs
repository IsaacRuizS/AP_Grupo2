using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Data.Entities
{
    public class FoodItemsFilterViewModel
    {
        public string RoleName { get; set; }
        public string OmitirRol { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public bool? IsPerishable { get; set; }
        public int? Calories { get; set; }
        public string Barcode { get; set; }
    }
}
