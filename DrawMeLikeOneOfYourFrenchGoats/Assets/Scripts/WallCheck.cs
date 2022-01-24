using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
	[SerializeField] private LayerMask platformLayerMask;
	[SerializeField] private Transform m_WallCheck;
	public bool isWalled;
	const float k_WalledRadius = .05f;


	/*private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = ((collision != null) && (((1 << collision.gameObject.layer) & platformLayerMask) != 0));
        int token = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }*/

	private void FixedUpdate()
	{
		isWalled = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_WallCheck.position, k_WalledRadius, platformLayerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isWalled = true;
			}
		}
	}
}
