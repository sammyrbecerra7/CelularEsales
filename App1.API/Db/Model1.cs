namespace App1.API.Db
{
    using Common.Models;
    using System.Data.Entity;
    public  class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelESales")
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Contrapartida> Contrapartida { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<FormaPago> FormaPago { get; set; }
        public virtual DbSet<InfoCredito> InfoCredito { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.VendedorCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Documentos)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contrapartida>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Contrapartida>()
                .Property(e => e.FormaPagoCodigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contrapartida>()
                .Property(e => e.EnviadoIdeal)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contrapartida>()
                .Property(e => e.EnviadoBwise)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.ClienteCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.SpecialGLIndicator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.FacturaNumeroLegal)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Referencia)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.TipoDocumento)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Texto)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.PaymentTerm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Moneda)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.NombreCorto)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.EbillingDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .HasMany(e => e.Pagos)
                .WithRequired(e => e.Documentos)
                .HasForeignKey(e => new { e.CodigoDocumento, e.FacturaNumeroLegal })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FormaPago>()
                .Property(e => e.Codigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FormaPago>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<FormaPago>()
                .HasMany(e => e.Contrapartida)
                .WithRequired(e => e.FormaPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FormaPago>()
                .HasMany(e => e.Pagos)
                .WithRequired(e => e.FormaPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InfoCredito>()
                .Property(e => e.VendedorCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.FacturaNumeroLegal)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.CodigoDocumento)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.FormaPagoCodigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Banco)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.NumeroCheque)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Postfechado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.NumeroTransferencia)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.EnviadoIdeal)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.EnviadoBwise)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Contrapartida)
                .IsUnicode(false);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.TransferidoSAP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vendedor>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Vendedor>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<Vendedor>()
                .Property(e => e.Sucursal)
                .IsUnicode(false);

            modelBuilder.Entity<Vendedor>()
                .HasMany(e => e.Cliente)
                .WithRequired(e => e.Vendedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendedor>()
                .HasMany(e => e.InfoCredito)
                .WithRequired(e => e.Vendedor)
                .WillCascadeOnDelete(false);
        }
    }
}
