using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    [SerializeField]
    private string textOfNote;

    public string GetTextOfNote()
    {
        return textOfNote;
    }
}
