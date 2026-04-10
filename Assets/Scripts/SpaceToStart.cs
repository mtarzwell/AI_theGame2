using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToStart : MonoBehaviour
{
    public string sceneToLoad = "SampleScene";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed - Loading Game...");
            if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
