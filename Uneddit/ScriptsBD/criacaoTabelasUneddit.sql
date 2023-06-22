-- Criação da tabela Usuario
CREATE DATABASE Uneddit 
go 
USE Uneddit 
go
CREATE TABLE Usuario (
  id INT PRIMARY KEY,
  username VARCHAR(50),
  email VARCHAR(50),
  data_nascimento DATE,
  foto_usuario VARBINARY,
  senha VARBINARY,
  salt VARCHAR(255)
);
go
-- Criação da tabela Forum
CREATE TABLE Forum (
  id INT PRIMARY KEY,
  titulo VARCHAR(255),
  descricao VARCHAR(255),
  data_criado DATE,
  criador INT,
  FOREIGN KEY (criador) REFERENCES Usuario(id)
);
go
-- Criação da tabela ForumUsuario
CREATE TABLE ForumUsuario (
  id INT PRIMARY KEY,
  forum INT,
  usuarios INT,
  FOREIGN KEY (forum) REFERENCES Forum(id),
  FOREIGN KEY (usuarios) REFERENCES Usuario(id)
);
go
-- Criação da tabela Post
CREATE TABLE Post (
  id INT PRIMARY KEY,
  titulo VARCHAR(255),
  conteudo VARCHAR(255),
  anexo VARBINARY,
  autor INT,
  forum INT,
  FOREIGN KEY (autor) REFERENCES Usuario(id),
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go
-- Criação da tabela Comentario
CREATE TABLE Comentario (
  id INT PRIMARY KEY,
  conteudo VARCHAR(255),
  usuario INT,
  post INT,
  FOREIGN KEY (usuario) REFERENCES Usuario(id),
  FOREIGN KEY (post) REFERENCES Post(id)
);
go
-- Criação da tabela Cargo
CREATE TABLE Cargo (
  id INT PRIMARY KEY,
  nome VARCHAR(255),
  forum INT,
  FOREIGN KEY (forum) REFERENCES Forum(id)
);
go
-- Criação da tabela Permissao
CREATE TABLE Permissao (
  id INT PRIMARY KEY,
  nome VARCHAR(255)
);
go
-- Criação da tabela CargoPermissao
CREATE TABLE CargoPermissao (
  id INT PRIMARY KEY,
  cargo INT,
  permissao INT,
  FOREIGN KEY (cargo) REFERENCES Cargo(id),
  FOREIGN KEY (permissao) REFERENCES Permissao(id)
);
go
-- Criação da tabela UpVote
CREATE TABLE UpVote (
  id INT PRIMARY KEY,
  post INT,
  usuario INT,
  FOREIGN KEY (post) REFERENCES Post(id),
  FOREIGN KEY (usuario) REFERENCES Usuario(id)
);
