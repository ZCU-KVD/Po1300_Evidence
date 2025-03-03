using Microsoft.JSInterop;

namespace Po1300_Evidence.Pages
{
	public partial class EvidenceZisku
	{
		#region Vlastnosti
		/// <summary>
		/// Seznam položek, kde uzivatel muze pridavat, upravovat a mazat jednotlive polozky
		/// </summary>
		private List<Models.Polozka> Polozky { get; set; } = new List<Models.Polozka>();
		/// <summary>
		/// Aktualni zadavana polozka
		/// </summary>
		public Models.Polozka Polozka { get; private set; } = new Models.Polozka();

		public bool IsEditace { get; private set; } = false;
		#endregion

		#region Metody
		private void Pridat()
		{
			Polozky.Add(new Models.Polozka(Polozka.Datum, Polozka.Vynosy, Polozka.Naklady, Polozka.Popis));
			//Polozka = new Models.Polozka();
		}

		private async Task SmazatZaznam(Models.Polozka polozka)
		{
			string zprava = $"Opravdu chcete smazat záznam z {polozka.Datum} se ziskem {polozka.Zisk}?";
			bool smazat = await JavaScript.InvokeAsync<bool>("confirm",zprava);
			if (smazat)
				Polozky.Remove(polozka);
		}

		private void Edituj(Models.Polozka polozka)
		{
			Polozka = polozka;
			IsEditace = true;

		}
		private void UkonciEditaci()
		{
			Polozka = new Models.Polozka();
			IsEditace = false;
		}
		#endregion

	}
}
