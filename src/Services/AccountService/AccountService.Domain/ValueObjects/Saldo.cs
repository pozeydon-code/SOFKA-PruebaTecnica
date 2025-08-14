namespace AccountService.Domain.ValueObjects;

public partial class Saldo
{
    public decimal Valor { get; init; }

    private Saldo(decimal valor) => Valor = valor;

    public static Saldo? Create(decimal valor)
    {
        if (valor < 0)
            return null;

        return new Saldo(valor);
    }

    public Saldo? AumentarSaldo(decimal cantidad)
    {
        if (cantidad < 0)
        {
            throw new ArgumentException("La cantidad a aumentar no puede ser negativa.");
        }

        return new Saldo(Valor + cantidad);
    }

    public Saldo? DisminuirSaldo(decimal cantidad)
    {
        if (cantidad < 0 || Valor - cantidad < 0)
        {
            throw new InvalidOperationException("No se puede disminuir el saldo por debajo de cero.");
        }

        return new Saldo(Valor - cantidad);
    }
    public static implicit operator decimal(Saldo cantidad) => cantidad.Valor;

    public override string ToString() => Valor.ToString("N2");
}
