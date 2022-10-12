namespace RaceCardViewer.Utility
{
    public class OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(bool result, string message):this()
        {
            Result = result;
            Message = message;
        }

        public bool Result { get; set; }
        public string Message { get; set; }

    }
}
