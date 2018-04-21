using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyCard : MonoBehaviour {

<<<<<<< HEAD
    #region Monitor Warnings
    [SerializeField]
    private GameObject[] warning;
    #endregion

    #region Shuttling Door
    [SerializeField]
    private GameObject door;
    #endregion

=======
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    #region SwitchActive Class
    [SerializeField]
    private SwitchActive switchAcitve;
    #endregion

    #region Canvas To Riddle
    [SerializeField]
    private Canvas canvas;
    #endregion

<<<<<<< HEAD
    #region Bools
=======
    #region Bool
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    [SerializeField]
    private bool canUseCard = false;

    [SerializeField]
    private bool correctCode = false;

    [SerializeField]
    private bool canAnswerToRiddle = false;
<<<<<<< HEAD

    [SerializeField]
    private bool canDie = false;
=======
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    #endregion

    #region Emergency Lights Class
    [SerializeField]
    private EmergencyLights emergencyLights;
    #endregion

    #region System Methods
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Riddle").GetComponent<Canvas>();
        switchAcitve = GameObject.FindGameObjectWithTag("Computer").GetComponent<SwitchActive>();
        emergencyLights = GameObject.FindGameObjectWithTag("LightState").GetComponent<EmergencyLights>();
    }
    #endregion

    #region Check If item is KeyCard
    public bool CheckCardUse(string nameOfItem)
    {
        if (nameOfItem == "KeyCard")
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Use Card
    public void UseCard()
    {
        canAnswerToRiddle = true;
<<<<<<< HEAD
        warning[0].SetActive(false);
        warning[1].SetActive(true);
        warning[2].SetActive(true);
        warning[3].SetActive(true);
        door.GetComponent<DoorIsOpenedAndClosed>().SetDoorClosed();
        canDie = true;
=======
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    }
    #endregion

    #region NormalMode Active
    public void NormalModeActivate()
    {
        if(emergencyLights.GetLightState()==LightsState.Emergency)
            switchAcitve.PlayPowerOnSound();
        emergencyLights.SetLightState(LightsState.Normal);
<<<<<<< HEAD
        warning[1].SetActive(false);
        warning[2].SetActive(false);
        warning[3].SetActive(false);
        door.GetComponent<DoorIsOpenedAndClosed>().SetDoorShutdownFalse();
        canDie = false;
=======
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    }
    #endregion

    #region Setters
    public void SetUseCard(bool canUseCard)
    {
        this.canUseCard = canUseCard;
    }

    public void SetCorrectCode(bool correctCode)
    {
       this.correctCode = correctCode;
    }

    public void SetCanAnswerToRiddle(bool canAnswerToRiddle)
    {
        this.canAnswerToRiddle = canAnswerToRiddle;
    }
    #endregion

    #region Check The Answer
    public void CheckAndSetRiddleAnswer(string riddleString)
    {
        if(riddleString == "1623")
        {
            correctCode = true;
        }
        else
        {
            Debug.Log("Wrong Code");
        }
    }
    #endregion

    #region Getters
    public bool GetUseCard()
    {
        return canUseCard;
    }

    public bool GetCorrectCode()
    {
        return correctCode;
    }

    public bool GetCanAnswerToRiddle()
    {
        return canAnswerToRiddle;
    }
<<<<<<< HEAD

    public bool GetCanDie()
    {
        return canDie;
    }
=======
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    #endregion
}
