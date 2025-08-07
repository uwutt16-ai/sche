using System.Collections;
using UnityEngine;

public class LandingSpotController : MonoBehaviour
{
	public bool _randomRotate = true;

	public Vector2 _autoCatchDelay = new Vector2(10f, 20f);

	public Vector2 _autoDismountDelay = new Vector2(10f, 20f);

	public float _maxBirdDistance = 20f;

	public float _minBirdDistance = 5f;

	public bool _takeClosest;

	public FlockController _flock;

	public bool _landOnStart;

	public bool _soarLand = true;

	public bool _onlyBirdsAbove;

	public float _landingSpeedModifier = 0.5f;

	public float _landingTurnSpeedModifier = 5f;

	public Transform _featherPS;

	public Transform _thisT;

	public int _activeLandingSpots;

	public float _snapLandDistance = 0.1f;

	public float _landedRotateSpeed = 0.01f;

	public float _gizmoSize = 0.2f;

	public void Start()
	{
		if (_thisT == null)
		{
			_thisT = base.transform;
		}
		if (_flock == null)
		{
			_flock = (FlockController)Object.FindObjectOfType(typeof(FlockController));
			Debug.Log(this?.ToString() + " has no assigned FlockController, a random FlockController has been assigned");
		}
		if (_landOnStart)
		{
			StartCoroutine(InstantLandOnStart(0.1f));
		}
	}

	public void ScareAll()
	{
		ScareAll(0f, 1f);
	}

	public void ScareAll(float minDelay, float maxDelay)
	{
		for (int i = 0; i < _thisT.childCount; i++)
		{
			if (_thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				_thisT.GetChild(i).GetComponent<LandingSpot>().Invoke("ReleaseFlockChild", Random.Range(minDelay, maxDelay));
			}
		}
	}

	public void LandAll()
	{
		for (int i = 0; i < _thisT.childCount; i++)
		{
			if (_thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				LandingSpot component = _thisT.GetChild(i).GetComponent<LandingSpot>();
				StartCoroutine(component.GetFlockChild(0f, 2f));
			}
		}
	}

	public IEnumerator InstantLandOnStart(float delay)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < _thisT.childCount; i++)
		{
			if (_thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				_thisT.GetChild(i).GetComponent<LandingSpot>().InstantLand();
			}
		}
	}

	public IEnumerator InstantLand(float delay)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < _thisT.childCount; i++)
		{
			if (_thisT.GetChild(i).GetComponent<LandingSpot>() != null)
			{
				_thisT.GetChild(i).GetComponent<LandingSpot>().InstantLand();
			}
		}
	}
}
