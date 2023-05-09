namespace OA.Service.DTO.Todo;
public class TodoModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public bool Done { get; set; }
}