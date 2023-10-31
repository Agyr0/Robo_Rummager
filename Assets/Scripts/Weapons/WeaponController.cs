using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class WeaponController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    private ObjectPooler weaponPooler;
    private AudioSource audioSource;
    private AudioManager audioManager;

    [SerializeField]
    private Transform playerHand;
    private Transform playerHandStartTransform;

    public WeaponData _curWeapon;
    public GameObject _weaponPrefab;
    public WeaponData[] _availableWeapons;

    [SerializeField]
    private Camera weaponCam;
    [SerializeField]
    private float baseFOV = 60f;
    [SerializeField]
    private float zoomFOV = 30f;
    private float curZoom, targetZoom;
    private bool isZoomed = false;
    private Coroutine lerpZoom;

    private int _weaponIndex = 0;
    public int WeaponIndex
    {
        get { return _weaponIndex; }
    }
    private const int _wrenchIndex = 0;
    private const int _laserIndex = 1;
    private const int _handsIndex = 2;
    private bool canShoot = true;
    private bool isSwinging, isReloading, swingCheck = false;
    private int swingCount = 0;
    private Coroutine wrenchAnim;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Text _curAmmoText;

    public Text CurAmmoText
    {
        get
        {
            return _curAmmoText;
        }
        set
        {

            if (_curWeapon.CurAmmo >= 0 && _curWeapon.CurAmmo < Mathf.Infinity)
            {
                _curAmmoText.text = _curWeapon.CurAmmo.ToString();
                if (isReloading)
                    _curAmmoText.text = "...";
            }
            else if (_curWeapon.CurAmmo >= Mathf.Infinity)
                _curAmmoText.text = "\u221e";
            _curAmmoText = value;
        }
    }

    private void Start()
    {
        _curWeapon = _availableWeapons[0];
        inputManager = InputManager.Instance;
        audioManager = AudioManager.Instance;
        playerHandStartTransform = playerHand.transform;

        //Input Events
        SubscribeInputEvents();

        InitializeWeapon();



        weaponPooler = Camera.main.gameObject.GetComponentInChildren<ObjectPooler>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        gameManager.weaponController = this;

        EventBus.Subscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Subscribe(EventType.SWING_WRENCH, HandleWrenchAnim);
        EventBus.Subscribe(EventType.WRENCH_RAYCAST, SendWrenchRaycast);
        EventBus.Subscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Subscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);
        EventBus.Subscribe(EventType.PLAYER_RELOAD, HandleReload);
        EventBus.Subscribe(EventType.PLAYER_ZOOM, ToggleZoom);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_SHOOT, ShootRifle);
        EventBus.Unsubscribe<Vector2>(EventType.WEAPON_SWITCH, SwitchWeapon);
        EventBus.Unsubscribe(EventType.DISPLAY_WEAPON, DisplayWeapon);
        EventBus.Unsubscribe(EventType.PLAYER_RELOAD, HandleReload);
        EventBus.Unsubscribe(EventType.SWING_WRENCH, HandleWrenchAnim);
        EventBus.Unsubscribe(EventType.WRENCH_RAYCAST, SendWrenchRaycast);
        EventBus.Unsubscribe(EventType.PLAYER_ZOOM, ToggleZoom);

    }
    //private void LateUpdate()
    //{
    //    if (!isSwinging)
    //        PointWeapon();
    //}
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
        //Zoom
        inputManager.playerControls.Player.Aim.performed += _ => EventBus.Publish(EventType.PLAYER_ZOOM);
        inputManager.playerControls.Player.Aim.canceled += _ => EventBus.Publish(EventType.PLAYER_ZOOM);
        //Reload
        inputManager.playerControls.Player.Reload.performed += _ => EventBus.Publish(EventType.PLAYER_RELOAD);
    }




    #region Wrench
    //Handles the animation for wrench swings
    private void HandleWrenchAnim()
    {
        swingCheck = true;

        if(wrenchAnim != null)
        {
            StopCoroutine(wrenchAnim);
            wrenchAnim = null;
        }
        if (wrenchAnim == null)
        {
            _animator.SetTrigger("Wrench_Swing");

            swingCount++;
            if (swingCount == 4)
                swingCount -= 2;

            _animator.SetInteger("Wrench_Attack", swingCount);
            wrenchAnim = StartCoroutine(HandleSwingWaitTime());
        }
    }

    //Sends out the actual raycast for wrench
    public void SendWrenchRaycast()
    {
        isSwinging = true;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();



        if (Physics.Raycast(ray, out hit, _curWeapon.Range))
        {
            //Play hit wrench audio
            audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.Wrench_Metal, audioManager.effectAudio), 0.25f);

            LootBag lootBag = hit.transform.gameObject.GetComponent<LootBag>();
            PetBuildingController petBuildingController = hit.transform.gameObject.GetComponent<PetBuildingController>();
            //If I hit an item with a lootbag script run drop resource
            if (lootBag != null)
            {
                lootBag.DropResource(hit.point);
                Debug.Log("Hit resource");
                return;
            }
            if (petBuildingController != null)
            {
                petBuildingController.BuildPiece();
                return;
            }
        }
        else
            //Play whoosh wrench audio
            audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.Wrench_Whoosh, audioManager.effectAudio), 0.25f);
        isSwinging = false;

    }


    private IEnumerator HandleSwingWaitTime()
    {
        float resetTime = 2f;
        float time = 0;

        while (time < resetTime)
        {
            if (!swingCheck)
            {
                yield break;
            }
            time += Time.deltaTime;
            yield return null;

        }
        _animator.SetTrigger("Wrench_Reset");
        swingCount = 0;
        swingCheck = false;
    }


    #endregion

    #region Rifle
    private void ShootRifle()
    {
        if (canShoot)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit = new RaycastHit();

            //Handle VFX
            //Spawn Laser Bullet
            // GameObject laser = Instantiate(_curWeapon.LaserBeam, _curWeapon.MuzzlePos.position, transform.rotation);
            //Spawn Muzzle Flash
            //GameObject muzzleFlash = Instantiate(_curWeapon.MuzzleFlash, transform.position, Quaternion.FromToRotation(transform.position, transform.forward));

            TrailRenderer trail = ObjectPooler.PullObjectFromPool(_curWeapon.Trail).GetComponent<TrailRenderer>();
            if(!audioSource.isPlaying)
                audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.Gun, audioManager.effectAudio));


            if (Physics.Raycast(ray, out hit, _curWeapon.Range))
            {

                IDamageable enemy = hit.transform.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_curWeapon.Damage);
                }
            }
            StartCoroutine(ShootTrail(trail, hit));
            
            
            _curWeapon.CurAmmo--;
            CurAmmoText = CurAmmoText;

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

    private IEnumerator ShootTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        trail.transform.position = _curWeapon.MuzzlePos.position;
        Vector3 startPos = trail.transform.position;
        trail.gameObject.SetActive(true);

        if(hit.point == Vector3.zero)
        {
            hit.point = new Ray(Camera.main.transform.position, Camera.main.transform.forward).GetPoint(100);
        }

        while (time < trail.time)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time / trail.time);
            time += Time.deltaTime;
            yield return null;
        }
        trail.transform.position = hit.point;
        yield return new WaitForSeconds(trail.time);
        trail.gameObject.SetActive(false);
    }

    #region Reloading
    private void HandleReload()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        isReloading = true;
        canShoot = false;
        CurAmmoText = CurAmmoText;
        yield return new WaitForSeconds(_curWeapon.ReloadTime);
        canShoot = true;
        _curWeapon.CurAmmo = _curWeapon.MagSize;
        isReloading = false;
        CurAmmoText = CurAmmoText;

        Debug.Log("Reload complete\n" + "Ammo: " + _curWeapon.CurAmmo);
    }
    #endregion

    #endregion

    #region Weapon Switching
    private void SwitchWeapon(Vector2 index)
    {
        
        if (index.y > 0)
            _weaponIndex++;
        else if (index.y < 0)
            _weaponIndex--;

        if (_weaponIndex > _availableWeapons.Length - 1)
        {
            _weaponIndex = 0;
        }
        else if (_weaponIndex < 0)
        {
            _weaponIndex = _availableWeapons.Length - 1;
        }

        //Assign curweapon and send event
        _curWeapon = _availableWeapons[_weaponIndex];

        _animator.SetInteger("Weapon_Index", _weaponIndex);
        CurAmmoText.text = _curWeapon.CurAmmo.ToString();
        //EventBus.Publish(EventType.DISPLAY_WEAPON);
    }

    //Used in workbench to force a weapon switch
    public void SwitchWeapon(int index)
    {
        _weaponIndex = index;

        if (_weaponIndex > _availableWeapons.Length - 1)
        {
            _weaponIndex = 0;
        }
        else if (_weaponIndex < 0)
        {
            _weaponIndex = _availableWeapons.Length - 1;
        }

        //Assign curweapon and send event
        _curWeapon = _availableWeapons[_weaponIndex];
        _animator.SetInteger("Weapon_Index", _weaponIndex);
        CurAmmoText.text = _curWeapon.CurAmmo.ToString();
        EventBus.Publish(EventType.DISPLAY_WEAPON);
    }

    private void DisplayWeapon()
    {
        //if(_curWeapon != _availableWeapons[_weaponIndex])
        //    _curWeapon = _availableWeapons[_weaponIndex];
        _animator.ResetTrigger("Weapon_Switched");

        if (playerHand.childCount > 0)
        {
            
            for (int i = 0; i < playerHand.childCount; i++)
            {
                if(playerHand.GetChild(i).GetComponent<IRobotPet>() != null)
                {
                    playerHand.GetChild(i).GetComponent<BaseRobotPetController>().HandleInteract();
                }
                else
                    Destroy(playerHand.GetChild(i).gameObject);
            }
        }
        _weaponPrefab = _curWeapon.WeaponPrefab;

        if (_weaponPrefab != null)
        {
            //Spawn weapon in the playerhand
            GameObject weaponInstance = Instantiate(_weaponPrefab, playerHand.position, playerHand.rotation, playerHand.transform);

            //Assign _curWeapon.MuzzlePos with the instanced muzzle pos if available
            if (weaponInstance.transform.childCount > 0)
                _curWeapon.MuzzlePos = weaponInstance.transform.GetChild(0);
        }

        _animator.SetTrigger("Weapon_Switched");
        //playerHand.transform.position = playerHandStartTransform.position;
        //playerHand.transform.rotation = playerHandStartTransform.rotation;
    }
    #endregion

    #region Zoom
    private void ToggleZoom()
    {
        isZoomed = !isZoomed;
        curZoom = gameManager.PlayerVCam.m_Lens.FieldOfView;
        targetZoom = isZoomed ? zoomFOV : baseFOV;

        if (lerpZoom != null)
            StopCoroutine(lerpZoom);
        lerpZoom = StartCoroutine(LerpZoom());

    }

    //Lerp camera FOV for zooming with easing
    private IEnumerator LerpZoom()
    {

        float duration = 1f;
        float time = 0f;
        float t;
        while (time < duration)
        {
            t = time / duration;
            t = t * t * (3 - 2 * t);
            time += Time.deltaTime;
            gameManager.PlayerVCam.m_Lens.FieldOfView = Mathf.Lerp(curZoom, targetZoom, t);
            weaponCam.fieldOfView = gameManager.PlayerVCam.m_Lens.FieldOfView;

            yield return null;

        }

        gameManager.PlayerVCam.m_Lens.FieldOfView = targetZoom;

    }
    #endregion
}

