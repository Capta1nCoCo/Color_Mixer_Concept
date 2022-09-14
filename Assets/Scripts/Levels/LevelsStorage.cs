using UnityEngine;

[CreateAssetMenu(fileName = "Levels Storage", menuName = "Configs/Levels Storage", order = 1)]
public class LevelsStorage : ScriptableObject
{
    [SerializeField] private GameObject[] levels;
    public int Count => levels.Length;
    public GameObject GetLevel(int index) => levels[index % Count];
}
