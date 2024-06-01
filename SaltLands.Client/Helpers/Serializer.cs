
using System.IO;
using System;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace SaltLands;

public class Serializer
{
    public static string GetPath(string name) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);

    public static T LoadJson<T>(string name, JsonTypeInfo<T> typeInfo) where T : new()
    {
        T json;
        string jsonPath = GetPath(name);

        if (File.Exists(jsonPath))
        {
            json = JsonSerializer.Deserialize(File.ReadAllText(jsonPath), typeInfo)!;
        }
        else
        {
            json = new T();
        }

        return json;
    }

    public static void SaveJson<T>(string name, T json, JsonTypeInfo<T> typeInfo)
    {
        string jsonPath = GetPath(name);
        Directory.CreateDirectory(Path.GetDirectoryName(jsonPath)!);
        string jsonString = JsonSerializer.Serialize(json, typeInfo);
        File.WriteAllText(jsonPath, jsonString);
    }

    public static T EnsureJson<T>(string name, JsonTypeInfo<T> typeInfo) where T : new()
    {
        T json;
        string jsonPath = GetPath(name);

        if (File.Exists(jsonPath))
        {
            json = JsonSerializer.Deserialize(File.ReadAllText(jsonPath), typeInfo)!;
        }
        else
        {
            json = new T();
            string jsonString = JsonSerializer.Serialize(json, typeInfo);
            Directory.CreateDirectory(Path.GetDirectoryName(jsonPath)!);
            File.WriteAllText(jsonPath, jsonString);
        }

        return json;
    }

}