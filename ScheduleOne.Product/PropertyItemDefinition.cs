using System;
using System.Collections.Generic;
using ScheduleOne.ItemFramework;
using ScheduleOne.Properties;
using UnityEngine;

namespace ScheduleOne.Product;

[Serializable]
[CreateAssetMenu(fileName = "PropertyItemDefinition", menuName = "ScriptableObjects/PropertyItemDefinition", order = 1)]
public class PropertyItemDefinition : StorableItemDefinition
{
	[Header("Properties")]
	public List<ScheduleOne.Properties.Property> Properties = new List<ScheduleOne.Properties.Property>();

	public virtual void Initialize(List<ScheduleOne.Properties.Property> properties)
	{
		Properties.AddRange(properties);
	}

	public bool HasProperty(ScheduleOne.Properties.Property property)
	{
		return Properties.Contains(property);
	}
}
