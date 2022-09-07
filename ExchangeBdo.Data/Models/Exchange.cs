namespace ExchangeBdo.Data.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public int BaseCurrencyId { get; set; }
        public int TargetCurrencyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
