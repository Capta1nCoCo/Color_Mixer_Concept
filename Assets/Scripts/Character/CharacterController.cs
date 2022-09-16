using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
{
    private const string Cheer = "_Cheer";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameEvents.LevelCompleted += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        GameEvents.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        animator.SetTrigger(Cheer);
    }
}
