namespace Entities.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"{name} id {key} was not found.") { }
    }
}
