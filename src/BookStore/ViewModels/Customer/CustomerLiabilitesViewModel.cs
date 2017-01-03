using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Customer
{
    public class CustomerLiabilitesViewModel
    {
        public int CustomerId { get; set; }
        public IQueryable<DebtViewModel> sourceDebts { get; set; }
        public List<TotalDebtsCorresponding> CurrentDebts { get; set; }
        public PaginatedList<DebtViewModel> Debts { get; set; }
        public decimal TotalDebts { get; set; }
    }
    
    public class DebtViewModel
    {
        public string Staff { get; set; }
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Note { get; set; }
        public decimal Value { get; set; }        
        public decimal Debt { get; set; }        
    }

    public class TotalDebtsCorresponding
    {
        public int Id { get; set; }
        public decimal CurrentDebt  { get; set; }
    }
}
