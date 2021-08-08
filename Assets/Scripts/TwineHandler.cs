using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Collections;
using UnityEngine.UI;
using Cradle;

public class TwineHandler : MonoBehaviour
{
    public Story story;
    public answerHandler AnswerHandler;
    public List<Button> optionButtons;
    public Text Name;
    


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
        if (story.Vars.ContainsKey("name"))
            Name.text = story.Vars["name"];
    }


    public IEnumerator waiter(int a, Story story)
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(a);
        if (story.State == StoryState.Paused)
            story.Resume();

    }

    void OnOutput(StoryOutput output)
    {
        
        //StartCoroutine(waiter(2));
        if (output is StoryText)
        {
            //storyText.text += output.Text;
            if (story.State == StoryState.Playing)
                story.Pause();
            // add to scroll list method :
            AnswerHandler.addToScroll(output.Text, story.Vars["player"] ? 1 : 0);
            StartCoroutine(waiter(2, story));
            
            
        }
        else if (output is LineBreak)
        {
            // DO NOTHING

        }
        else if (output is StoryLink)
        {
            foreach (Button button in optionButtons)
            {
                if (!button.gameObject.activeSelf)
                {
                    button.gameObject.SetActive(true);
                    button.GetComponentInChildren<Text>().text = output.Text;
                    AnswerHandler.showAnswerBox();
                    break;
                }

            }


        }


        
    }

    public void MakeChoice(int choice)
    {

        story.DoLink(optionButtons[choice].GetComponentInChildren<Text>().text);
        

    }

    void OnPassageEnter(StoryPassage passage)
    {
        //storyText.text = "";
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
        AnswerHandler.hideAnswerBox();
    }


}
