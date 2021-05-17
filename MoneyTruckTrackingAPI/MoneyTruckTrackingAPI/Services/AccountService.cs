using MoneyTruckTrackingAPI.Helpers;
using MoneyTruckTrackingAPI.Models;
using MoneyTruckTrackingAPI.Requests;
using MoneyTruckTrackingAPI.Responses;
using MoneyTruckTrackingAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountDbContext _DbContext;
        
        public AccountService(AccountDbContext context)
        {
            _DbContext = context;
        }
        public async Task<LoginResponse> Login (LoginRequest request)
        {
            var account = _DbContext.Accounts
                .SingleOrDefault(acc => acc.Active && acc.Username == request.Username);
            
            if(account == null)
            {
                return null;
            }

            var passwordHash = HashingHelper.HashUsingPbkdf2(request.Password, account.PasswordSalt);

            if(account.Password != passwordHash)
            {
                return null;
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(account));

            return new LoginResponse
            {
                Username = account.Username,
                Name = account.Name,
                Email = account.Email,
                Phone = account.Phone,
                Unit = account.Unit,
                Active = account.Active,
                Admin = account.Admin,
                Token = token
            };
        }

        public async Task<SignupResponse> Signup(SignupRequest request)
        {
            var account = _DbContext.Accounts
                .SingleOrDefault(acc => acc.Username == request.Username);

            if(account != null)
            {
                return null;
            }
            var bytes = Encoding.UTF8.GetBytes(request.PasswordSalt);
            string salt = Convert.ToBase64String(bytes).ToString();

            var newAccount = new Account
            {
                Username = request.Username,
                Password = HashingHelper.HashUsingPbkdf2(request.Password, salt),
                PasswordSalt = salt,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Unit = request.Unit,
                Active = true,
                Admin = Convert.ToBoolean(request.Admin)
            };

            // save new account info to database
            _DbContext.Accounts.Add(newAccount);
            await _DbContext.SaveChangesAsync();

            return new SignupResponse
            {
                Username = newAccount.Username,
                Password = newAccount.Password,
                PasswordSalt = salt,
                Name = newAccount.Name,
                Email = newAccount.Email,
                Phone = newAccount.Phone,
                Unit = newAccount.Unit,
                Admin = newAccount.Admin.ToString(),
                Message = "New Account Created"
            };
        }
    }
}
