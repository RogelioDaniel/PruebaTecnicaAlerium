SELECT 
    p.idPedido, 
    p.NumPedido, 
    p.FechaPedido, 
    p.FechaEntrega, 
    r.FechaCreacion
FROM 
    Pedidos p
JOIN 
    Cotizaciones c ON p.idCotizacion = c.idCotizacion
JOIN 
    Requisiciones r ON c.idRequisicion = r.idRequisicion
WHERE 
    MONTH(r.FechaCreacion) = MONTH(GETDATE()) 
    AND YEAR(r.FechaCreacion) = YEAR(GETDATE());
