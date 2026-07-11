using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private Volume levelVolume;
    [SerializeField] private Texture dayTexture;
    [SerializeField] private Texture nightTexture;

    private bool isDay = true;
    private VolumeProfile profileVolume;
    private HDRISky sky;
    private InputSystem_Actions inputSystem;
    void Start()
    {
        profileVolume = levelVolume.sharedProfile;
        profileVolume.TryGet<HDRISky>(out sky);

        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();

        ChangeDayNight(true);
    }

    void Update()
    {
        if (inputSystem.Player.DebugButtonN.WasPressedThisFrame())
        {
            ChangeDayNight(!isDay);
        }
    }

    public void ChangeDayNight(bool toDay)
    {
        isDay = toDay;
        sky.hdriSky.Override(isDay ? dayTexture : nightTexture);
    }
}
