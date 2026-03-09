using Microsoft.AspNetCore.Components;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		protected override void OnInitialized()
		{
			 if (EvidenceService.Transakce.Count == 0)
			{
				 EvidenceService.VygenerovatNahodnaData(30);
			}
		}
	}
}
