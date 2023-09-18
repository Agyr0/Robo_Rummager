using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    [SerializeField]
    private Transform playerHand;

    [SerializeField]
    private WeaponData _curWeapon;
    private GameObject _weaponPrefab;
    public WeaponData[] _availableWeapons;
    private int _weaponIndex = 0;
    private const int _wrenchIndex = 0;
    private const int _laserIndex = 1;
    private const int _handsIndex = 2;
    private bool canShoot = true;
    private bool isSwinging = false;
    [SerializeField]
    private Animator _animator;


    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.weaponController = this;

        _curWeapon = _availableWeapons[0];
        inputManager = InputManager.Instance;
        _animator = GetComponent<Animator>();
        //Input Events
        SubscribeInputEvents();

        InitializeWeapon();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Subscribe(EventType.SWING_WRENCH, StartWrenchSwing);
        EventBus.Subscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Subscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);
        EventBus.Subscribe(EventType.PLAYER_RELOAD, HandleReload);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Unsubscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Unsubscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);
        EventBus.Unsubscribe(EventType.PLAYER_RELOAD, HandleReload);
        EventBus.Unsubscribe(EventType.SWING_WRENCH, StartWrenchSwing);

    }
    private void LateUpdate()
    {
        if(!isSwinging)
            PointWeapon();
    }
    private void InitializeWeapon()
    {
        //Ensure weapon is displayed on start
        EventBus.Publish(EventType.DISPLAY_WEAPON);

        //Set ammo to magsize for each
        for (int i = 0; i < _availableWeapons.Length; i++)
        {
            _availableWeapons[i].CurAmmo = _availableWeapons[i].MagSize;
        }
    }
    private void SubscribeInputEvents()
    {
        //Weapon Switch
        inputManager.playerControls.Player.SwitchWeapon.performed += ctx => EventBus.Publish(EventType.WEAPON_SWITCH, ctx.ReadValue<Vector2>());

        //Shooting
        inputManager.playerControls.Player.Shoot.performed += _ =>
        {
            if (_weaponIndex == _wrenchIndex && !isSwinging)
                EventBus.Publish(EventType.SWING_WRENCH);
            else if (_weaponIndex == _laserIndex)
                EventBus.Publish(EventType.PLAYER_SHOOT);
            else if (_weaponIndex == _handsIndex)
                EventBus.Publish(EventType.PUNCH_HANDS);
        };

        //Reload
        inputManager.playerControls.Player.Reload.performed += _ => EventBus.Publish(EventType.PLAYER_RELOAD);
    }

    
    private void PointWeapon()
    {
        transform.rotation = gameManager.CameraTransform.rotation;
    }

    #region Wrench
    private void StartWrenchSwing()
    {
        isSwinging = true;
        _animator.SetTrigger("Attack");
    }
    public void StopWrenchSwing()
    {
        isSwinging = false;
    }

    public void PlayAttack()
    {
        StartCoroutine(AttackRaycast(10));
    }
    private IEnumerator AttackRaycast(int numHits)
    {
        while (numHits > 0)
        {
            if(Physics.Raycast(_curWeapon.MuzzlePos.position, _curWeapon.MuzzlePos.forward,out RaycastHit hit, _curWeapon.Range))
            {
                LootBag lootBag = hit.transform.gameObject.GetComponent<LootBag>();
                //If I hit an item with a lootbag script run drop resource
                if(lootBag != null)
                {
                    lootBag.DropResource(hit.point);
                    break;
                }
            }
            //debug ray for seeing where the swing is sending out detection 
            //Debug.DrawRay(_curWeapon.MuzzlePos.position, _curWeapon.MuzzlePos.forward * _curWeapon.Range, Color.yellow, 1000f);
            yield return null;
            numHits--;
        }
    }
    #endregion

    #region Rifle
    private void ShootRifle()
    {
        if (canShoot)
        {
            Ray ray = new Ray(gameManager.CameraTransform.position, gameManager.CameraTransform.forward);


            //Handle VFX
            //Spawn Laser Bullet
            GameObject laser = Instantiate(_curWeapon.LaserBeam, _curWeapon.MuzzlePos.position, transform.rotation);
            //Spawn Muzzle Flash
            //GameObject muzzleFlash = Instantiate(_curWeapon.MuzzleFlash, transform.position, Quaternion.FromToRotation(transform.position, transform.forward));


            if (Physics.Raycast(ray, out RaycastHit hit, _curWeapon.Range))
            {
                IDamageable enemy = hit.transform.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_curWeapon.Damage);
                }
            }
            _curWeapon.CurAmmo--;
            if (_curWeapon.CurAmmo <= 0)
            {
                canShoot = false;
                Debug.Log("Out of Ammo");
            }
            else if (_curWeapon.CurAmmo > 0 && (_curWeapon.MagSize / 5) > _curWeapon.CurAmmo)
            {
                EventBus.Publish(EventType.LOW_AMMO);
                Debug.Log("Low on Ammo");
            }
            Debug.Log("Ammo: " + _curWeapon.CurAmmo);
        }
    }

    #region Reloading
    private void HandleReload()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        Debug.Log("Reloading...");

        canShoot = false;
        yield return new WaitForSeconds(_curWeapon.ReloadTime);
        canShoot = true;
        _curWeapon.CurAmmo = _curWeapon.MagSize;
        Debug.Log("Reload complete\n" + "Ammo: " + _curWeapon.CurAmmo);
    }
    #endregion

    #endregion

    #region Weapon Switching
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

        if (playerHand.childCount > 0)
        {
            for (int i = 0; i < playerHand.childCount; i++)
            {
                Destroy(playerHand.GetChild(i).gameObject);
            }
        }
        _weaponPrefab = _curWeapon.WeaponPrefab;

        if (_weaponPrefab != null)
        {
            //Spawn weapon in the playerhand
            GameObject weaponInstance = Instantiate(_weaponPrefab, playerHand.position, transform.rotation, playerHand.transform);

            //Assign _curWeapon.MuzzlePos with the instanced muzzle pos if available
            if (weaponInstance.transform.childCount > 0)
                _curWeapon.MuzzlePos = weaponInstance.transform.GetChild(0);
        }
    }
    #endregion
}
