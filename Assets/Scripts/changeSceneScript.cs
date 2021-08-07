using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class changeSceneScript : MonoBehaviour
{
    public void DiscussionScene()
    {
        SceneManager.LoadScene("Discussions");
    }
    public void conversationScene()
    {
        SceneManager.LoadScene("conversation");
    }
    


}
