namespace KindleNotesConverter.Core;

internal class KindleClassSelectors
{
    public class Notebook
    {
        public static readonly string Title = "bookTitle";
        public static readonly string Author = "authors";
        public static readonly string Chapter = "sectionHeading";

        public class Note
        {
            public static readonly string Title = "noteHeading";
            public static readonly string Content = "noteText";
        }
    }
}
