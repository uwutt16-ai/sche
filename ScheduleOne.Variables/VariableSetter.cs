using System;
using ScheduleOne.DevUtilities;

namespace ScheduleOne.Variables;

[Serializable]
public class VariableSetter
{
	public string VariableName;

	public string NewValue;

	public void Execute()
	{
		NetworkSingleton<VariableDatabase>.Instance.SetVariableValue(VariableName, NewValue);
	}
}
