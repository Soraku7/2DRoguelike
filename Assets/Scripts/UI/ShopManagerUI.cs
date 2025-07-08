using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
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
    
	[Header("Item Info Elements")]
    [SerializeField] private RectTransform itemInfoPanel;
    
    private float itemheight;
    
    IEnumerator Start()
    {
        yield return null;
        
        ConfigurePlayerStatsPanel();
        ConfigureInventoryPanel();
        ConfigureItemInfoPanel();
    }

    private void ConfigureItemInfoPanel()
    {
        itemheight = Screen.height / (2 * itemInfoPanel.lossyScale.x);
        itemInfoPanel.offsetMax = itemInfoPanel.offsetMax.With(y: itemheight);
        
        itemInfoPanel.gameObject.SetActive(false);
    }

    private void ConfigureInventoryPanel()
    {
        inventoryWidth = Screen.width / (4f * inventoryPanel.lossyScale.x);
        inventoryPanel.offsetMin = inventoryPanel.offsetMax.With(x: -inventoryWidth);
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

    public void HideInventory(bool hideItemInfo = true)
    {
        inventoryClosePanel.SetActive(false);
        
        inventoryPanel.DOKill();
        inventoryPanel.DOAnchorPos(Vector2.right * (inventoryWidth / 2), 0.5f);
        
        if(hideItemInfo) HideItemInfo();
    }
    
    [Button]
    public void ShowItemInfo()
    {
        itemInfoPanel.gameObject.SetActive(true);
        
        itemInfoPanel.DOKill();
        itemInfoPanel.DOAnchorPos(Vector2.up * (itemheight / 2), 0.5f);
    }
    
    [Button]
    public void HideItemInfo()
    {
        itemInfoPanel.DOKill();
        itemInfoPanel.DOAnchorPos(Vector2.down * (itemheight / 2), 0.5f).OnComplete(() =>
        {
            itemInfoPanel.gameObject.SetActive(false);
        });
    }
}
