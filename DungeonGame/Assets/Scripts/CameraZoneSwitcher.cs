using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZoneSwitcher : MonoBehaviour{
    [SerializeField] private string triggerTag;
    [SerializeField] private CinemachineCamera primaryCamera;
    [SerializeField] private CinemachineCamera[] virtualCameras;

    private void Start(){
        SwitchToCamera(primaryCamera);
    }
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag(triggerTag)) {
            CinemachineCamera targetCamera = other.GetComponentInChildren<CinemachineCamera>();
            SwitchToCamera(targetCamera);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag(triggerTag)){
            SwitchToCamera(primaryCamera);
        }
    }

    private void SwitchToCamera(CinemachineCamera cinemachineCamera){
        foreach(CinemachineCamera camera in virtualCameras) {
            camera.enabled = camera == cinemachineCamera;
        }
    }
}
