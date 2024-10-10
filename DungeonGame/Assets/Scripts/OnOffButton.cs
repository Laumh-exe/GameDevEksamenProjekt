using System.Collections.Generic;
using UnityEngine;

public class OnOffButton : MonoBehaviour{
    [SerializeField] private List<AButtonControlled> buttonControlledObjects;
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private GameObject buttonPressedVisual;

    bool IsOn = false;

    private void Start(){
        buttonPressedVisual.SetActive(IsOn);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")) {
            ToggleButton();
        }
    }

    private void ToggleButton(){
        IsOn = !IsOn;
        buttonPressedVisual.SetActive(IsOn);
        SoundManager.Instance.PlayButtonPressedSound();
        foreach (AButtonControlled btncontrolled in buttonControlledObjects) {
            btncontrolled.ButtonPressed();
        }
    }
}