using Microsoft.AspNetCore.Components;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		#region Stav komponenty
		private Models.Transakce formularTransakce = new Models.Transakce();
		private Models.Transakce? originalEditovaneTransakce = null;

		private bool JeEditace => originalEditovaneTransakce != null;
		#endregion

		#region Zivotní cyklus komponenty 
		protected override void OnInitialized()
		{
			 if (EvidenceService.Transakce.Count == 0)
			{
				 EvidenceService.VygenerovatNahodnaData(1);
			}
		}
		#endregion

		#region Formulař CRUD
		private void UlozitTransakci()
		{
			if (!JeEditace)
			{
				EvidenceService.PridatTransakci(formularTransakce);
			}
			else
			{ 

			}

			formularTransakce = new Models.Transakce();
		}

		private void ZacitEditaci(Models.Transakce transakce)
		{
			formularTransakce = transakce.Klonovat();
			originalEditovaneTransakce = transakce;
		}
		#endregion
	}
}
