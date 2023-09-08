using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    [SerializeField]
    private Transform playerHand;
    public Transform _muzzlePos;

    [SerializeField]
    private WeaponData _curWeapon;
    private GameObject _weaponPrefab;
    public WeaponData[] _availableWeapons;
    private int _weaponIndex = 0;
    private bool canShoot = true;

    private void Start()
    {
        gameManager = GameManager.Instance;
        _curWeapon = _availableWeapons[0];
        inputManager = InputManager.Instance;


        EventBus.Publish(EventType.DISPLAY_WEAPON);
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Subscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Subscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Unsubscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Unsubscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);

    }

    private void Update()
    {
        if (canShoot)
            HandleShoot();
        HandleWeaponSwitch();
    }
    private void HandleShoot()
    {
        inputManager.playerControls.Player.Shoot.performed += _ =>
        {
            EventBus.Publish(EventType.PLAYER_SHOOT);
        };
    }
    private void LateUpdate()
    {
        PointWeapon();
    }
    private void PointWeapon()
    {
        transform.rotation = gameManager.CameraTransform.rotation;
    }

    private void ShootRifle()
    {
        Ray ray = new Ray(gameManager.CameraTransform.position, gameManager.CameraTransform.forward);


        //Handle VFX
        //Spawn Laser Bullet
        GameObject laser = Instantiate(_curWeapon.LaserBeam, _muzzlePos.position, transform.rotation);
        //Spawn Muzzle Flash
        //GameObject muzzleFlash = Instantiate(_curWeapon.MuzzleFlash, transform.position, Quaternion.FromToRotation(transform.position, transform.forward));


        if (Physics.Raycast(ray, out RaycastHit hit, _curWeapon.Range))
        {
            //Handle Hit
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Hit enemmy of type " + hit.transform.gameObject.layer);
            }
            else
                Debug.Log("Hit " + hit.transform.gameObject.layer);

        }

    }

    private void HandleWeaponSwitch()
    {
        inputManager.playerControls.Player.SwitchWeapon.performed += ctx =>
        {
            EventBus.Publish(EventType.WEAPON_SWITCH, ctx.ReadValue<Vector2>());
        };
    }

    private void SwitchWeapon(Vector2 index)
    {
        if (index.y > 0)
            _weaponIndex++;
        else if(index.y < 0)
            _weaponIndex--;

        if(_weaponIndex > _availableWeapons.Length - 1)
        {
            _weaponIndex = 0;
        }
        else if (_weaponIndex < 0)
        {
            _weaponIndex = _availableWeapons.Length - 1;
        }

        //Assign curweapon and send event
        _curWeapon = _availableWeapons[_weaponIndex];

        EventBus.Publish(EventType.DISPLAY_WEAPON);
    }

    private void DisplayWeapon()
    {
        //if(_curWeapon != _availableWeapons[_weaponIndex])
        //    _curWeapon = _availableWeapons[_weaponIndex];

        if (transform.childCount > 1)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        _weaponPrefab = _curWeapon.WeaponPrefab;

        if (_weaponPrefab != null)
        {
            //Spawn weapon in the playerhand
            Instantiate(_weaponPrefab, playerHand.position, transform.rotation, transform);
            
            //Working on assigning the correct muzzlepos
            _curWeapon.MuzzlePos = _curWeapon.WeaponPrefab.transform.GetChild(0);
            _muzzlePos = _curWeapon.MuzzlePos;
        }
    }
}
