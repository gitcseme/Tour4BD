// See https://aka.ms/new-console-template for more information
using Domain.Utilities;

string test = "Hello, World!";

var encrypted = EncryptionHelper.Encrypt(test);
var decrypted = EncryptionHelper.Decrypt(encrypted);

Console.WriteLine(encrypted + "\n" + decrypted);