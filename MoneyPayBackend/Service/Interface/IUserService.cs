using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService;

public interface IUserService
{
    LoginResponse Login(LoginRequest loginRequest);
    string Register(RegisterRequest registerRequest);
    GetUserProfileResponse GetProfile(string userEmail);
    GetUserProfileResponse UpdateProfile(string userEmail, UpdateUserProfileRequest updateUserProfileRequest);
    bool ChangePassword(string userEmail, ChangePasswordRequest changePasswordRequest);
}
