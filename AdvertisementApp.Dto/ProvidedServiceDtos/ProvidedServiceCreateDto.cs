using AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dtos.ProvidedServiceDtos
{
    public class ProvidedServiceCreateDto:IDto
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
     
    }
}
