-- Cria��o da tabela Usuario
CREATE DATABASE Uneddit
go

USE Uneddit 
go

CREATE TABLE Usuario (
  id INT IDENTITY PRIMARY KEY,
  username VARCHAR(50) not null,
  email VARCHAR(50) not null,
  data_nascimento DATE not null,
  foto_usuario VARBINARY,
  senha VARCHAR(MAX) not null,
  salt VARCHAR(255)
);
go

-- Cria��o da tabela Forum
CREATE TABLE Forum (
  id INT IDENTITY PRIMARY KEY,
  titulo VARCHAR(255),
  descricao VARCHAR(255),
  data_criado DATE,
  criador INT,
  FOREIGN KEY (criador) REFERENCES Usuario(id)
);
go

-- Cria��o da tabela ForumUsuario
CREATE TABLE ForumUsuario (
  id INT IDENTITY PRIMARY KEY,
  forum INT,
  usuarios INT,
  FOREIGN KEY (forum) REFERENCES Forum(id),
  FOREIGN KEY (usuarios) REFERENCES Usuario(id)
);
go

-- Cria��o da tabela Post
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

-- Cria��o da tabela Comentario
CREATE TABLE Comentario (
  id INT IDENTITY PRIMARY KEY,
  conteudo VARCHAR(255),
  usuario INT,
  post INT,
  FOREIGN KEY (usuario) REFERENCES Usuario(id),
  FOREIGN KEY (post) REFERENCES Post(id)
);
go

-- Cria��oda tabela Cargo
CREATE TABLE Cargo (
  id INT IDENTITY PRIMARY KEY,
  nome VARCHAR(255),
  forum INT,
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
  cargo INT,
  permissao INT,
  FOREIGN KEY (cargo) REFERENCES Cargo(id),
  FOREIGN KEY (permissao) REFERENCES Permissao(id)
);
go

-- Cria��o da tabela UpVote
CREATE TABLE UpVote (
  id INT IDENTITY PRIMARY KEY,
  post INT,
  usuario INT,
  FOREIGN KEY (post) REFERENCES Post(id),
  FOREIGN KEY (usuario) REFERENCES Usuario(id)
);
