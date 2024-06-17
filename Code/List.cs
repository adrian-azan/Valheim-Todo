using Godot;
using Godot.Collections;
using System;

public partial class List : Node2D
{
    private Dictionary<Materials, int> _materialsRequired;
    private Label _materialsRequiredText;

    public override void _Ready()
    {
        _materialsRequired = new Dictionary<Materials, int>();
        _materialsRequiredText = GetNode("ListText") as Label;
    }

    public void AddMaterials(Dictionary<Materials, int> _recipe)
    {
        foreach (Materials material in _recipe.Keys)
        {
            if (_materialsRequired.ContainsKey(material))
                _materialsRequired[material] += _recipe[material];
            else
                _materialsRequired[material] = _recipe[material];
        }

        UpdateListText();
    }

    public void RemoveMaterials(Dictionary<Materials, int> _recipe)
    {
        foreach (Materials material in _recipe.Keys)
        {
            if (_materialsRequired.ContainsKey(material))
            {
                _materialsRequired[material] -= _recipe[material];

                if (_materialsRequired[material] == 0)
                    _materialsRequired.Remove(material);
            }
        }

        UpdateListText();
    }

    public void UpdateListText()
    {
        String text = "";

        foreach (Materials item in _materialsRequired.Keys)
        {
            text += String.Format("{0}: {1}\n", item.ToString(), _materialsRequired[item].ToString());
        }

        _materialsRequiredText.Text = text;
    }
}