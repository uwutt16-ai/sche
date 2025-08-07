using System;
using ScheduleOne.DevUtilities;
using ScheduleOne.Employees;
using ScheduleOne.Management;
using ScheduleOne.Persistence.Datas;
using UnityEngine;

namespace ScheduleOne.Persistence.Loaders;

public class ChemistLoader : EmployeeLoader
{
	public override string NPCType => typeof(ChemistData).Name;

	public override void Load(string mainPath)
	{
		Employee employee = LoadAndCreateEmployee(mainPath);
		if (employee == null)
		{
			return;
		}
		Chemist chemist = employee as Chemist;
		if (chemist == null)
		{
			Console.LogWarning("Failed to cast employee to chemist");
			return;
		}
		ChemistConfigurationData configData;
		if (TryLoadFile(mainPath, "Configuration", out var contents))
		{
			configData = JsonUtility.FromJson<ChemistConfigurationData>(contents);
			if (configData != null)
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration);
			}
		}
		ChemistData data;
		if (TryLoadFile(mainPath, "NPC", out var contents2))
		{
			data = null;
			try
			{
				data = JsonUtility.FromJson<ChemistData>(contents2);
			}
			catch (Exception ex)
			{
				Console.LogError(GetType()?.ToString() + " error reading data: " + ex);
			}
			if (data == null)
			{
				Console.LogWarning("Failed to load chemist data");
			}
			else
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration2);
			}
		}
		void LoadConfiguration()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration);
			ChemistConfiguration obj = chemist.Configuration as ChemistConfiguration;
			obj.Bed.Load(configData.Bed);
			obj.Stations.Load(configData.Stations);
		}
		void LoadConfiguration2()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration2);
			chemist.MoveItemBehaviour.Load(data.MoveItemData);
		}
	}
}
