using UnityEngine;

namespace VLB;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[SelectionBase]
[HelpURL("http://saladgamer.com/vlb-doc/comp-lightbeam-hd/")]
public class VolumetricLightBeamHD2D : VolumetricLightBeamHD
{
	[SerializeField]
	private int m_SortingLayerID;

	[SerializeField]
	private int m_SortingOrder;

	public int sortingLayerID
	{
		get
		{
			return m_SortingLayerID;
		}
		set
		{
			m_SortingLayerID = value;
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingLayerID = value;
			}
		}
	}

	public string sortingLayerName
	{
		get
		{
			return SortingLayer.IDToName(sortingLayerID);
		}
		set
		{
			sortingLayerID = SortingLayer.NameToID(value);
		}
	}

	public int sortingOrder
	{
		get
		{
			return m_SortingOrder;
		}
		set
		{
			m_SortingOrder = value;
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.sortingOrder = value;
			}
		}
	}

	public override Dimensions GetDimensions()
	{
		return Dimensions.Dim2D;
	}

	public override bool DoesSupportSorting2D()
	{
		return true;
	}

	public override int GetSortingLayerID()
	{
		return sortingLayerID;
	}

	public override int GetSortingOrder()
	{
		return sortingOrder;
	}
}
