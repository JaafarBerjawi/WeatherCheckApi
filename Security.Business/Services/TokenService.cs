namespace Security.Business
{
    public class TokenService
    {
        private readonly EncryptionService _encryptionService;
        public TokenService(EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }
        public string GenerateToken()
        {
            return _encryptionService.Encrypt(Guid.NewGuid().ToString());
        }
    }
}
