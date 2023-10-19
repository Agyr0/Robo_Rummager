using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioGroup
{
    public AudioType type;
    [ArrayElementTitle("name")]
    public List<AudioController> myControllers = new List<AudioController>();


}


public enum AudioType
{
    Gun,

    Wrench_Metal,
    Wrench_Whoosh,

    Hands,

    Printer_PowerOn,
    Printer_Ding,
    Printer_Hum,
    Printer_Switch,
    Printer_Idle,

    Hologram_Hum,
    Hologram_PowerOn,
    Hologram_Idle,

    DropOffBox,

    SellerBin_Yes,
    SellerBin_No,

    Positive_SFX,


    RogueBot_Alert,
    RogueBot_Death,
    RogueBot_Idle,

    FootSteps_Dirt,
    FootSteps_Gravel,
    FootSteps_Metal,


}