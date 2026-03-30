using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		[Inject] private IJSRuntime JS {  get; set; } = default!;

		#region Stav komponenty
		private Models.Transakce formularTransakce = new Models.Transakce();
		private Models.Transakce? originalEditovaneTransakce = null;

		private bool JeEditace => originalEditovaneTransakce != null;
		#endregion

		#region Zivotní cyklus komponenty 
		protected override void OnInitialized()
		{
			 if (EvidenceService.TransakceSeznam.Count == 0)
			{
				 EvidenceService.VygenerovatNahodnaData(10);
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
				EvidenceService.Aktualizovat(originalEditovaneTransakce!, formularTransakce);
				originalEditovaneTransakce = null;
			}

			formularTransakce = new Models.Transakce();
		}

		private void ZacitEditaci(Models.Transakce transakce)
		{
			formularTransakce = transakce.Klonovat();
			originalEditovaneTransakce = transakce;
		}

		private async Task SmazatTransakci(Models.Transakce transakce)
		{
			bool potvrzeni = await JS.InvokeAsync<bool>("confirm", $"Opravdu chcete smazat transakci z {transakce.Datum}?");
			if (potvrzeni)
			{
				if (originalEditovaneTransakce == transakce)
				{
					ZrusitEditaci();
				}
				EvidenceService.OdebratTransakci(transakce);
			}
		}

		private void ZrusitEditaci()
		{
			originalEditovaneTransakce = null;
			formularTransakce = new Models.Transakce();
		}
		#endregion
	}
}
