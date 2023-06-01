using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface ISendEmailRepository : IGenericRepository<SendEmail>
    {
        void EmailSend(SendEmail email);    
    }
}
