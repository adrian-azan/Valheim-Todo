using Godot;
using System;

using Godot.Collections;

public partial class Menu : Node2D
{
    public override void _Ready()
    {
        FileAccess file = FileAccess.Open("res://Data/" + Name.ToString() + ".json", FileAccess.ModeFlags.Read);
        Dictionary blueprintDetails = Json.ParseString(file.GetAsText()).AsGodotDictionary();

        Array<Node> blueprints = GetNode("Blueprints").GetChildren();

        foreach (Blueprint blueprint in blueprints)
        {
            try
            {
                Dictionary<String, int> dictionary = blueprintDetails[blueprint.Name].AsGodotDictionary<String, int>();

                foreach (String material in dictionary.Keys)
                {
                    if (Enum.TryParse<Materials>(material, out var materialType) == false)
                        GD.PushWarning("Failed to process " + material.ToString() + " in " + blueprint.Name);
                    else
                        blueprint._recipe.Add(materialType, dictionary[material]);
                }
            }
            catch (Exception e)
            {
                GD.PushWarning("Could not process " + blueprint.Name);
            }
        }
    }
}