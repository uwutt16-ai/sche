using System;
using ScheduleOne.DevUtilities;
using UnityEngine;

namespace ScheduleOne.Variables;

[Serializable]
public class Condition
{
	public enum EConditionType
	{
		GreaterThan,
		LessThan,
		EqualTo,
		NotEqualTo,
		GreaterThanOrEqualTo,
		LessThanOrEqualTo
	}

	public string VariableName = "Variable Name";

	public EConditionType Operator = EConditionType.EqualTo;

	public string Value = "true";

	public bool Evaluate()
	{
		if (!NetworkSingleton<VariableDatabase>.InstanceExists)
		{
			return false;
		}
		BaseVariable variable = NetworkSingleton<VariableDatabase>.Instance.GetVariable(VariableName);
		if (variable == null)
		{
			Debug.LogError("Variable " + VariableName + " not found");
			return false;
		}
		return variable.EvaluateCondition(Operator, Value);
	}
}
