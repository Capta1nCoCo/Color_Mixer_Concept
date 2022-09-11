using UnityEngine;

public class Wobble : MonoBehaviour
{
    [SerializeField] private float MaxWobble = 0.03f;
    [SerializeField] private float WobbleSpeed = 1f;
    [SerializeField] private float Recovery = 1f;
    
    private float wobbleAmountX;
    private float wobbleAmountZ;
    private float wobbleAmountToAddX;
    private float wobbleAmountToAddZ;
    private float pulse;
    private float time = 0.5f;

    private Renderer rend;
    private Vector3 lastPos;
    private Vector3 velocity;
    private Vector3 lastRot;
    private Vector3 angularVelocity;

    private bool isWobbleble;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {        
        if (isWobbleble)
        {
            time += Time.deltaTime;
            DecreaseWobbleOverTime();
            MakeASinWaveOfTheDecreasingWobble();
            SendWobbleAmountToTheShader();
            AdjustVelocity();
            AddClampedVelocityToWobble();
            KeepLastPosition();
        }        
    }
    
    private void DecreaseWobbleOverTime()
    {
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));
    }

    private void MakeASinWaveOfTheDecreasingWobble()
    {
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);
    }

    private void SendWobbleAmountToTheShader()
    {
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);
    }

    private void AdjustVelocity()
    {
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;
    }

    private void AddClampedVelocityToWobble()
    {
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
    }

    private void KeepLastPosition()
    {
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }
}
