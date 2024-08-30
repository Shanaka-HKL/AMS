using AMS;
using System;
using System.Configuration;

public class AuthClass
{
    public AuthClass()
    {
        // Constructor
    }

    public static string Getconstring()
    {
        try
        {
            // Retrieve the encrypted connection string and the Kripton_Key from the config
            string encryptedDbKey = ConfigurationManager.AppSettings["Kripton_Key"];
            string encryptedConStr = ConfigurationManager.AppSettings["ConStr"];

            // Decrypt the Kripton_Key
            string decryptedDbKey = Kripta.Decrypt(encryptedDbKey, "PPASha@#$%-=.Pas").ToString().Trim();

            // Decrypt the connection string and replace the placeholder with the decrypted key
            string decryptedConStr = Kripta.Decrypt(encryptedConStr, "Sha@#$%-=.Con").ToString().Trim().Replace("pass", decryptedDbKey);

            return decryptedConStr;
        }
        catch
        {
            return string.Empty;
        }
    }
}
