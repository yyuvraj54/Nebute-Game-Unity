using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{

    [SerializeField]private AudioSource finishedSound;
    private bool levelCompleted = false;

    void Start()
    {
        finishedSound = GetComponent<AudioSource>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted) {
            finishedSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);

       
        }
    }


    private void CompleteLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
