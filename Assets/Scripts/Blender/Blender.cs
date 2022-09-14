using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    [SerializeField] private GameObject juiceInTheJug;
    [SerializeField] private Material juiceMaterial;

    private const string Fill = "_Fill";
    private const float fillAmountPerFruit = 0.15f;

    private float currentFillAmount;

    private void Awake()
    {
        juiceInTheJug.SetActive(true);
        FillTheJugWithJuice();
    }

    public void Mix()
    {        
        print("Mixing Fruits...");
        GameEvents.JugIsEmpty();
        GameEvents.MixColors();
    }

    public void AddToFillAmount(int items)
    {
        currentFillAmount += fillAmountPerFruit * items;
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, 1f);
        FillTheJugWithJuice();
        print("Current Fill Amount = " + currentFillAmount);
    }

    private void FillTheJugWithJuice()
    {
        juiceMaterial.SetFloat(Fill, currentFillAmount);
    }
}
