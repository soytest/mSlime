using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Modify Health")]
[RequireComponent(typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public class ModifyHealthAttribute : MonoBehaviour
{

	public int healthChange = -1;


    private void OnTriggerStay2D(Collider2D collision)
    {
        HealthSystemAttribute healthScript = collision.gameObject.GetComponent<HealthSystemAttribute>();
        if (healthScript != null)
        {
            // subtract health from the player
            if(healthScript.ModifyHealth(healthChange))
                GetComponent<AudioSource>().Play();
        }
    }
}
