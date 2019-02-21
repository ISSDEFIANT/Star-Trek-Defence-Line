namespace Model.Ship
{
    public interface IShipObject : IWorldObject
    {
        ShipTypeBase GetShipType();
        string GetName();
    }
}