namespace EfSamurai.Domain
{
    public class Quotes
    {
        public int Id { get; set; }
        public string SamuraiQuotes { get; set; }

        public virtual Samurai Samurai { get; set; }
        public int SamuraiID { get; set; }
        public QuoteTypes QuoteType { get; set; }

        public enum QuoteTypes
        {
            Lame,
            Chessy,
            Awsume,
           
        }


    }
}
