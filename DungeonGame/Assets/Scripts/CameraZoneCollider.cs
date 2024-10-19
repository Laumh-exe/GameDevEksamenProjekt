using Unity.Cinemachine;
using UnityEngine;

public class CameraZoneCollider : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    
    public CinemachineCamera GetVirtualCamera(){
        return virtualCamera;
    }
}
