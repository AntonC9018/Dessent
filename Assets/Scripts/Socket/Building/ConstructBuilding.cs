
using UnityEngine;

public class ConstructBuildingRequest : Request
{
    public BuildingName type;
    public Vector2Int coord;

    public ConstructBuildingRequest()
    {
        headerName = HeaderName.ConstructBuilding;
    }
}


public class ConstructBuildingResponse : Response
{
    public Ack ack;
    public BuildingName type;
    public Vector2Int coord;
    public ApplyIlluminateBuffResponse illuminate;

    public ConstructBuildingResponse()
    {
        headerName = HeaderName.ConstructBuilding;
    }
}



public class ConstructBuildingPacket : Packet
{
    public BuildingName type;
    public Vector2Int coord;

    public ConstructBuildingPacket()
    {
        headerName = HeaderName.ConstructBuilding;
    }
}