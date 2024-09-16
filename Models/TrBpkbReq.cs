using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class TrBpkbReq
    {
        public string AgreementNumber { get; set; }
        public string BranchId { get; set; }
        public string NoBPKB { get; set; }
        public DateTime BPKBCheckInDate { get; set; }
        public DateTime BPKBDate { get; set; }
        public string FakturNumber { get; set; }
        public DateTime FakturDate { get; set; }
        public string PoliceNumber { get; set; }
        public string StorageLocation { get; set; } 
        public IEnumerable<SelectListItem> StorageLocations { get; set; }
    }
}
