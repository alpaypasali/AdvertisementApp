using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.AdvertisementDtos
{
    public class AdvertisementCreateDto:IDto
    {
       
        public string Title { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
   

    }
}
