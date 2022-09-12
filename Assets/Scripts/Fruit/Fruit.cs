using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;
    public FruitType GetFruitType { get { return fruitType; } }
}
