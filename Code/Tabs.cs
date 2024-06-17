using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class Tabs : Node2D
{
    private int _selected;
    private int _size;

    private Godot.Collections.Array<Node2D> _menus;

    public override void _Ready()
    {
        _menus = new Godot.Collections.Array<Node2D>();
        _menus.Add(GetNode("../Menu_Misc") as Node2D);
        _menus.Add(GetNode("../Menu_Crafting") as Node2D);

        foreach (Node2D node in _menus)
        {
            node.Visible = false;
        }

        _menus[0].Visible = true;

        _selected = 0;
        _size = 2;
    }

    public static Tabs operator ++(Tabs target)
    {
        target._menus[target._selected].Visible = false;

        target._selected += 1;

        if (target._selected >= target._size)
        {
            target._selected = 0;
        }

        target._menus[target._selected].Visible = true;

        return target;
    }

    public static Tabs operator --(Tabs target)
    {
        target._menus[target._selected].Visible = false;

        target._selected -= 1;

        if (target._selected < 0)
        {
            target._selected = target._size - 1; ;
        }

        target._menus[target._selected].Visible = true;

        return target;
    }
}