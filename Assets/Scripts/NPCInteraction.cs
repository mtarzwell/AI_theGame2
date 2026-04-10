using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public DialogueData dialogue;
    public DialogueManager dialogueManager;

    public void Interact()
    {
        if (dialogueManager != null && dialogue != null)
        {
            dialogueManager.DisplayDialogue(dialogue);
        }
    }
}
