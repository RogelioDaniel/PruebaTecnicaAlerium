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
    Pedidos p ON c.idCotizacion = p.idCotizacion
WHERE 
    p.Estatus = 1;

