using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Take")]
public class Take : InputAction
{
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)
    {
        Dictionary<string, string> takeDic = gameController.interactableItems.Take(separatedInputWords);
        if(takeDic != null)
        {
            gameController.LogStringWithReturn(gameController.TestVerbDictionaryWithNoun(takeDic, separatedInputWords[0], separatedInputWords[1]));
        }
    }
}
