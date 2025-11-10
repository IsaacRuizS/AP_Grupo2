using System;

namespace FB.Data
{
    public partial class FoodItem
    {

    }

    public class FilterFoodItemDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Unit { get; set; }
        public int? QuantityInStock { get; set; }
        public DateTime? StartExpirationDate { get; set; }
        public DateTime? EndExpirationDate { get; set; }
        public bool? IsPerishable { get; set; }
        public int? CaloriesPerServing { get; set; }
        public string Barcode { get; set; }
        public int? RoleId { get; set; }
        public bool FilterByRole { get; set; }
    }
}
