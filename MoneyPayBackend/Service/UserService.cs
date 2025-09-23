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
    private readonly IUserRepo _userRepo;
    private readonly ITypeRepo _typeRepo;
    public UserService(IUserRepo userRepo, ITypeRepo typeRepo)
    {
        _userRepo = userRepo;
        _typeRepo = typeRepo;
    }
    public string Register(RegisterRequest registerRequest)
    {
        var existingUser = _userRepo.GetUserByEmail(registerRequest.email);
        if (existingUser != null) return "Email Already Registered";

        var newUser = new UserModel
        {
            email = registerRequest.email,
            password = HashPassword(registerRequest.password),
            name = registerRequest.name,
            createTime = DateTime.Now
        };

        if (_userRepo.AddUser(newUser))
        {
            if (!_typeRepo.AddDefaultTypesForUser(newUser.email)) return "AddDefaultTypesForUser Error";
            if (!_typeRepo.AddDefaultTypeRemarksForUser(newUser.email)) return "AddDefaultTypeRemarksForUser Error";

            return "Registration Successful";
        }
        return "Registration Failed";
    }

    public LoginResponse Login(LoginRequest loginRequest)
    {
        var userData = _userRepo.GetUserByEmail(loginRequest.email);
        if (userData == null) return new LoginResponse { message = "Not Registered Yet" };
        if (HashPassword(loginRequest.password) != userData.password) return new LoginResponse { message = "Wrong Password" };
        return new LoginResponse { token = GenerateJwtToken(userData) };
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


}