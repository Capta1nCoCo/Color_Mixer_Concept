using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject sceneBubble;
    [SerializeField] private GameObject startProps;
    [SerializeField] private GameObject finishProps;
    [SerializeField] private float delayInSeconds = 3f;

    private int levelIndexValue;

    private void Awake()
    {
        levelIndexValue = PlayerPrefs.GetInt(Constants.CurrentLevelIndex);
        ActivateLevelObjects(false);
        GameEvents.LevelStarted += OnLevelStarted;
        GameEvents.LevelCompleted += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        GameEvents.LevelStarted -= OnLevelStarted;
        GameEvents.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelStarted()
    {
        ActivateLevelObjects(true);
    }    

    private void OnLevelCompleted()
    {
        levelIndexValue++;
        PlayerPrefs.SetInt(Constants.CurrentLevelIndex, levelIndexValue);
        StartLevelCompletedSequence();
    }

    private void ActivateLevelObjects(bool isActive)
    {
        canvasCamera.gameObject.SetActive(isActive);
        mainCanvas.gameObject.SetActive(!isActive);
        sceneBubble.SetActive(!isActive);
        startProps.SetActive(!isActive);        
    }

    private void StartLevelCompletedSequence()
    {
        var isActive = false;
        canvasCamera.gameObject.SetActive(isActive);
        mainCanvas.gameObject.SetActive(!isActive);
        finishProps.SetActive(!isActive);
        StartCoroutine(LoadMainSceneWithDelay());
    }

    private IEnumerator LoadMainSceneWithDelay()
    {
        var mainSceneIndex = 0;
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(mainSceneIndex);
    }    
}
