Vasageld
========

Assembly version attributes incrementor

Sample usage
----------

```cs
public class Sandbox
{
    public static void Main(string[] args)
    {
        FileIncrementor incrementor = new FileIncrementor(@"h:\project\test\AssemblyInfo.cs");
        var results = incrementor.Increment(new SimpleProvider(),
            Versions.AssemblyFileVersion | Versions.AssemblyInformationalVersion);
    }
}

public class SimpleProvider : IVersionsProvider
{
    public Versions SupportedVersions
    {
        get { return Versions.AssemblyFileVersion | Versions.AssemblyInformationalVersion; }
    }

    public string GetAssemblyVersion(string current)
    {
        throw new NotSupportedException();
    }

    public string GetAssemblyFileVersion(string current)
    {
        return "1.0.0.5";
    }

    public string GetAssemblyInformationalVersion(string current)
    {
        return Version.Parse(current).IncrementBuild().ToString();
    }
}
```