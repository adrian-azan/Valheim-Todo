using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class Blueprint : Button
{
    [Export]
    private String _title;

    public Dictionary<Materials, int> _recipe;

    private List _rootList;
    private int _total;
    private Label _totalLabel;

    public override void _Ready()
    {
        _recipe = new Dictionary<Materials, int>();

        _rootList = GetNode("../../../List") as List;
        _total = 0;
        _totalLabel = GetNode("Total") as Label;
    }

    private void Clicked(InputEvent @event)
    {
        if (@event is InputEventMouse inputEventMouse)
        {
            if (inputEventMouse is InputEventMouseMotion)
                return;

            if (inputEventMouse.ButtonMask == MouseButtonMask.Left)
            {
                _rootList.AddMaterials(_recipe);
                _total++;
            }
            else if (inputEventMouse.ButtonMask == MouseButtonMask.Right && _total > 0)
            {
                _rootList.RemoveMaterials(_recipe);
                _total--;
            }

            if (_total > 0)
                _totalLabel.Text = _total.ToString();
            else
                _totalLabel.Text = "";
        }
    }
}