using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private bool isFloating = false;
    public void ButtonPressed()
    {
        isFloating = !isFloating;
    }

    // Kaldt n�r knappen slippes (kan v�re tom)
    public void ButtonReleased()
    {
    }
    public bool GetIsFloating()
        { return isFloating; }



}
