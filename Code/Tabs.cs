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
        _menus.Add(GetNode("../Menu_Food") as Node2D);

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

        if (target._selected != 3)
        {
            foreach (Label node in target._menuTotals)
            {
                node.Visible = true;
            }
        }
        else
        {
            foreach (Label node in target._menuTotals)
            {
                node.Visible = false;
            }
        }

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

        if (target._selected != 3)
        {
            foreach (Label node in target._menuTotals)
            {
                node.Visible = true;
            }
        }
        else
        {
            foreach (Label node in target._menuTotals)
            {
                node.Visible = false;
            }
        }

        return target;
    }

    public void Add(int amount)
    {
        //Cooking menu has no total tab yet. This prevents out of bounds exceptions
        if (_selected == 3)
            return;

        int total = _menuTotals[_selected].GetMeta("total", -1).As<int>();

        if (total == -1)
        {
            GD.PushWarning(String.Format("Meta Data 'Total' does not exist in object {0}", Name));
        }

        total += amount;
        _menuTotals[_selected].SetMeta("total", Variant.From(total));

        if (total == 0)
            _menuTotals[_selected].Text = "";
        else
            _menuTotals[_selected].Text = total.ToString();
    }
}