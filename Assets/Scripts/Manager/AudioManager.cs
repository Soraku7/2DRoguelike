using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public bool IsSFXOn { get; private set; }
    public bool IsMusicOn { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SettingsManager.onMusicStateChanged += SFXStatChangedCallback;
        SettingsManager.onSFXStateChanged += MusicStatChangedCallback;
    }
    
    private void OnDestroy()
    {
        SettingsManager.onMusicStateChanged -= SFXStatChangedCallback;
        SettingsManager.onSFXStateChanged -= MusicStatChangedCallback;
    }

    private void SFXStatChangedCallback(bool sfxState)
    {
        IsSFXOn = sfxState;
    }

    private void MusicStatChangedCallback(bool musicState)
    {
        IsMusicOn = musicState;
    }
}
