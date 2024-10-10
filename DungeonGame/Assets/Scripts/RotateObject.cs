using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private bool isFloating = false;
    public void ButtonPressed()
    {
        isFloating = !isFloating;
    }

    // Kaldt når knappen slippes (kan være tom)
    public void ButtonReleased()
    {
    }
    public bool GetIsFloating()
        { return isFloating; }



}
