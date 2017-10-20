namespace TextSplitter
{
    public class TextSplitter
    {
        public string[] Split(string text)
        {
            return text.Split(new char[] {'\n', '\r', '\t', ' '});
        }
    }
}