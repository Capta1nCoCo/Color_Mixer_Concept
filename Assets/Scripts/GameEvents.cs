using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action JugIsFull;
    public static Action JugIsEmpty;
    public static Action MixColors;
    public static Action OpenTheLit;
    public static Action LevelStarted;
    public static Action LevelCompleted;
    public static Action<string> ChangeCurrentComplinceValue;
}
