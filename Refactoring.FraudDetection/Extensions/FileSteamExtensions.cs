namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Extensions
{
    using System.IO;

    public static class FileSteamExtensions
    {
        public static byte[] ReadAllBytes(this FileStream stream)
        {
            var maxLength = stream.Length < int.MaxValue ? (int)stream.Length : int.MaxValue;
            var buffer = new byte[maxLength];

            stream.Read(buffer, 0, maxLength);

            return buffer;
        }
    }
}
