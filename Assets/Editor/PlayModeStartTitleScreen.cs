#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Ensures Play Mode in the Editor starts from the title screen (build index 0),
/// so you get the full Title → Game flow even when SampleScene was left open.
/// </summary>
[InitializeOnLoad]
static class PlayModeStartTitleScreen
{
    const string TitleScenePath = "Assets/Scenes/TitleScreen.unity";

    static PlayModeStartTitleScreen()
    {
        var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(TitleScenePath);
        if (scene != null)
            EditorSceneManager.playModeStartScene = scene;
    }
}
#endif
