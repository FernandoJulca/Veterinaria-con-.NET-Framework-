USE MASTER
GO

if db_id('Veterinaria') is not null 
begin 
	drop database Veterinaria
end
go

CREATE DATABASE Veterinaria
GO


USE Veterinaria
GO

CREATE TABLE ESTADO(
	IdEstado int primary key,
	Descripcion varchar(15)
);
GO

INSERT INTO ESTADO VALUES
(1,'activo'),
(2,'eliminado')
GO

CREATE TABLE CLIENTES(
	IdCliente int primary key,
	Nombre nvarchar(50),
	Apellido nvarchar(50),
	Documento char(8),
	Telefono nvarchar(20),
	Correo nvarchar(50),
	Direccion nvarchar(100),
	flgEstado int foreign key references ESTADO(IdEstado)
);
GO

CREATE TABLE ESPECIE(
	IdEspecie int primary key,
	Descripcion nvarchar(50)
);
GO

CREATE TABLE ANIMALES(
	IdAnimal int primary key,
	Nombre nvarchar(50),
	Especie int foreign key references ESPECIE(IdEspecie),
	Raza nvarchar(50),
	Sexo char(1),
	Edad int,
	IdCliente int foreign key references CLIENTES(IdCliente),
	flgEstado int foreign key references ESTADO(IdEstado)
);
GO

CREATE TABLE ATENCIONES(
	IdAtencion int primary key,
	Fecha datetime,
	Motivo nvarchar(200),
	Diagnostico nvarchar(200),
	Tratamiento nvarchar(200),
	IdAnimal int foreign key references ANIMALES(IdAnimal),
	flgEstado int foreign key references ESTADO(IdEstado)
);
GO


CREATE TABLE CATEGORIA(
	IdCategoria int primary key,
	Descripcion nvarchar(50)
);
GO

INSERT INTO CATEGORIA VALUES
(1,'Medicina'),
(2,'Alimentación'),
(3,'Accesorios'),
(4,'Higiene'),
(5,'Juguetes')
GO

CREATE TABLE PRODUCTO(
	IdProducto int primary key,
	Nombre nvarchar(50),
	Categoria int foreign key references CATEGORIA(IdCategoria),
	Precio decimal(10,2),
	stock int,
	flgEstado int foreign key references ESTADO(IdEstado)
);
GO

INSERT INTO PRODUCTO VALUES
(1,'Antipulgas Frontline',1,30.00,50,1),
(2,'Kit de Primeros Auxilios para Mascotas',1, 55.00,50,1),
(3,'Pipetas Revolution',1, 25.00,50,1),
(4,'Antiparasitario Interno Drontal Plus',1, 25.00,50,1),
(5,'Alimento Royal Canin Gato Esterilizado',2, 35.00,50,1),
(6,'Croquetas Hill’s Science Diet',2, 30.00,50,1),
(7,'Golosinas Pedigree Dentastix',2, 25.00,50,1),
(8,'Suplemento Nutri-Vet Multi-Vitamina',2, 25.00,50,1),
(9,'Cama Acolchonada para Mascotas',3, 30.00,50,1),
(10,'Jaula de Transporte Mediana',3, 25.00,50,1),
(11,'Arnés de Paseo Ajustable',3,20.00,50,1),
(12,'Comedero Doble de Acero Inoxidable',3, 15.00,50,1),
(13,'Cepillo Dental para Perros y Gatos',4, 10.00,50,1),
(14,'Shampoo Dermocanis',4, 25.00,50,1),
(15,'Arena Sanitaria Aglomerante para Gatos',4, 20.00,50,1),
(16,'Peine Quitapulgas de Acero',4, 10.00,50,1),
(17,'Pelota de Goma para Perros',5, 8.00,50,1),
(18,'Juguete Mordedor Dental',5, 12.00,50,1),
(19,'Ratón de Peluche con Catnip',5, 18.00,50,1),
(20,'Juguete Interactivo con Sonido',5, 16.00,50,1)
GO


CREATE TABLE VENTAS(
	IdVenta int primary key,
	Fecha datetime,
	Total decimal(10,2),
	IdCliente int foreign key references CLIENTES(IdCliente)
);
GO

CREATE TABLE DETALLEVENTA(
	IdDetalle int primary key,
	IdVenta int foreign key references VENTAS(IdVenta),
	IdProducto int foreign key references PRODUCTO(IdProducto),
	Cantidad int,
	Precio decimal(10,2)
);
GO

CREATE OR ALTER PROC usp_listar_estado
AS
BEGIN
	SELECT
	*
	FROM ESTADO
END
GO

CREATE OR ALTER PROC usp_listar_categoria
AS
BEGIN
	SELECT
	*
	FROM CATEGORIA
END
GO


CREATE OR ALTER PROC usp_listar_productos_mantenimiento
AS
BEGIN
	SELECT
	PROD.IdProducto,
	PROD.Nombre,
	CAT.IdCategoria,
	
	PROD.Precio,
	PROD.stock,
	EST.IdEstado
	
	FROM PRODUCTO PROD INNER JOIN CATEGORIA CAT
	ON CAT.IdCategoria = PROD.Categoria INNER JOIN ESTADO EST
	ON EST.IdEstado = PROD.flgEstado
END
GO

EXEC usp_listar_productos_mantenimiento
GO


CREATE OR ALTER PROC usp_insertar_productos
@nombre varchar(50),
@categoria int,
@precio decimal(10,2),
@stock int,
@flgEstado int
AS
BEGIN
	DECLARE @idprod int = isnull((select max (IdProducto) from PRODUCTO),0) + 1
	INSERT INTO PRODUCTO VALUES
	(@idprod,@nombre,@categoria,@precio,@stock,1)
END
GO


CREATE OR ALTER PROC usp_actualiza_productos
@idprod int,
@nombre varchar(50),
@categoria int,
@precio decimal(10,2),
@stock int
AS
BEGIN
	UPDATE PRODUCTO 
	SET Nombre = @nombre, Categoria=@categoria,Precio=@precio,
	stock=@stock
	WHERE IdProducto = @idprod
END
GO

CREATE OR ALTER PROC usp_elimina_productos
@idprod int
AS
BEGIN
	UPDATE PRODUCTO
	SET flgEstado = 2
	WHERE IdProducto=@idprod
END
GO


exec usp_elimina_productos 1
go

select * from PRODUCTO