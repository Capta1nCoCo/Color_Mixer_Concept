using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FullnessManager : MonoBehaviour
{
    private const int blenderCapacity = 5;

    private List<GameObject> fruitsInTheJug = new List<GameObject>();

    private ColorMixer colorMixer;
    private Blender blender;

    private void Awake()
    {
        colorMixer = GetComponentInParent<ColorMixer>();
        blender = GetComponentInParent<Blender>();
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
        
        if (fruitsInTheJug.Count == blenderCapacity)
        {
            GameEvents.JugIsFull();
        }
    }

    private void OnJugIsEmpty()
    {        
        foreach (GameObject fruit in fruitsInTheJug)
        {
            var fruitObj = fruit.GetComponentInParent<Rigidbody>().gameObject;
            colorMixer.AddFruitToMix(fruitObj.GetComponent<Fruit>());
            fruitObj.SetActive(false);           
        }
        blender.AddToFillAmount(fruitsInTheJug.Count);
        fruitsInTheJug.Clear();      
    }
}
