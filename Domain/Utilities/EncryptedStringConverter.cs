using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
namespace Domain.Utilities;

public class EncryptedStringConverter : ValueConverter<string, string>
{
    public EncryptedStringConverter() 
        : base(
            convertToProviderExpression: plainText => EncryptionHelper.Encrypt(plainText), 
            convertFromProviderExpression: cypherText => EncryptionHelper.Decrypt(cypherText)
        )
    {
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class EncryptedAttribute : Attribute { }