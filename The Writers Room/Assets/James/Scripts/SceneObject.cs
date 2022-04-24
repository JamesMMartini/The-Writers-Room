using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneObject", menuName = "MadLibs/SceneObject")]
public class SceneObject : ScriptableObject
{
    [SerializeField][TextArea(5, 20)] string[] chunks;
    [SerializeField][TextArea(5, 20)] string[] prompts;
    [SerializeField][TextArea(5, 20)] string[] responses;

    public string[] Chunks { get { return chunks; } set { chunks = value; } }

    public string[] Prompts { get { return prompts; } set { prompts = value; } }

    public string[] Responses { get { return responses; } set { responses = value; } }
}
