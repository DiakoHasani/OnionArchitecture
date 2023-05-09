namespace OA.Service.DTO.User;

public class ResponseLoginModel
{
    public bool Result { get; set; } = false;
    public string Message { get; set; } = "";
    public string UserId { get; set; }
}