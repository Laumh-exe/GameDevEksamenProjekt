using UnityEngine;

public class LaserButtonBehavior : AButtonControlled
{
    [SerializeField] private GameObject laser;
    bool isOn = false;
    public override void ButtonPressed(){
        laser.SetActive(isOn);
        isOn = !isOn;
    }
}
