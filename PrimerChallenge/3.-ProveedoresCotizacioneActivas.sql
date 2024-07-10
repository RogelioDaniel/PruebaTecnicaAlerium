SELECT DISTINCT 
    pr.idProveedor, 
    pr.Codigo, 
    pr.RazonSocial, 
    pr.RFC
FROM 
    Proveedores pr
JOIN 
    Cotizaciones c ON pr.idProveedor = c.idProveedor
JOIN 
    Requisiciones r ON c.idRequisicion = r.idRequisicion
LEFT JOIN 
    Pedidos p ON c.idCotizacion = p.idCotizacion
WHERE 
    r.Estatus = 1 
    AND c.Cancelada = 0
    AND p.idPedido IS NULL;
