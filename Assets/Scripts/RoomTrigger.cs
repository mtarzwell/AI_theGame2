using UnityEngine;

public class RoomTrigger : MonoBehaviour, IInteractable
{
    public Room targetRoom;
    public RoomNavigation navigation;

    public string requiredFlag;
    public DialogueData lockedDialogue;

    public void Interact()
    {
        if (navigation != null && targetRoom != null)
        {
            if (!string.IsNullOrEmpty(requiredFlag))
            {
                if (!GameStateManager.Instance.GetFlag(requiredFlag))
                {
                    if (navigation.GetComponent<DialogueManager>() != null && lockedDialogue != null)
                    {
                        navigation.GetComponent<DialogueManager>().DisplayDialogue(lockedDialogue);
                    }
                    Debug.Log("Room is locked. Missing flag: " + requiredFlag);
                    return;
                }
            }
            navigation.GoToRoom(targetRoom);
        }
    }
}
