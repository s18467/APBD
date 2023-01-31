using StudentVisor.Utils;
using System.Text;
using System.Text.Json;

namespace StudentVisor;

/// <summary>
/// Interface for parsing data from file to object and vice versa
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDataParser<T> where T : class
{
    /// <summary>
    /// Zależnie od typu Format  wszystkie metody tej klasy będą zwracały wyniki zgodnie z podanym formatem.
    /// </summary>
    public FormatType Format { get; set; }

    /// <summary>
    /// Konwertuje podaną listę obiektów do łańcucha znaków.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ConvertToStringFormat(List<T> entities)
    {
        var stringData = Format switch
        {
            FormatType.CSV => ToCsv(entities),
            FormatType.JSON => JsonSerializer.Serialize(entities),
            _ => throw new ArgumentOutOfRangeException(nameof(Format), Format, null)
        };
        return stringData;
    }

    private string ToCsv(IEnumerable<T> entities)
    {
        var csv = new StringBuilder();
        foreach (var line in entities.Select(ConvertToStringFormat))
        {
            csv.Append(line);
        }
        return csv.ToString();
    }

    /// <summary>
    /// Konwertuje podany łańcuch znaków do listy obiektów.
    /// </summary>
    /// <param name="stringData"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public List<T> ConvertToList(string stringData)
    {
        var list = Format switch
        {
            FormatType.CSV => stringData.Split(Environment.NewLine).Select(ConvertToObject).Where(z => z != null).ToList(),
            FormatType.JSON => JsonSerializer.Deserialize<List<T>>(stringData) ?? new List<T>(),
            _ => throw new ArgumentOutOfRangeException(nameof(Format), Format, null)
        };
        return list;
    }

    /// <summary>
    /// Zależnie od Format zwraca tekstowa wartosc podanego obiektu
    /// </summary>
    /// <param name="entity">Obiekt do serializacji</param>
    /// <returns>Tekstowa forma reprezentacji obiektu</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ConvertToStringFormat(T entity);

    /// <summary>
    /// Zależnie od Format zwraca obiekt na podstawie podanego łańcucha znaków
    /// </summary>
    /// <param name="stringData">zserialiazowany obiekt w formacie Format</param>
    /// <returns>Zdeserializowany obiekt</returns>
    public T? ConvertToObject(string stringData);
}