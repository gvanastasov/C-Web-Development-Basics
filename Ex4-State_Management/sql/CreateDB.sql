Use PizzaMoreContext
go


create table Users
(
	[Id] int primary key identity(1,1),
	[Email] nvarchar(200) not null,
	[Password] nvarchar(200) not null,
)

insert into Users values
('pesho@abv.bg', '123')


















