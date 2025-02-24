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
		public Models.Polozka Polozka { get; set; } = new Models.Polozka();
		#endregion

		#region Metody
		private void Pridat()
		{
			Polozky.Add(Polozka);
			
		}
		#endregion

	}
}
