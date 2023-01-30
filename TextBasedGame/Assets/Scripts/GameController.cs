using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //ref to text in scene - show all text on the screen
    public Text displayText;

    //array with actions in the game (go,use,examine etc.)
    public InputAction[] inputActions;

    public InputField inputfield;

    [HideInInspector]
    public RoomNavigation roomNavigation;
    
    //everything in current room that the player can interact with - exits, items etc.
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    [HideInInspector]
    public InteractableItems interactableItems;

    //everything that happens or happend
    List<string> actionLog = new List<string>();

    void Awake()
    {
        interactableItems = GetComponent<InteractableItems>();
        roomNavigation = GetComponent<RoomNavigation>();
    }

    void Start()
    {
        //prepare the room to be displayed - clear previous data for room,
        //unpack the new room and add to actionLog the description of the current room and
        //the descriptions of interactions in the room
        DisplayRoomAsText();
        //display text in scene is changed and shows everything in actionLog
        DisplayLoggedText();
    }

    //any time we want to add a string to the actionLog
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public void DisplayRoomAsText()
    {
        //clear previous room info
        ClearCollectionsForNewRoom();
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        //description of room + description of interactions in room
        string combinedText = roomNavigation.currentRoom.descriptionOfRoom + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
        EndGame();
    }

    //display everything that's in actionLog
    public void DisplayLoggedText()
    {
        //actionLog list converted to array to be able to join - one long string
        string logAsText = string.Join("\n", actionLog.ToArray());
        //how we display everything
        displayText.text = logAsText;
    }

    //pass the exits to the game controller
    void UnpackRoom()
    {
        roomNavigation.unpackExitsInRoom();
        PrepareObjToTakeOrExamine(roomNavigation.currentRoom);
    }

    //prepare for a new room - clear the info about the previouse one
    void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    void PrepareObjToTakeOrExamine(Room currentRoom)
    {
            for (int i = 0; i < currentRoom.interactableObjsInRoom.Length; i++)
            {
            if (currentRoom.interactableObjsInRoom[i] == null) break;
                //objects that aren't in inventory

                string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
                //is the object in inventory

                if (descriptionNotInInventory != null)
                {
                    interactionDescriptionsInRoom.Add(descriptionNotInInventory);
                }

                InteractableObj interactableInRoom = currentRoom.interactableObjsInRoom[i];

                for (int j = 0; j < interactableInRoom.interactions.Length; j++)
                {
                    Interaction interaction = interactableInRoom.interactions[j];
                    if (interaction.inputAction.keyword == "examine")
                    {
                        interactableItems.examineDic.Add(interactableInRoom.noun, interaction.textResponse);
                    }
                    if (interaction.inputAction.keyword == "take")
                    {
                        interactableItems.takeDic.Add(interactableInRoom.noun, interaction.textResponse);
                    }
                }
            }
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string,string> verbDic, string verb, string noun)
    {
        if (verbDic.ContainsKey(noun))
        {
            return verbDic[noun];
        }
        return "You can't " + verb + " " + noun;
    }

    private void EndGame()
    {
        if (roomNavigation.currentRoom.roomName.Equals("happy end") || roomNavigation.currentRoom.roomName.Equals("sad end"))
        {
            this.LogStringWithReturn("Press ESC to exit the game");
            this.DisplayLoggedText();
            inputfield.DeactivateInputField();
            inputfield.enabled= false;
            
        }
    }
    private void Update()
    {
        if (roomNavigation.currentRoom.roomName.Equals("happy end") || roomNavigation.currentRoom.roomName.Equals("sad end"))
        {
            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }
    }

}


