using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse
{
    public Room roomToChangeTo;
    public override bool DoActionResponse(GameController gameController)
    {
        if(gameController.roomNavigation.currentRoom.roomName == requiredString)
        {
            gameController.roomNavigation.currentRoom = roomToChangeTo;
            gameController.DisplayRoomAsText();
            return true;
        }
        return false;
    }

}
