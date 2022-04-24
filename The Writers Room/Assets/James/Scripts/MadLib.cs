using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadLib
{
    public string[] chunks;
    public string[] prompts;
    public string[] responses;

    public MadLib(SceneObject activeScene)
    {
        chunks = activeScene.Chunks;
        prompts = activeScene.Prompts;
        responses = activeScene.Responses;
    }
}
