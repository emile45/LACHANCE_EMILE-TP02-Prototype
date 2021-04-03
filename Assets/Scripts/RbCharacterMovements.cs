using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbCharacterMovements : MonoBehaviour
{
    public float speedWalking;
    public float speedRunning;


    public float jumpHeight = 1f;

    // Transform de la position des pieds
    public Transform feetPosition;

    public Camera mainCam;
    public Camera aimCam;


    private float inputVertical;
    private float inputHorizontal;    

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool isGrounded = true;

    private Animator animatorVanguard;

    private float speed = 0.1f;

    private float animationSpeed = 1;

    private float lerpSpeed = 0.08f;

    public bool isAiming=false;

    //Suis-je entrain de bouger ?
    bool isMoving;

    // Start is called before the first frame update
    void Awake()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        animatorVanguard = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPosition.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // Vérifier les inputs du joueur
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");

        // Verifier les deadzones.

        isMoving = Mathf.Abs(inputHorizontal) + Mathf.Abs(inputVertical) > 0f;

        animatorVanguard.SetBool("isMoving", isMoving);
        
        // Animations ---------------------
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Courir
            animationSpeed = Mathf.Lerp(animationSpeed, 2f, lerpSpeed);
            speed = Mathf.Lerp(speed, speedRunning, lerpSpeed);

        }
        else
        {
            // Marche
            animationSpeed = Mathf.Lerp(animationSpeed, 1f, lerpSpeed);
            speed = Mathf.Lerp(speed, speedWalking, lerpSpeed);
        }
        animatorVanguard.SetFloat("Horizontal", inputHorizontal * animationSpeed);
        animatorVanguard.SetFloat("Vertical", inputVertical * animationSpeed);

        // ---------------------------------

        // Vecteur de mouvements (Avant/arrière + Gauche/Droite)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;  
        
        // Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            animatorVanguard.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // Viser
        if (Input.GetMouseButtonDown(1))
        {
            Aim();
        }
        if (Input.GetMouseButtonUp(1))
        {
            stopAim();
        }

        // Regarder vers le haut / bas en visant

    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
    }
    void Aim()
    {
        // Changer pour la camera de visé
        aimCam.enabled = true;
        mainCam.enabled = false;
        animatorVanguard.SetTrigger("Aiming");
        isAiming = true;
    }
    void stopAim()
    {
        // Changer pour la camera principale
        aimCam.enabled = false;
        mainCam.enabled = true;
        animatorVanguard.SetTrigger("StopAim");
        isAiming = false;
    }

}
