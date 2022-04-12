using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Controller3D : MonoBehaviourPunCallbacks
{
    [Header("Multiplayer")]
    private bool isMine;

    [Header("Weapon")]
    [SerializeField] private GameObject weaponRotation;
    [SerializeField] private GameObject muzzlePoint;
    [SerializeField] private PhotonView bullet;
    [SerializeField] private float bulletSpeed;
    private string pathBullet = "Prefab/Player/Bullet";

    [Header("Player")]
    [SerializeField] private float skinWidth = 0.5f;
    [SerializeField] private float groundCheckDistance;

    [Header("Camera settings")]
    [SerializeField] private bool isFPS;
    [SerializeField] private GameObject camPositionFPS;
    [SerializeField] Vector3 cameraOffsetTPS;
    [SerializeField] Vector3 cameraOffsetFPS;
    [SerializeField] float smoothFactorQuick = 0.23f;
    [SerializeField] float smoothFactorSlow = 0.05f;
    [SerializeField] float radius = 1.0f;
    private Vector3 smoothedPos;
    private Vector3 topPos;
    private Vector2 cameraRotation;
    private Camera mainCam;
    private Vector3 offset;

    [Header("Physics")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] RaycastHit groundHit;
    private CapsuleCollider capsuleCollider;
    private Vector3 upperPoint;
    private Vector3 lowerPoint;

    [Header("Input")]
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float minXrotation = 1f;
    [SerializeField] private float maxXrotation = 1f;
    private Vector3 input;

    [Header("States")]
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private State[] states;

    [Header("PhysicsBody")]
    private PhysicsBody body;
    public PhysicsBody Body => body;

    [Header("Turrets")]
    [SerializeField] private PhotonView turret;
    [SerializeField] private Transform turretPos;
    [SerializeField] private int maxTurretToSpawn = 3;
    [SerializeField] private int turretCount;
    [SerializeField] private Vector3 turretOffset;
    private GameObject turretObject;
    private bool canPutDownTurret;
    private string pathTurret = "Prefab/Player/TurretAssembly";

    private void Awake()
    {
        stateMachine = new StateMachine(states, this);
        groundCheckDistance = 10 * skinWidth;

        capsuleCollider = GetComponent<CapsuleCollider>();
        body = GetComponent<PhysicsBody>();

        mainCam = Camera.main;
        isMine = photonView.IsMine;
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        if (isMine)
        {
            InputHandling();
            PlayerRotation();
            WeaponRotation();
            stateMachine.UpdateStates();
        }
    }

    private void InputHandling()
    {
        groundHit = IsGrounded();
        if (isMine)
        {
            input = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
            cameraRotation.y += Input.GetAxisRaw("MouseX") * mouseSensitivity;
            cameraRotation.x -= Input.GetAxisRaw("MouseY") * mouseSensitivity;
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, minXrotation, maxXrotation);
            input = mainCam.transform.rotation * input;
            input = Vector3.ProjectOnPlane(input, Body.GroundHit.normal).normalized;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                isFPS = !isFPS;
            }
        }
    }

    private void PlayerRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, cameraRotation.y, transform.rotation.z);
    }

    private void WeaponRotation()
    {
        weaponRotation.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
        Physics.Raycast(muzzlePoint.transform.position, weaponRotation.transform.rotation * Vector3.forward * 10f, out RaycastHit hit, obstacleLayer);
        Debug.DrawRay(muzzlePoint.transform.position, weaponRotation.transform.rotation * Vector3.forward * 100f, Color.red);

        if (!canPutDownTurret)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bull = PhotonNetwork.Instantiate(pathBullet, muzzlePoint.transform.position, weaponRotation.transform.rotation);
                Projectile projectile = bull.GetComponent<Projectile>();
                projectile.Velocity = weaponRotation.transform.rotation * Vector3.forward * bulletSpeed;
                projectile.IsShot = true;
            }
        }

        if (turretCount < maxTurretToSpawn)
        {
            if (Input.GetMouseButtonDown(1))
            {
                canPutDownTurret = true;
                turretObject = PhotonNetwork.Instantiate(pathTurret, turretPos.position, Quaternion.identity);
            }

            if (canPutDownTurret && turretObject != null && Input.GetMouseButton(1))
            {
                turretObject.transform.position = turretPos.position;
                turretObject.transform.rotation = Quaternion.FromToRotation(turretObject.transform.up, Vector3.up) * turretObject.transform.rotation;
            }

            if (canPutDownTurret && Input.GetMouseButtonUp(1))
            {
                if (hit.collider != null)
                {
                    turretObject.transform.rotation = Quaternion.FromToRotation(turretObject.transform.up, Vector3.up) * turretObject.transform.rotation;
                    turretObject.transform.position = turretPos.position;
                    turretObject.GetComponent<Turret>().IsPlaced = true;
                    turretCount++;
                }
            }
        }
    }

    private void UpdateCamera()
    {
        mainCam.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0.0f);

        if (isFPS)
        {
            mainCam.transform.position = camPositionFPS.transform.position;
        }
        else
        {
            topPos = transform.position + upperPoint;
            offset = mainCam.transform.rotation * cameraOffsetTPS;
            Vector3 direction = offset - topPos;
            Debug.DrawLine(
                transform.position,
                transform.position + offset,
                Color.red);

            Vector3 newPos;
            if (Physics.SphereCast(
                transform.position + upperPoint,
                radius,
                direction.normalized,
                out RaycastHit hit,
                direction.magnitude))
            {
                newPos = cameraOffsetTPS.normalized * hit.distance;
            }
            else
            {
                newPos = cameraOffsetTPS;
            }
            smoothedPos = Vector3.Lerp(smoothedPos, newPos, Time.deltaTime * (hit.collider ? smoothFactorQuick : smoothFactorSlow));
            mainCam.transform.position = topPos + mainCam.transform.rotation * smoothedPos;
        }
    }

    private void LateUpdate()
    {
        if (isMine)
            UpdateCamera();
    }

    private RaycastHit IsGrounded()
    {
        Physics.CapsuleCast(
            upperPoint,
            lowerPoint,
            capsuleCollider.radius,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance + skinWidth,
            obstacleLayer);
        return hit;
    }

    public Vector3 GetInput() { return input; }


    private void OnDrawGizmos()
    {
        if (mainCam)
            Gizmos.DrawWireSphere(mainCam.transform.position, radius);
    }
}
