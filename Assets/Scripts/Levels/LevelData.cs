using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;

    [SerializeField] private Material referenceJuiceMaterial;
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
        referenceJuiceMaterial.SetFloat(Constants.Fill, 1f);
        referenceJuiceMaterial.SetColor(Constants.SideColor, refrenceColor);
        referenceJuiceMaterial.SetColor(Constants.TopColor, refrenceColor);
    }
}
