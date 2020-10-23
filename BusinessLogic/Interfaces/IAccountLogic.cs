using Services.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IAccountLogic
    {
        bool Register(UserRegistrationDTO request, string Role);
    }
}
