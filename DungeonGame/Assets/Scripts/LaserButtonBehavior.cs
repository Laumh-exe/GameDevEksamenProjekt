using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserButtonBehavior : AButtonControlled
{
    [SerializeField] private GameObject laser;
    [SerializeField] private bool isOn = true;

    private void Start(){
        laser.SetActive(isOn);
    }

    public override void ButtonPressed(){
        isOn = !isOn;
        laser.SetActive(isOn);
    }
}
