using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRidgidbody;
    public float flapStrength;
    public LogicScript logic;
    private List<Animation> wingAnimations;
    private Vector3 screenPosition;
    private AudioSource wingFlapSound;


    public bool birdIsAlive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        wingFlapSound = gameObject.transform.Find("wings").GetComponent<AudioSource>();

        wingAnimations = new List<Animation>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();
        var wingObjects = GameObject.FindGameObjectsWithTag("wings");
        foreach (GameObject obj in wingObjects)
        {
            Animation animation = obj.GetComponent<Animation>();
            if (animation != null)
            {
                wingAnimations.Add(animation);
            }
        }
        Debug.Log($"Found wings: {wingAnimations.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if(!inScreen())
        {
            killBird();
        }

        if(screenPosition.y < -100F)
        {
            Destroy(gameObject);
        }


        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0))) 
        {
            flapWings();
        }
    }

    private void flapWings()
    {
            if (birdIsAlive)
            {
                wingFlapSound.time = 0.8F;
                wingFlapSound.Play();
                foreach(Animation wing in wingAnimations)
                {
                    wing.Play();
                }
                myRidgidbody.linearVelocity = Vector2.up * flapStrength;
            }
    }

    private bool inScreen() {

        if (screenPosition.y > Screen.height || screenPosition.y < 0) 
        {
            return false;
        }
        return true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        killBird();
    }

    private void killBird()
    {
        if (birdIsAlive)
        {
            Debug.Log("Kill Bird");
            logic.gameOver();
            birdIsAlive = false;
        }
    }
}
