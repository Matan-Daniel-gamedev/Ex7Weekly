using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This component chases a given target object.
 */
public class Chaser : TargetMover
{
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    public Vector3 TargetObjectPosition()
    {
        return targetObject.position;
    }

    private void Update()
    {
        SetTarget(targetObject.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
