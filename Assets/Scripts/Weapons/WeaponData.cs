using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public WeaponType _weapon;
    public GameObject _weaponPrefab;
    public float _damage;
    public float _reloadTime = 0;
    public float _range = Mathf.Infinity;
    public float _curAmmo = Mathf.Infinity;
    public float _magSize = Mathf.Infinity;
    public int _wrenchLevel = 0;
    public float _wrenchSpeed = 1f;
    public float _disassembleTime = 0;
    public Sprite _icon;
    public GameObject _trail;
    public GameObject _muzzleFlash;
    public Transform _muzzlePos;
    private Text _curAmmoText;


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
    public GameObject WeaponPrefab
    {
        get
        {
            return _weaponPrefab;
        }
        set
        {
            _weaponPrefab = value;
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
            return _reloadTime; 
        } 
        set 
        { 
            _reloadTime = value; 
        } 
    }
    public float Range
    {
        get
        {
            return _range;
        }
        set
        {
            _range = value;
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
    public int WrenchLevel
    {
        get
        {
            return _wrenchLevel;
        }
        set
        {
            _wrenchLevel = value;
        }
    }
    public float DisassembleTime
    {
        get
        {
            return _disassembleTime;
        }
        set
        {
            _disassembleTime = value;
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
    public GameObject Trail
    {
        get
        {
            return _trail;
        }
        set
        {
            _trail = value;
        }
    }
    public GameObject MuzzleFlash
    {
        get
        {
            return _muzzleFlash;
        }
        set
        {
            _muzzleFlash = value;
        }
    }
    public Transform MuzzlePos
    {
        get
        {
            return _muzzlePos;
        }
        set
        {
            _muzzlePos = value;
        }
    }

    public float WrenchDelay
    {
        get { return _reloadTime; }
        set { _reloadTime = value; }
    }
    public float WrenchSpeed
    {
        get { return _wrenchSpeed; }
        set { _wrenchSpeed = value; }
    }
}

public enum WeaponType
{
    LaserRifle,
    Wrench,
    Hands
}
