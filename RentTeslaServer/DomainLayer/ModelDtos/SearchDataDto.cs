using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.ModelDtos
{

    public class SearchDataDto
    {
        public string NamePickUp { get; set; }
        public string NameDropOff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
