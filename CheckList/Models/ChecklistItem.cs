namespace CheckList.Models
{
    //public class ChecklistItem
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public bool IsCompliant { get; set; }
    //    public string Observations { get; set; }
    //    public int ChecklistId { get; set; }
    //    public Checklist Checklist { get; set; }
    //}
    public class ChecklistItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }

        public int? ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
    }
}
