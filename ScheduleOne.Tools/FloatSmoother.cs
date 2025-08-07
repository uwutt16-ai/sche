using System;
using System.Collections.Generic;
using ScheduleOne.DevUtilities;
using ScheduleOne.GameTime;
using UnityEngine;

namespace ScheduleOne.Tools;

[Serializable]
public class FloatSmoother
{
	public class Override
	{
		public float Value;

		public int Priority;

		public string Label;
	}

	[SerializeField]
	private float DefaultValue = 1f;

	[SerializeField]
	private float SmoothingSpeed = 1f;

	private List<Override> overrides = new List<Override>();

	public float CurrentValue { get; private set; }

	public float Multiplier { get; private set; } = 1f;

	public void Initialize()
	{
		SetDefault(DefaultValue);
		if (NetworkSingleton<TimeManager>.InstanceExists)
		{
			TimeManager instance = NetworkSingleton<TimeManager>.Instance;
			instance.onUpdate = (Action)Delegate.Combine(instance.onUpdate, new Action(Update));
		}
	}

	public void Destroy()
	{
		if (NetworkSingleton<TimeManager>.InstanceExists)
		{
			TimeManager instance = NetworkSingleton<TimeManager>.Instance;
			instance.onUpdate = (Action)Delegate.Remove(instance.onUpdate, new Action(Update));
		}
	}

	public void SetDefault(float value)
	{
		AddOverride(value, 0, "Default");
		CurrentValue = value;
	}

	public void SetMultiplier(float value)
	{
		Multiplier = value;
	}

	public void SetSmoothingSpeed(float value)
	{
		SmoothingSpeed = value;
	}

	public void AddOverride(float value, int priority, string label)
	{
		Override obj = overrides.Find((Override x) => x.Label.ToLower() == label.ToLower());
		if (obj == null)
		{
			obj = new Override();
			obj.Label = label;
			overrides.Add(obj);
		}
		obj.Value = value;
		obj.Priority = priority;
		overrides.Sort((Override x, Override y) => y.Priority.CompareTo(x.Priority));
	}

	public void RemoveOverride(string label)
	{
		Override obj = overrides.Find((Override x) => x.Label.ToLower() == label.ToLower());
		if (obj != null)
		{
			overrides.Remove(obj);
		}
		overrides.Sort((Override x, Override y) => y.Priority.CompareTo(x.Priority));
	}

	public void Update()
	{
		if (overrides.Count != 0)
		{
			Override obj = overrides[0];
			CurrentValue = Mathf.Lerp(CurrentValue, obj.Value, SmoothingSpeed * Time.deltaTime) * Multiplier;
		}
	}
}
