using UnityEngine;

public class CollisonHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collision with freindly object");
                break;
            case "Finish":
                Debug.Log("Collision with finish");
                break;
        }
    }
}
