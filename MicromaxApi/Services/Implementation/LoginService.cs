using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Model;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Interface;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Serilog;
using System.Text;

namespace MicromaxApi.Services.Implementation
{
    public class LoginService : ErrorService, ILoginService
    {
        public ILoginRepository _loginRepo;
        IDisposable disposable = null;

        public LoginService(ILoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }
        public async Task<LoginResponse> GetLoginById(string id)
        {
            try
            {
                var result = await _loginRepo.GetById(id);

                var response = new LoginResponse
                {
                    UserId = result.UserId,
                    UsrPassword = result.UsrPassword
                };
                return response;

            }
            catch (Exception ex)
            {
                Validation.Add("errors", ex.Message);
                return null;
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginModel model)
        {
            try
            {
                var user = await _loginRepo.GetById(model.UserId.ToUpper());
                byte[] key = Encoding.UTF8.GetBytes("password");
                byte[] encrypted = RC4Encrypt(model.Password, key);
                string convertedPassword = BitConverter.ToString(encrypted).Replace("-", "");

                if (user == null)
                {
                    Validation.Add("errors", $"No user found with ID: {model.UserId}");
                    Log.Warning("$No user found with ID: {model.UserId}");
                    return null;
                }
                else if (convertedPassword != user.UsrPassword)
                {
                    Validation.Add("errors", $"Invalid Password for ID: {model.UserId}");
                    Log.Warning($"Invalid Password for ID: {model.UserId}");
                    return null;
                }
                else
                {
                    Log.Information("Successful Login");
                    return new LoginResponse
                    {
                        UserId = "Successful login of user " + user.UserId,
                    };
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "error");
                throw;
            }
            finally
            {
                disposable?.Dispose();
            }
        }

        static byte[] RC4Encrypt(string plaintext, byte[] key)
        {
            IStreamCipher cipher = new RC4Engine();
            cipher.Init(true, new KeyParameter(key));

            byte[] input = Encoding.UTF8.GetBytes(plaintext);
            byte[] output = new byte[input.Length];

            cipher.ProcessBytes(input, 0, input.Length, output, 0);

            return output;
        }
    }
}
