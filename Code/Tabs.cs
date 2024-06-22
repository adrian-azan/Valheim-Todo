using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class Tabs : Node2D
{
    private int _selected;
    private int _size;

    private Godot.Collections.Array<Node2D> _menus;

    private Array<Label> _menuTotals;

    public override void _Ready()
    {
        _menus = new Godot.Collections.Array<Node2D>();
        _menus.Add(GetNode("../Menu_Misc") as Node2D);
        _menus.Add(GetNode("../Menu_Crafting") as Node2D);
        _menus.Add(GetNode("../Menu_Furniture") as Node2D);

        _menuTotals = new Array<Label>();
        _menuTotals.Add(GetNode("Misc/Total") as Label);
        _menuTotals.Add(GetNode("Crafting/Total") as Label);
        _menuTotals.Add(GetNode("Furniture/Total") as Label);

        foreach (Node2D node in _menus)
        {
            node.Visible = false;
        }

        _menus[0].Visible = true;

        _selected = 0;
        _size = _menus.Count;

        GetNode<CustomSignals>("/root/CustomSignals").AddToTotal += Add;
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

    public void Add()
    {
        int total = GetMeta("total", -1).As<int>();

        if (total == -1)
        {
            GD.PushWarning(String.Format("Meta Data 'total' does not exist in object {0}", Name));
        }

        SetMeta("total", Variant.From(total));
        _menuTotals[_selected].Text = total.ToString();
    }

    public void Remove()
    {
    }
}