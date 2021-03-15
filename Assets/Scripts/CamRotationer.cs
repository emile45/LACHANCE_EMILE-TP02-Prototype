using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotationer : MonoBehaviour
{
    public float lerpElasticity = 5f;    

    // Pour empêcher la caméra de flippé si la souris va trop vers le haut/bas
    float xAxisClamp = 0f;

    // Le transform de la caméra
    Transform mainCamTransform;

    void Start()
    {
        // Vérouiller le curseur dans la fenètre
        Cursor.lockState = CursorLockMode.Confined;

        // Trouver la caméra
        mainCamTransform = FindObjectOfType<Camera>().transform;

        // Modifier la position du Cam Positioner
        transform.position = mainCamTransform.position;
        transform.rotation = mainCamTransform.rotation;

        // Retirer le parent de la caméra
        mainCamTransform.parent = null;
    }

    void Update()
    {
        // Calcul de la position et de la rotation
        RotateCamPosition();
    }

    void FixedUpdate()
    {        
        // Positionner la caméra en FixedUpdate, car le personnage bouge en FixedUpdate
        RepositionCamera();
    }

    void RotateCamPosition()
    {
        // Positions X et Y du curseur
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xAxisClamp -= mouseY;

        // Rotation du camPositioner et du character
        Vector3 rotCam = transform.rotation.eulerAngles;

        // Le positioner tourne en fonction de la position de la souris (x seulement)
        rotCam.x -= mouseY;
        rotCam.z = 0f;

        // Empêcher la caméra de flip en rotation
        if (xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            rotCam.x = 90f;
        } else if (xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            rotCam.x = 270f;
        }

        // Appliquer les rotations
        transform.rotation = Quaternion.Euler(rotCam);
    }

    /// <summary>
    /// La caméra se déplace vers la position pour que ça soit smooth
    /// </summary>
    void RepositionCamera()
    {
        mainCamTransform.position = Vector3.Lerp(mainCamTransform.position, transform.position, lerpElasticity * Time.fixedDeltaTime);
        mainCamTransform.rotation = Quaternion.Lerp(mainCamTransform.rotation, transform.rotation, lerpElasticity * Time.fixedDeltaTime);
    }

}
