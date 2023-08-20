using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace AWSDB.Models
{
    public class LeadDetailsEntity
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set;}  
    }
}
