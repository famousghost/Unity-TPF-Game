using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleAnswer : MonoBehaviour {

    [SerializeField]
    private char[] answerToRiddle;

    [SerializeField]
    private bool canOpenRiddle = false;

    [SerializeField]
    private EmergencyCard emergencyCard;

    [SerializeField]
    private int counter = 0;

    void Start()
    {
        answerToRiddle = new char[4];
        emergencyCard = GameObject.Find("UsableItems").GetComponent<EmergencyCard>();
    }

    void Update()
    {
        SetAnswer();
    }

    public void RiddleEnable()
    {
        canOpenRiddle = true;
    }

    public void RiddleDisable()
    {
        canOpenRiddle = false;
    }

    public bool SetAnswer()
    {
        Debug.Log(answerToRiddle);
        if (answerToRiddle[0] == '1' && answerToRiddle[1] == '2' && answerToRiddle[2] == '6' && answerToRiddle[3] == '3' )
        {
            canOpenRiddle = false;
            emergencyCard.SetCorrectCode(true);
            return true;
        }
        else
        {
            if (counter > 3)
            {
                counter = 0;
                return false;
            }
        }
        return false;
    }

    public void AddToAnswer(char answerToRiddle)
    {
        this.answerToRiddle[counter] = answerToRiddle;
        Debug.Log(answerToRiddle);
    }

    public void SetCorrectAnswer()
    {
        emergencyCard.SetCorrectCode(true);
    }

    public bool GetCanOpenRiddle()
    {
        return canOpenRiddle;
    }

    public void AddCounter(int i)
    {
        counter += i;
    }
}
