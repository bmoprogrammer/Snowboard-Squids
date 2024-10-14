using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] int delayAmount = 2;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    bool hasCrashed = false;

    CircleCollider2D playerHead;

    void Start() {
        playerHead = GetComponent<CircleCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground" && !hasCrashed && playerHead.IsTouching(other.collider)) {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", delayAmount);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
