using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PointAndClickManager : MonoBehaviour
{
    const string GameInputsResourcesPath = "GameInputs";
#if UNITY_EDITOR
    const string GameInputsAssetPath = "Assets/Resources/GameInputs.inputactions";
#endif

    public InputActionAsset inputActions;
    private InputAction _clickAction;
    private InputAction _fallbackClick;

    void Awake()
    {
        if (inputActions == null)
        {
            inputActions = Resources.Load<InputActionAsset>(GameInputsResourcesPath);
            if (inputActions == null)
            {
                var obj = Resources.Load(GameInputsResourcesPath);
                inputActions = obj as InputActionAsset;
            }
        }
#if UNITY_EDITOR
        if (inputActions == null)
            inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(GameInputsAssetPath);
#endif

        if (inputActions != null)
        {
            var actionMap = inputActions.FindActionMap("Game");
            if (actionMap != null)
                _clickAction = actionMap.FindAction("Click");
        }

        if (_clickAction == null)
        {
            _fallbackClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
            Debug.LogWarning(
                "PointAndClickManager: Using built-in mouse click (assign GameInputs asset for full input map).",
                this);
        }
    }

    void OnEnable()
    {
        if (_clickAction != null)
        {
            _clickAction.Enable();
            _clickAction.performed += OnClick;
        }
        else if (_fallbackClick != null)
        {
            _fallbackClick.Enable();
            _fallbackClick.performed += OnClick;
        }
    }

    void OnDisable()
    {
        if (_clickAction != null)
        {
            _clickAction.performed -= OnClick;
            _clickAction.Disable();
        }
        if (_fallbackClick != null)
        {
            _fallbackClick.performed -= OnClick;
            _fallbackClick.Disable();
        }
    }

    void OnDestroy()
    {
        if (_fallbackClick != null)
        {
            _fallbackClick.Dispose();
            _fallbackClick = null;
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (Mouse.current == null) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        var cam = Camera.main;
        if (cam == null)
            cam = Object.FindFirstObjectByType<Camera>();
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}

public interface IInteractable
{
    void Interact();
}
