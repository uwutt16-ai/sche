using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[AddComponentMenu("")]
public class AmplifyColorTriggerProxy : AmplifyColorTriggerProxyBase
{
	private SphereCollider sphereCollider;

	private Rigidbody rigidBody;

	private void Start()
	{
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.radius = 0.01f;
		sphereCollider.isTrigger = true;
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		rigidBody.isKinematic = true;
	}

	private void LateUpdate()
	{
		base.transform.position = Reference.position;
		base.transform.rotation = Reference.rotation;
	}
}
