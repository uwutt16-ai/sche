using System.Collections.Generic;
using System.Linq;
using ScheduleOne.ItemFramework;
using ScheduleOne.StationFramework;
using ScheduleOne.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScheduleOne.UI.Stations;

public class StationRecipeEntry : MonoBehaviour
{
	public static Color ValidColor = Color.white;

	public static Color InvalidColor = new Color32(byte.MaxValue, 80, 80, byte.MaxValue);

	public Button Button;

	public Image Icon;

	public TextMeshProUGUI TitleLabel;

	public TextMeshProUGUI CookingTimeLabel;

	public RectTransform[] IngredientRects;

	private TextMeshProUGUI[] IngredientQuantities;

	public bool IsValid { get; private set; }

	public StationRecipe Recipe { get; private set; }

	public void AssignRecipe(StationRecipe recipe)
	{
		Recipe = recipe;
		Icon.sprite = recipe.Product.Item.Icon;
		TitleLabel.text = recipe.RecipeTitle;
		if (recipe.Product.Quantity > 1)
		{
			TitleLabel.text = TitleLabel.text + "(" + recipe.Product.Quantity + "x)";
		}
		Icon.GetComponent<ItemDefinitionInfoHoverable>().AssignedItem = recipe.Product.Item;
		int num = recipe.CookTime_Mins / 60;
		int num2 = recipe.CookTime_Mins % 60;
		CookingTimeLabel.text = $"{num}h";
		if (num2 > 0)
		{
			CookingTimeLabel.text += $" {num2}m";
		}
		IngredientQuantities = new TextMeshProUGUI[IngredientRects.Length];
		for (int i = 0; i < IngredientRects.Length; i++)
		{
			if (i < recipe.Ingredients.Count)
			{
				IngredientRects[i].Find("Icon").GetComponent<Image>().sprite = recipe.Ingredients[i].Item.Icon;
				IngredientQuantities[i] = IngredientRects[i].Find("Quantity").GetComponent<TextMeshProUGUI>();
				IngredientQuantities[i].text = recipe.Ingredients[i].Quantity + "x";
				IngredientRects[i].GetComponent<ItemDefinitionInfoHoverable>().AssignedItem = recipe.Ingredients[i].Item;
				IngredientRects[i].gameObject.SetActive(value: true);
			}
			else
			{
				IngredientRects[i].gameObject.SetActive(value: false);
			}
		}
	}

	public void RefreshValidity(List<ItemInstance> ingredients)
	{
		if (!Recipe.Unlocked)
		{
			IsValid = false;
			base.gameObject.SetActive(value: false);
			return;
		}
		IsValid = true;
		for (int i = 0; i < Recipe.Ingredients.Count; i++)
		{
			List<ItemInstance> list = new List<ItemInstance>();
			foreach (ItemDefinition ingredientVariant in Recipe.Ingredients[i].Items)
			{
				List<ItemInstance> collection = ingredients.Where((ItemInstance x) => x.ID == ingredientVariant.ID).ToList();
				list.AddRange(collection);
			}
			int num = 0;
			for (int num2 = 0; num2 < list.Count; num2++)
			{
				num += list[num2].Quantity;
			}
			if (num >= Recipe.Ingredients[i].Quantity)
			{
				IngredientQuantities[i].color = ValidColor;
				continue;
			}
			IngredientQuantities[i].color = InvalidColor;
			IsValid = false;
		}
		base.gameObject.SetActive(value: true);
		Button.interactable = IsValid;
	}

	public float GetIngredientsMatchDelta(List<ItemInstance> ingredients)
	{
		int num = Recipe.Ingredients.Sum((StationRecipe.IngredientQuantity x) => x.Quantity);
		int num2 = 0;
		for (int num3 = 0; num3 < Recipe.Ingredients.Count; num3++)
		{
			List<ItemInstance> list = new List<ItemInstance>();
			foreach (ItemDefinition ingredientVariant in Recipe.Ingredients[num3].Items)
			{
				List<ItemInstance> collection = ingredients.Where((ItemInstance x) => x.ID == ingredientVariant.ID).ToList();
				list.AddRange(collection);
			}
			int num4 = 0;
			for (int num5 = 0; num5 < list.Count; num5++)
			{
				num4 += list[num5].Quantity;
			}
			num2 += Mathf.Min(num4, Recipe.Ingredients[num3].Quantity);
		}
		return (float)num2 / (float)num;
	}
}
