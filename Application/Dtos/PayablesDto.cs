

namespace Application.Dtos
{
    public class PayablesDto
    {
        public double Availables { get; set; }
        public double WaitingFunds { get; set; }

        public PayablesDto(double availables, double waitingFunds)
        {
            Availables = availables; 
            WaitingFunds = waitingFunds;   
        }


    }
}
