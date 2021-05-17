using MoneyTruckTrackingAPI.Requests;
using MoneyTruckTrackingAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<SignupResponse> Signup(SignupRequest request);
    }
}
