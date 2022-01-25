using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VDT.TransactionDaily.API.Models
{
    public partial class u158751192_STMNAContext : DbContext
    {
        public u158751192_STMNAContext()
        {
        }

        public u158751192_STMNAContext(DbContextOptions<u158751192_STMNAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VdtProduct> VdtProducts { get; set; } = null!;
        public virtual DbSet<VdtProductDictionary> VdtProductDictionaries { get; set; } = null!;
        public virtual DbSet<VdtTransaction> VdtTransactions { get; set; } = null!;
        public virtual DbSet<VdtTransactionDetail> VdtTransactionDetails { get; set; } = null!;
        public virtual DbSet<VdtUser> VdtUsers { get; set; } = null!;
        public virtual DbSet<VdtUserSession> VdtUserSessions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=185.210.145.52;database=u158751192_STMNA;user=u158751192_vdthang;password=12345678@Abc", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.12-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<VdtProduct>(entity =>
            {
                entity.ToTable("vdt_product");

                entity.HasComment("Bảng chứa thông tin sản phẩm/hàng hóa");

                entity.HasIndex(e => e.DictonaryId, "FK_vdt_product_DictonaryID");

                entity.HasIndex(e => e.UserId, "FK_vdt_product_UserID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("''")
                    .HasComment("Mã sản phẩm");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasComment("Mô tả");

                entity.Property(e => e.DictonaryId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("DictonaryID")
                    .HasComment("ID danh mục");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("Tên sản phẩm");

                entity.Property(e => e.Price)
                    .HasPrecision(18, 5)
                    .HasComment("Giá sản phẩm");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(d => d.Dictonary)
                    .WithMany(p => p.VdtProducts)
                    .HasForeignKey(d => d.DictonaryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vdt_product_DictonaryID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VdtProducts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_vdt_product_UserID");
            });

            modelBuilder.Entity<VdtProductDictionary>(entity =>
            {
                entity.ToTable("vdt_product_dictionary");

                entity.HasComment("Bảng chứa danh mục sản phẩm");

                entity.HasIndex(e => e.UserId, "FK_vdt_product_dictionary_UserID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasComment("Mã danh mục");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("Tên danh mục");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VdtProductDictionaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_vdt_product_dictionary_UserID");
            });

            modelBuilder.Entity<VdtTransaction>(entity =>
            {
                entity.ToTable("vdt_transaction");

                entity.HasComment("Bảng thực hiện lưu trữ thông tin đơn hàng");

                entity.HasIndex(e => e.UserId, "FK_vdt_transaction_UserID");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.Author)
                    .HasMaxLength(100)
                    .HasComment("Người tạo giao dịch");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''")
                    .HasComment("Mã giao dịch đơn hàng");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("Tên giao dịch đơn hàng");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VdtTransactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_vdt_transaction_UserID");
            });

            modelBuilder.Entity<VdtTransactionDetail>(entity =>
            {
                entity.ToTable("vdt_transaction_detail");

                entity.HasComment("Bảng thực hiện lưu trữ detail đơn hàng");

                entity.HasIndex(e => e.UserId, "FK_vdt_transaction_detail_UserID");

                entity.HasIndex(e => e.ProductId, "FK_vdt_transaction_detail_vdt_product");

                entity.HasIndex(e => e.MasterId, "FK_vdt_transaction_detail_vdt_transaction");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.MasterId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("MasterID")
                    .HasComment("Khóa ngoại đơn hàng");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("ProductID")
                    .HasComment("Khóa ngoại sản phẩm");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Số lượng sản phẩm");

                entity.Property(e => e.TotalPrice)
                    .HasPrecision(18, 5)
                    .HasDefaultValueSql("'0.00000'")
                    .HasComment("Tổng tiền đơn hàng");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.VdtTransactionDetails)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_vdt_transaction_detail_vdt_transaction");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.VdtTransactionDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vdt_transaction_detail_vdt_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VdtTransactionDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_vdt_transaction_detail_UserID");
            });

            modelBuilder.Entity<VdtUser>(entity =>
            {
                entity.ToTable("vdt_user");

                entity.HasComment("Bảng lưu trữ thông tin người dùng");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<VdtUserSession>(entity =>
            {
                entity.ToTable("vdt_user_session");

                entity.HasComment("Bảng lưu trữ phiên đăng nhập người dùng");

                entity.HasIndex(e => e.UserId, "FK_vdt_user_session_UserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Khóa chính");

                entity.Property(e => e.AccessToken)
                    .HasColumnType("text")
                    .HasComment("AccessToken");

                entity.Property(e => e.Browser)
                    .HasColumnType("text")
                    .HasComment("Tên trình duyệt");

                entity.Property(e => e.ClientIp)
                    .HasColumnType("text")
                    .HasColumnName("ClientIP")
                    .HasComment("IP client");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasDefaultValueSql("''")
                    .HasComment("ID thiết bị");

                entity.Property(e => e.LoginDay)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian đăng nhập");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.Os)
                    .HasColumnType("text")
                    .HasColumnName("OS")
                    .HasComment("Hệ điều hành");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(32)
                    .HasDefaultValueSql("''")
                    .IsFixedLength()
                    .HasComment("Token refresh");

                entity.Property(e => e.RefreshTokenExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("Hạn Token refresh");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("ID người dùng");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VdtUserSessions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_vdt_user_session_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
