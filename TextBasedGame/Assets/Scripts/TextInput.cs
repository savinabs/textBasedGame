using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    GameController gameController;

    public InputField inputField;

    void Awake()
    {
        gameController = GetComponent<GameController>();
        //call AcceptStringInput when the onEndEdit is raised by
        //the event system - whatever we click (UI element) or press Enter
        inputField.onEndEdit.AddListener(AcceptStringInput);        /*if (Input.GetKey(KeyCode.KeypadEnter))

        {
            
        }*/
}
    void AcceptStringInput(string userInput)
    {
        Debug.Log(userInput);
        if (!string.IsNullOrEmpty(userInput))
        {
            userInput = userInput.ToLower();
            //mirror input to the user
            gameController.LogStringWithReturn(userInput);

            char[] delimiterChars = { ' ' };
            //array of strings - user input separated 
            string[] separatedInputWords = userInput.Split(delimiterChars);

            for (int i = 0; i < gameController.inputActions.Length; i++)
            {
                InputAction inputAction = gameController.inputActions[i];

                //is there an action with that keyword
                if (separatedInputWords[0] == inputAction.keyword)
                {
                    inputAction.RespondToInput(gameController, separatedInputWords);
                }

            }

            InputComplete();
        }
    }

    void InputComplete()
    {
        //display text - update actionLog
        gameController.DisplayLoggedText();
        //reactivate inputField
        inputField.ActivateInputField();
        //empty inputField
        inputField.text = null;
    }
}
