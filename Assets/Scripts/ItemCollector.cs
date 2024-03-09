using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCollector : MonoBehaviour
{
    private int Score = 0;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private AudioSource collectSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Cherry")) {
            Destroy(collision.gameObject);
            Score++;
            Debug.Log("Cherries "+ Score);
            collectSoundEffect.Play();
            scoreText.text = "Score : " +Score;
        }
        
    }


}
