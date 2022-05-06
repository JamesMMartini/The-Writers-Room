using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAnimatic : MonoBehaviour
{

    private int currScene;

    void Start()
    {
        currScene = 0;
    }

    // function that handles setting the new image, the new drawing, and the new scene number on scene load or on button press

    // create function that's called when we set a new image / caption
    // function should check the number of the scene within the larger animatic, if scene 1 disable back button and if scene 4 (last) disable the next button

}
