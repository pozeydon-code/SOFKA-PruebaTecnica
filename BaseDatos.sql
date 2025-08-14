-- Al ser un esquema que viene del dominio no de la base al proyecto decidi extraer el esquema que se genera despues de la primera migracion
-- de esa forma se evitaria alguna referencia no funcional.
CREATE DATABASE banking;

CREATE TABLE banking.dbo.Clientes (
	ClienteId int IDENTITY(1,1) NOT NULL,
	Password nvarchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	Estado bit NOT NULL,
	Nombre nvarchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	Genero nvarchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	Edad int NOT NULL,
	Identificacion nvarchar(450) COLLATE Latin1_General_CI_AS NOT NULL,
	Direccion nvarchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	Telefono nvarchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT PK_Clientes PRIMARY KEY (ClienteId)
);
 CREATE  UNIQUE NONCLUSTERED INDEX IX_Clientes_Identificacion ON dbo.Clientes (  Identificacion ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

CREATE TABLE banking.dbo.Cuentas (
	NumeroCuenta int IDENTITY(1,1) NOT NULL,
	Tipo char(1) COLLATE Latin1_General_CI_AS NOT NULL,
	SaldoInicial decimal(18,2) NOT NULL,
	SaldoActual decimal(18,2) NOT NULL,
	Estado bit NOT NULL,
	ClienteId int NOT NULL,
	CONSTRAINT PK_Cuentas PRIMARY KEY (NumeroCuenta)
);

CREATE TABLE banking.dbo.Movimientos (
	Id int IDENTITY(1,1) NOT NULL,
	Fecha datetime2 NOT NULL,
	Tipo char(1) COLLATE Latin1_General_CI_AS NOT NULL,
	Valor decimal(18,2) NOT NULL,
	SaldoPosterior decimal(18,2) NOT NULL,
	NumeroCuenta int NOT NULL,
	CONSTRAINT PK_Movimientos PRIMARY KEY (Id),
	CONSTRAINT FK_Movimientos_Cuentas_NumeroCuenta FOREIGN KEY (NumeroCuenta) REFERENCES banking.dbo.Cuentas(NumeroCuenta) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Movimientos_NumeroCuenta ON dbo.Movimientos (  NumeroCuenta ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
