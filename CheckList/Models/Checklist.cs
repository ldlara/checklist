namespace CheckList.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public SupervisorApproval SupervisorApproval { get; set; }
        public ICollection<ChecklistItem>? Items { get; set; }
    }

}
