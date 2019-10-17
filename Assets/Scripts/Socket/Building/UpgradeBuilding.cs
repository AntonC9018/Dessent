
using UnityEngine;

public class UpgradeBuildingRequest : Request
{
    // for validation
    public int currentLevel;
    public Vector2Int coord;

    public UpgradeBuildingRequest()
    {
        headerName = HeaderName.UpgradeBuilding;
    }
}


public class UpgradeBuildingResponse : Response
{
    public Ack ack;
    public int currentLevel;
    public Vector2Int coord;

    public UpgradeBuildingResponse()
    {
        headerName = HeaderName.UpgradeBuilding;
    }
}



public class UpgradeBuildingPacket : Packet
{
    public int currentLevel;
    public Vector2Int coord;

    public UpgradeBuildingPacket()
    {
        headerName = HeaderName.UpgradeBuilding;
    }
}