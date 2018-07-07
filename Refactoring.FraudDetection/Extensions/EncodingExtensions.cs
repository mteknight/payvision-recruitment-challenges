namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Extensions
{
    using System.Text;

    public static class EncodingExtensions
    {
        public static string GetCleanString(this Encoding encoding, byte[] buffer)
        {
            return encoding.GetString(buffer)
                .Trim('\uFEFF', '\u200B', '\u202f', '\u205f');
        }
    }
}
