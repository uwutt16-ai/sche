using System;
using ScheduleOne.DevUtilities;
using ScheduleOne.Employees;
using ScheduleOne.Management;
using ScheduleOne.Persistence.Datas;
using UnityEngine;

namespace ScheduleOne.Persistence.Loaders;

public class PackagerLoader : EmployeeLoader
{
	public override string NPCType => typeof(PackagerData).Name;

	public override void Load(string mainPath)
	{
		Employee employee = LoadAndCreateEmployee(mainPath);
		if (employee == null)
		{
			return;
		}
		Packager packager = employee as Packager;
		if (packager == null)
		{
			Console.LogWarning("Failed to cast employee to packager");
			return;
		}
		PackagerConfigurationData configData;
		if (TryLoadFile(mainPath, "Configuration", out var contents))
		{
			configData = JsonUtility.FromJson<PackagerConfigurationData>(contents);
			if (configData != null)
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration);
			}
		}
		PackagerData data;
		if (TryLoadFile(mainPath, "NPC", out var contents2))
		{
			data = null;
			try
			{
				data = JsonUtility.FromJson<PackagerData>(contents2);
			}
			catch (Exception ex)
			{
				Console.LogError(GetType()?.ToString() + " error reading data: " + ex);
			}
			if (data == null)
			{
				Console.LogWarning("Failed to load packager data");
			}
			else
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration2);
			}
		}
		void LoadConfiguration()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration);
			PackagerConfiguration obj = packager.Configuration as PackagerConfiguration;
			obj.Bed.Load(configData.Bed);
			obj.Stations.Load(configData.Stations);
		}
		void LoadConfiguration2()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration2);
			packager.MoveItemBehaviour.Load(data.MoveItemData);
		}
	}
}
