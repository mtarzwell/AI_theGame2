using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class UIForceAssigner : MonoBehaviour
{
    public PanelSettings panelSettings;
    public VisualTreeAsset visualTree;

    void OnEnable()
    {
        var uiDoc = GetComponent<UIDocument>();
        if (uiDoc != null)
        {
            if (uiDoc.panelSettings == null) uiDoc.panelSettings = panelSettings;
            if (uiDoc.visualTreeAsset == null) uiDoc.visualTreeAsset = visualTree;
        }
    }

    void Update()
    {
        // Keep it assigned in editor
        if (!Application.isPlaying)
        {
            OnEnable();
        }
    }
}
