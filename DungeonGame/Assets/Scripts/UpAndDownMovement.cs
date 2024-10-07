using UnityEngine;

public class FloatObject : MonoBehaviour
{
    // Variabler for bevægelse
    public float amplitude = 0.5f;  // Hvor meget objektet bevæger sig op og ned
    public float frequency = 1f;    // Hvor hurtigt objektet bevæger sig op og ned

    // Privat variabel til at huske objektets startposition
    private Vector3 startPos;

    void Start()
    {
        // Gem objektets startposition
        startPos = transform.position;
    }

    void Update()
    {
        // Beregn den nye position baseret på en sinusbølge
        Vector3 newPos = startPos;
        newPos.y += Mathf.Sin(Time.time * frequency) * amplitude;

        // Opdater objektets position
        transform.position = newPos;
    }
}
