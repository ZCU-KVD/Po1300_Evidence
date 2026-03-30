using System.ComponentModel.DataAnnotations;

namespace Evidence.Models
{
	public class Transakce
	{
		public Transakce() { }
		public Transakce(DateOnly datum, decimal vynosy, decimal naklady, string popis) 
		{
			Datum = datum;
			Vynosy = vynosy;
			Naklady = naklady;
			Popis = popis;
		}
		public Guid Id { get; set; } = Guid.NewGuid();
		private decimal vynosy;
		private decimal naklady;
		private string popis = string.Empty;

		public DateOnly Datum { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		[Required(ErrorMessage = "Popis je povinný")]
		public string Popis
		{
			get => popis;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Popis je povinný");
				}
				popis = value;
			}
		}

		[Range(0, double.MaxValue, ErrorMessage = "Výnosy musí být nezáporné")]
		public decimal Vynosy
		{
			get => vynosy;
			set
			{
				if (value < 0) 
				{
					throw new ArgumentException("Výnosy musí být nezáporné");
				}
				vynosy = value;
			}
		}

		[Range(0, double.MaxValue, ErrorMessage = "Náklady musí být nezáporné")]
		public decimal Naklady
		{
			get => naklady;
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Náklady musí být nezáporné");
				}
				naklady = value;
			}
		}
		public decimal Zisk => Vynosy - Naklady;

		#region Pomocne metody
		public Transakce Klonovat()
		{
			return new Transakce(this.Datum, this.Vynosy, this.Naklady, this.Popis)
			{
				Id = this.Id
			};
		}

		public void AktualizovatZ(Transakce noveHodnoty)
		{
			this.Datum = noveHodnoty.Datum;
			this.Vynosy = noveHodnoty.Vynosy;
			this.Naklady = noveHodnoty.Naklady;
			this.Popis = noveHodnoty.Popis;
		}
		#endregion

	}
}
