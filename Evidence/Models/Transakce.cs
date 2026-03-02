namespace Evidence.Models
{
	public class Transakce
	{
		public DateOnly Datum { get; set; }
		public string Popis { get; set; } = string.Empty;
		public decimal Vynosy { get; set; }
		public decimal Naklady { get; set; }
		public decimal Zisk => Vynosy - Naklady;

	}
}
