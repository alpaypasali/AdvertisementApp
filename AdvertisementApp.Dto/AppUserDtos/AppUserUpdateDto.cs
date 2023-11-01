using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.AppUserDtos
{
    public class AppUserUpdateDto : IUpdateDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }

    }
}
