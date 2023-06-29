using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Reddit.Model;

public partial class UnedditContext : DbContext
{
    public UnedditContext()
    {
    }

    public UnedditContext(DbContextOptions<UnedditContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<CargoPermissao> CargoPermissaos { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<ForumUsuario> ForumUsuarios { get; set; }

    public virtual DbSet<Permissao> Permissaos { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<UpVote> UpVotes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-00186\\SQLEXPRESS;Initial Catalog=Uneddit;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cargo__3213E83FA5A6419D");

            entity.ToTable("Cargo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Forum).HasColumnName("forum");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nome");

            entity.HasOne(d => d.ForumNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.Forum)
                .HasConstraintName("FK__Cargo__forum__47DBAE45");
        });

        modelBuilder.Entity<CargoPermissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CargoPer__3213E83FC2306800");

            entity.ToTable("CargoPermissao");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Permissao).HasColumnName("permissao");

            entity.HasOne(d => d.CargoNavigation).WithMany(p => p.CargoPermissaos)
                .HasForeignKey(d => d.Cargo)
                .HasConstraintName("FK__CargoPerm__cargo__4CA06362");

            entity.HasOne(d => d.PermissaoNavigation).WithMany(p => p.CargoPermissaos)
                .HasForeignKey(d => d.Permissao)
                .HasConstraintName("FK__CargoPerm__permi__4D94879B");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3213E83FAA54D52D");

            entity.ToTable("Comentario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Conteudo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("conteudo");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("FK__Comentario__post__44FF419A");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Comentari__usuar__440B1D61");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3213E83F60891FAB");

            entity.ToTable("Forum");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Criador).HasColumnName("criador");
            entity.Property(e => e.DataCriado)
                .HasColumnType("date")
                .HasColumnName("data_criado");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.CriadorNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Criador)
                .HasConstraintName("FK__Forum__criador__398D8EEE");
        });

        modelBuilder.Entity<ForumUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumUsu__3213E83F1434AC2C");

            entity.ToTable("ForumUsuario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Forum).HasColumnName("forum");
            entity.Property(e => e.Usuarios).HasColumnName("usuarios");

            entity.HasOne(d => d.ForumNavigation).WithMany(p => p.ForumUsuarios)
                .HasForeignKey(d => d.Forum)
                .HasConstraintName("FK__ForumUsua__forum__3C69FB99");

            entity.HasOne(d => d.UsuariosNavigation).WithMany(p => p.ForumUsuarios)
                .HasForeignKey(d => d.Usuarios)
                .HasConstraintName("FK__ForumUsua__usuar__3D5E1FD2");
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissa__3213E83FD78A49FB");

            entity.ToTable("Permissao");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3213E83FD0AB2C6F");

            entity.ToTable("Post");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Anexo)
                .HasMaxLength(1)
                .HasColumnName("anexo");
            entity.Property(e => e.Autor).HasColumnName("autor");
            entity.Property(e => e.Conteudo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("conteudo");
            entity.Property(e => e.Forum).HasColumnName("forum");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.AutorNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Autor)
                .HasConstraintName("FK__Post__autor__403A8C7D");

            entity.HasOne(d => d.ForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Forum)
                .HasConstraintName("FK__Post__forum__412EB0B6");
        });

        modelBuilder.Entity<UpVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UpVote__3213E83FC8FF4F84");

            entity.ToTable("UpVote");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.UpVotes)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("FK__UpVote__post__5070F446");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.UpVotes)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__UpVote__usuario__5165187F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F5E369490");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FotoUsuario)
                .HasMaxLength(1)
                .HasColumnName("foto_usuario");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");
            entity.Property(e => e.Senha)
                .HasMaxLength(150)
                .HasColumnName("senha");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
