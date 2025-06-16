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

CREATE TABLE CLIENTES (
    IdCliente INT PRIMARY KEY,
    Nombre NVARCHAR(50),
    Apellido NVARCHAR(50),
    Documento CHAR(8),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(50),
    Contrasenia NVARCHAR(50),
    Direccion NVARCHAR(100),
    Tipo CHAR(1) CHECK (Tipo IN ('C', 'A')),
    flgEliminado BIT
);
GO

INSERT INTO CLIENTES (IdCliente, Nombre, Apellido, Documento, Telefono, Correo, Contrasenia, Direccion, Tipo, flgEliminado)
VALUES (1, 'Ana', 'Ramirez', '12345678', '987654321', 'ana.ramirez@mail.com', 'Pass@123', 'Calle 123, Lima', 'A', 0);

INSERT INTO CLIENTES (IdCliente, Nombre, Apellido, Documento, Telefono, Correo, Contrasenia, Direccion, Tipo, flgEliminado)
VALUES (2, 'Luis', 'Garcia', '87654321', '912345678', 'luis.garcia@mail.com', 'Seguro#2024', 'Av. Los Pinos 456', 'A', 0);

INSERT INTO CLIENTES (IdCliente, Nombre, Apellido, Documento, Telefono, Correo, Contrasenia, Direccion, Tipo, flgEliminado)
VALUES (3, 'Maria', 'Fernandez', '11223344', '998877665', 'maria.f@mail.com', 'Clave!789', 'Jr. Las Flores 789', 'C', 0);

INSERT INTO CLIENTES (IdCliente, Nombre, Apellido, Documento, Telefono, Correo, Contrasenia, Direccion, Tipo, flgEliminado)
VALUES (4, 'Carlos', 'Quispe', '44556677', '911223344', 'carlosq@mail.com', 'MiClaveSegura', 'Pasaje Central 321', 'C', 0);

INSERT INTO CLIENTES (IdCliente, Nombre, Apellido, Documento, Telefono, Correo, Contrasenia, Direccion, Tipo, flgEliminado)
VALUES (5, 'Lucia', 'Mendoza', '55667788', '900112233', 'lucia.m@mail.com', 'Lucia#2025', 'Avenida America 654', 'C', 0);

CREATE TABLE SERVICIOS(
	IdServicios int primary key,
	Descripcion nvarchar(30),
	flgEliminado bit
);

INSERT INTO SERVICIOS VALUES
(1,'Consulta Medica',0),
(2, 'Vacunacion',0),
(3,'Cirugias',0),
(4,'Servicio de peluqueria',0),
(5,'Ecografias',0),
(6,'Limpieza dental',0),
(7,'Rehabilitacion',0),
(8,'Alojamiento',0)
GO

CREATE TABLE ESPECIALIDADES (
	IdEspecialidad INT PRIMARY KEY,
	Nombre NVARCHAR(50),
	flg_Eliminado BIT
)
GO

INSERT INTO ESPECIALIDADES VALUES
(1, 'Cirugía veterinaria', 0),
(2, 'Diagnóstico en ecografías', 0),
(3, 'Tratamiento de enfermedades crónicas', 0),
(4, 'Fisioterapia y rehabilitación', 0),
(5, 'Vacunación y desparasitación', 0),
(6, 'Odontología veterinaria', 0),
(7, 'Estética y peluquería', 0),
(8, 'Esterilización', 0),
(9, 'Oncología veterinaria', 0);
GO

CREATE TABLE VETERINARIOS (
	IdVeterinario INT PRIMARY KEY,
	Nombre NVARCHAR(25),
	Apellido NVARCHAR(30),
	Imagen varbinary(MAX),
	IdEspecialidad INT,
	flg_Eliminado BIT,
	FOREIGN KEY (IdEspecialidad) REFERENCES ESPECIALIDADES(IdEspecialidad)
)
GO

INSERT INTO VETERINARIOS VALUES
(1,'Luis', 'Garcia',Null,1,0),
(2,'Ana','Martinez',Null,2,0),
(3,'Eduardo', 'Lopez',Null,3,0),
(4,'Valeria','Sanchez',Null,4,0),
(5,'Andres','Ramirez',Null,5,0),
(6,'Carlos','Rodriguez',Null,6,0),
(7,'Laura','Hernandez',Null,7,0),
(8,'Jose','Gomez',Null,1,0),
(9,'Mariana','Perez',Null,8,0),
(10,'Isabel','Flores',Null,9,0);
GO

CREATE TABLE SEDES (
	IdSedes int primary key,
	Nombre nvarchar(25),
	Imagen varbinary(MAX),
	Direccion nvarchar(50),
	Telefono nvarchar(10),
	flgEliminado bit
);
GO

INSERT INTO SEDES VALUES
(1,'Sede Principal',null,'Jr. Camana C/ Av. Bolivia 148','12345685',0),
(2,'Sede Norte',null,'Av. Universitaria 7895-7817, Comas 15314','10547856',0),
(3,'Sede Sur',null,'Av. Guardia Civil Sur, Lima 15056','25489645',0)
GO

CREATE TABLE ESPECIE(
	IdEspecie int primary key,
	Descripcion nvarchar(50),
	flgEliminado bit
);
GO

INSERT INTO ESPECIE (IdEspecie, Descripcion, flgEliminado) VALUES
(1, 'Perro', 0),
(2, 'Gato', 0),
(3, 'Conejo', 0),
(4, 'Hamster', 0),
(5, 'Cobaya', 0),
(6, 'Hurón', 0)
GO

CREATE TABLE RAZA (
	IdRaza INT PRIMARY KEY,
	Descripcion NVARCHAR(50),
	IdEspecie INT FOREIGN KEY REFERENCES ESPECIE(IdEspecie),
	flgEliminado BIT
);
GO

INSERT INTO RAZA (IdRaza, Descripcion, IdEspecie, flgEliminado) VALUES
(1, 'Labrador Retriever', 1, 0),
(2, 'Bulldog', 1, 0),
(3, 'Golden Retriever', 1, 0),
(4, 'Pastor Alemán', 1, 0),
(5, 'Siames', 2, 0),
(6, 'Persa', 2, 0),
(7, 'Maine Coon', 2, 0),
(8, 'Angora', 3, 0),
(9, 'Cabeza de León', 3, 0),
(10, 'Ruso', 4, 0),
(12, 'Cobaya Común', 5, 0),
(13, 'Hurón Europeo', 6, 0)
GO


CREATE TABLE ANIMALES(
	IdAnimal int primary key,
	Nombre nvarchar(50),
	Especie int foreign key references ESPECIE(IdEspecie),
	IdRaza INT FOREIGN KEY REFERENCES RAZA(IdRaza),
	Sexo char(1),
	Edad int,
	IdCliente int foreign key references CLIENTES(IdCliente),
	flgEliminado bit
);
GO

INSERT INTO ANIMALES (IdAnimal, Nombre, Especie, IdRaza, Sexo, Edad, IdCliente, flgEliminado) VALUES
(1, 'Fido', 1, 1, 'M', 4, 1, 0),
(2, 'Luna', 2, 6, 'F', 3, 2, 0),
(3, 'Bunny', 3, 9, 'F', 2, 3, 0),
(4, 'Toby', 1, 3, 'M', 5, 1, 0),
(5, 'Misi', 2, 5, 'F', 1, 2, 0),
(6, 'Rex', 1, 4, 'M', 6, 3, 0),
(7, 'Nina', 5, 12, 'F', 2, 1, 0),
(8, 'Pipo', 4, 10, 'M', 1, 2, 0),
(9, 'Hurón', 6, 13, 'M', 3, 3, 0),
(10, 'Coco', 1, 2, 'M', 2, 1, 0);
GO

CREATE TABLE ATENCIONES (
	IdAtencion INT PRIMARY KEY,
	Fecha DATETIME,
	Motivo NVARCHAR(200),
	Diagnostico NVARCHAR(200),
	Tratamiento NVARCHAR(200),
	IdAnimal INT FOREIGN KEY REFERENCES ANIMALES(IdAnimal),
	IdServicio INT FOREIGN KEY REFERENCES SERVICIOS(IdServicios),
	IdVeterinario INT FOREIGN KEY REFERENCES VETERINARIOS(IdVeterinario),
	flgEliminado BIT
);
GO


CREATE TABLE CATEGORIA(
	IdCategoria int primary key,
	Descripcion nvarchar(50),
	flgEliminado bit
);
GO

INSERT INTO CATEGORIA VALUES
(1,'Medicina',0),
(2,'Alimentación',0),
(3,'Accesorios',0),
(4,'Higiene',0),
(5,'Juguetes',0)
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
go

CREATE TABLE VENTAS(
	IdVenta int primary key,
	Fecha datetime not null default getdate(),
	Total decimal(10,2),
	IdCliente int foreign key references CLIENTES(IdCliente)
);
GO

CREATE TABLE DETALLEVENTA(
	IdDetalle int primary key,
	IdVenta int foreign key references VENTAS(IdVenta),
	IdProducto int foreign key references PRODUCTO(IdProducto),
	Cantidad int not null,
	Precio decimal(10,2) not null
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

CREATE OR ALTER PROC usp_listar_productos_x_categoria
	@idCategoria int
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
	Where CAT.IdCategoria = @idCategoria And PROD.flgEliminado = 0
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
	Where PROD.flgEliminado = 0
END
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

CREATE OR ALTER PROC usp_elimina_productos
@idprod int
AS
BEGIN
	UPDATE PRODUCTO
	SET flgEliminado = 1
	WHERE IdProducto=@idprod
END
GO

/* Procedures de session */
CREATE OR ALTER PROC usp_iniciar_session
	@Correo NVARCHAR(50),
    @Contrasenia NVARCHAR(50)
AS
BEGIN
	Select IdCliente, Nombre,Apellido,Documento,Telefono,
		Correo,Contrasenia,Direccion,Tipo
	from CLIENTES
	where Correo = @Correo And Contrasenia = @Contrasenia
END
GO

CREATE OR ALTER PROC usp_registrar_usuario
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Documento CHAR(8),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(50),
    @Contrasenia NVARCHAR(50),
	@Direccion NVARCHAR(100),
	@Mensaje varchar(255) OUTPUT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM CLIENTES WHERE Documento = @Documento)
		BEGIN
			SET @Mensaje = 'El Nro Documento ya existe en el sistema';
			Return;
		END
	ELSE
		BEGIN
			DECLARE @IdCliente INT = ISNULL((SELECT MAX(IdCliente) FROM CLIENTES), 0) + 1;
			INSERT INTO CLIENTES(IdCliente,Nombre,Apellido,Documento,Telefono,Correo,Contrasenia,Direccion,Tipo,flgEliminado)
			VALUES(@IdCliente,@Nombre,@Apellido,@Documento,@Telefono,@Correo,@Contrasenia,@Direccion,'C',0)
			SET @Mensaje = 'Usuario registrado correctamente'
		END
END
GO

CREATE OR ALTER PROC usp_listar_cliente
AS
BEGIN
	SELECT IdCliente,Nombre,Apellido,Documento,Telefono,Correo,Contrasenia,Direccion
	FROM CLIENTES
	WHERE Tipo = 'C'
END
GO

/* Procedures Categoria */
CREATE OR ALTER PROC usp_listar_categoria
AS
BEGIN
	SELECT
	IdCategoria,Descripcion
	FROM CATEGORIA
	WHERE flgEliminado = 0
END
GO

CREATE OR ALTER PROC usp_create_categoria
	@Descripcion nvarchar(50)
AS
BEGIN
	DECLARE @idCat INT = ISNULL((SELECT MAX(IdCategoria) FROM CATEGORIA), 0) + 1;
	INSERT INTO CATEGORIA(IdCategoria,Descripcion,flgEliminado)
	VALUES(@idCat,@Descripcion,0)
END
GO

CREATE OR ALTER PROC usp_update_categoria
	@idCat int,
	@Descripcion nvarchar(50)
AS
BEGIN
	UPDATE CATEGORIA
	SET Descripcion = @Descripcion
	WHERE IdCategoria = @idCat
END
GO

CREATE OR ALTER PROC usp_delete_categoria
	@idCat int
AS
BEGIN
	UPDATE CATEGORIA
	SET flgEliminado = 1
	WHERE IdCategoria = @idCat
END
GO

/*PROCEDURES SERVICIOS*/
CREATE OR ALTER PROC usp_list_servicios
AS
	SELECT IdServicios, Descripcion
	FROM SERVICIOS
	WHERE flgEliminado = 0
Go

CREATE OR ALTER PROC usp_create_servicios
	@Descripcion nvarchar(30)
AS
BEGIN
	DECLARE @idServicio INT = ISNULL((SELECT MAX(IdServicios) FROM SERVICIOS), 0) + 1;
	INSERT INTO SERVICIOS(IdServicios,Descripcion,flgEliminado)
	VALUES(@idServicio,@Descripcion,0)
END
GO

CREATE OR ALTER PROC usp_update_servicios
	@idServicio int,
	@Descripcion nvarchar(30)
AS
BEGIN
	UPDATE SERVICIOS
	SET Descripcion = @Descripcion
	WHERE IdServicios = @idServicio
END
GO

CREATE OR ALTER PROC usp_delete_servicios
	@idServicio int
AS
BEGIN
	UPDATE SERVICIOS
	SET flgEliminado = 1
	WHERE IdServicios = @idServicio
END
GO

/* PROCEDURES SEDES */
CREATE OR ALTER PROC usp_list_sedes
AS
	SELECT IdSedes, Nombre,Imagen, Direccion, Telefono
	FROM SEDES
	WHERE flgEliminado = 0
Go

CREATE OR ALTER PROC usp_create_sedes
	@Nombre nvarchar(25),
	@Imagen varbinary(MAX),
	@Direccion nvarchar(50),
	@Telefono nvarchar(10)
AS
BEGIN
	DECLARE @idSede INT = ISNULL((SELECT MAX(IdSedes) FROM SEDES), 0) + 1;
	INSERT INTO SEDES(IdSedes,Nombre,Imagen,Direccion,Telefono,flgEliminado)
	VALUES(@idSede,@Nombre,@Imagen,@Direccion,@Telefono,0)
END
GO

CREATE OR ALTER PROC usp_update_sedes
	@idSede int,
	@Nombre nvarchar(25),
	@Imagen varbinary(MAX),
	@Direccion nvarchar(50),
	@Telefono nvarchar(10)
AS
BEGIN
	UPDATE SEDES
	SET Nombre = @Nombre, Imagen = @Imagen,@Direccion = @Direccion, Telefono = @Telefono
	WHERE IdSedes= @idSede
END
GO

CREATE OR ALTER PROC usp_delete_sedes
	@idSede int
AS
BEGIN
	UPDATE SEDES
	SET flgEliminado = 1
	WHERE IdSedes = @idSede
END
GO

/* PROCEDURES VETERINARIOS */
CREATE OR ALTER PROC usp_list_veterinarios
AS
	SELECT V.IdVeterinario, V.Nombre,V.Apellido,V.Imagen, E.IdEspecialidad,E.Nombre
	FROM VETERINARIOS AS V
	JOIN ESPECIALIDADES AS E
	ON V.IdEspecialidad = E.IdEspecialidad
	WHERE V.flg_Eliminado = 0
Go

CREATE OR ALTER PROC usp_create_veterinarios
	@Nombre nvarchar(25),
	@Apellido nvarchar(30),
	@Imagen varbinary(max),
	@IdEspecialidad int
AS
BEGIN
	DECLARE @IdVeterinario INT = ISNULL((SELECT MAX(IdVeterinario) FROM VETERINARIOS), 0) + 1;
	INSERT INTO VETERINARIOS(IdVeterinario,Nombre,Apellido,Imagen,IdEspecialidad,flg_Eliminado)
	VALUES(@IdVeterinario,@Nombre,@Apellido,@Imagen,@IdEspecialidad,0)
END
GO

CREATE OR ALTER PROC usp_update_veterinarios
	@IdVeterinario int,
	@Nombre nvarchar(25),
	@Apellido nvarchar(30),
	@Imagen varbinary(max),
	@IdEspecialidad int
AS
BEGIN
	UPDATE VETERINARIOS
	SET Nombre = @Nombre, Apellido = @Apellido, Imagen = @Imagen, IdEspecialidad = @IdEspecialidad
	WHERE IdVeterinario= @IdVeterinario
END
GO

CREATE OR ALTER PROC usp_delete_veterinarios
	@IdVeterinario int
AS
BEGIN
	UPDATE VETERINARIOS
	SET flg_Eliminado = 1
	WHERE IdVeterinario = @IdVeterinario
END
GO

/* PROCEDURES ESPECIALIDAD */
CREATE OR ALTER PROC usp_listar_especialidad
AS
BEGIN
	SELECT IdEspecialidad, Nombre
	FROM ESPECIALIDADES
	WHERE flg_Eliminado = 0
END
GO

CREATE OR ALTER PROC usp_create_especialidad
	@Nombre NVARCHAR(50)
AS
BEGIN
	DECLARE @idEsp INT = ISNULL((SELECT MAX(IdEspecialidad) FROM ESPECIALIDADES), 0) + 1;

	INSERT INTO ESPECIALIDADES (IdEspecialidad, Nombre, flg_Eliminado)
	VALUES (@idEsp, @Nombre, 0)
END
GO

CREATE OR ALTER PROC usp_update_especialidad
	@IdEspecialidad INT,
	@Nombre NVARCHAR(50)
AS
BEGIN
	UPDATE ESPECIALIDADES
	SET Nombre = @Nombre
	WHERE IdEspecialidad = @IdEspecialidad
END
GO

CREATE OR ALTER PROC usp_delete_especialidad
	@IdEspecialidad INT
AS
BEGIN
	UPDATE ESPECIALIDADES
	SET flg_Eliminado = 1
	WHERE IdEspecialidad = @IdEspecialidad
END
GO

/* PROCEDURES DE ESPECIE */
CREATE OR ALTER PROC usp_listar_especie
AS
BEGIN
	SELECT IdEspecie, Descripcion
	FROM ESPECIE
	WHERE flgEliminado = 0
END
GO

CREATE OR ALTER PROC usp_create_especie
	@Descripcion NVARCHAR(50)
AS
BEGIN
	DECLARE @idEsp INT = ISNULL((SELECT MAX(IdEspecie) FROM ESPECIE), 0) + 1;

	INSERT INTO ESPECIE (IdEspecie, Descripcion, flgEliminado)
	VALUES (@idEsp, @Descripcion, 0)
END
GO

CREATE OR ALTER PROC usp_update_especie
	@IdEspecie INT,
	@Descripcion NVARCHAR(50)
AS
BEGIN
	UPDATE ESPECIE
	SET Descripcion = @Descripcion
	WHERE IdEspecie = @IdEspecie
END
GO

CREATE OR ALTER PROC usp_delete_especie
	@IdEspecie INT
AS
BEGIN
	UPDATE ESPECIE
	SET flgEliminado = 1
	WHERE IdEspecie = @IdEspecie
END
GO

/* PROCEDURES DE RAZA */
CREATE OR ALTER PROC usp_listar_raza
AS
BEGIN
	SELECT R.IdRaza, R.Descripcion, E.IdEspecie, E.Descripcion
	FROM RAZA R
	JOIN ESPECIE E
	ON E.IdEspecie = R.IdEspecie
	WHERE R.flgEliminado = 0
END
GO

CREATE OR ALTER PROC usp_create_raza
	@Descripcion NVARCHAR(50),
	@IdEspecie INT
AS
BEGIN
	DECLARE @idRaza INT = ISNULL((SELECT MAX(IdRaza) FROM RAZA), 0) + 1;

	INSERT INTO RAZA (IdRaza, Descripcion, IdEspecie, flgEliminado)
	VALUES (@idRaza, @Descripcion, @IdEspecie, 0)
END
GO

CREATE OR ALTER PROC usp_update_raza
	@IdRaza INT,
	@Descripcion NVARCHAR(50),
	@IdEspecie INT
AS
BEGIN
	UPDATE RAZA
	SET Descripcion = @Descripcion,
		IdEspecie = @IdEspecie
	WHERE IdRaza = @IdRaza
END
GO

CREATE OR ALTER PROC usp_delete_raza
	@IdRaza INT
AS
BEGIN
	UPDATE RAZA
	SET flgEliminado = 1
	WHERE IdRaza = @IdRaza
END
GO

/* PROCEDURES DE VENTA */

CREATE OR ALTER PROC usp_agregar_venta
@IdVenta int output,
@Total decimal(10,2),
@IdCliente int
AS
BEGIN
	SET @IdVenta  = ISNULL((SELECT MAX(IdVenta) FROM VENTAS), 0) + 1;
	INSERT INTO VENTAS(IdVenta,Fecha,Total,IdCliente) Values
	(@IdVenta,GETDATE(),@Total,@IdCliente)
END
GO


CREATE OR ALTER PROC usp_agregar_detalle_venta

@IdVenta int,
@IdProducto int,
@Cantidad int,
@Precio decimal(10,2)
AS
BEGIN
	DECLARE @IdDetalle INT = ISNULL((SELECT MAX(IdDetalle) FROM DETALLEVENTA), 0) + 1;
	INSERT INTO DETALLEVENTA(IdDetalle,IdVenta,IdProducto,Cantidad,Precio) Values
	(@IdDetalle,@IdVenta,@IdProducto,@Cantidad,@Precio)
END
GO

/*FILTROS PROCEDURES PRODUCTO*/
CREATE OR ALTER PROC usp_buscar_producto_nombre
	@nombre varchar(50)
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
	WHERE PROD.Nombre LIKE '%' + @nombre+ '%'
	AND PROD.flgEliminado = 0
END
GO

CREATE OR ALTER PROC usp_productos_entre_precios
	@preciomin decimal(10,2),
	@preciomax decimal(10,2)
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
	WHERE PROD.Precio BETWEEN @preciomin AND @preciomax
	AND PROD.flgEliminado = 0
END
GO

/*Probando dashboard*/
INSERT INTO VENTAS (IdVenta, Fecha, Total, IdCliente) VALUES
(1, '2025-01-15', 120.50, 3),
(2, '2025-02-03', 300.00, 3),
(3, '2025-02-20', 185.75, 3),
(4, '2025-03-10', 240.00, 4),
(5, '2025-03-22', 75.90, 4),
(6, '2025-04-05', 450.25, 4),
(7, '2025-04-18', 99.99, 5),
(8, '2025-05-01', 320.00, 5);

INSERT INTO DETALLEVENTA (IdDetalle, IdVenta, IdProducto, Cantidad, Precio) VALUES
(1, 1, 1, 2, 40.00),
(2, 1, 2, 1, 40.50),
(3, 2, 1, 3, 100.00),
(4, 3, 3, 2, 92.87),
(5, 4, 2, 4, 60.00),
(6, 5, 3, 1, 75.90),
(7, 6, 1, 5, 90.05),
(8, 7, 2, 2, 49.99),
(9, 8, 3, 4, 80.00);

select * from VENTAS
select * from DETALLEVENTA
GO

CREATE OR ALTER PROC usp_ventas_mes
AS
BEGIN
	SELECT 
		FORMAT(Fecha, 'yyyy-MM') AS Mes, 
		SUM(Total) AS TotalMensual
	FROM VENTAS
	WHERE Fecha >= DATEADD(MONTH, -5, EOMONTH(GETDATE()))
	GROUP BY FORMAT(Fecha, 'yyyy-MM')
	ORDER BY Mes;
END
GO

/*Perfil de cliente*/
CREATE OR ALTER PROC usp_historial_compras
	@idCliente int
AS
	BEGIN
		Select v.IdVenta,V.Fecha,v.Total,d.IdDetalle, p.Nombre,p.Precio,
		d.cantidad from VENTAS V
		join DETALLEVENTA D on v.IdVenta = d.IdVenta
		join PRODUCTO P on d.IdProducto = p.IdProducto
		Where v.IdCliente = @idCliente
	END
GO

CREATE OR ALTER PROC usp_obtener_cliente
	@idCliente int
AS
BEGIN
	Select IdCliente, Nombre,Apellido,Documento,Telefono,
		Correo,Contrasenia,Direccion,Tipo
	from CLIENTES
	where IdCliente = @idCliente 
END
GO

exec usp_historial_compras 5
exec usp_obtener_cliente 5

/*CREATE OR ALTER PROC usp_historial_reservas pendiente
	@idCliente int
AS
	BEGIN
		Select v.IdVenta,V.Fecha,v.Total,d.IdDetalle, p.Nombre,p.Precio,
		d.cantidad from VENTAS V
		join DETALLEVENTA D on v.IdVenta = d.IdVenta
		join PRODUCTO P on d.IdProducto = p.IdProducto
		Where v.IdCliente = @idCliente
	END
GO*/

select * from ATENCIONES
select * from ventas
select * from detalleVenta
select * from PRODUCTO
select * from clientes