using Evidence.Models;

namespace Evidence.Services
{
	public class EvidenceService
	{
		public List<Transakce> Transakce { get; set; } = new List<Transakce>();

		public void VygenerovatNahodnaData(int pocet)
		{
			var random = new Random();
			string[] popisy = { "Prodej zboží", "Konzultace", "Služby", "Oprava", "Pronájem" };

			for (int i = 0; i < pocet; i++)
			{
				var transakce = new Transakce
				{
					Datum = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(0, 365))),
					Popis = popisy[random.Next(popisy.Length)],
					Vynosy = random.Next(1000, 50000),
					Naklady = random.Next(500, 40000)
				};
				Transakce.Add(transakce);
			}
		}
	}
}
