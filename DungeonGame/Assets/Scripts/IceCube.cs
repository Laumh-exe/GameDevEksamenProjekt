using UnityEngine;

public class IceCube : MonoBehaviour
{
    private float meltSpeed = .5f;
    
    private void Update(){
        if(transform.localScale.y < 0.3){
            Destroy(gameObject);
        }
    }

    public void Melt(){
        Vector3 scale = transform.localScale;
        float previousYScale = scale.y;
        
        scale.y -= meltSpeed * Time.deltaTime;
        transform.localScale = scale;
        
        float yOffset = (previousYScale - scale.y) / 2;
        transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);

    }
}
