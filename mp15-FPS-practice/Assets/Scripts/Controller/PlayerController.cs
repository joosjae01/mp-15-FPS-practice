using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor, IDamageable
{
    [SerializeField]
    public GameObject GameObject { get => gameObject; }
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    private IInteractable _targetInteractable;
    private PlayerMovement _movement;
    private PlayerWeapon _weapon;
    private PlayerData _playerData;
    private Transform _cameraTransform;

    private bool _isPressedInteractionKey => Input.GetKeyDown(_interactionKey);
    private bool _hasTargetInteractable => _targetInteractable != null;
    private bool _canInteract => _isPressedInteractionKey && _hasTargetInteractable;

    //  =====   =====   =====   =====   ===== //
    private void Awake() => CacheComponents();
    private void Start() => Initialize();
    private void FixedUpdate() => _movement.Move();
    private void Update()
    {
        _movement.Jump();
        _movement.Rotate();
        _weapon.StartReload();
        _weapon.EndReload();
        _weapon.Fire();
        _weapon.ThrowGrenade();
        DetectInteractable();
        TryInteract();

        if (Input.GetKeyDown(KeyCode.P)) GameManager.Instance.Pause();
        else if (Input.GetKeyDown(KeyCode.O)) GameManager.Instance.Run();
    }
    private void LateUpdate()
    {
        SetWeaponTransform();
        SetCameraTransform();
    }
    //  =====   =====   =====   =====   ===== //
    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, _detectionRange))
        {
            _targetInteractable?.Untargeting();
            _targetInteractable = null;
            return;
        }

        if (_hasTargetInteractable)
        {
            if (hit.collider.gameObject == _targetInteractable.GameObject)
            {
                return;
            }
        }

        _targetInteractable?.Untargeting();
        _targetInteractable = hit.collider.GetComponent<IInteractable>();

        _targetInteractable?.Targeting();
    }

    public void TryInteract()
    {
        if (!_canInteract) return;

        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }

    public void TakeDamage(int damage)
    {
        if (_playerData.CurrentHealth.Value > damage)
        {
            _playerData.CurrentHealth.Value -= damage;
        }
        else
        {
            _playerData.CurrentHealth.Value = 0;
        }
    }

    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();

        _cameraTransform = Camera.main.transform;
    }

    private void Initialize()
    {
        LockCursor();
        _movement.SetPlayerData(_playerData);
    }
}