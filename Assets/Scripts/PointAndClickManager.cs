using UnityEngine;
using UnityEngine.InputSystem;

public class PointAndClickManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction _clickAction;

    void Awake()
    {
        var actionMap = inputActions.FindActionMap("Game");
        _clickAction = actionMap.FindAction("Click");
    }

    void OnEnable()
    {
        _clickAction.Enable();
        _clickAction.performed += OnClick;
    }

    void OnDisable()
    {
        _clickAction.Disable();
        _clickAction.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
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
