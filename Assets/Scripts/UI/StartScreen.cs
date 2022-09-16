using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                gameObject.SetActive(false);
                GameEvents.LevelStarted();
            }
        }
    }
}
