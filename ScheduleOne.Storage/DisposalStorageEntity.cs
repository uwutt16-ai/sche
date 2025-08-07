namespace ScheduleOne.Storage;

public class DisposalStorageEntity : StorageEntity
{
	private bool NetworkInitialize___EarlyScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted;

	private bool NetworkInitialize__LateScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted;

	public override void NetworkInitialize___Early()
	{
		if (!NetworkInitialize___EarlyScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted)
		{
			NetworkInitialize___EarlyScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted = true;
			base.NetworkInitialize___Early();
		}
	}

	public override void NetworkInitialize__Late()
	{
		if (!NetworkInitialize__LateScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted)
		{
			NetworkInitialize__LateScheduleOne_002EStorage_002EDisposalStorageEntityAssembly_002DCSharp_002Edll_Excuted = true;
			base.NetworkInitialize__Late();
		}
	}

	public override void NetworkInitializeIfDisabled()
	{
		NetworkInitialize___Early();
		NetworkInitialize__Late();
	}

	public override void Awake()
	{
		NetworkInitialize___Early();
		base.Awake();
		NetworkInitialize__Late();
	}
}
