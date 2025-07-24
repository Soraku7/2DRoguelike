using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Elements")]
    [SerializeField] private MobileJoystick playerJoystick;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetMoveVector()
    {
        return playerJoystick.GetMoveVector();
        
        if(SystemInfo.deviceType == DeviceType.Desktop) return GetDesktopMoveVector();
        else if(SystemInfo.deviceType == DeviceType.Handheld) return playerJoystick.GetMoveVector(); 
        
        
    }

    private Vector2 GetDesktopMoveVector()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
