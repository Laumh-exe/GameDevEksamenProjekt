using UnityEngine;

public abstract class AButtonControlled: MonoBehaviour {
    public virtual void ButtonPressed(){
        Debug.Log("Nothing assigned");
    }

    public virtual void ButtonReleased(){
        Debug.Log("Nothing assigned");
    }
}
