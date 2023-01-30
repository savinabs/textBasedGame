using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string descriptionOfRoom;

    public string roomName;

    public Exit[] exits;

    public InteractableObj[] interactableObjsInRoom;
}
