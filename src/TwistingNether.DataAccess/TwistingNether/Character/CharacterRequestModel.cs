using System.ComponentModel.DataAnnotations;

namespace TwistingNether.DataAccess.TwistingNether.Character
{
    public class CharacterRequestModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 12 characters long.")]
        [MaxLength(12, ErrorMessage = "Name must be between 3 and 12 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Realm is required.")]
        public string Realm { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        [AllowedValues("us", "eu", "kr", "tw", "cn", "US", "EU", "KR", "TW", "CN", "uS", "eU", "kR", "tW", "cN", "Us", "Eu", "Kr", "Tw", "Cn", ErrorMessage = "Region must be one of the following: us, eu, kr, tw, cn.")]
        public string Region { get; set; }

    }
}
