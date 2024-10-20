create database ExamenImSoftware
go

use ExamenImSoftware
go



create table Personas
(
idpersona int primary key not null identity(1,1),
Nombre nvarchar(50),
Edad int,
Email nvarchar(50)
)
go


create proc SP_GetPersonas
as
select * from Personas
go

create proc SP_NuevaPersona
@Nombre nvarchar(50),
@Edad int,
@Email nvarchar(50)
as
insert into Personas (Nombre, Edad, Email) values
(@Nombre, @Edad, @Email)
go