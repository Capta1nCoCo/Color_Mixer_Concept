using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private LevelsStorage levelsStorage;
    [SerializeField] private Canvas blenderCanvas;

    //TODO: Load MainScene each new level and get incremented currentLevelIndex from PlayerPrefs;
    [SerializeField] private int currentLevelIndex;

    private void Awake()
    {
        Instance = this;
        var level = Instantiate(levelsStorage.GetLevel(currentLevelIndex), blenderCanvas.transform);
        level.transform.position = blenderCanvas.transform.position;
    }
}
