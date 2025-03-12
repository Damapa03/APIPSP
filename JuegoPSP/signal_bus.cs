using Godot;
using System;

public partial class signal_bus : Node
{
    internal void Connect(string v, Action<Node, int> on_signal_health_changed)
    {
        throw new NotImplementedException();
    }

    [Signal]public delegate void on_health_changedEventHandler(Node node, int damage);

    private void OnDatoEnviado(string usuario,int dato)
    {
        GD.Print($"Main recibió: {dato}, {usuario}");
    }
}
