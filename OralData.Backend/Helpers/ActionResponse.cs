namespace OralData.Backend.Helpers
{
    public class ActionResponse<T>
    {
        public bool WasSuccess { get; internal set; }
        public string Message { get; internal set; }
    }
}