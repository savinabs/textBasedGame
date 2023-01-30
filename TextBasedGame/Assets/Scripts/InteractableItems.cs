using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//item manager

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObj> usableItemList;

    public Dictionary<string, string> examineDic = new Dictionary<string, string>();
    public Dictionary<string, string> takeDic = new Dictionary<string, string>();

    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    Dictionary<string, ActionResponse> useDic = new Dictionary<string, ActionResponse>();

    List<string> nounsInInventory = new List<string>();

    GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    //show description of an object that's not in the inventory - it's in the room
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        //check the i item in the array of interactable objects in current room
        InteractableObj interactableObjInRoom = currentRoom.interactableObjsInRoom[i];
        //if the interactable object is not in the inventory
        //then add it to the list of objects that are in the room and return its description
        if (!nounsInInventory.Contains(interactableObjInRoom.noun))
        {
            nounsInRoom.Add(interactableObjInRoom.noun);
            return interactableObjInRoom.description;
        }else return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            InteractableObj interactableObjInInventory = GetInteractableObjFromUsableList(noun);
            if(interactableObjInInventory == null) continue;

            for (int j = 0; j < interactableObjInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjInInventory.interactions[j];
                if(interaction.actionResponse == null) continue;

                if (!useDic.ContainsKey(noun))
                {
                    useDic.Add(noun, interaction.actionResponse);
                }
            }
            
        }
    }

    InteractableObj GetInteractableObjFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            } 
        }
        return null;
    }

    public void DisplayInventory()
    {
        gameController.LogStringWithReturn("You look in your pocket. Inside you have: ");
        if(nounsInInventory.Count == 0) gameController.LogStringWithReturn("Nothing. You're pocket is empty");
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            gameController.LogStringWithReturn(nounsInInventory[i]);
        }
    }

    public void ClearCollections()
    {
        examineDic.Clear();
        takeDic.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string,string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDic;
        }
        else
        {
            gameController.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void Use(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];
        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDic.ContainsKey(nounToUse))
            {
                bool actionResult = useDic[nounToUse].DoActionResponse(gameController);
                if (!actionResult)
                {
                    gameController.LogStringWithReturn("Hmmm. Nothing happens.");
                }
            }
            else
            {
                gameController.LogStringWithReturn("You cannot use the " + nounToUse);
            }
        }
        else
        {
            gameController.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use.");
        }
       }
      }
