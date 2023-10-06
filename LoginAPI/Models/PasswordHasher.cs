using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    private const int SaltSize = 16; // You can adjust the salt size
    private const int HashSize = 20; // Size of the resulting hash

    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        // Create the Rfc2898DeriveBytes object with the password and salt
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

        // Get the hash bytes
        byte[] hash = pbkdf2.GetBytes(HashSize);

        // Combine the salt and hash
        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // Convert the byte array to a base64-encoded string
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public static bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        // Extract the salt and hash from the stored password
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Compute the hash of the entered password with the same salt
        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(HashSize);

        // Compare the computed hash with the stored hash
        for (int i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false; // Passwords don't match
            }
        }

        return true; // Passwords match
    }
}
