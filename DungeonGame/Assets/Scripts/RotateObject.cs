    using System;
using UnityEngine;

public class RotateObject : AButtonControlled
{
    [SerializeField] private bool isFloating;
    public override void ButtonPressed()
    {
        isFloating = !isFloating;
    }
    
    public override void ButtonReleased()
    {
        isFloating = !isFloating;
    }
    
    public bool GetIsFloating()
        { return isFloating; }



}
