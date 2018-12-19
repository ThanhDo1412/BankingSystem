using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingDatabase.Entity
{
    public class AccountInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String AccountName { get; set; }
        public Boolean IsDeleted { get; set; }

        public ICollection<AccountDetail> AccountDetails { get; set; }
    }
}
