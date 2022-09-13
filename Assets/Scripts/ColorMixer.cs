using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    [SerializeField] private Material juiceMaterial;
    [SerializeField] private Color[] referenceColors;
    [SerializeField] private Color[] fruitColors;

    private const string SideColor = "_SideColor";
    private const string TopColor = "_TopColor";

    private int currentLevelIndex;
    private int dividerR;
    private int dividerG;
    private int dividerB;

    private float currentR;
    private float currentG;
    private float currentB;

    private bool isSingleTypeFruits;

    private Color singleTypeFruitsColor = Color.white;

    private List<Enum> fruitTypesToMix = new List<Enum>();
    private List<Enum> singleTypeFruits = new List<Enum>();

    private void Awake()
    {
        isSingleTypeFruits = true;
        GameEvents.MixColors += OnMixColors;
    }   

    private void Start()
    {
        //TEST AREA
        //TODO: currentLevelIndex and dividersRGB will be passed each level by GameController
        dividerR = 2;
        dividerG = 3;
        AdjustJuiceColor();
    }

    private void OnDestroy()
    {
        GameEvents.MixColors -= OnMixColors;
    }

    public void AddFruitTypeToMix(FruitType fruitTypeInTheJug)
    {
        fruitTypesToMix.Add(fruitTypeInTheJug);
    }

    private void OnMixColors()
    {
        MixFruitsColors();
        
        if (isSingleTypeFruits)
        {
            ApplyColorToJuiceMaterial(singleTypeFruitsColor);
        }
        else
        {
            AdjustJuiceColor();
        }        
    }

    private void MixFruitsColors()
    {
        foreach (FruitType fruit in fruitTypesToMix)
        {
            if (isSingleTypeFruits)
            {
                if (singleTypeFruits.Count == 0)
                {
                    singleTypeFruits.Add(fruit);
                    singleTypeFruitsColor = GetFruitColor(fruit);
                }
                else if (!singleTypeFruits.Contains(fruit))
                {
                    AddSingleTypeFruitsAsColors();
                    AddFruitAsColor(fruit);                    
                }
                else
                {
                    singleTypeFruits.Add(fruit);
                }
            }
            else
            {
                AddFruitAsColor(fruit);
            }            
        }
        fruitTypesToMix.Clear();
    }

    private Color GetFruitColor(FruitType fruit)
    {
        switch (fruit)
        {
            case FruitType.Apple: return fruitColors[0];
            case FruitType.Banana: return fruitColors[1];
            case FruitType.Orange: return fruitColors[2];
            case FruitType.Cherry: return fruitColors[3];
            case FruitType.Tomato: return fruitColors[4];
            case FruitType.Broccoli: return fruitColors[5];
            case FruitType.Eggplant: return fruitColors[6];
            default: return fruitColors[0];
        }
    }

    private void AddSingleTypeFruitsAsColors()
    {
        isSingleTypeFruits = false;

        foreach (FruitType fruit in singleTypeFruits)
        {
            AddFruitAsColor(fruit);
        }
        singleTypeFruits.Clear();
    }

    private void AddFruitAsColor(FruitType fruit)
    {        
        var fruitPartOfR = referenceColors[currentLevelIndex].r / dividerR;
        var fruitPartOfG = referenceColors[currentLevelIndex].g / dividerG;

        switch (fruit)
        {
            case FruitType.Banana:
                currentR += fruitPartOfR;
                currentR = Mathf.Clamp(currentR, 0f, referenceColors[currentLevelIndex].r);
                break;

            case FruitType.Apple & FruitType.Broccoli:
                currentG += fruitPartOfG;
                currentG = Mathf.Clamp(currentG, 0f, referenceColors[currentLevelIndex].g);
                break;
        }
    }

    private void AdjustJuiceColor()
    {
        Color temp = referenceColors[currentLevelIndex];
        temp.r = currentR;
        temp.g = currentG;
        ApplyColorToJuiceMaterial(temp);
    }

    private void ApplyColorToJuiceMaterial(Color color)
    {
        juiceMaterial.SetColor(SideColor, color);
        juiceMaterial.SetColor(TopColor, color);
    }
}
