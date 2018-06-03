public class CommandLineBuilder : UnityEngine.MonoBehaviour
{
    public static void PerformWebGlBuild()
    {
        var args = System.Environment.GetCommandLineArgs();

        string locationPathName = null;
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i] == "-folderPathToSave")
            {
                locationPathName = args[i + 1];
                break;
            }
        }

        if (locationPathName == null)
            throw new System.Exception("Arg '-folderPathToSave' not specified correctly");

        var buildOptions = new UnityEditor.BuildPlayerOptions
        {
            scenes = new[] {"Assets/Scenes/MainScene.unity"},
            target = UnityEditor.BuildTarget.WebGL,
            locationPathName = locationPathName
        };

        UnityEditor.BuildPipeline.BuildPlayer(buildOptions);
    }
}