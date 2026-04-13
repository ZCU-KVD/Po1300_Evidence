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

		public string FiltrText { get; set; } = string.Empty;
		public decimal? FiltrZiskHodnota { get; set; }
		public Models.OperatorZisku FiltrZiskOperator { get; set; } = Models.OperatorZisku.Rovno;

		public List<Models.Transakce> FiltrovanySeznamTransakci => EvidenceService.Filtrovat(FiltrText, FiltrZiskOperator, FiltrZiskHodnota);
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

		#region Ukladani Dat
		private async Task UlozitData()
		{ 
			var json = System.Text.Json.JsonSerializer.Serialize(EvidenceService.TransakceSeznam);
			await JS.InvokeVoidAsync("localStorage.setItem", "transakceData", json);
			await JS.InvokeVoidAsync("alert", "Data byla uložena do localStorage.");
		}

		private async Task NactiData()
		{
			var json = await JS.InvokeAsync<string>("localStorage.getItem", "transakceData");
			if (!string.IsNullOrEmpty(json))
			{
				List<Models.Transakce>? nactenySeznamTransakci = System.Text.Json.JsonSerializer.Deserialize<List<Models.Transakce>>(json);
				
				if (nactenySeznamTransakci != null)
				{
					EvidenceService.NahraditSeznamTransakci(nactenySeznamTransakci);
					await JS.InvokeVoidAsync("alert", "Data byla načtena z localStorage.");
				}
				else
				{
					await JS.InvokeVoidAsync("alert", "Neplatná data v localStorage.");
				}
			}
			else
			{
				await JS.InvokeVoidAsync("alert", "Žádná data v localStorage.");
			}
		}

		private void SmazatData()
		{
			EvidenceService.TransakceSeznam.Clear();
		}
		#endregion
	}
}
