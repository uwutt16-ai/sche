using System;

namespace VLB;

[Flags]
public enum ShadowUpdateRate
{
	Never = 1,
	OnEnable = 2,
	OnBeamMove = 4,
	EveryXFrames = 8,
	OnBeamMoveAndEveryXFrames = 0xC
}
