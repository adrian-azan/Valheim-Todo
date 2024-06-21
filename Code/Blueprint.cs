using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class Blueprint : Button
{
    [Export]
    private String _title;

    public Dictionary<Materials, int> _blueprint;

    private List _cumulativeList;
    private int _total;
    private Label _totalLabel;

    public override void _Ready()
    {
        _blueprint = new Dictionary<Materials, int>();

        _cumulativeList = GetNode("../../../List") as List;
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
                _cumulativeList.AddMaterials(_blueprint);
                _total++;
            }
            else if (inputEventMouse.ButtonMask == MouseButtonMask.Right && _total > 0)
            {
                _cumulativeList.RemoveMaterials(_blueprint);
                _total--;
            }

            if (_total > 0)
                _totalLabel.Text = _total.ToString();
            else
                _totalLabel.Text = "";
        }
    }
}