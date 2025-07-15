using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image lockImage;
    
    public Button Button
    {
        get { return GetComponent<Button>(); }
        private set{} 
    }

    public void Configure(Sprite characterIcon , bool unlock)
    {
        characterImage.sprite = characterIcon;
        
        if(unlock) UnLock();
        else Lock();
    }

    public void Lock()
    {
        lockImage.gameObject.SetActive(true);
        characterImage.color = Color.gray;
    }
    
    public void UnLock()
    {
        lockImage.gameObject.SetActive(false);
        characterImage.color = Color.white;
    }
}
