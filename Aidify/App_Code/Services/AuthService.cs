// REQUIRES: BCrypt.Net-Next NuGet package
// Install in Visual Studio: Tools > NuGet Package Manager > Manage NuGet Packages for Solution
// Search "BCrypt.Net-Next" and install. This file will not compile without it.

using System;
using BCrypt.Net;

namespace Aidify
{
    public class AuthService
    {
        private readonly UserRepository _userRepo = new UserRepository();

        // Returns authenticated user or null. Logs the attempt to LoginHistory.
        public UserDto Authenticate(string email, string password, string ipAddress)
        {
            var user = _userRepo.GetByEmail(email);

            if (user == null)
            {
                _userRepo.LogLoginAttempt(null, false, ipAddress);
                return null;
            }

            bool ok = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            _userRepo.LogLoginAttempt(user.UserId, ok, ipAddress);

            if (!ok) return null;
            if (!user.IsActive) return null;

            return user;
        }

        // Returns new UserId. Throws if email already exists.
        public int RegisterUser(string fullName, string email, string password)
        {
            if (_userRepo.EmailExists(email))
                throw new InvalidOperationException("An account with that email already exists.");

            string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);
            return _userRepo.Insert(fullName, email, hash, Constants.RoleLearner);
        }

        // Returns true if the account should be locked (5+ failures in last 15 min)
        public bool IsAccountLocked(string email)
        {
            return _userRepo.GetRecentFailCount(email, withinMinutes: 15) >= 5;
        }

        // Generates a reset/confirm token, stores it, returns the token string
        public string CreateEmailToken(int userId, string purpose, int expiryHours = 24)
        {
            string token = Guid.NewGuid().ToString("N");
            _userRepo.InsertEmailToken(userId, token, purpose, DateTime.UtcNow.AddHours(expiryHours));
            return token;
        }

        public bool ResetPassword(string token, string newPassword)
        {
            var tokenRow = _userRepo.GetValidEmailToken(token, "Reset");
            if (tokenRow == null) return false;

            int userId   = Convert.ToInt32(tokenRow["UserId"]);
            int tokenId  = Convert.ToInt32(tokenRow["TokenId"]);
            string hash  = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: 11);

            _userRepo.UpdatePasswordHash(userId, hash);
            _userRepo.MarkTokenUsed(tokenId);
            return true;
        }

        public bool ConfirmEmail(string token)
        {
            var tokenRow = _userRepo.GetValidEmailToken(token, "Confirm");
            if (tokenRow == null) return false;

            int userId  = Convert.ToInt32(tokenRow["UserId"]);
            int tokenId = Convert.ToInt32(tokenRow["TokenId"]);

            _userRepo.ConfirmEmail(userId);
            _userRepo.MarkTokenUsed(tokenId);
            return true;
        }
    }
}
