namespace MoE.ECE.Domain.Read.Model.Services
{
    public class OperatingSessionModel
    {
        public SessionTimeModel StartTime { get; set; } = new SessionTimeModel();
       
        public SessionTimeModel EndTime { get; set; } = new SessionTimeModel();
        
        public int SessionType { get; set; }
        
        public string? SessionTypeDescription { get; set; }
        
        public int? MaxChildren { get; set; }
        
        public int? MaxChildrenUnder2 { get; set; }
    }
}