namespace TV_Backend.Models.HotelProduct.getProductInfo
{
    public class GetProductInfoRequest
    {
        public int productType { get; set; }
        public int ownerProvider { get; set; }
        public string product { get; set; }
        public string culture { get; set; }
    }
}