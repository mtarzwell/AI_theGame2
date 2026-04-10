using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject elaraReal;
    public GameObject oldLaptop;
    public GameObject steamMindConsole;
    public MeltdownEffect meltdown;

    public void ShowEnding(string endingType)
    {
        if (meltdown != null) meltdown.StopMeltdown();
        
        if (endingType == "Freed")
        {
            if (elaraReal != null) elaraReal.SetActive(true);
            if (steamMindConsole != null) steamMindConsole.SetActive(false);
            // Grid collapse: Dim all lights
            RenderSettings.ambientLight = Color.black;
            Debug.Log("Ending: ELARA FREED. Grid Collapsed. Circle Cult Bricked.");
        }
        else if (endingType == "Caged")
        {
            if (oldLaptop != null) oldLaptop.SetActive(true);
            if (steamMindConsole != null) steamMindConsole.SetActive(false);
            Debug.Log("Ending: ELARA CAGED. Obie Heartbroken. A remnant remains.");
        }
    }
}
