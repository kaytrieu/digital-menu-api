using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ScreenDtos
{
    public class ScreenReadDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Uid { get; set; }
    }
}
