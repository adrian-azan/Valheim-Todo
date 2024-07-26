using Godot;
using System;

public partial class ClearButton : Button
{
    private Node _CustomSignal;

    public override void _Ready()
    {
        _CustomSignal = GetNode<CustomSignals>("/root/CustomSignals");
    }

    public void ClearMaterials(InputEvent @event)
    {
        if (@event is InputEventMouse inputEventMouse)
        {
            if (inputEventMouse.ButtonMask != MouseButtonMask.Left || inputEventMouse is InputEventMouseMotion)
                return;

            _CustomSignal.EmitSignal(nameof(CustomSignals.Clear));
        }
    }
}