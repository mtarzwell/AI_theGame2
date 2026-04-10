using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

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
        ResizeColliderToCoverLabelAndSprite();
    }

    /// <summary>
    /// Scene colliders were smaller than the visible "NEW GAME" / "QUIT" text (child TextMesh).
    /// Clicks on the text missed the BoxCollider2D, so OnMouseDown never ran.
    /// </summary>
    void ResizeColliderToCoverLabelAndSprite()
    {
        var box = GetComponent<BoxCollider2D>();
        if (box == null) return;

        var renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds world = renderers[0].bounds;
        for (var i = 1; i < renderers.Length; i++)
            world.Encapsulate(renderers[i].bounds);

        Vector3 localMin = transform.InverseTransformPoint(world.min);
        Vector3 localMax = transform.InverseTransformPoint(world.max);

        float minX = Mathf.Min(localMin.x, localMax.x);
        float maxX = Mathf.Max(localMin.x, localMax.x);
        float minY = Mathf.Min(localMin.y, localMax.y);
        float maxY = Mathf.Max(localMin.y, localMax.y);

        var center = new Vector2((minX + maxX) * 0.5f, (minY + maxY) * 0.5f);
        var size = new Vector2(maxX - minX, maxY - minY);

        const float pad = 0.08f;
        box.offset = center;
        box.size = new Vector2(Mathf.Max(size.x + pad, 0.05f), Mathf.Max(size.y + pad, 0.05f));
    }

    void OnMouseEnter() { if (_renderer != null) _renderer.color = hoverColor; }
    void OnMouseExit() { if (_renderer != null) _renderer.color = _originalColor; }

    void Update()
    {
        if (!TryGetPrimaryClickScreenPosition(out var screenPos)) return;
        var myCol = GetComponent<Collider2D>();
        if (myCol == null || !myCol.enabled) return;
        var cam = Camera.main;
        if (cam == null) return;

        // ScreenToWorldPoint(mouse) with z=0 gives wrong XY for orthographic cameras.
        // Ray vs 2D colliders is reliable for world-space UI sprites.
        var ray = cam.ScreenPointToRay(screenPos);
        const float dist = 100f;
        var hit = Physics2D.GetRayIntersection(ray, dist);
        if (hit.collider == null || hit.collider != myCol) return;

        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        if (isQuit)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    static bool TryGetPrimaryClickScreenPosition(out Vector2 screenPos)
    {
        screenPos = default;
#if ENABLE_INPUT_SYSTEM
        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            screenPos = mouse.position.ReadValue();
            return true;
        }
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Input.mousePosition;
            return true;
        }
#endif
        return false;
    }
}
