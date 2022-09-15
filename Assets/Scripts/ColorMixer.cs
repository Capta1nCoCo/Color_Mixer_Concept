using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    [SerializeField] private Material juiceMaterial;

    private int dividerR;
    private int dividerG;
    private int dividerB;

    private float currentR;
    private float currentG;
    private float currentB;

    private bool isSingleTypeFruits;

    private Color referenceColor = Color.white;
    private Color singleTypeFruitsColor = Color.white;

    private List<Fruit> fruitsToMix = new List<Fruit>();
    private List<Fruit> singleTypeFruits = new List<Fruit>();

    private void Awake()
    {
        isSingleTypeFruits = true;
        GameEvents.MixColors += OnMixColors;
    }

    private void Start()
    {        
        GetLevelData();
    }

    private void OnDestroy()
    {
        GameEvents.MixColors -= OnMixColors;
    }

    public void AddFruitToMix(Fruit fruitInTheJug)
    {
        fruitsToMix.Add(fruitInTheJug);
    }

    private void GetLevelData()
    {
        var levelData = LevelData.Instance;
        referenceColor = levelData.GetRefrenceColor;
        dividerR = levelData.GetDividerR;
        dividerG = levelData.GetDividerG;
        dividerB = levelData.GetDividerB;
    }

    private void OnMixColors()
    {
        MixFruitColors();
        ApplyFruitColorsToJuice();
    }
    
    private void MixFruitColors()
    {
        foreach (Fruit fruit in fruitsToMix)
        {
            if (isSingleTypeFruits)
            {
                SortFruitsByType(fruit);
            }
            else
            {
                AddFruitAsColor(fruit);
            }            
        }
        fruitsToMix.Clear();
    }
    
    private void ApplyFruitColorsToJuice()
    {
        if (isSingleTypeFruits)
        {
            ApplyColorToJuiceMaterial(singleTypeFruitsColor);
        }
        else
        {
            AdjustJuiceColor();
        }
    }

    private void SortFruitsByType(Fruit fruit)
    {
        var firstItemIndex = 0;
        if (singleTypeFruits.Count == firstItemIndex)
        {
            singleTypeFruits.Add(fruit);
            SetSingleTypeFruitsColor(fruit);
        }
        else if (fruit.GetFruitType != singleTypeFruits[firstItemIndex].GetFruitType)
        {
            AddSingleTypeFruitsAsColors();
            AddFruitAsColor(fruit);
        }
        else
        {
            singleTypeFruits.Add(fruit);
        }
    }

    private void SetSingleTypeFruitsColor(Fruit fruit)
    {
        singleTypeFruitsColor = fruit.GetFruitColor;
    }

    private void AddSingleTypeFruitsAsColors()
    {
        foreach (Fruit fruit in singleTypeFruits)
        {
            AddFruitAsColor(fruit);
        }
        singleTypeFruits.Clear();
        
        isSingleTypeFruits = false;
    }

    private void AddFruitAsColor(Fruit fruit)
    {
        var refColor = referenceColor;
        var fruitPartOfR = refColor.r / dividerR;
        var fruitPartOfG = refColor.g / dividerG;
        var fruitPartOfB = refColor.b / dividerB;

        switch (fruit.GetColorChannel)
        {
            case ColorChannel.R:
                currentR += fruitPartOfR;
                currentR = Mathf.Clamp(currentR, 0f, refColor.r);
                break;

            case ColorChannel.G:
                currentG += fruitPartOfG;
                currentG = Mathf.Clamp(currentG, 0f, refColor.g);
                break;

            case ColorChannel.B:
                currentB += fruitPartOfB;
                currentB = Mathf.Clamp(currentB, 0f, refColor.b);
                break;
        }
    }

    private void AdjustJuiceColor()
    {
        Color temp = referenceColor;
        temp.r = currentR;
        temp.g = currentG;
        if (dividerB > 0) { temp.b = currentB; }        
        ApplyColorToJuiceMaterial(temp);
    }

    private void ApplyColorToJuiceMaterial(Color color)
    {
        juiceMaterial.SetColor(Constants.SideColor, color);
        juiceMaterial.SetColor(Constants.TopColor, color);
    }
}
