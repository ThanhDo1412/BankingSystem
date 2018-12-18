using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chilindo_Database.Entity
{
    public class AccountDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AcountInfoId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public bool IsDeleted { get; set; }
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Version { get; set; }

        [ForeignKey("AcountInfoId")]
        public AccountInfo AccountInfo { get; set; }
    }
}
