namespace My.ZhiCore;

public interface IZhiCoreExceptionConverter
{
    string TryToLocalizeExceptionMessage(Exception exception);
}