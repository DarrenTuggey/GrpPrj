using System;

namespace GroupProject.Services
{
    // Service is used to create a token to register a user as an admin allowing them to perform actions assigned to admin only.
    // Currently disabled for security.
    /*
    public class AdminRegistrationTokenService
    {
        private readonly Lazy<ulong> _creationKey = 
            new Lazy<ulong>(() => BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 7));

        public ulong CreationKey => _creationKey.Value;
    }
    */
}
