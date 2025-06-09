using System.ComponentModel.DataAnnotations;

namespace WebApiApp03.Models
{
    public class iot_datas
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime sensing_dt { get; set; }

        [Required]
        public string loc_id { get; set; }
        
        public float temp {  get; set; }
        public float humid { get; set; }
    }
}
