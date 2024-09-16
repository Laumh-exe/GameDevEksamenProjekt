using UnityEngine;

public class BallScript : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    
    void Start()
    {
        moveDirection = Vector3.forward;
    }
    
    void Update()
    {
        MoveBall();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        ChangeDirection();
    }

    private void MoveBall(){
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void ChangeDirection(){
        moveDirection = moveDirection * -1;
    }
}
