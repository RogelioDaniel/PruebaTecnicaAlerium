CREATE TABLE Requisiciones (
    idRequisicion INT IDENTITY(1,1) PRIMARY KEY,
    NumRequisicion VARCHAR(50) NOT NULL,
    FechaCreacion DATE NOT NULL,
    Solicitante VARCHAR(100),
    Observaciones TEXT,
    Estatus INT,
    Revisada BIT
);
CREATE TABLE Proveedores (
    idProveedor INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(50) NOT NULL,
    RazonSocial VARCHAR(100),
    RFC VARCHAR(13)
);

CREATE TABLE Cotizaciones (
    idCotizacion INT IDENTITY(1,1) PRIMARY KEY,
    NumCotizacion VARCHAR(50) NOT NULL,
    idRequisicion INT,
    idProveedor INT,
    Observaciones TEXT,
    Cancelada BIT,
    SubTotal DECIMAL(18, 2),
    IVA DECIMAL(18, 2),
    Total DECIMAL(18, 2),
    FOREIGN KEY (idRequisicion) REFERENCES Requisiciones(idRequisicion),
    FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor)
);
CREATE TABLE Pedidos (
    idPedido INT IDENTITY(1,1)  PRIMARY KEY,
    NumPedido VARCHAR(50) NOT NULL,
    idCotizacion INT,
    FechaPedido DATE NOT NULL,
    FechaEntrega DATE,
    Observaciones TEXT,
    RazonSocialFactura VARCHAR(100),
    PesoPedido DECIMAL(18, 2),
    MontoPedido DECIMAL(18, 2),
    SubTotal DECIMAL(18, 2),
    IVA DECIMAL(18, 2),
    TotalPedido DECIMAL(18, 2),
    VoBo VARCHAR(50),
    Autorizo VARCHAR(50),
    Estatus INT,
    Notas TEXT,
    FOREIGN KEY (idCotizacion) REFERENCES Cotizaciones(idCotizacion)
);
