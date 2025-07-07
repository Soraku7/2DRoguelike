using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ShopManagerUI : MonoBehaviour
{
    [Header("Player Stats Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private GameObject playerStatsClosePanel;

    private float width;
    
    
    IEnumerator Start()
    {
        yield return null;
        
        ConfigurePlayerStatsPanel();
    }

    private void ConfigurePlayerStatsPanel()
    {
        width = Screen.width / (4f * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);
        
        HidePlayerStats();
    }

    public void ShowPlayerStats()
    {
        playerStatsClosePanel.SetActive(true);

        playerStatsPanel.DOKill();
        playerStatsPanel.DOAnchorPos(Vector2.right * (width / 2), 0.5f);
    }
    
    public void HidePlayerStats()
    {
        playerStatsClosePanel.SetActive(false);
        
        playerStatsPanel.DOKill();
        playerStatsPanel.DOAnchorPos(Vector2.left * (width / 2), 0.5f);
    }
}
