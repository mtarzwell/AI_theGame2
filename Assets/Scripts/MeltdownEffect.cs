using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class MeltdownEffect : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D tealLight;
    public Light2D redLight;
    
    private bool _isMeltdownActive = false;

    public void StartMeltdown()
    {
        if (_isMeltdownActive) return;
        _isMeltdownActive = true;
        StartCoroutine(MeltdownRoutine());
    }

    private IEnumerator MeltdownRoutine()
    {
        while (_isMeltdownActive)
        {
            float wait = Random.Range(0.05f, 0.2f);
            
            // Flicker Teal
            tealLight.intensity = Random.Range(1f, 4f);
            // Flicker Red
            redLight.intensity = Random.Range(0.5f, 3f);
            // Dim Global
            if (globalLight != null) globalLight.intensity = Random.Range(0.1f, 0.3f);

            yield return new WaitForSeconds(wait);
        }
    }

    public void StopMeltdown()
    {
        _isMeltdownActive = false;
        tealLight.intensity = 1f;
        redLight.intensity = 0f;
        if (globalLight != null) globalLight.intensity = 1f;
    }
}
