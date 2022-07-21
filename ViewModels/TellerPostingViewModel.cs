namespace App.ViewModels
{
    public class TellerPostingViewModel
    {
        public decimal Amount { get; set; }

        public string Narration { get; set; }


        public DateTime? Date { get; set; }

        public string PostingType { get; set; }

   
        public string CustomerAccount { get; set; }


        public string? User { get; set; }

        public string TillAccount { get; set; }

        public string Status { get; set; }
    }
}
