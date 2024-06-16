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

    public void AddMaterials(Array<Materials> materials, Array<int> quantities)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (_materialsRequired.ContainsKey(materials[i]))
                _materialsRequired[materials[i]] += quantities[i];
            else
                _materialsRequired[materials[i]] = quantities[i];
        }

        UpdateListText();
    }

    public void RemoveMaterials(Array<Materials> materials, Array<int> quantities)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (_materialsRequired.ContainsKey(materials[i]))
            {
                _materialsRequired[materials[i]] -= quantities[i];

                if (_materialsRequired[materials[i]] == 0)
                    _materialsRequired.Remove(materials[i]);
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