using UnityEngine;

public class Rotate : MonoBehaviour
{
    public RotateObject rotateObject;

    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    

    void Update()
    {
        if (!rotateObject.GetIsFloating())
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }

  
}
