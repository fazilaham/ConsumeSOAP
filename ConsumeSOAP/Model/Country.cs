namespace ConsumeSOAP.Model
{
    public class Country
    {
        public Country() { }
        public Country(string capital)
        {
            this.Capital = capital;
        }
        public string Capital { get; set; }
    }
}
