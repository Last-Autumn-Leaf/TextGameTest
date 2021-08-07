using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTextEdit : MonoBehaviour
{
    public Text dialogueText;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = text;
    }

    public void changeText(string newText)
    {
        dialogueText.text = newText;
    }

    
}
