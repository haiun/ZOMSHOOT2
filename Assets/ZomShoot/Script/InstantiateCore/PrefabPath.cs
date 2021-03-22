using System;

public class PrefabPath : Attribute
{
    public string Path { get; set; }
    public PrefabPath(string path)
    {
        Path = path;
    }
}