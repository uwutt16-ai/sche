using System;
using ScheduleOne.DevUtilities;
using ScheduleOne.Employees;
using ScheduleOne.Management;
using ScheduleOne.Persistence.Datas;
using UnityEngine;

namespace ScheduleOne.Persistence.Loaders;

public class BotanistLoader : EmployeeLoader
{
	public override string NPCType => typeof(BotanistData).Name;

	public override void Load(string mainPath)
	{
		Employee employee = LoadAndCreateEmployee(mainPath);
		if (employee == null)
		{
			return;
		}
		Botanist botanist = employee as Botanist;
		if (botanist == null)
		{
			Console.LogWarning("Failed to cast employee to botanist");
			return;
		}
		BotanistConfigurationData configData;
		if (TryLoadFile(mainPath, "Configuration", out var contents))
		{
			configData = JsonUtility.FromJson<BotanistConfigurationData>(contents);
			if (configData != null)
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration);
			}
		}
		BotanistData data;
		if (TryLoadFile(mainPath, "NPC", out var contents2))
		{
			data = null;
			try
			{
				data = JsonUtility.FromJson<BotanistData>(contents2);
			}
			catch (Exception ex)
			{
				Console.LogError(GetType()?.ToString() + " error reading data: " + ex);
			}
			if (data == null)
			{
				Console.LogWarning("Failed to load botanist data");
			}
			else
			{
				Singleton<LoadManager>.Instance.onLoadComplete.AddListener(LoadConfiguration2);
			}
		}
		void LoadConfiguration()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration);
			BotanistConfiguration obj = botanist.Configuration as BotanistConfiguration;
			obj.Bed.Load(configData.Bed);
			obj.Supplies.Load(configData.Supplies);
			obj.Pots.Load(configData.Pots);
		}
		void LoadConfiguration2()
		{
			Singleton<LoadManager>.Instance.onLoadComplete.RemoveListener(LoadConfiguration2);
			botanist.MoveItemBehaviour.Load(data.MoveItemData);
		}
	}
}
