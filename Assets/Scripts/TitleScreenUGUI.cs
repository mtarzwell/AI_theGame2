using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenUGUI : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public string gameSceneName = "SampleScene";

    void Start()
    {
        if (startButton != null) startButton.onClick.AddListener(OnStartClicked);
        if (quitButton != null) quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnStartClicked()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnQuitClicked()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
