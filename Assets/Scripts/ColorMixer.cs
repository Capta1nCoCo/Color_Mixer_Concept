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

    private List<Enum> fruitTypesToMix = new List<Enum>();

    private void Awake()
    {        
        GameEvents.MixColors += OnMixColors;
    }   

    private void Start()
    {
        //TEST AREA
        //TODO: currentLevelIndex and dividers should be passed each level
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
        AdjustJuiceColor();
    }

    private void MixFruitsColors()
    {
        foreach (FruitType fruit in fruitTypesToMix)
        {
            AddFruitAsColor(fruit);
            print(fruit);
        }
        fruitTypesToMix.Clear();
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
