namespace AccountService.Domain.ValueObjects;

public enum TipoMovimiento : byte { Deposito = 1, Retiro = 2 }

public static class TipoMovimientoMap
{
    public const string Deposito = "D";
    public const string Retiro = "R";

    public static string ToCode(TipoMovimiento v) =>
        v == TipoMovimiento.Deposito ? Deposito :
        v == TipoMovimiento.Retiro ? Retiro :
        throw new ArgumentOutOfRangeException(nameof(v));

    public static TipoMovimiento FromCode(string v) =>
        v == Deposito ? TipoMovimiento.Deposito :
        v == Retiro ? TipoMovimiento.Retiro :
        throw new ArgumentOutOfRangeException(nameof(v));
}
