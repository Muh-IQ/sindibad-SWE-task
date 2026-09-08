namespace Sindibad.Api.Requests
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;

        public bool Completed { get; set; }
    }
}
