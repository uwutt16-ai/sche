using System;

namespace ScheduleOne.Vision;

[Serializable]
public class UniqueVisibilityAttribute : VisibilityAttribute
{
	public string uniquenessCode;

	public UniqueVisibilityAttribute(string _name, float _pointsChange, string _uniquenessCode, float _multiplier = 1f, int attributeIndex = -1)
		: base(_name, _pointsChange, _multiplier, attributeIndex)
	{
		uniquenessCode = _uniquenessCode;
	}
}
