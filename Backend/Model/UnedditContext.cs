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
        => optionsBuilder.UseSqlServer("Data Source=BUMACHINE;Initial Catalog=Uneddit;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cargo__3213E83F2E15BB04");

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
                .HasConstraintName("FK__Cargo__forum__34C8D9D1");
        });

        modelBuilder.Entity<CargoPermissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CargoPer__3213E83F7B81D0CD");

            entity.ToTable("CargoPermissao");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Permissao).HasColumnName("permissao");

            entity.HasOne(d => d.CargoNavigation).WithMany(p => p.CargoPermissaos)
                .HasForeignKey(d => d.Cargo)
                .HasConstraintName("FK__CargoPerm__cargo__398D8EEE");

            entity.HasOne(d => d.PermissaoNavigation).WithMany(p => p.CargoPermissaos)
                .HasForeignKey(d => d.Permissao)
                .HasConstraintName("FK__CargoPerm__permi__3A81B327");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3213E83F25A1AD7A");

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
                .HasConstraintName("FK__Comentario__post__31EC6D26");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__Comentari__usuar__30F848ED");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3213E83F861A0E33");

            entity.ToTable("Forum");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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
                .HasConstraintName("FK__Forum__criador__267ABA7A");
        });

        modelBuilder.Entity<ForumUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumUsu__3213E83F995970AE");

            entity.ToTable("ForumUsuario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Forum).HasColumnName("forum");
            entity.Property(e => e.Usuarios).HasColumnName("usuarios");

            entity.HasOne(d => d.ForumNavigation).WithMany(p => p.ForumUsuarios)
                .HasForeignKey(d => d.Forum)
                .HasConstraintName("FK__ForumUsua__forum__29572725");

            entity.HasOne(d => d.UsuariosNavigation).WithMany(p => p.ForumUsuarios)
                .HasForeignKey(d => d.Usuarios)
                .HasConstraintName("FK__ForumUsua__usuar__2A4B4B5E");
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissa__3213E83F33C93B9E");

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
            entity.HasKey(e => e.Id).HasName("PK__Post__3213E83F9607F0B6");

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
                .HasConstraintName("FK__Post__autor__2D27B809");

            entity.HasOne(d => d.ForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Forum)
                .HasConstraintName("FK__Post__forum__2E1BDC42");
        });

        modelBuilder.Entity<UpVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UpVote__3213E83F8088C7EB");

            entity.ToTable("UpVote");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.UpVotes)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("FK__UpVote__post__3D5E1FD2");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.UpVotes)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK__UpVote__usuario__3E52440B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F3DE1C280");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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
                .HasMaxLength(1)
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
