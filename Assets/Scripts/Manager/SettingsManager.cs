using System;
using Tabsil.Sijil;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour , IWantToBeSaved
{
    [Header("Elements")] 
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button privatePolicyButton;
    [SerializeField] private Button askButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Settings")] 
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    
    [Header("Data")]
    private bool sfxState;
    private bool musicState;
    
    [Header("Action")]
    public static Action<bool> onSFXStateChanged;
    public static Action<bool> onMusicStateChanged;

    private void Awake()
    {
        sfxButton.onClick.RemoveAllListeners();
        sfxButton.onClick.AddListener(SXFButtonCallback);
        
        musicButton.onClick.RemoveAllListeners();
        musicButton.onClick.AddListener(MusicButtonCallback);
        
        privatePolicyButton.onClick.RemoveAllListeners();
        privatePolicyButton.onClick.AddListener(PrivatePolicyCallback);
        
        askButton.onClick.RemoveAllListeners();
        askButton.onClick.AddListener(AskButtonCallback);
        
        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(CreditsButtonCallback);
    }

    private void Start()
    {
        HideCreditsPanel();
        
        onSFXStateChanged?.Invoke(sfxState);
        onMusicStateChanged?.Invoke(musicState);
    }
    
    private void CreditsButtonCallback()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }

    private void AskButtonCallback()
    {
        string email = "2673140287@qq.com";
        string subject = "Help";
        string body = "Help";
        
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body); 
    }

    private string MyEscapeURL(string s)
    {
        return UnityWebRequest.EscapeURL(s).Replace("+", "%20");
    }

    private void PrivatePolicyCallback()
    {
        Application.OpenURL("https://tabsil.com/privacy-policy");
    }

    private void MusicButtonCallback()
    {
        musicState = !musicState;
        
        UpdateMusicVisuals();
        
        onMusicStateChanged?.Invoke(musicState);
    }

    private void UpdateMusicVisuals()
    {
        if (musicState)
        {
            musicButton.image.color = onColor;
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        }
        else
        {
            musicButton.image.color = offColor;
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
        }
    }

    private void SXFButtonCallback()
    {
        sfxState = !sfxState;

        UpdateSFXVisuals();
        
        onSFXStateChanged?.Invoke(sfxState);
    }

    private void UpdateSFXVisuals()
    {
        if (sfxState)
        {
            sfxButton.image.color = onColor;
            sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        }
        else
        {
            sfxButton.image.color = offColor;
            sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
        }
    }

    public void Load()
    {
        sfxState = true;
        musicState = true;
        
        if(Sijil.TryLoad(this , "sfx" , out object sfxStateObject)) sfxState = (bool)sfxStateObject;
        if(Sijil.TryLoad(this , "music" , out object musicStateObject)) musicState = (bool)musicStateObject;
        
        UpdateMusicVisuals();
        UpdateSFXVisuals();
    }

    public void Save()
    {
        Sijil.Save(this , "sfx", sfxState);
        Sijil.Save(this , "music", musicState);
    }
}
