using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactable Object")]
public class InteractableObj : ScriptableObject
{
    public string noun = "name";

    [TextArea]
    public string description = "description in room";

    public Interaction[] interactions;
}
