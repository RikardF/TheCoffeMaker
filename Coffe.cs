namespace TheCoffeMaker
{
    public class Coffe
    {
        public CupSize Cupsize { get; set; }
        public bool MilkChoise { get; set; }
        public bool SugarChoise { get; set; }
        public Coffe(CupSize cupSize, bool milkChoise, bool sugarChoise)
        {
            this.Cupsize = cupSize;
            this.MilkChoise = milkChoise;
            this.SugarChoise = sugarChoise;
        }
    }
}