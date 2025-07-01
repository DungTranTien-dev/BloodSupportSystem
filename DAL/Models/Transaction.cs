using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace DAL.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } // Unique identifier for the transaction
        public Guid BloodRequestId { get; set; } // Reference to the associated blood request
        public string TransactionCode { get; set; } // Unique code for the transaction
        public DateTime TransactionDate { get; set; } // Date and time of the transaction
        public double Amount { get; set; } // Amount involved in the transaction

        
        public TransactionStatus Status { get; set; } // Status of the transaction (e.g., Pending, Completed, Failed)
  
        public Guid UserId { get; set; } // Reference to the user who made the transaction
        public User User { get; set; } // Navigation property to the User entity

    }
}
