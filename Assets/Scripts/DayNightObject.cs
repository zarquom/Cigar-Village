using UnityEngine;

public class DayNightObject : MonoBehaviour
{
    [SerializeField] private Vector3 NightPosition;
    private Vector3 DayPosition;

    private void Start()
    {
        DayPosition = transform.localPosition;
        DayNightManager dayNightManager = FindAnyObjectByType<DayNightManager>();
        if (dayNightManager != null)
        {
            dayNightManager.OnChangeDayNight += OnChangeDayNight;
        }
    }
    public void OnChangeDayNight(bool isDay)
    {
        // Implement the logic to change the object's state based on day/night
        if (isDay)
        {
            // Change to day state
            transform.localPosition = DayPosition;
        }
        else
        {
            // Change to night state
            transform.localPosition = NightPosition;
        }
    }
}