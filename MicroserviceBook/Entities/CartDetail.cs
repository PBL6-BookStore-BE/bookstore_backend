namespace MicroserviceBook.Entities

{
    public class CartDetail: BaseEntity
    {
        public int Quantity { get; set; }
        public int IdCart { get; set; }
        public string IdBook { get; set; }
    }
}
