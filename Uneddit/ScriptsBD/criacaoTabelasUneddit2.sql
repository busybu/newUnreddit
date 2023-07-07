-- Cria��o da tabela Usuario
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
-- Cria��o da tabela Forum
CREATE TABLE Forum (
  id INT IDENTITY PRIMARY KEY,
  titulo VARCHAR(255) NOT NULL,
  descricao VARCHAR(255) NOT NULL,
  data_criado DATE NOT NULL,
  criador INT NOT NULL, 
  quantidade INT NOT NULL,
  FOREIGN KEY (criador) REFERENCES Usuario(id)
);
go
-- Cria��o da tabela ForumUsuario
CREATE TABLE ForumUsuario (
  id INT IDENTITY PRIMARY KEY,
  forum INT NOT NULL,
  usuarios INT NOT NULL,
  FOREIGN KEY (forum) REFERENCES Forum(id),
  FOREIGN KEY (usuarios) REFERENCES Usuario(id)
);
go
-- Cria��o da tabela Post
CREATE TABLE Post (
  id INT IDENTITY PRIMARY KEY,
  titulo VARCHAR(255),
  conteudo VARCHAR(MAX),
  anexo VARBINARY,
  data_criado DATETIME DEFAULT(GETDATE()) NOT NULL,
  autor INT NOT NULL,
  forum INT NOT NULL,
  FOREIGN KEY (autor) REFERENCES Usuario(id),
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go
-- Cria��o da tabela Comentario
CREATE TABLE Comentario (
  id INT IDENTITY PRIMARY KEY,
  conteudo VARCHAR(255) NOT NULL,
  usuario INT NOT NULL,
  post INT NOT NULL,
  FOREIGN KEY (usuario) REFERENCES Usuario(id),
  FOREIGN KEY (post) REFERENCES Post(id)
);
go
-- Cria��o da tabela Cargo
CREATE TABLE Cargo (
  id INT IDENTITY PRIMARY KEY,
  nome VARCHAR(255),
  forum INT NOT NULL,
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go
-- Cria��o da tabela Permissao
CREATE TABLE Permissao (
  id INT IDENTITY PRIMARY KEY,
  nome VARCHAR(255)
);
go
-- Cria��o da tabela CargoPermissao
CREATE TABLE CargoPermissao (
  id INT IDENTITY PRIMARY KEY,
  cargo INT NOT NULL,
  permissao INT NOT NULL,
  FOREIGN KEY (cargo) REFERENCES Cargo(id),
  FOREIGN KEY (permissao) REFERENCES Permissao(id)
);
go
-- Cria��o da tabela UpVote
CREATE TABLE UpVote (
  id INT IDENTITY PRIMARY KEY,
  post INT NOT NULL,
  usuario INT NOT NULL,
  FOREIGN KEY (post) REFERENCES Post(id),
  FOREIGN KEY (usuario) REFERENCES Usuario(id)
);


SELECT * FROM Usuario

SELECT * FROM Forum

SELECT * FROM Post