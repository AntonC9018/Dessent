
public enum HeaderName
{
    ApplySpell, //! request + response, packet (for some spells) 
    ApplyBuff, //! request + response, packet if vision
    ApplyHit, // just response
    ConstructBuilding, //! request, packet if vision
    UpgradeBuilding, //! request, packet if vision
    //LowerGround, //(?) request + response, packet if vision
    //RaiseGround, //(?) request + response, packet if vision
    // ApplyBonus, // response to ConstructBuilding, (?) packet to opponent
    //IlluminationSelf, //! request + response with data about bonuses of the cell, no packet
    //IlluminationOpponent, //! request + response with data about cell, no packet
    SpendMana, //(ex for mp) request, no packet
    EndTurn, // request + response,(?) packet
    StartTurn, // packet
}


public enum Ack
{
    Success,
    Warning,
    Failure,
    Discrepancy,
    Error
}