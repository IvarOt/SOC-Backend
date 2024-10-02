namespace SOC_backend.logic.ExceptionHandling.Exceptions
{
    public class PropertyException : Exception
    {
        public override string Message { get; }
        public string Property { get; private set; }

        public PropertyException(string message, string property)
        {
            Message = message;
            Property = property;
        }
    }
}
