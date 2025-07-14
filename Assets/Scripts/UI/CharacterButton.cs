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

    public void Configure(Sprite characterIcon)
    {
        characterImage.sprite = characterIcon;
    }
}
