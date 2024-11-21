using System.ComponentModel.DataAnnotations;

namespace TwistingNether.DataAccess.TwistingNether.Tables
{
    public class tbl_Classes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
