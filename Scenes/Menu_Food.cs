using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Menu_Food : Node2D
{
    public GridContainer _recipes;

    [Export]
    public PackedScene blueprint;

    public override void _Ready()
    {
        _recipes = GetNode<GridContainer>("Recipes");

        FileAccess file = FileAccess.Open("res://Data/" + Name.ToString() + ".json", FileAccess.ModeFlags.Read);
        Dictionary allRecipes = Json.ParseString(file.GetAsText()).AsGodotDictionary();

        foreach (string recipeName in allRecipes.Keys)
        {
            Dictionary recipeDetails = allRecipes.GetValueOrDefault(recipeName).AsGodotDictionary();
            Dictionary ingredients = recipeDetails.GetValueOrDefault("Ingredients").AsGodotDictionary();
            Dictionary stats = recipeDetails.GetValueOrDefault("Stats").AsGodotDictionary();

            var recipeButton = blueprint.Instantiate() as Blueprint;
            recipeButton.Text = string.Format("{0,-20} {1,-8} {2}     ", recipeName, stats["Health"], stats["Stamina"]);

            _recipes.AddChild(recipeButton);

            //Display a total for each recipe at the end of the row
            Vector2 adjustedPosition = recipeButton._totalLabel.Position;
            adjustedPosition.X = _recipes.Size.X;
            recipeButton._totalLabel.Position = adjustedPosition;

            try
            {
                foreach (String material in ingredients.Keys)
                {
                    if (Enum.TryParse<Materials>(material, out var materialType) == false)
                        GD.PushWarning("Failed to process " + material.ToString() + " in " + recipeName);
                    else
                        recipeButton._blueprint.Add(materialType, ingredients[material].As<int>());
                }
            }
            catch (Exception e)
            {
                GD.PushWarning("Could not process " + recipeName);
            }
        }
    }
}