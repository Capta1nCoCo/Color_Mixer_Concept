using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType fruitVariety;
    public FruitType GetFruitVariety { get { return fruitVariety; } }
}
