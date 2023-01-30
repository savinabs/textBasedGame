using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    private GameController gameController;

    //dictionary that stores the direction we have to go and the room to which it leads
    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }


    //go over the array of exits in the current room
    //add them to the dictionary that stores direction and Room
    //and pass them over to the game controller to display on the screen
    public void unpackExitsInRoom()
    {
       for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].stringKey, currentRoom.exits[i].roomValue);
            gameController.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }


    //check if the current room has an exit to the direction we pass
    //if the dictionary contains the direction - currentRoom has a new value
    //display the decription of the new room
    //else error message

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitDictionary.ContainsKey(directionNoun))
        {
            currentRoom = exitDictionary[directionNoun];
            gameController.LogStringWithReturn("You head off to the " + directionNoun);
            gameController.DisplayRoomAsText();
        }
        else
        {
            gameController.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    //enter new room - clear the exits of the previous one
    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}
