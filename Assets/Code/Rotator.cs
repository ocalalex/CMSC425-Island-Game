using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationPeriod = 5.0f;

    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = 360.0f / rotationPeriod;
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void ChangeRotationPeriod(float newRotationPeriod)
    {
        rotationPeriod = newRotationPeriod;
        rotationSpeed = 360.0f / rotationPeriod;
    }
}
