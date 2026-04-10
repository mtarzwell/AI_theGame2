using UnityEngine;
using UnityEngine.UIElements;

public class SteamMindInteraction : MonoBehaviour, IInteractable
{
    public DialogueData initialDialogue;
    public DialogueManager dialogueManager;
    public GameObject entityVisual;

    public void Interact()
    {
        if (dialogueManager != null && initialDialogue != null)
        {
            if (entityVisual != null) entityVisual.SetActive(true);
            dialogueManager.DisplayDialogue(initialDialogue);
        }
    }
}
