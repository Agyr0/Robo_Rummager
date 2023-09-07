using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;


    [SerializeField]
    private WeaponData _curWeapon;
    public WeaponData[] _availableWeapons;
    private bool canShoot = true;

    private void Start()
    {
        gameManager = GameManager.Instance;
        _curWeapon = _availableWeapons[0];
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_SHOOT, Shoot);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_SHOOT, Shoot);
    }

    private void Update()
    {
        if (canShoot)
            HandleShoot();
    }
    private void HandleShoot()
    {
        inputManager.playerControls.Player.Shoot.performed += _ =>
        {
            EventBus.Publish(EventType.PLAYER_SHOOT);
        };
    }

    private void Shoot()
    {
        Ray ray = new Ray(gameManager.CameraTransform.position, gameManager.CameraTransform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, _curWeapon.Range))
        {
            //Handle VFX
            //Spawn Laser Bullet
            GameObject laser = Instantiate(_curWeapon.LaserBeam, transform.position,transform.rotation);
            //Spawn Muzzle Flash
           // GameObject muzzleFlash = Instantiate(_curWeapon.MuzzleFlash, transform.position, Quaternion.FromToRotation(transform.position, transform.forward));

            //Handle Hit
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Hit enemmy of type " + hit.transform.gameObject.layer);
            }
            else
                Debug.Log("Hit "+ hit.transform.gameObject.layer);

        }
    }
}
