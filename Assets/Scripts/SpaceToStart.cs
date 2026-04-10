using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SpaceToStart : MonoBehaviour
{
    public string sceneToLoad = "SampleScene";

    void Update()
    {
        if (!WasSpacePressedThisFrame()) return;
        Debug.Log("Space pressed - Loading Game...");
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        SceneManager.LoadScene(sceneToLoad);
    }

    static bool WasSpacePressedThisFrame()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            return true;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(KeyCode.Space))
            return true;
#endif
        return false;
    }
}
