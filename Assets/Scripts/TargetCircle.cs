using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;

    public float minSpeed = 50f;
    public float maxSpeed = 200f;

    public float minChangeTime = 1f;
    public float maxChangeTime = 3f;

    private float currentSpeed;
    private float timer;

    void Start()
    {
        SetRandomRotation();

    }

    void Update()
    {
        transform.Rotate(0, 0, currentSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomRotation();
        }

    }

    void SetRandomRotation()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        if (Random.value < 0.5f)
            currentSpeed *= -1f;
            
        timer = Random.Range(minChangeTime, maxChangeTime);
    }
}
