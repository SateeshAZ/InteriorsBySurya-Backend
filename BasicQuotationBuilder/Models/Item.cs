namespace BasicQuotationBuilder.Models
{
    public class Item
    {
        public string ItemId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public string ItemOrderNumber { get; set; }
        public string ItemType { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Area { get; set; }
        public List<ItemCarcassMaterialType> CarcassMaterialType { get; set; }
        public List<ItemShutterMaterialType> ShutterMaterialType { get; set; }
        public List<ItemFinishMaterialType> FinishMaterialType { get; set; }
    }
}
