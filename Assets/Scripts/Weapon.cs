using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform barrel;

    public float explosionRadius;
    public float explosionFoce;

    //Déclaration de la Source Audio et le système de particule
    public AudioSource audioSrcGun;
    public ParticleSystem psGunFire;

    void Start()
    {

    }

    void Update()
    {
        // Détection du clic gauche
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            // Activation des particules et bruit pour le tir
            audioSrcGun.Play();
            psGunFire.Play();
        }
    }

    void Shoot()
    {
        // Créer un rayon qui pointe vers l'avant du pistolet
        Ray pistolRay = new Ray(barrel.position, barrel.forward);
        RaycastHit hit;

        // Impact du rayon ?
        if (Physics.Raycast(pistolRay, out hit, 50f))
        {
            // Mini-explosion
            // Explosion
            Explosion explosion = new Explosion(explosionFoce, hit.point, explosionRadius, 0.25f);
        }

    }
    // Aout du gizmo visible seulement sur l'editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(barrel.position, barrel.forward * 50f);
    }
}

