using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FullnessManager : MonoBehaviour
{
    private const int blenderCapacity = 5;

    private List<GameObject> fruitsInTheJug = new List<GameObject>();

    private ColorMixer colorMixer;

    private void Awake()
    {
        colorMixer = GetComponentInParent<ColorMixer>();
        GameEvents.JugIsEmpty += OnJugIsEmpty;
    }

    private void OnDestroy()
    {
        GameEvents.JugIsEmpty -= OnJugIsEmpty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fruitsInTheJug.Contains(other.gameObject)) { return; }

        fruitsInTheJug.Add(other.gameObject);      
        print("Fruit Delivered, fullness: " + fruitsInTheJug.Count + "/" + blenderCapacity);
        
        if (fruitsInTheJug.Count == blenderCapacity)
        {
            GameEvents.JugIsFull();
            print("Jar is full!");
        }
    }

    private void OnJugIsEmpty()
    {        
        foreach (GameObject fruit in fruitsInTheJug)
        {
            var fruitObj = fruit.GetComponentInParent<Rigidbody>().gameObject;
            colorMixer.AddFruitTypeToMix(fruitObj.GetComponent<Fruit>().GetFruitType); 
            fruitObj.SetActive(false);           
        }
        Blender.Instance.AddToFillAmount(fruitsInTheJug.Count);
        fruitsInTheJug.Clear();

        print("Jar is empty!");      
    }
}
