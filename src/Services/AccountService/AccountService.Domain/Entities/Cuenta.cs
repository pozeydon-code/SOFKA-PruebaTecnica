namespace AccountService.Domain.Entities;

public class Cuenta
{
    public int NumeroCuenta { get; private set; }

    public TipoCuenta Tipo { get; private set; }

    public Saldo SaldoInicial { get; private set; }

    public Saldo SaldoActual { get; private set; }

    public bool Estado { get; private set; }

    public int ClienteId { get; private set; }

    private readonly List<Movimiento> _movimientos = new();

    public IReadOnlyList<Movimiento> Movimientos => _movimientos.AsReadOnly();

    public Cuenta() { }
    public Cuenta(int numeroCuenta, TipoCuenta tipo, Saldo saldoInicial, Saldo saldoActual, bool estado, int clienteId)
    {
        NumeroCuenta = numeroCuenta;
        Tipo = tipo;
        SaldoInicial = saldoInicial;
        SaldoActual = saldoActual;
        Estado = estado;
        ClienteId = clienteId;
    }
    public static Cuenta UpdateCuenta(int numeroCuenta, TipoCuenta tipo, Saldo saldoInicial, Saldo saldoActual, bool estado, int clienteId)
    {
        return new Cuenta(numeroCuenta, tipo, saldoInicial, saldoActual, estado, clienteId);
    }

    public void Depositar(Saldo monto)
    {
        SaldoActual = SaldoActual.AumentarSaldo(monto.Valor);
        _movimientos.Add(new Movimiento(0, DateTime.UtcNow, TipoMovimiento.Deposito, monto, SaldoActual, NumeroCuenta));
    }

    public void Retirar(Saldo monto)
    {
        SaldoActual = SaldoActual.DisminuirSaldo(monto.Valor);
        _movimientos.Add(new Movimiento(0, DateTime.UtcNow, TipoMovimiento.Retiro, monto, SaldoActual, NumeroCuenta));
    }

}
