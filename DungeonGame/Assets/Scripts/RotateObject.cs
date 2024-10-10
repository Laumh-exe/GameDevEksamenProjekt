using System;
using UnityEngine;

public class RotateObject : AButtonControlled
{
    private bool isFloating = false;
    public override void ButtonPressed()
    {
        isFloating = !isFloating;
    }
    
    public override void ButtonReleased()
    {
    }
    public bool GetIsFloating()
        { return isFloating; }



}
