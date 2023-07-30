using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WeatherCheck.Entities
{
    public class CurrentWeatherCondition
    {
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        [JsonIgnore]
        public long UserId { get; set; }
        [Required]
        public string City { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime RequestTime { get; set; }  
    }
}
