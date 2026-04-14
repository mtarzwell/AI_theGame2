using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [SerializeField] private UIDocument transitionUIDocument;
    [SerializeField] private float fadeDuration = 0.5f;

    private VisualElement _fadeOverlay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (transitionUIDocument != null)
        {
            _fadeOverlay = transitionUIDocument.rootVisualElement.Q<VisualElement>("fade-overlay");
            if (_fadeOverlay != null)
            {
                _fadeOverlay.style.opacity = 0;
                _fadeOverlay.pickingMode = PickingMode.Ignore; // Don't block clicks when transparent
            }
        }
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        if (_fadeOverlay != null)
        {
            _fadeOverlay.pickingMode = PickingMode.Position; // Block clicks
            yield return StartCoroutine(AnimateFade(0, 1));
        }

        yield return SceneManager.LoadSceneAsync(sceneName);

        if (_fadeOverlay != null)
        {
            yield return StartCoroutine(AnimateFade(1, 0));
            _fadeOverlay.pickingMode = PickingMode.Ignore;
        }
    }

    private IEnumerator AnimateFade(float start, float end)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            if (_fadeOverlay != null) _fadeOverlay.style.opacity = alpha;
            yield return null;
        }
        if (_fadeOverlay != null) _fadeOverlay.style.opacity = end;
    }
}
