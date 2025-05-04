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
(1,'Disponible'),
(2,'Suspendido'),
(3,'Agotado')
GO

CREATE TABLE CLIENTES(
	IdCliente int primary key,
	Nombre nvarchar(50),
	Apellido nvarchar(50),
	Documento char(8),
	Telefono nvarchar(20),
	Correo nvarchar(50),
	Direccion nvarchar(100),
	flgEliminado bit
);
GO

CREATE TABLE SERVICIOS(
	IdServicios int primary key,
	Descripcion nvarchar(30)
);

INSERT INTO SERVICIOS VALUES
(1,'Consulta Medica'),
(2, 'Vacunacion'),
(3,'Cirugias'),
(4,'Servicio de peluqueria'),
(5,'Ecografias'),
(6,'Limpieza dental'),
(7,'Rehabilitacion'),
(8,'Alojamiento')
GO

CREATE TABLE VETERINARIOS(
	IdVeterinario int primary key,
	Nombre nvarchar(25),
	Apellido nvarchar(30),
	Especialidad nvarchar(50)
);
GO

INSERT INTO VETERINARIOS VALUES
(1,'Luis', 'Garcia','Cirugia veterinaria'),
(2,'Ana','Martinez','Diagnostico en ecografias'),
(3,'Eduardo', 'Lopez','Diagnostico y tratamiento de enfermades cronicas'),
(4,'Valeria','Sanchez','Fisioterapia y rehabilitacion'),
(5,'Andres','Ramirez','Vacunacion y despacitacion'),
(6,'Carlos','Rodriguez','Odontologo veterinario'),
(7,'Laura','Hernandez','Peluqueria, corte y cuidado estetico'),
(8,'Jose','Gomez','Cirugia veterinaria'),
(9,'Mariana','Perez','Cirugia y esterilizacion'),
(10,'Isabel','Flores','Oncologia veterinaria')
GO

CREATE TABLE SEDES (
	IdSedes int primary key,
	Nombre nvarchar(25),
	Direccion nvarchar(50),
	Telefono nvarchar(10)
);
GO

INSERT INTO SEDES VALUES
(1,'Sede Principal','Jr. Camana C/ Av. Bolivia 148','12345685'),
(2,'Sede Norte','Av. Universitaria 7895-7817, Comas 15314','10547856'),
(3,'Sede Sur','Av. Guardia Civil Sur, Lima 15056','25489645')
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
	flgEliminado bit
);
GO

CREATE TABLE ATENCIONES(
	IdAtencion int primary key,
	Fecha datetime,
	Motivo nvarchar(200),
	Diagnostico nvarchar(200),
	Tratamiento nvarchar(200),
	IdAnimal int foreign key references ANIMALES(IdAnimal),
	flgEliminado bit
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
	Imagen varbinary(MAX),
	Nombre nvarchar(50),
	Categoria int foreign key references CATEGORIA(IdCategoria),
	Precio decimal(10,2),
	stock int,
	flgEstado int foreign key references ESTADO(IdEstado),
	flgEliminado bit
);
GO

INSERT INTO PRODUCTO (IdProducto, Imagen, Nombre, Categoria, Precio, Stock, flgEstado, flgEliminado) VALUES
(1, NULL, 'Antipulgas Frontline', 1, 30.00, 50, 1, 0),
(2, NULL, 'Kit de Primeros Auxilios para Mascotas', 1, 55.00, 50, 1, 0),
(3, NULL, 'Pipetas Revolution', 1, 25.00, 50, 1, 0),
(4, NULL, 'Antiparasitario Interno Drontal Plus', 1, 25.00, 50, 1, 0),
(5, NULL, 'Alimento Royal Canin Gato Esterilizado', 2, 35.00, 50, 1, 0),
(6, NULL, 'Croquetas Hill’s Science Diet', 2, 30.00, 50, 1, 0),
(7, NULL, 'Golosinas Pedigree Dentastix', 2, 25.00, 50, 1, 0),
(8, NULL, 'Suplemento Nutri-Vet Multi-Vitamina', 2, 25.00, 50, 1, 0),
(9, NULL, 'Cama Acolchonada para Mascotas', 3, 30.00, 50, 1, 0),
(10, NULL, 'Jaula de Transporte Mediana', 3, 25.00, 50, 1, 0),
(11, NULL, 'Arnés de Paseo Ajustable', 3, 20.00, 50, 1, 0),
(12, NULL, 'Comedero Doble de Acero Inoxidable', 3, 15.00, 50, 1, 0),
(13, NULL, 'Cepillo Dental para Perros y Gatos', 4, 10.00, 50, 1, 0),
(14, NULL, 'Shampoo Dermocanis', 4, 25.00, 50, 1, 0),
(15, NULL, 'Arena Sanitaria Aglomerante para Gatos', 4, 20.00, 50, 1, 0),
(16, NULL, 'Peine Quitapulgas de Acero', 4, 10.00, 50, 1, 0),
(17, NULL, 'Pelota de Goma para Perros', 5, 8.00, 50, 1, 0),
(18, NULL, 'Juguete Mordedor Dental', 5, 12.00, 50, 1, 0),
(19, NULL, 'Ratón de Peluche con Catnip', 5, 18.00, 50, 1, 0),
(20, NULL, 'Juguete Interactivo con Sonido', 5, 16.00, 50, 1, 0);

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
	IdEstado,Descripcion
	FROM ESTADO
END
GO

CREATE OR ALTER PROC usp_listar_categoria
AS
BEGIN
	SELECT
	IdCategoria,Descripcion
	FROM CATEGORIA
END
GO


CREATE OR ALTER PROC usp_listar_productos_mantenimiento
AS
BEGIN
	SELECT
	PROD.IdProducto,
	PROD.Nombre,
	PROD.Imagen,
	CAT.IdCategoria,
	CAt.Descripcion,
	PROD.Precio,
	PROD.stock,
	EST.IdEstado,
	EST.Descripcion,
	Prod.flgEliminado
	FROM PRODUCTO PROD JOIN CATEGORIA CAT
	ON CAT.IdCategoria = PROD.Categoria JOIN ESTADO EST
	ON EST.IdEstado = PROD.flgEstado
	Where flgEliminado = 0
END
GO

EXEC usp_listar_productos_mantenimiento
GO


CREATE OR ALTER PROC usp_insertar_productos
@nombre VARCHAR(50),
@imagen VARBINARY(MAX),
@categoria INT,
@precio DECIMAL(10,2),
@stock INT,
@flgEstado INT
AS
BEGIN
    DECLARE @idprod INT = ISNULL((SELECT MAX(IdProducto) FROM PRODUCTO), 0) + 1;

    INSERT INTO PRODUCTO (IdProducto,Nombre,Imagen,Categoria,Precio,Stock,flgEstado,flgEliminado)
    VALUES (@idprod,@nombre,@imagen,@categoria,@precio,@stock,@flgEstado,0);
END
GO

CREATE OR ALTER PROC usp_actualiza_productos
@idprod int,
@nombre VARCHAR(50),
@imagen VARBINARY(MAX),
@categoria INT,
@precio DECIMAL(10,2),
@stock INT,
@flgEstado INT
AS
BEGIN
	UPDATE PRODUCTO 
	SET Nombre = @nombre, Imagen = @imagen,Categoria=@categoria,Precio=@precio,
	stock=@stock, flgEstado = @flgEstado
	WHERE IdProducto = @idprod
END
GO

exec usp_actualiza_productos 1, 'Antipulgas Frontline', null, 3, 15.20, 10, 3
go

CREATE OR ALTER PROC usp_elimina_productos
@idprod int
AS
BEGIN
	UPDATE PRODUCTO
	SET flgEliminado = 1
	WHERE IdProducto=@idprod
END
GO


exec usp_elimina_productos 1
go


/* Procedures index*/
CREATE OR ALTER PROC usp_list_veterinarios
AS
	SELECT IdVeterinario, Nombre,Apellido, Especialidad
	FROM VETERINARIOS
Go

CREATE OR ALTER PROC usp_list_servicios
AS
	SELECT Top 5 IdServicios, Descripcion
	FROM SERVICIOS
Go

CREATE OR ALTER PROC usp_list_sedes
AS
	SELECT IdSedes, Nombre, Direccion, Telefono
	FROM SEDES
Go
