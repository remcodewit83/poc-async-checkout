using System.ComponentModel.DataAnnotations;

namespace Api.Latency
{
    public class UpdateRequest
    {
        [Required]
        public int FastestMs { get; set; }
        [Required]
        public int SlowestMs { get; set; }
    }
}
