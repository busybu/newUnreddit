use master
go

if exists(select * from sys.databases where name = 'Uneddit')
  drop database uneddit
go

CREATE DATABASE Uneddit
go

USE Uneddit 
go

CREATE TABLE Usuario (
  id INT IDENTITY PRIMARY KEY,
  username VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  data_nascimento DATE NOT NULL,
  foto_usuario VARBINARY,
  senha VARBINARY(150) NOT NULL,
  salt VARCHAR(255) NOT NULL
);
go

CREATE TABLE Forum (
  id INT IDENTITY PRIMARY KEY,
  titulo VARCHAR(255) NOT NULL,
  descricao VARCHAR(255) NOT NULL,
  data_criado DATE,
  criador INT,
  FOREIGN KEY (criador) REFERENCES Usuario(id)
);
go

CREATE TABLE ForumUsuario (
  id INT IDENTITY PRIMARY KEY,
  forum INT,
  usuarios INT,
  FOREIGN KEY (forum) REFERENCES Forum(id),
  FOREIGN KEY (usuarios) REFERENCES Usuario(id)
);
go

CREATE TABLE Post (
  id INT IDENTITY PRIMARY KEY,
  titulo VARCHAR(255),
  conteudo VARCHAR(255),
  anexo VARBINARY,
  autor INT,
  forum INT,
  FOREIGN KEY (autor) REFERENCES Usuario(id),
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go

CREATE TABLE Comentario (
  id INT IDENTITY PRIMARY KEY,
  conteudo VARCHAR(255),
  usuario INT,
  post INT,
  FOREIGN KEY (usuario) REFERENCES Usuario(id),
  FOREIGN KEY (post) REFERENCES Post(id)
);
go

CREATE TABLE Cargo (
  id INT IDENTITY PRIMARY KEY,
  nome VARCHAR(255),
  forum INT,
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go

CREATE TABLE Permissao (
  id INT IDENTITY PRIMARY KEY,
  nome VARCHAR(255)
);
go

CREATE TABLE CargoPermissao (
  id INT IDENTITY PRIMARY KEY,
  cargo INT,
  permissao INT,
  FOREIGN KEY (cargo) REFERENCES Cargo(id),
  FOREIGN KEY (permissao) REFERENCES Permissao(id)
);
go

CREATE TABLE UpVote (
  id INT IDENTITY PRIMARY KEY,
  post INT,
  usuario INT,
  FOREIGN KEY (post) REFERENCES Post(id),
  FOREIGN KEY (usuario) REFERENCES Usuario(id)
);

select * from Usuario