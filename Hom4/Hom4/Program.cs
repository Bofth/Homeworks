using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Mapper
{
    public static void MapTo<TSource, TTarget>(TSource source, TTarget target, List<UserToUIModelMapping> mappings = null)
    {
        var sourceProps = typeof(TSource).GetProperties();
        var targetProps = typeof(TTarget).GetProperties();

        foreach (var sourceProp in sourceProps)
        {
            var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
            if (targetProp == null || !targetProp.CanWrite)
            {
                continue;
            }

            object value = null;
            if (mappings != null)
            {
                var mapping = mappings.FirstOrDefault(m => m.SourcePropertyName == sourceProp.Name && m.TargetPropertyName == targetProp.Name);
                if (mapping != null && mapping.ConversionFunction != null)
                {
                    value = mapping.ConversionFunction(sourceProp.GetValue(source));
                }
            }

            if (value == null)
            {
                value = sourceProp.GetValue(source);
            }

            targetProp.SetValue(target, value);
        }
    }
}

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class UIModel
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
}

public class UserToUIModelMapping
{
    public string SourcePropertyName { get; set; }
    public string TargetPropertyName { get; set; }
    public Func<object, object> ConversionFunction { get; set; }
}

public class Program
{
    public static void Main()
    {
        var user = new User { UserName = "john.doe", Password = "123456" };
        var uiModel = new UIModel();

        // Automatic mapping
        Mapper.MapTo(user, uiModel);

        // Custom mapping (It works almost every time ;) )
        Mapper.MapTo(user, uiModel, new List<UserToUIModelMapping>
        {
            new UserToUIModelMapping { SourcePropertyName = "Password", TargetPropertyName = "PasswordHash", ConversionFunction = EncryptPassword }
        });

        Console.WriteLine($"UIModel.UserName = {uiModel.UserName}");
        Console.WriteLine($"UIModel.PasswordHash = {uiModel.PasswordHash}");
    }

    private static object EncryptPassword(object password)
    {
        return password.ToString().GetHashCode();
    }
}
