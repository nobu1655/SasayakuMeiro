using UnityEngine;

public class PlayerPickupSound : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            audioSource.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        }
    }
}
