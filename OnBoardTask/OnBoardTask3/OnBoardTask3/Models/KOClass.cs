using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnBoardTask3.Models
{
    public class KOClass
    {
        public int Id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ProductId { get; set; }
        [DisplayName("DateSold")]
        public System.DateTime DateSold { get; set; }

        public string ReturnDateForDisplay
        {

            get
            {
                return this.DateSold.ToString("dd/MM/yyyy");
            }
        }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }

        public string CustomerName { get; internal set; }
        public string ProductName { get; internal set; }
        public string StoreName { get; internal set; }
    }

}