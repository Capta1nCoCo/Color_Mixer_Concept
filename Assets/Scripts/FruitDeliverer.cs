using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FruitDeliverer : MonoBehaviour
{
    [SerializeField] private GameObject fruitPrefab;    
    [SerializeField] private int poolSize = 5;
    
    private float delayInSeconds = 0.5f;

    private Queue<GameObject> fruitsPool = new Queue<GameObject>();

    private bool isDeliverable = true;
    private bool isSpamProtected;

    private Transform dropPoint;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        dropPoint = GameObject.FindGameObjectWithTag(Constants.FruitDropPoint).transform;
        button.onClick.AddListener(() => DropFruit());
        SpawnFruits();

        GameEvents.JugIsFull += OnJugIsFull;
        GameEvents.JugIsEmpty += OnJugIsEmpty;
    }

    private void OnDestroy()
    {
        GameEvents.JugIsFull -= OnJugIsFull;
        GameEvents.JugIsEmpty -= OnJugIsEmpty;
    }    

    public void DropFruit()
    {
        if (isDeliverable)
        {
            if (!isSpamProtected)
            {
                var fruit = GetFruit();
                fruit.transform.position = dropPoint.position;
                GameEvents.OpenTheLit();
                StartCoroutine(ProtectFromSpam());
            }
        }        
    }

    private GameObject GetFruit()
    {
        var fruit = fruitsPool.Dequeue();
        fruit.SetActive(true);
        fruitsPool.Enqueue(fruit);
        return fruit;
    }

    private IEnumerator ProtectFromSpam()
    {
        isSpamProtected = true;
        yield return new WaitForSeconds(delayInSeconds);
        isSpamProtected = false;
    }

    private void SpawnFruits()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var fruit = Instantiate(fruitPrefab, dropPoint);
            fruitsPool.Enqueue(fruit);
            fruit.SetActive(false);
        }
    }
    
    private void OnJugIsFull()
    {
        isDeliverable = false;
    }

    private void OnJugIsEmpty()
    {
        isDeliverable = true;
    }
}
