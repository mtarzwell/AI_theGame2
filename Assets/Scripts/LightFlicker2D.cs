using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker2D : MonoBehaviour
{
    private Light2D _light;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        _light = GetComponent<Light2D>();
    }

    void Update()
    {
        _light.intensity = Mathf.Lerp(_light.intensity, Random.Range(minIntensity, maxIntensity), flickerSpeed);
    }
}
