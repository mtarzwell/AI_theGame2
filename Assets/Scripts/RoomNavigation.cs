using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Camera mainCamera;

    public void GoToRoom(Room room)
    {
        if (room != null && mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(room.cameraPosition.x, room.cameraPosition.y, mainCamera.transform.position.z);
            Debug.Log("Navigated to: " + room.roomName);
            
            if (AudioManager.Instance != null)
            {
                if (room.roomName.Contains("Lab")) AudioManager.Instance.SwitchToLab();
                else if (room.roomName.Contains("Apartment") || room.roomName.Contains("Room")) AudioManager.Instance.SwitchToApartment();
            }
        }
    }
}
