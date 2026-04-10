using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreenManager : MonoBehaviour
{
    public UIDocument uiDocument;
    public string gameSceneName = "SampleScene"; // Update if scene name is different

    void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();
    }

    void Start()
    {
        if (uiDocument == null) return;
        var root = uiDocument.rootVisualElement;
        if (root == null) return;

        var startButton = root.Q<Button>("start-button");
        var quitButton = root.Q<Button>("quit-button");

        if (startButton != null)
            startButton.clicked += OnStartClicked;

        if (quitButton != null)
            quitButton.clicked += OnQuitClicked;
    }

    void OnDisable()
    {
        if (uiDocument == null) return;
        var root = uiDocument.rootVisualElement;
        if (root == null) return;

        var startButton = root.Q<Button>("start-button");
        var quitButton = root.Q<Button>("quit-button");

        if (startButton != null)
            startButton.clicked -= OnStartClicked;

        if (quitButton != null)
            quitButton.clicked -= OnQuitClicked;
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
