using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Service;

public class UserService : IUserService
{
    private readonly MoneyPayDBContext _context;
    private readonly IUserRepo _userRepo;
    private readonly ITypeRepo _typeRepo;
    public UserService(MoneyPayDBContext context, IUserRepo userRepo, ITypeRepo typeRepo)
    {
        _context = context;
        _userRepo = userRepo;
        _typeRepo = typeRepo;
    }
    public string Register(RegisterRequest registerRequest)
    {
        var existingUser = _userRepo.GetUserByEmail(registerRequest.email);
        if (existingUser != null) return "Email Already Registered";

        using var transaction = _context.Database.BeginTransaction();

        var newUser = new UserModel
        {
            email = registerRequest.email,
            password = HashPassword(registerRequest.password),
            name = registerRequest.name,
            createTime = DateTime.Now
        };

        try
        {
            if (!_userRepo.AddUser(newUser))
            {
                transaction.Rollback();
                return "Registration Failed";
            }

            if (!_typeRepo.AddDefaultTypesForUser(newUser.userId))
            {
                transaction.Rollback();
                return "AddDefaultTypesForUser Error";
            }

            if (!_typeRepo.AddDefaultTypeRemarksForUser(newUser.userId))
            {
                transaction.Rollback();
                return "AddDefaultTypeRemarksForUser Error";
            }

            transaction.Commit();
            return "Registration Successful";
        }
        catch
        {
            transaction.Rollback();
            return "Registration Failed";
        }
    }

    public LoginResponse Login(LoginRequest loginRequest)
    {
        var userData = _userRepo.GetUserByEmail(loginRequest.email);
        if (userData == null) return new LoginResponse { message = "Not Registered Yet" };
        if (HashPassword(loginRequest.password) != userData.password) return new LoginResponse { message = "Wrong Password" };
        return new LoginResponse { token = GenerateJwtToken(userData) };
    }

    public GetUserProfileResponse GetProfile(string userEmail)
    {
        var userData = _userRepo.GetUserByEmail(userEmail);
        if (userData == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        return MapUserProfile(userData);
    }

    public GetUserProfileResponse UpdateProfile(string userEmail, UpdateUserProfileRequest updateUserProfileRequest)
    {
        var userData = _userRepo.GetUserByEmail(userEmail);
        if (userData == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        if (string.IsNullOrWhiteSpace(updateUserProfileRequest.name))
        {
            throw new ArgumentException("Name is required");
        }

        userData.name = updateUserProfileRequest.name.Trim();
        _userRepo.UpdateUser(userData);
        return MapUserProfile(userData);
    }

    public bool ChangePassword(string userEmail, ChangePasswordRequest changePasswordRequest)
    {
        var userData = _userRepo.GetUserByEmail(userEmail);
        if (userData == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        if (string.IsNullOrWhiteSpace(changePasswordRequest.currentPassword) ||
            string.IsNullOrWhiteSpace(changePasswordRequest.newPassword))
        {
            throw new ArgumentException("Password is required");
        }

        if (HashPassword(changePasswordRequest.currentPassword) != userData.password)
        {
            throw new ArgumentException("Current password is incorrect");
        }

        if (changePasswordRequest.currentPassword == changePasswordRequest.newPassword)
        {
            throw new ArgumentException("New password must be different");
        }

        userData.password = HashPassword(changePasswordRequest.newPassword);
        return _userRepo.UpdateUser(userData);
    }

    private string HashPassword(string password)
    {
        string salt = "salt!@#$%^&*()";
        string saltedPassword = password + salt;

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    private string GenerateJwtToken(UserModel userData)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, userData.email),
            new Claim("userEmail", userData.email),
            new Claim("name", userData.name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OOTimbroTimbroTimbroTimbroTimbroTimbroOO"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MoneyPay",
            audience: "MoneyPay",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static GetUserProfileResponse MapUserProfile(UserModel userData)
    {
        return new GetUserProfileResponse
        {
            userId = userData.userId,
            email = userData.email ?? string.Empty,
            name = userData.name ?? string.Empty,
            createTime = userData.createTime
        };
    }

}
