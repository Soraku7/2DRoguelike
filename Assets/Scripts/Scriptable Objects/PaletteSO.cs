using UnityEngine;

[CreateAssetMenu(fileName = "Palette", menuName = "Scriptable Objects/Palette")]
public class PaletteSO : ScriptableObject
{
    [field: SerializeField] public Color[] LevelColors { get; private set; }
    [field: SerializeField] public Color[] LevelOutlineColors { get; private set; }
}
