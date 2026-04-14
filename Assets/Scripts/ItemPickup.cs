using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemName;
    public string flagToSet;
    public DialogueData pickupDialogue;
    public DialogueManager dialogueManager;
    public bool destroyOnPickup = true;

    public void Interact()
    {
        Debug.Log("Interacting with " + itemName);

        if (!string.IsNullOrEmpty(flagToSet) && GameStateManager.Instance != null)
            GameStateManager.Instance.SetFlag(flagToSet, true);

        if (dialogueManager != null && pickupDialogue != null)
            dialogueManager.DisplayDialogue(pickupDialogue);

        if (destroyOnPickup)
            gameObject.SetActive(false);
    }
}
