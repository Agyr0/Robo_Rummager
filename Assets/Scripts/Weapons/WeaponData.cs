using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public WeaponType _weapon;
    public float _damage;
    public float _reloadTimeDisassembleTime = 0;
    public float _curAmmo = Mathf.Infinity;
    public float _magSize = Mathf.Infinity;
    public Sprite _icon;



    public WeaponType Weapon 
    { 
        get 
        { 
            return _weapon; 
        } 
        set 
        { 
            _weapon = value; 
        } 
    }
    public float Damage 
    {  
        get 
        { 
            return _damage; 
        } 
        set 
        { 
            _damage = value; 
        } 
    }
    public float ReloadTime
    {  
        get 
        { 
            return _reloadTimeDisassembleTime; 
        } 
        set 
        { 
            _reloadTimeDisassembleTime = value; 
        } 
    }
    public float DisassembleTime
    {
        get
        {
            return _reloadTimeDisassembleTime;
        }
        set
        {
            _reloadTimeDisassembleTime = value;
        }
    }
    public float CurAmmo 
    {  
        get 
        { 
            return _curAmmo; 
        } 
        set 
        { 
            _curAmmo = value; 
        } 
    }
    public float MagSize 
    {  
        get 
        { 
            return _magSize; 
        } 
        set 
        { 
            _magSize = value; 
        } 
    }
    public Sprite Icon 
    {  
        get 
        { 
            return _icon; 
        } 
        set 
        { 
            _icon = value; 
        } 
    }

}

public enum WeaponType
{
    LaserRifle,
    Wrench,
    Hands
}
