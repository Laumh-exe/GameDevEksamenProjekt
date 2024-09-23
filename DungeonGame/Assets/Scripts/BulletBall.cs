using UnityEngine;

public class BulletBall : MonoBehaviour
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

    private void MoveBall(){
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void ChangeDirection(){
        moveDirection = moveDirection * -1;
    }
}
