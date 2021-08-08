using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class answerHandler : MonoBehaviour
{
    const int  DATE = 2;
    const int BLUE = 1;
    const int GREY = 0;


    public  GameObject blue;
    public GameObject grey;
    public GameObject date;
    public GameObject scroll;
    public GameObject answerBox;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    // sender 0== grey | 1== blue | 2==date
    public void addToScroll(string message, int sender)
    {

        GameObject messageBox;
        switch (sender)
        {
            case 0:
                messageBox = Instantiate(grey) as GameObject;
                BubbleTextEdit a = messageBox.GetComponent<BubbleTextEdit>();
                a.text = message;
                break;
            case 2:
                messageBox = Instantiate(date) as GameObject;
                Text b = messageBox.GetComponent<Text>();
                b.text = message;
                break;
            default:
                messageBox = Instantiate(blue) as GameObject;
                BubbleTextEdit c = messageBox.GetComponent<BubbleTextEdit>();
                c.text = message;
                break;
        }
        
        //scroll = GameObject.Find("CardScroll");
        if (scroll != null)
        {
            //ScrollViewGameObject container object
            messageBox.transform.SetParent(scroll.transform, false);
            // scroll to the end
            Canvas.ForceUpdateCanvases();
            scroll.GetComponentInParent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
        }



    }

    public void hideAnswerBox()
    {
        Animator ani = answerBox.GetComponent<Animator>();
        ani.SetBool("isHidden", true);
    }

    public void showAnswerBox()
    {
        Animator ani = answerBox.GetComponent<Animator>();
        ani.SetBool("isHidden", false);
    }

}
