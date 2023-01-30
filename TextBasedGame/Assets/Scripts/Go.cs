using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputAction/Go")]
public class Go : InputAction
{
    //check for direction, try to go to the room in this direction
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)
    {
        if(separatedInputWords.Length > 1) gameController.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
        else gameController.LogStringWithReturn("Type direction"); 
    }
}
