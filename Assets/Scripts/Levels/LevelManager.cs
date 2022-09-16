using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelsStorage levelsStorage;
    [SerializeField] private Canvas blenderCanvas;

    private int currentLevelIndex;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt(Constants.CurrentLevelIndex);
        var level = Instantiate(levelsStorage.GetLevel(currentLevelIndex), blenderCanvas.transform);
        level.transform.position = blenderCanvas.transform.position;
    }
}
