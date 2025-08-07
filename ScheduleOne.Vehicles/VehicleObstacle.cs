using UnityEngine;

namespace ScheduleOne.Vehicles;

public class VehicleObstacle : MonoBehaviour
{
	public enum EObstacleType
	{
		Generic,
		TrafficLight
	}

	public Collider col;

	[Header("Settings")]
	public bool twoSided = true;

	public EObstacleType type;
}
