namespace Utilities
{
    public enum PlayerState
    {
        WALKING,
        INTERACTING,
    }

    public enum InterActionType
    {
        PICKUP,
        WORKSTATION,
    }

    public enum StationType
    {
        REPAIR,
        RECYCLE,
        COMPLETED,
    }

    public enum PickUpState
    {
        SCRAP,
        COMPLETED,
        DESTROYED,
    }

    public enum PickUpObjectType
    {
        NONE,
        ARM,
        SHOULDER,
        BATTERY,
        PLATE,
    }
}