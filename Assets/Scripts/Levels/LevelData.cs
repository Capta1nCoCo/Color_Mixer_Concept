using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;

    [SerializeField] private Color refrenceColor = Color.white;
    [SerializeField] private int dividerR;
    [SerializeField] private int dividerG;
    [Tooltip("Keep at 0 if less then 3 fruits used.")]
    [SerializeField] private int dividerB;

    public Color GetRefrenceColor { get { return refrenceColor; } }
    public int GetDividerR { get { return dividerR; } }
    public int GetDividerG { get { return dividerG; } }
    public int GetDividerB { get { return dividerB; } }

    private void Awake()
    {
        Instance = this;
    }
}
