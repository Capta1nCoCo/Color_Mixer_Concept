using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FullnessManager : MonoBehaviour
{
    private const int blenderCapacity = 5;

    private List<GameObject> fruitsInTheJug = new List<GameObject>();
    private List<Enum> fruitTypesInTheJug = new List<Enum>();

    private void Awake()
    {
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
            fruitTypesInTheJug.Add(fruitObj.GetComponent<Fruit>().GetFruitVariety); 
            fruitObj.SetActive(false);           
        }        
        fruitsInTheJug.Clear();

        print("Fruits in the Jar = " + fruitsInTheJug.Count);
        print("Jar is empty!");

        Blender.Instance.AddToFillAmount(fruitTypesInTheJug.Count);

        foreach (FruitType fruit in fruitTypesInTheJug)
        {
            //print(fruit);
        }
        fruitTypesInTheJug.Clear();
    }
}
