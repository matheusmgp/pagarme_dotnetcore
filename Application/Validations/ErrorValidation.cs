

namespace Application.Validations
{
    public class ErrorValidation
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public ErrorValidation(string field,string message)
        {
            Field = field;
            Message = message;
        }
        public ErrorValidation()
        {
           
        }
    }
}
