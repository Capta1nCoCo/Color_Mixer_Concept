using System;
using UnityEngine;

public class Blender : MonoBehaviour
{
    [SerializeField] private GameObject juiceInTheJug;
    [SerializeField] private Material juiceMaterial;
    [SerializeField] [Range(0f, 1f)] private float fillAmountPerFruit = 0.15f;

    private float currentFillAmount;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        juiceInTheJug.SetActive(true);
        FillTheJugWithJuice();
        GameEvents.OpenTheLit += OnOpenTheLit;
    }

    private void OnDestroy()
    {
        GameEvents.OpenTheLit -= OnOpenTheLit;
    }

    public void Mix()
    {        
        GameEvents.JugIsEmpty();
        GameEvents.MixColors();
        animator.SetTrigger(Constants.Mix);
    }

    private void OnOpenTheLit()
    {
        animator.SetTrigger(Constants.Open);
    }

    public void AddToFillAmount(int items)
    {
        currentFillAmount += fillAmountPerFruit * items;
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, 1f);
        FillTheJugWithJuice();
    }

    private void FillTheJugWithJuice()
    {
        juiceMaterial.SetFloat(Constants.Fill, currentFillAmount);
    }
}
