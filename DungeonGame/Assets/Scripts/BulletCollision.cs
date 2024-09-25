using UnityEngine;

public class BulletCollision : MonoBehaviour {
    [SerializeField] private BulletBall bullet;

    private GameObject currentCollision;
    void OnTriggerEnter(Collider other)
    {
        if (currentCollision==null || currentCollision.gameObject != other.gameObject) {
            if(other.gameObject.CompareTag("Wall")) {
                bullet.ChangeDirection();
            }

        }
        currentCollision = other.gameObject;
    }
}
