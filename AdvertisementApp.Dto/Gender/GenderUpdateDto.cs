using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.Gender
{
    public class GenderUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }

    }
}
