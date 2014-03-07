using UnityEngine;
using System;
using System.Collections;

public class IngredientSelection : MonoBehaviour 
{
	public UIPopupList thisPopupList;
	public int ingredientNum = 0;
	public IngredientManager ingredientManager;

	void Start()
	{
		EventDelegate.Add(thisPopupList.onChange, OnSelect);
	}

	public void OnSelect()
	{
		if (UIPopupList.current == null)
			return;

		ingredientManager.UpdateResultDisplay(ingredientNum, UIPopupList.current.value);
	}
}