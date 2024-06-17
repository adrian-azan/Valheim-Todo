using Godot;
using System;

public partial class Root : Node2D
{
    private Tabs _tabs;

    public override void _Ready()
    {
        _tabs = GetNode("Tabs") as Tabs;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Switch_Right"))
        {
            _tabs++;
        }

        if (Input.IsActionJustPressed("Switch_Left"))
        {
            _tabs--;
        }
    }
}