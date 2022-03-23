using System.Runtime.CompilerServices;

namespace KindleNotesConverter.Core.Converters;

public interface IMarkdownConverter<T> where T : class
{
    string Convert(T model, [CallerArgumentExpression("model")] string message = "");
}
