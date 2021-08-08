using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Collections;
using UnityEngine.UI;
using Cradle;

public class TwineTest : MonoBehaviour
{
    public Story story;
    public List<Button> optionButtons;
    public Text storyText;
    public Text hapiness;
    public Text videoClipNameText;


    // Start is called before the first frame update
    void Start()
    {
        story.OnOutput += OnOutput;
        story.OnPassageEnter += OnPassageEnter;

        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }

        story.Begin();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOutput(StoryOutput output)
    {
        if (output is StoryText)
        {
            storyText.text += output.Text;
        }
        else if (output is LineBreak)
        {
            storyText.text += "\n";

        }
        else if (output is StoryLink)
        {
            foreach(Button button in optionButtons)
            {
                if (!button.gameObject.activeSelf)
                {
                    button.gameObject.SetActive(true);
                    button.GetComponentInChildren<Text>().text = output.Text;
                    break;
                }
                
            }


        }

        if (story.Vars.ContainsKey("video") && story.Vars["video"] != "")
        {
            videoClipNameText.text = story.Vars["video"];
        }
    }

    public void MakeChoice(int choice)
    {
        
        story.DoLink(optionButtons[0].GetComponentInChildren<Text>().text);
        UpdateHUD();

    }

    void OnPassageEnter ( StoryPassage passage)
    {
        storyText.text = "";
        foreach(Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    void UpdateHUD()
    {
        int bonheur = story.Vars["happiness"];
        hapiness.text = bonheur.ToString();
    }

    public void PopAXanax()
    {
        int happ = story.Vars["happiness"];
        story.Vars["happiness"] += 1;
        storyText.text = storyText.text + "\n\nYou pop a Xanax.";

        UpdateHUD();
    }


}
