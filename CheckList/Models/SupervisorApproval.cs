namespace CheckList.Models
{
    public class SupervisorApproval
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string Comments { get; set; }
        public DateTime ApprovedAt { get; set; }
    }
}
