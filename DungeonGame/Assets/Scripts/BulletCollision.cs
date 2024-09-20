using UnityEngine;

public class BulletCollision : MonoBehaviour{
    [SerializeField] private BulletBall bullet;

    private GameObject currentCollision;
    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(" collided with " + collision.gameObject.name);
        if (currentCollision==null || currentCollision.gameObject != collision.gameObject) {
            Debug.Log("Changing Direction");
            bullet.ChangeDirection();

        }
        currentCollision = collision.gameObject;
    }
}
