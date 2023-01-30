using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all inputs
public abstract class InputAction:ScriptableObject
{
    //input that's it gonna respond to
    public string keyword;
    public abstract void RespondToInput(GameController gameController, string[] separatedInputWords);
}
