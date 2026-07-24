using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    private Action onShopClosedCallback;
    private void Start()
    {
        shopUI.SetActive(false); // Ensure the shop UI is hidden at the start

    }
    public void OpenShop(Action callback)
    {
        onShopClosedCallback = callback;
        shopUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void CloseShop()
    {
        onShopClosedCallback?.Invoke();
        shopUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
