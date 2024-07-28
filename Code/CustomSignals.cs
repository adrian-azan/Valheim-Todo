using Godot;
using System;

public partial class CustomSignals : Node
{
    [Signal]
    public delegate void AddToTotalEventHandler(int amount);

    [Signal]
    public delegate void ClearEventHandler();
}