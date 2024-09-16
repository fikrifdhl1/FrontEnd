using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class StorageLocationModel
    {
        public string Value { get; set; }
        public IEnumerable<SelectListItem> StorageLocations{ get; set; }
    }
}
