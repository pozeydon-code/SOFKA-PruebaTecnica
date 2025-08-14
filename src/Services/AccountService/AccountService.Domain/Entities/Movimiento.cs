namespace AccountService.Domain.Entities;

public class Movimiento
{
    public int Id { get; private set; }

    public DateTime Fecha { get; private set; }

    public TipoMovimiento Tipo { get; private set; }

    public Saldo Valor { get; private set; }

    public Saldo SaldoPosterior { get; private set; }
    public int NumeroCuenta { get; private set; }
    public Cuenta Cuenta { get; private set; }


    public Movimiento() { }
    public Movimiento(int id, DateTime fecha, TipoMovimiento tipo, Saldo valor, Saldo saldoPosterior, int numeroCuenta)
    {
        Id = id;
        Fecha = fecha;
        Tipo = tipo;
        Valor = valor;
        SaldoPosterior = saldoPosterior;
        NumeroCuenta = numeroCuenta;
    }

    public static Movimiento UpdateMovimiento(int id, DateTime fecha, TipoMovimiento tipo, Saldo valor, Saldo saldoPosterior, int numeroCuenta)
    {
        return new Movimiento(id, fecha, tipo, valor, saldoPosterior, numeroCuenta);
    }

}
