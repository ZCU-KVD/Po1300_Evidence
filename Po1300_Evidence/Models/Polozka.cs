namespace Po1300_Evidence.Models
{
	public class Polozka
	{
		public Polozka() 
		{
			Datum = DateOnly.FromDateTime(DateTime.Now);
		}

		public Polozka(DateOnly datum, double vynosy, double naklady, string popis)
		{
			Datum = datum;
			Vynosy = vynosy;
			Naklady = naklady;
			Popis = popis;
		}

		private double vynosy;
		private double naklady;

		public DateOnly Datum { get; set; }
		public double Vynosy { get => vynosy; set => vynosy = Math.Round(Math.Abs(value),2); }
		public double Naklady { get => naklady; set => naklady = Math.Round(Math.Abs(value), 2); }
		public string Popis { get; set; }
		public double Zisk => Vynosy - Naklady;
		/// <summary>
		/// Vlastnost pro zobrazení zisku v HTML formátu
		/// </summary>
		public string ZiskHtml => (Zisk > 0) ? $"<span style=\"color:green;\"> {Zisk:C2} </span >" : $"<span style=\"color:red;\"> {Zisk:C2} </span >"; //Ternární operátor

	}
}
