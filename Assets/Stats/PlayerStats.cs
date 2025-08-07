using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Scriptable Objects/EntityStats")]
[Serializable]
public class PlayerStats : ScriptableObject
{
    //[Header("Level")]
    //public int level;
    //public int baseXP;

    [Header("Ingame Stats")]
    public float HP;
    public float WlkSPD;
    public float RunSPD;
    public float StnDur;


    //[Header("Base Stats")]
    //public int base_HP;
    //public int base_Armor;
    //public int base_Attack;
    //public float base_CR;
    //public float base_CDmg;

    //[Header("Attributes")]
    //[Range(1, 99)] public int STR;
    //[Range(1, 99)] public int AGI;
    //[Range(1, 99)] public int VIT;
    //[Range(1, 99)] public int DEX;
    //[Range(1, 99)] public int LUK;
}
