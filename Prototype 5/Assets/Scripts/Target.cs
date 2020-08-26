using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class Target : MonoBehaviour
{

    private Rigidbody targetRb;
    public ParticleSystem explosionParticle;
    private float launchForce;
    private float launchForceRangeMin = 11.0f;
    private float launchForceRangeMax = 15.0f;
    private float launchPositionRange = 4.0f;
    private float launchPositionY = -3.0f;
    private float outOfBoundsY = -10.0f;
    private float torqueRange = 10.0f;
    private float horizontalForceRange = 2.0f;
    private GameManager gameManager;
    private int scoreUpBig = 5;
    private int scoreUpMedium = 10;
    private int scoreUpSmall = 15;
    private int scoreDownBad = -30;


    // Start is called before the first frame update
    void Start()
    {


        // Launch the newly-created object upwards with random force and torque from a random x position

        transform.position = ReturnRandomSpawnPosition();
        launchForce = ReturnRandomFloat(launchForceRangeMin, launchForceRangeMax);
        targetRb = gameObject.GetComponent<Rigidbody>();
        targetRb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        targetRb.AddForce(Vector3.right * ReturnRandomFloat(-horizontalForceRange, horizontalForceRange), ForceMode.Impulse);
        targetRb.AddTorque(ReturnRandomVector3(-torqueRange, torqueRange), ForceMode.Impulse);

        // Get a reference to the Game Manager in order to change the score variable

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < outOfBoundsY)
        {
            if (!gameObject.CompareTag("Baddie"))
            {
                gameManager.GameOver();
            }

            Destroy(gameObject);
        }

        if (gameManager.isGameOver)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }


    }
    // If player left-clicks a target, update score depending on target type and destroy it.
    private void OnMouseOver()
    {
        if (!gameManager.isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gameObject.CompareTag("Baddie"))
                {
                    gameManager.UpdateScore(scoreDownBad);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

                }

                else if (gameObject.CompareTag("Goodie Small"))


                {
                    gameManager.UpdateScore(scoreUpSmall);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                }

                else if (gameObject.CompareTag("Goodie Medium"))

                {
                    gameManager.UpdateScore(scoreUpMedium);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                }

                else
                {
                    gameManager.UpdateScore(scoreUpBig);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                }


                Destroy(gameObject);
            }
        }
    }

    private float ReturnRandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

    // Return a varying spawn position on the X-axis, below the camera
    private Vector3 ReturnRandomSpawnPosition()
    {
        return new Vector3(ReturnRandomFloat(-launchPositionRange, launchPositionRange), launchPositionY);
    }

    private Vector3 ReturnRandomVector3(float min, float max)
    {
        float x = ReturnRandomFloat(min, max);
        float y = ReturnRandomFloat(min, max);
        float z = ReturnRandomFloat(min, max);

        return new Vector3(x, y, z);
    }
}
