using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{
    public RotateObject rotateObject;

    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPos;
    

    void Start()
    {

        startPos = transform.position;
    }

    void Update()
    {
        if (!rotateObject.GetIsFloating())
        {
            Vector3 newPos = startPos;
            newPos.y += Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = newPos;
        }
    }

  
}
