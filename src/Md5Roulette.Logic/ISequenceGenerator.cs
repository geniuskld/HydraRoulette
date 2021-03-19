namespace Md5Roulette.Logic
{
    public interface ISequenceGenerator
    {
        string GetSequence(int minNumber, int maxNumber);
    }
}