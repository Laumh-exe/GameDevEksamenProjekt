using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZoneSwitcher : MonoBehaviour{
    [SerializeField] private string triggerTag;
    [SerializeField] private CinemachineCamera primaryCamera;
    [SerializeField] private CinemachineCamera[] virtualCameras;
    [SerializeField] private GameObject[] gameObjectsToHide;
    
    public static Action<bool> OnCamChange;

    private void Start(){
        SwitchToCamera(primaryCamera);
    }
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag(triggerTag)) {
            Debug.Log(other.gameObject.name);
            CameraZoneCollider targetCamera = other.GetComponentInChildren<CameraZoneCollider>(); 
            SwitchToCamera(targetCamera.GetVirtualCamera());
            HideGameObjects();
            OnCamChange?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag(triggerTag)){
            SwitchToCamera(primaryCamera);
            ShowGameObjects();
            OnCamChange?.Invoke(false);
        }
    }

    private void SwitchToCamera(CinemachineCamera cinemachineCamera){
        foreach(CinemachineCamera camera in virtualCameras) {
            camera.enabled = camera == cinemachineCamera;
        }
    }
    
    private void HideGameObjects(){
        foreach(GameObject go in gameObjectsToHide) {
            go.GetComponent<Renderer>().enabled = false;
        }
    }
    
    private void ShowGameObjects(){
        foreach(GameObject go in gameObjectsToHide) {
            go.GetComponent<Renderer>().enabled = true;
        }
    }
}
