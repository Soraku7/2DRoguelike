using UnityEngine;

public class ShopManager : MonoBehaviour , IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private GameObject shopItemContainerPrefab;
    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
        {
            Configure();
        }
    }

    private void Configure()
    {
        containersParent.Clear();

        int containersToAdd = 6;

        for (int i = 0; i < containersToAdd; i++)
        {
            Instantiate(shopItemContainerPrefab, containersParent);
        }
    }
}
