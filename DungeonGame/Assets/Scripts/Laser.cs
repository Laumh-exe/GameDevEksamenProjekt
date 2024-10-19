using System;
using System.Threading;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 500f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private bool showLine = true;
    private HealthManager healthManager;
    [SerializeField] private float damageCooldownTimerSeconds;

    private RaycastHit rayHit;
    private Ray ray;
    private float timeRemaining;
    private bool isTimerRunning;
    private bool playerTakeDamage;
    private bool isLaserOn = true;
    private float maxPoints = 100f;

    private void Start(){
        healthManager = HealthManager.Instance;
        playerTakeDamage = true;
        isTimerRunning = false;
        timeRemaining = damageCooldownTimerSeconds;
    }

    private void Update(){
        if (isLaserOn) {
            DrawLaser(transform.position, transform.forward);
            CountdownTimer();
            if (rayHit.collider != null) {
                CheckForPlayer();
            }
        }
        else {
            lineRenderer.positionCount = 0;
        }
    }

    private void DrawLaser(Vector3 position, Vector3 direction){
        ray = new Ray(position, direction);

        if (Physics.Raycast(ray, out rayHit, laserDistance)) {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, position);
            lineRenderer.SetPosition(1, rayHit.point);

            HandleRayHit(direction);
        }
        else {
            ResetRayHit();
            DrawLaserForward(position, direction);
        }
    }

    private void HandleRayHit(Vector3 direction){
        if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("WhatIsMirror")) {
            Debug.Log("Mirror hit: " + rayHit.collider.gameObject.name);
            ReflectLaser(rayHit.point, Vector3.Reflect(direction, rayHit.normal));
        }
        else if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("WhatIsIceCube")) {
            rayHit.collider.gameObject.GetComponent<IceCube>().Melt();
        }
    }

    private void ReflectLaser(Vector3 startPosition, Vector3 reflectedDirection){
        if (lineRenderer.positionCount < maxPoints) {
            Ray reflectedRay = new Ray(startPosition, reflectedDirection);

            if (Physics.Raycast(reflectedRay, out RaycastHit hit, laserDistance)) {
                rayHit = hit;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, rayHit.point);

                HandleRayHit(reflectedDirection); // Continue reflection logic if allowed
            }
            else {
                ExtendLaser(startPosition, reflectedDirection); // No reflection, extend laser
            }
        }
        else {
            // If the reflection limit is reached, just extend the laser forward
            ExtendLaser(startPosition, reflectedDirection);
        }
    }

    private void ExtendLaser(Vector3 startPosition, Vector3 direction){
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition + direction * laserDistance);
    }

    private void DrawLaserForward(Vector3 position, Vector3 direction){
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + direction * laserDistance);
    }

    private void ResetRayHit(){
        rayHit = new RaycastHit();
    }

    private void OnDrawGizmos(){
        if (showLine) {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * laserDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(rayHit.point, 0.2f);
        }
    }

    private void CountdownTimer(){
        if (isTimerRunning) {
            if (timeRemaining >= 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                ResetDamageTimer();
            }
        }
    }

    private void CheckForPlayer(){
        if (rayHit.collider.gameObject.CompareTag("Player")) {
            if (playerTakeDamage) {
                healthManager.TakeDamage();
                StartDamageCooldown();
            }
        }
        else {
            ResetDamageTimer();
        }
    }

    private void StartDamageCooldown(){
        timeRemaining = damageCooldownTimerSeconds;
        isTimerRunning = true;
        playerTakeDamage = false;
    }

    private void ResetDamageTimer(){
        timeRemaining = damageCooldownTimerSeconds;
        playerTakeDamage = true;
        isTimerRunning = false;
    }

    public void SetLaserOn(bool value){
        isLaserOn = value;
    }

    private void OnDestroy(){
        ResetRayHit();
    }
}