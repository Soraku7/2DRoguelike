using System;
using UnityEngine;

public class ColorHolder : MonoBehaviour
{
    public static ColorHolder instance;

    [Header("Elements")]
    [SerializeField] private PaletteSO palette;

    private void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Debug.LogError("Multiple instances of ColorHolder found in the scene. Please ensure only one exists.");
            Destroy(gameObject);
        }
    }

    public static Color GetOutlineColor(int level)
    {
        level = Mathf.Clamp(level , 0, instance.palette.LevelOutlineColors.Length - 1);
        return instance.palette.LevelOutlineColors[level];
    }
    public static Color GetColor(int level)
    {
        level = Mathf.Clamp(level , 0, instance.palette.LevelColors.Length - 1);
        return instance.palette.LevelColors[level];
    }
}
