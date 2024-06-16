using Godot;
using Godot.Collections;
using System;

public partial class List : Node2D
{
    private Dictionary<Ingredients, int> _items;
    private Label _text;

    public override void _Ready()
    {
        _items = new Dictionary<Ingredients, int>();
        _text = GetNode("Label") as Label;
    }

    public void AddItems(Array<Ingredients> ingredients, Array<int> quantities)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (_items.ContainsKey(ingredients[i]))
                _items[ingredients[i]] += quantities[i];
            else
                _items[ingredients[i]] = quantities[i];
        }

        String text = "";

        foreach (Ingredients item in _items.Keys)
        {
            text += String.Format("{0}: {1}\n", item, _items[item]);
        }

        _text.Text = text;
    }

    public void RemoveItems(Array<Ingredients> ingredients, Array<int> quantities)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (_items.ContainsKey(ingredients[i]))
                _items[ingredients[i]] -= quantities[i];
        }

        String text = "";

        foreach (Ingredients item in _items.Keys)
        {
            text += String.Format("{0}: {1}\n", item, _items[item]);
        }

        _text.Text = text;
    }

    /* public override void _Draw()
     {
         String text = "";

         foreach (Ingredients item in _items.Keys)
         {
             text += String.Format("{}: {}\n", item, _items[item]);
         }

         _text.Text = text;
     }*/
}