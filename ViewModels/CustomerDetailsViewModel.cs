namespace App.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int CustomerID { get; set; }

        public int AccountNumber { get; set; }
        public string AccountName { get; set; }

        public decimal AccountBalance { get; set; }

        public string Accounttype { get; set; }

        public DateTime DateOpened { get; set; }

        public bool IsActivated { get; set; }
    }
}
