using UnityEngine;

namespace ScheduleOne.Tools;

public class EditionConditionalObject : MonoBehaviour
{
	public enum EType
	{
		ActiveInDemo,
		ActiveInFullGame
	}

	public EType type;

	private void Awake()
	{
		if (type == EType.ActiveInFullGame)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
