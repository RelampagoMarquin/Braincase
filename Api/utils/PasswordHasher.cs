using System.Security.Cryptography;
namespace Api.utils
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Gere um salt aleatório
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Crie o hash da senha com o salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine o salt e o hash em uma única matriz
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Converta a matriz de bytes para uma string base64
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Converta a string base64 de volta para uma matriz de bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extraia o salt da matriz de bytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Calcule o hash da senha fornecida com o salt extraído
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare o hash calculado com o hash armazenado
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
