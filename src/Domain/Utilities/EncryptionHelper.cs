using System;
using System.IO;
using System.Security.Cryptography;

namespace Domain.Utilities;

public class EncryptionHelper
{
    private const string SecretKey = "SUPER_SECRET_KEY$2024xyz54321";
    private const int IterationCount = 5000;
    private static HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
    private static byte[] Salt = new byte[16];

    public static string Encrypt(string plainText)
    {
        using Aes aesAlg = Aes.Create();
        // Derive a key and IV from the secret key using a Key Derivation Function (KDF)
        using (var keyDerivation = new Rfc2898DeriveBytes(SecretKey, Salt, IterationCount, HashAlgorithm))
        {
            aesAlg.Key = keyDerivation.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = keyDerivation.GetBytes(aesAlg.BlockSize / 8);
        }

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using var aesAlg = Aes.Create();

        // Derive a key and IV from the secret key using a Key Derivation Function (KDF)
        using (var keyDerivation = new Rfc2898DeriveBytes(SecretKey, Salt, IterationCount, HashAlgorithm))
        {
            aesAlg.Key = keyDerivation.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = keyDerivation.GetBytes(aesAlg.BlockSize / 8);
        }

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Ensure the cipher text is properly base64-decoded
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        
        return srDecrypt.ReadToEnd();
    }

}
