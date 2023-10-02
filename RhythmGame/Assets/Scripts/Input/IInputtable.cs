using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputtable
{
    public void PressStarted();

    public void PressPerformed();

    public void PressCanceled();
}
