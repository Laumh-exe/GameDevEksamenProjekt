using UnityEngine;

public class LaserButtonBehavior : AButtonControlled
{
    [SerializeField] private GameObject laser;
    public override void ButtonPressed(){
        Debug.Log("Button Pressed");
        laser.SetActive(false);
    }

    public override void ButtonReleased(){
        Debug.Log("Button Released");
        laser.SetActive(true);
    }
}
