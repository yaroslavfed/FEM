namespace VectorFEM.Data;

/// <summary>
/// Структура ребра конечного элемента
/// </summary>
/// <param name="Number">Номер ребра в конечном элементе</param>
/// <param name="First">Первый узел ребра</param>
/// <param name="Second">Второй узел ребра</param>
public record Edge(
    int Number,
    Node First,
    Node Second
)
{
    public override string ToString() =>
        $"{Number}\t|\t({First.X}, {First.Y}, {First.Z}) \t\t\t ({Second.X}, {Second.Y}, {Second.Z})";
}