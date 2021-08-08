using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class topBar : MonoBehaviour

{
    public Image imageComp;
    public Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        imageComp.sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
