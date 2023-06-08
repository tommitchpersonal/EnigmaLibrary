public class WheelFactory : IWheelFactory
{
    public IWheel BuildWheel(WheelSetting wheelSetting)
    {
        return new Wheel(wheelSetting);
    }
}