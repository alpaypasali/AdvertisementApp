using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.Gender
{
    public class GenderListDto:IDto {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
