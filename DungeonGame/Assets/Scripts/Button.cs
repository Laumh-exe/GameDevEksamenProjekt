using System;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour{
    [SerializeField] private AButtonControlled[] buttonControlledObjects;
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private GameObject buttonPressedVisual;

    bool isPressed = false;

    private void Start(){
        buttonPressedVisual.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
        if (!isPressed) {
            buttonPressedVisual.SetActive(true);
            SoundManager.instance.PlayButtonPressedSound();
            foreach (AButtonControlled btncontrolled in buttonControlledObjects) {
                btncontrolled.ButtonPressed();
            }
        }
        isPressed = !isPressed;
    }

    private void OnTriggerExit(Collider other){
        if (isPressed) {
            buttonPressedVisual.SetActive(false);
            foreach (AButtonControlled btncontrolled in buttonControlledObjects) {
                btncontrolled.ButtonReleased();
            }
        }
        isPressed = !isPressed;
    }
}