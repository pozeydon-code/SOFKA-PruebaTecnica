namespace AccountService.Domain.ValueObjects;
public enum TipoCuenta : byte { Ahorro = 1, Corriente = 2 }

public static class TipoCuentaMap
{
    public const string Ahorro = "A";
    public const string Credito = "C";

    public static string ToCode(TipoCuenta v) =>
        v == TipoCuenta.Ahorro ? Ahorro :
        v == TipoCuenta.Corriente ? Credito :
        throw new ArgumentOutOfRangeException(nameof(v));

    public static TipoCuenta FromCode(string v) =>
        v == Ahorro ? TipoCuenta.Ahorro :
        v == Credito ? TipoCuenta.Corriente :
        throw new ArgumentOutOfRangeException(nameof(v));
}

