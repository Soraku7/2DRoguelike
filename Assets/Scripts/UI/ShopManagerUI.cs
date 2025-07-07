using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ShopManagerUI : MonoBehaviour
{
    [Header("Player Stats Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private GameObject playerStatsClosePanel;

    private float playerStatsWidth;
    
    [Header("Inventory Elements")]
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private GameObject inventoryClosePanel;
    
    private float inventoryWidth;
    IEnumerator Start()
    {
        yield return null;
        
        ConfigurePlayerStatsPanel();
        ConfigureInventoryPanel();
    }

    private void ConfigureInventoryPanel()
    {
        inventoryWidth = Screen.width / (4f * inventoryPanel.lossyScale.x);
        inventoryPanel.offsetMax = inventoryPanel.offsetMin.With(x: -inventoryWidth);
        Debug.Log(inventoryPanel.offsetMin.With(x: -inventoryWidth));
        HideInventory();
    }

    private void ConfigurePlayerStatsPanel()
    {
        playerStatsWidth = Screen.width / (4f * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: playerStatsWidth);
        HidePlayerStats();
    }

    public void ShowPlayerStats()
    {
        playerStatsClosePanel.SetActive(true);

        playerStatsPanel.DOKill();
        playerStatsPanel.DOAnchorPos(Vector2.right * (playerStatsWidth / 2), 0.5f);
    }
    
    public void HidePlayerStats()
    {
        playerStatsClosePanel.SetActive(false);
        
        playerStatsPanel.DOKill();
        playerStatsPanel.DOAnchorPos(Vector2.left * (playerStatsWidth / 2), 0.5f);
    }

    public void ShowInventory()
    {
        inventoryClosePanel.SetActive(true);

        inventoryPanel.DOKill();
        inventoryPanel.DOAnchorPos(Vector2.left * (inventoryWidth / 2), 0.5f);
    }

    public void HideInventory()
    {
        inventoryClosePanel.SetActive(false);
        
        inventoryPanel.DOKill();
        inventoryPanel.DOAnchorPos(Vector2.right * (inventoryWidth / 2), 0.5f);
    }
}
