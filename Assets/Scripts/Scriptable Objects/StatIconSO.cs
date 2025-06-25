using UnityEngine;

[CreateAssetMenu(fileName = "StatIcon", menuName = "Scriptable Objects/Stat Icons")]
public class StatIconSo : ScriptableObject
{
    [field:SerializeField] public StatIcon[] StatIcons { get; private set; }
}

[System.Serializable]
public struct StatIcon
{
    public Stat stat;
    public Sprite icon;
}
