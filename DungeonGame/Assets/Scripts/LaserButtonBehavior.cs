using System;
using UnityEngine;

public class LaserButtonBehavior : AButtonControlled
{
    [SerializeField] private GameObject laser;
    [SerializeField] private bool isOff;

    private void Start(){
        laser.SetActive(!isOff);
    }

    public override void ButtonPressed(){
        isOff = !isOff;
        laser.SetActive(isOff);
    }
}
