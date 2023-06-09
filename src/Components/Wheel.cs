using log4net;

public class Wheel : IWheel
{
    private int[]? _currentState;
    private int _numberOfTimesRotated;
    private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public Wheel(WheelSetting settings)
    {
        _currentState = settings.Mappings;
    }

    public int Map(int input)
    {
        var output = _currentState![input];
        _log.Info($"Output from wheel: {output}");   
        return _currentState![input];
    }

    public int ReverseMap(int input)
    {
        int output = 0;

        for (var i = 0; i < _currentState!.Length; i ++)
        {
            if (_currentState[i] == input)
            {
                output = i;
            }
        }

        _log.Info($"Output from reverse wheel: {output}");   
        return output;
    }

    public void Rotate()
    {
        int[] newState = new int[_currentState!.Length];

        for (int i = 0; i < _currentState!.Length - 1; i++)
        {
            newState[i + 1] = _currentState![i];
        }
        
        newState[0] = _currentState.Last();
        _currentState = newState;
        
        if (_numberOfTimesRotated != 25)
        {
            _numberOfTimesRotated ++;
        }
        else
        {
            _numberOfTimesRotated = 0;
        }

    }

    public void Reset()
    {
        var numberOfTimesToRotateBack = _numberOfTimesRotated;

        for (int i = 0; i < 26 - numberOfTimesToRotateBack; i++)
        {
            Rotate();
        }

        _numberOfTimesRotated = 0;
    }

    private void CheckWhetherCurrentStateIsNull()
    {
        if (_currentState == null)
        {
            throw new EncryptionException("Current state of wheel is null!");
        }
    }
}