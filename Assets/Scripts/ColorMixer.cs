using UnityEngine;
using UnityEngine.UI;

public class ColorMixer : MonoBehaviour
{
    Color lerpedColor = Color.white;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //Operator Test
    private void Start()
    {
        lerpedColor = Color.green * Color.yellow;
        image.color = lerpedColor;
    }

    //Lerp Test
    /*void Update()
    {
        lerpedColor = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time, 1));
        image.color = lerpedColor;
    }*/
}
