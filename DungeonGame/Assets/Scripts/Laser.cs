using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class Laser : MonoBehaviour{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 50f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private bool showLine = true;
    [SerializeField] private UnityEvent OnHitTarget;

    private Transform mirror;
    private RaycastHit rayHit;
    private Ray ray;

     private void Update() {
        DrawLaser(transform.position, transform.forward);
    }

    private void DrawLaser(Vector3 position, Vector3 direction) {
        ray = new Ray(position, direction);

        if (Physics.Raycast(ray, out rayHit, laserDistance)) {
            lineRenderer.positionCount = 2;
            
            lineRenderer.SetPosition(0, position);
            lineRenderer.SetPosition(1, rayHit.point);

            if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsMirror")) {
                Vector3 reflectedDirection = Vector3.ReflecGIt(direction, rayHit.normal);
                ReflectLaser(rayHit.point, reflectedDirection); // Reflect from the hit point
            }

            if (rayHit.collider.CompareTag("Player")) {
                Debug.Log("HIT PLAYER :(... Check once every 3 seconds to slowly drain life.");
            }
        }
        else {
            // If no hit, just draw the laser forward
            lineRenderer.SetPosition(0, position);
            lineRenderer.SetPosition(1, position + direction * laserDistance);
        }
    }

    private void ReflectLaser(Vector3 startPosition, Vector3 reflectedDirection) {
        Ray reflectedRay = new Ray(startPosition, reflectedDirection);

        if (Physics.Raycast(reflectedRay, out RaycastHit hit, laserDistance)) {
            lineRenderer.positionCount += 1; // Add another point to the line for the reflection

            // Add reflected point
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

            // If the new hit is another mirror, keep reflecting
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsMirror")) {
                Vector3 newReflectedDirection = Vector3.Reflect(reflectedDirection, hit.normal);
                ReflectLaser(hit.point, newReflectedDirection);
            }
        }
        else {
            // If no further hit, extend the laser in the reflected direction
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition + reflectedDirection * laserDistance);
        }
    }

    private void OnDrawGizmos(){
        //Gizmos.DrawCube(transform.position, Vector3.one * 0.1f);
        if (showLine) {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * laserDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(rayHit.point, 0.2f);
        }
    }
}
