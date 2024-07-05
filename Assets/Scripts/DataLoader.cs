using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DataLoader
{
    public static string[] GetAllJsonFiles(string folder)
    {
        string resourcsPath = $"{Application.dataPath}/Resources/{folder}";

        string[] _fileNames = Directory.GetFiles(resourcsPath)
            .Where(x => Path.GetExtension(x) == ".json").ToArray();

        for (int i = 0; i < _fileNames.Length; i++)
        {
            _fileNames[i] = Path.GetFileNameWithoutExtension(_fileNames[i]);
        }

        return _fileNames;
    }

    public static LeaderboardData GetLeaderboardData(string folder, string fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        var jsonTextFile = Resources.Load<TextAsset>($"{folder}/{fileName}");
        return JsonUtility.FromJson<LeaderboardData>(jsonTextFile.text);
    }

    public static Sprite GetSprite(string folder, string fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        return Resources.Load<Sprite>($"{folder}/{fileName}");

    }
}
