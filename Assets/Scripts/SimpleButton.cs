using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleButton : MonoBehaviour
{
    public string sceneToLoad;
    public bool isQuit;
    public Color hoverColor = Color.yellow;
    private Color _originalColor;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer != null) _originalColor = _renderer.color;
    }

    void OnMouseEnter() { if (_renderer != null) _renderer.color = hoverColor; }
    void OnMouseExit() { if (_renderer != null) _renderer.color = _originalColor; }

    void OnMouseDown()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        if (isQuit)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
