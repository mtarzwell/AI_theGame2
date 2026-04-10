using UnityEngine;

public class RoomTrigger : MonoBehaviour, IInteractable
{
    public Room targetRoom;
    public RoomNavigation navigation;

    public void Interact()
    {
        if (navigation != null && targetRoom != null)
        {
            navigation.GoToRoom(targetRoom);
        }
    }
}
