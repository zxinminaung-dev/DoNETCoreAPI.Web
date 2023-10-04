namespace DoNETCoreAPI.Web.Utilities.Exceptions
{
    public class CannotDeleteDefaultEntityException : Exception
    {
        public CannotDeleteDefaultEntityException() 
            : base("Default Entity cannot be deleted!") 
        { 
        }
    }
}
