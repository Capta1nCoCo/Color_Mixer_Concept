using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;
    [SerializeField] private ColorChannel colorChannel;
    [SerializeField] private Color fruitColor = Color.white;
    public FruitType GetFruitType { get { return fruitType; } }
    public ColorChannel GetColorChannel { get { return colorChannel; } }
    public Color GetFruitColor { get { return fruitColor; } }
}
