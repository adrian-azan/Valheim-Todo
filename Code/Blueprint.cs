using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class Blueprint : Button
{
    [Export]
    private String _title;

    [Export]
    private Array<Materials> _materials;

    [Export]
    private Array<int> _quantities;

    private List _rootList;
    private int _total;
    private Label _totalLabel;

    public override void _Ready()
    {
        _rootList = GetNode("../../List") as List;
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
                _rootList.AddMaterials(_materials, _quantities);
                _total++;
            }
            else if (inputEventMouse.ButtonMask == MouseButtonMask.Right && _total > 0)
            {
                _rootList.RemoveMaterials(_materials, _quantities);
                _total--;
            }

            if (_total > 0)
                _totalLabel.Text = _total.ToString();
            else
                _totalLabel.Text = "";
        }
    }
}