using Evidence.Models;

namespace Evidence.Services
{
	public class EvidenceService
	{
		public List<Transakce> TransakceSeznam { get; set; } = new List<Transakce>();

		public void PridatTransakci(Transakce itemTransakce)
		{
			TransakceSeznam.Add(itemTransakce);
		}

		public void Aktualizovat(Transakce puvodni, Transakce noveHodnoty)
		{
			//var pom = TransakceSeznam.FirstOrDefault(t => t.Id == puvodni.Id);
			//if (pom != null)
			//{
			//	pom.AktualizovatZ(noveHodnoty);
			//	return;
			//}
			//puvodni.Vynosy = noveHodnoty.Vynosy;
			//puvodni.Naklady = noveHodnoty.Naklady;
			//puvodni.Popis = noveHodnoty.Popis;
			//puvodni.Datum = noveHodnoty.Datum;
			puvodni.AktualizovatZ(noveHodnoty);
		}

		public void OdebratTransakci(Transakce mazanaTransakce)
		{
			TransakceSeznam.Remove(mazanaTransakce);
		}

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
				TransakceSeznam.Add(transakce);
			}
		}
	}
}
