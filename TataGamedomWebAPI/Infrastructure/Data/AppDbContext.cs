﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TataGamedomWebAPI.Models.EFModels;

namespace TataGamedomWebAPI.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<ApprovalStatusCode> ApprovalStatusCodes { get; set; }

    public virtual DbSet<BackendMember> BackendMembers { get; set; }

    public virtual DbSet<BackendMembersPermissionsCode> BackendMembersPermissionsCodes { get; set; }

    public virtual DbSet<BackendMembersRolePermission> BackendMembersRolePermissions { get; set; }

    public virtual DbSet<BackendMembersRolesCode> BackendMembersRolesCodes { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<BoardNotification> BoardNotifications { get; set; }

    public virtual DbSet<BoardsModerator> BoardsModerators { get; set; }

    public virtual DbSet<BoardsModeratorsApplication> BoardsModeratorsApplications { get; set; }

    public virtual DbSet<BranchesOfSeven> BranchesOfSevens { get; set; }

    public virtual DbSet<BucketLog> BucketLogs { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<CouponsProduct> CouponsProducts { get; set; }

    public virtual DbSet<DiscountTypeCode> DiscountTypeCodes { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameClassificationGame> GameClassificationGames { get; set; }

    public virtual DbSet<GameClassificationsCode> GameClassificationsCodes { get; set; }

    public virtual DbSet<GameComment> GameComments { get; set; }

    public virtual DbSet<GamePlatformsCode> GamePlatformsCodes { get; set; }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueStatusCode> IssueStatusCodes { get; set; }

    public virtual DbSet<IssueTypesCode> IssueTypesCodes { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberFollow> MemberFollows { get; set; }

    public virtual DbSet<MemberProductView> MemberProductViews { get; set; }

    public virtual DbSet<MembersBoard> MembersBoards { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsCategoryCode> NewsCategoryCodes { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<NewsLike> NewsLikes { get; set; }

    public virtual DbSet<NewsView> NewsViews { get; set; }

    public virtual DbSet<Newsletter> Newsletters { get; set; }

    public virtual DbSet<NewsletterLog> NewsletterLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderItemReturn> OrderItemReturns { get; set; }

    public virtual DbSet<OrderItemsCoupon> OrderItemsCoupons { get; set; }

    public virtual DbSet<OrderStatusCode> OrderStatusCodes { get; set; }

    public virtual DbSet<PaymentStatusCode> PaymentStatusCodes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostCommentReport> PostCommentReports { get; set; }

    public virtual DbSet<PostCommentUpDownVote> PostCommentUpDownVotes { get; set; }

    public virtual DbSet<PostEditLog> PostEditLogs { get; set; }

    public virtual DbSet<PostReport> PostReports { get; set; }

    public virtual DbSet<PostUpDownVote> PostUpDownVotes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductStatusCode> ProductStatusCodes { get; set; }

    public virtual DbSet<Reply> Replies { get; set; }

    public virtual DbSet<ShipmentMethod> ShipmentMethods { get; set; }

    public virtual DbSet<ShipmentStatusesCode> ShipmentStatusesCodes { get; set; }

    public virtual DbSet<StandardProduct> StandardProducts { get; set; }

    public virtual DbSet<StockInSheet> StockInSheets { get; set; }

    public virtual DbSet<StockInStatusCode> StockInStatusCodes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TrackProduct> TrackProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Announce__3214EC07112DD6C9");

            entity.ToTable("Announcement");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ApprovalStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Approval__3214EC0766C40A3B");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<BackendMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BackendM__3214EC079BE94635");

            entity.Property(e => e.Account)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.BackendMembersRole).WithMany(p => p.BackendMembers)
                .HasForeignKey(d => d.BackendMembersRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BackendMe__Backe__25518C17");
        });

        modelBuilder.Entity<BackendMembersPermissionsCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BackendM__3214EC071E7D50B8");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<BackendMembersRolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BackendM__3214EC07C79F3D79");

            entity.HasOne(d => d.BackendMemberPermission).WithMany(p => p.BackendMembersRolePermissions)
                .HasForeignKey(d => d.BackendMemberPermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BackendMe__Backe__2739D489");

            entity.HasOne(d => d.BackendMembersRole).WithMany(p => p.BackendMembersRolePermissions)
                .HasForeignKey(d => d.BackendMembersRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BackendMe__Backe__2645B050");
        });

        modelBuilder.Entity<BackendMembersRolesCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BackendM__3214EC078586029C");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Boards__3214EC07F357DB00");

            entity.Property(e => e.BoardHeaderCoverImg).IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedBackendMember).WithMany(p => p.Boards)
                .HasForeignKey(d => d.CreatedBackendMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boards__CreatedB__2B0A656D");

            entity.HasOne(d => d.Game).WithMany(p => p.Boards)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Boards__GameId__2BFE89A6");
        });

        modelBuilder.Entity<BoardNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoardNot__3214EC07D8EC101D");

            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsReaded).HasColumnName("isReaded");
            entity.Property(e => e.Link).HasMaxLength(1000);
            entity.Property(e => e.ReadTime).HasColumnType("datetime");

            entity.HasOne(d => d.RecipientMember).WithMany(p => p.BoardNotificationRecipientMembers)
                .HasForeignKey(d => d.RecipientMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BoardNoti__Recip__282DF8C2");

            entity.HasOne(d => d.RelationMember).WithMany(p => p.BoardNotificationRelationMembers)
                .HasForeignKey(d => d.RelationMemberId)
                .HasConstraintName("FK__BoardNoti__Relat__2A164134");

            entity.HasOne(d => d.RelationPost).WithMany(p => p.BoardNotifications)
                .HasForeignKey(d => d.RelationPostId)
                .HasConstraintName("FK__BoardNoti__Relat__14E61A24");
        });

        modelBuilder.Entity<BoardsModerator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoardsMo__3214EC07C3C90791");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Board).WithMany(p => p.BoardsModerators)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BoardsMod__Board__2CF2ADDF");

            entity.HasOne(d => d.ModeratorMember).WithMany(p => p.BoardsModerators)
                .HasForeignKey(d => d.ModeratorMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BoardsMod__Moder__2DE6D218");
        });

        modelBuilder.Entity<BoardsModeratorsApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoardsMo__3214EC07790A16E1");

            entity.Property(e => e.ApplyDate).HasColumnType("datetime");
            entity.Property(e => e.ApplyReason).HasMaxLength(500);
            entity.Property(e => e.ApprovalStatusDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.BoardsModeratorsApplications)
                .HasForeignKey(d => d.ApprovalStatusId)
                .HasConstraintName("FK__BoardsMod__Appro__2EDAF651");

            entity.HasOne(d => d.BackendMember).WithMany(p => p.BoardsModeratorsApplications)
                .HasForeignKey(d => d.BackendMemberId)
                .HasConstraintName("FK__BoardsMod__Backe__2FCF1A8A");

            entity.HasOne(d => d.Board).WithMany(p => p.BoardsModeratorsApplications)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BoardsMod__Board__30C33EC3");

            entity.HasOne(d => d.Member).WithMany(p => p.BoardsModeratorsApplications)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BoardsMod__Membe__31B762FC");
        });

        modelBuilder.Entity<BranchesOfSeven>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Branches__3214EC27C5E9490C");

            entity.ToTable("BranchesOfSeven");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.StoreName).HasMaxLength(100);
            entity.Property(e => e.StoreNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BucketLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BucketLo__3214EC072FFDAC9A");

            entity.Property(e => e.BucketReason).HasMaxLength(500);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.BackendMmember).WithMany(p => p.BucketLogs)
                .HasForeignKey(d => d.BackendMmemberId)
                .HasConstraintName("FK__BucketLog__Backe__32AB8735");

            entity.HasOne(d => d.Board).WithMany(p => p.BucketLogs)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BucketLog__Board__339FAB6E");

            entity.HasOne(d => d.BucketMember).WithMany(p => p.BucketLogBucketMembers)
                .HasForeignKey(d => d.BucketMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BucketLog__Bucke__3493CFA7");

            entity.HasOne(d => d.ModeratorMember).WithMany(p => p.BucketLogModeratorMembers)
                .HasForeignKey(d => d.ModeratorMemberId)
                .HasConstraintName("FK__BucketLog__Moder__3587F3E0");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carts__3214EC071667CB8E");

            entity.HasOne(d => d.Member).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carts__MemberId__367C1819");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carts__ProductId__37703C52");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatMessage_Members");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coupons__3214EC07A29B38A8");

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(30);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedBackendMember).WithMany(p => p.CouponCreatedBackendMembers)
                .HasForeignKey(d => d.CreatedBackendMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coupons__Created__395884C4");

            entity.HasOne(d => d.DiscountType).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.DiscountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coupons__Discoun__3A4CA8FD");

            entity.HasOne(d => d.ModifiedBackendMember).WithMany(p => p.CouponModifiedBackendMembers)
                .HasForeignKey(d => d.ModifiedBackendMemberId)
                .HasConstraintName("FK__Coupons__Modifie__3B40CD36");
        });

        modelBuilder.Entity<CouponsProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CouponsP__3214EC073CD86874");

            entity.HasOne(d => d.Coupon).WithMany(p => p.CouponsProducts)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CouponsPr__Coupo__3C34F16F");

            entity.HasOne(d => d.Product).WithMany(p => p.CouponsProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CouponsPr__Produ__3D2915A8");
        });

        modelBuilder.Entity<DiscountTypeCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC07FC8C8A11");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FAQ__3214EC07F0C29A53");

            entity.ToTable("FAQ");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IssueType).WithMany(p => p.Faqs)
                .HasForeignKey(d => d.IssueTypeId)
                .HasConstraintName("FK__FAQ__IssueTypeId__3E1D39E1");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3214EC07DBA889C7");

            entity.Property(e => e.ChiName).HasMaxLength(50);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1500);
            entity.Property(e => e.EngName).HasMaxLength(100);
            entity.Property(e => e.GameCoverImg).HasMaxLength(100);
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedBackendMember).WithMany(p => p.GameCreatedBackendMembers)
                .HasForeignKey(d => d.CreatedBackendMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Games__CreatedBa__43D61337");

            entity.HasOne(d => d.ModifiedBackendMember).WithMany(p => p.GameModifiedBackendMembers)
                .HasForeignKey(d => d.ModifiedBackendMemberId)
                .HasConstraintName("FK__Games__ModifiedB__44CA3770");
        });

        modelBuilder.Entity<GameClassificationGame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameClas__3214EC07399E1FB7");

            entity.HasOne(d => d.GameClassification).WithMany(p => p.GameClassificationGames)
                .HasForeignKey(d => d.GameClassificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameClass__GameC__3F115E1A");

            entity.HasOne(d => d.Game).WithMany(p => p.GameClassificationGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameClass__GameI__40058253");
        });

        modelBuilder.Entity<GameClassificationsCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameClas__3214EC073BEBEF10");

            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<GameComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameComm__3214EC07603D3FE2");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.DeleteDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.DeleteBackendMember).WithMany(p => p.GameComments)
                .HasForeignKey(d => d.DeleteBackendMemberId)
                .HasConstraintName("FK__GameComme__Delet__40F9A68C");

            entity.HasOne(d => d.Game).WithMany(p => p.GameComments)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameComme__GameI__41EDCAC5");

            entity.HasOne(d => d.Member).WithMany(p => p.GameComments)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameComme__Membe__42E1EEFE");
        });

        modelBuilder.Entity<GamePlatformsCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GamePlat__3214EC076331A2D5");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC079176FDB4");

            entity.HasIndex(e => e.Index, "UQ__Inventor__9A5B6229FEDBFE67").IsUnique();

            entity.Property(e => e.Cost).HasColumnType("decimal(8, 0)");
            entity.Property(e => e.GameKey).HasMaxLength(50);
            entity.Property(e => e.Index).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Produ__45BE5BA9");

            entity.HasOne(d => d.StockInSheet).WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.StockInSheetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Stock__46B27FE2");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Issues__3214EC076F9BA709");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.File).HasMaxLength(600);

            entity.HasOne(d => d.IssueType).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueTypeId)
                .HasConstraintName("FK__Issues__IssueTyp__47A6A41B");

            entity.HasOne(d => d.Member).WithMany(p => p.Issues)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Issues__MemberId__489AC854");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Issues)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK__Issues__Status__498EEC8D");
        });

        modelBuilder.Entity<IssueStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IssueSta__3214EC07BB2954F4");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<IssueTypesCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IssueTyp__3214EC075DA23D8F");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Members__3214EC079531C88F");

            entity.Property(e => e.AboutMe).HasMaxLength(500);
            entity.Property(e => e.Account)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.ConfirmCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IconImg).HasMaxLength(100);
            entity.Property(e => e.LastOnlineTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MemberFollow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberFo__3213E83FCB3F974F");

            entity.HasOne(d => d.FollowedMember).WithMany(p => p.MemberFollowFollowedMembers)
                .HasForeignKey(d => d.FollowedMemberId)
                .HasConstraintName("FK__MemberFol__follo__55BFB948");

            entity.HasOne(d => d.FollowerMember).WithMany(p => p.MemberFollowFollowerMembers)
                .HasForeignKey(d => d.FollowerMemberId)
                .HasConstraintName("FK__MemberFol__follo__54CB950F");
        });

        modelBuilder.Entity<MemberProductView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberPr__3214EC0702524952");

            entity.Property(e => e.ViewTime).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberProductViews)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberPro__Membe__4C6B5938");

            entity.HasOne(d => d.Product).WithMany(p => p.MemberProductViews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberPro__Produ__4D5F7D71");
        });

        modelBuilder.Entity<MembersBoard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MembersB__3214EC0797B849EB");

            entity.HasOne(d => d.Board).WithMany(p => p.MembersBoards)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MembersBo__Board__4E53A1AA");

            entity.HasOne(d => d.Member).WithMany(p => p.MembersBoards)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MembersBo__Membe__4F47C5E3");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__News__3214EC07746A61CB");

            entity.Property(e => e.CoverImg).HasMaxLength(100);
            entity.Property(e => e.DeleteDatetime).HasColumnType("datetime");
            entity.Property(e => e.EditDatetime).HasColumnType("datetime");
            entity.Property(e => e.ScheduleDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.BackendMember).WithMany(p => p.NewsBackendMembers)
                .HasForeignKey(d => d.BackendMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__BackendMem__503BEA1C");

            entity.HasOne(d => d.DeleteBackendMember).WithMany(p => p.NewsDeleteBackendMembers)
                .HasForeignKey(d => d.DeleteBackendMemberId)
                .HasConstraintName("FK__News__DeleteBack__51300E55");

            entity.HasOne(d => d.Games).WithMany(p => p.News)
                .HasForeignKey(d => d.GamesId)
                .HasConstraintName("FK__News__GamesId__5224328E");

            entity.HasOne(d => d.NewsCategory).WithMany(p => p.News)
                .HasForeignKey(d => d.NewsCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__NewsCatego__531856C7");
        });

        modelBuilder.Entity<NewsCategoryCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsCate__3214EC079E03BAC3");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NewsComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsComm__3214EC0754680840");

            entity.Property(e => e.Content).HasMaxLength(280);
            entity.Property(e => e.DeleteDatetime).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.DeleteBackendMember).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.DeleteBackendMemberId)
                .HasConstraintName("FK__NewsComme__Delet__55009F39");

            entity.HasOne(d => d.DeleteMember).WithMany(p => p.NewsCommentDeleteMembers)
                .HasForeignKey(d => d.DeleteMemberId)
                .HasConstraintName("FK__NewsComme__Delet__540C7B00");

            entity.HasOne(d => d.Member).WithMany(p => p.NewsCommentMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsComme__Membe__55F4C372");

            entity.HasOne(d => d.News).WithMany(p => p.NewsComments)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsComme__NewsI__56E8E7AB");
        });

        modelBuilder.Entity<NewsLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsLike__3214EC07B327BFC9");

            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.NewsLikes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsLikes__Membe__5AB9788F");

            entity.HasOne(d => d.News).WithMany(p => p.NewsLikes)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsLikes__NewsI__5BAD9CC8");
        });

        modelBuilder.Entity<NewsView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsView__3214EC075ED04278");

            entity.Property(e => e.ViewTime).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.NewsViews)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsViews__Membe__5CA1C101");

            entity.HasOne(d => d.News).WithMany(p => p.NewsViews)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsViews__NewsI__5D95E53A");
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Newslett__3214EC07CEDE5AD7");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.BackendMember).WithMany(p => p.Newsletters)
                .HasForeignKey(d => d.BackendMemberId)
                .HasConstraintName("FK__Newslette__Backe__59C55456");
        });

        modelBuilder.Entity<NewsletterLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Newslett__3214EC0743354FBA");

            entity.Property(e => e.AddresseeMemberEmail)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AddresseeMemberId).HasColumnName("AddresseeMemberID");
            entity.Property(e => e.AddresseeMemberName).HasMaxLength(50);
            entity.Property(e => e.EmailSentDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.AddresseeMember).WithMany(p => p.NewsletterLogs)
                .HasForeignKey(d => d.AddresseeMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Newslette__Addre__57DD0BE4");

            entity.HasOne(d => d.Newsletter).WithMany(p => p.NewsletterLogs)
                .HasForeignKey(d => d.NewsletterId)
                .HasConstraintName("FK__Newslette__Newsl__58D1301D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0743A819E5");

            entity.HasIndex(e => e.Index, "UQ__Orders__9A5B62299A9DCB7B").IsUnique();

            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreditCardBrand).HasMaxLength(20);
            entity.Property(e => e.DeliveredAt).HasColumnType("datetime");
            entity.Property(e => e.Index).HasMaxLength(30);
            entity.Property(e => e.LinePayTransactionId).HasMaxLength(30);
            entity.Property(e => e.MaskedCreditCardNumber).HasMaxLength(20);
            entity.Property(e => e.PaidAt).HasColumnType("datetime");
            entity.Property(e => e.ReceiverCellPhone).HasMaxLength(20);
            entity.Property(e => e.ReceiverEmail).HasMaxLength(50);
            entity.Property(e => e.RecipientName).HasMaxLength(20);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.ShippingFee).HasColumnType("decimal(8, 0)");
            entity.Property(e => e.ToAddress).HasMaxLength(50);
            entity.Property(e => e.TrackingNum).HasMaxLength(20);

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__MemberId__51300E55");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__OrderSta__5224328E");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentS__531856C7");

            entity.HasOne(d => d.ShipmentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipmentMethodId)
                .HasConstraintName("FK__Orders__Shipmemt__540C7B00");

            entity.HasOne(d => d.ShipmentStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipmentStatusId)
                .HasConstraintName("FK__Orders__Shipment__55009F39");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07A2DE618F");

            entity.HasIndex(e => e.InventoryItemId, "UQ__OrderIte__3BB2AC81819DD448").IsUnique();

            entity.HasIndex(e => e.Index, "UQ__OrderIte__9A5B62295905CB9C").IsUnique();

            entity.Property(e => e.Index).HasMaxLength(20);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(8, 0)");

            entity.HasOne(d => d.InventoryItem).WithOne(p => p.OrderItem)
                .HasForeignKey<OrderItem>(d => d.InventoryItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Inven__4C6B5938");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__4D5F7D71");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__4E53A1AA");
        });

        modelBuilder.Entity<OrderItemReturn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07661B879E");

            entity.HasIndex(e => e.OrderItemId, "UQ__OrderIte__57ED0680FBF10CC1").IsUnique();

            entity.HasIndex(e => e.Index, "UQ__OrderIte__9A5B6229DB395A7A").IsUnique();

            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.Index).HasMaxLength(50);
            entity.Property(e => e.IssuedAt).HasColumnType("datetime");
            entity.Property(e => e.LinePayRefundTransactionId).HasMaxLength(25);
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.OrderItem).WithOne(p => p.OrderItemReturn)
                .HasForeignKey<OrderItemReturn>(d => d.OrderItemId)
                .HasConstraintName("FK__OrderItem__Order__4B7734FF");
        });

        modelBuilder.Entity<OrderItemsCoupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07E352582E");

            entity.HasOne(d => d.Coupon).WithMany(p => p.OrderItemsCoupons)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__OrderItem__Coupo__625A9A57");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.OrderItemsCoupons)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("FK__OrderItem__Order__503BEA1C");
        });

        modelBuilder.Entity<OrderStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC0760CAB591");

            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<PaymentStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentS__3214EC07AC743720");

            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC0782B66CC7");

            entity.Property(e => e.Content).HasMaxLength(2500);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.DeleteDatetime).HasColumnType("datetime");
            entity.Property(e => e.LastEditDatetime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Board).WithMany(p => p.Posts)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK__Posts__BoardId__76619304");

            entity.HasOne(d => d.DeleteBackendMember).WithMany(p => p.Posts)
                .HasForeignKey(d => d.DeleteBackendMemberId)
                .HasConstraintName("FK__Posts__DeleteBac__7755B73D");

            entity.HasOne(d => d.DeleteMember).WithMany(p => p.PostDeleteMembers)
                .HasForeignKey(d => d.DeleteMemberId)
                .HasConstraintName("FK__Posts__DeleteMem__7849DB76");

            entity.HasOne(d => d.Member).WithMany(p => p.PostMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__MemberId__793DFFAF");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostComm__3214EC07FFFF4D08");

            entity.Property(e => e.Content).HasMaxLength(280);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.DeleteDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.DeleteBackendMember).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.DeleteBackendMemberId)
                .HasConstraintName("FK__PostComme__Delet__6CD828CA");

            entity.HasOne(d => d.DeleteMember).WithMany(p => p.PostCommentDeleteMembers)
                .HasForeignKey(d => d.DeleteMemberId)
                .HasConstraintName("FK__PostComme__Delet__6BE40491");

            entity.HasOne(d => d.Member).WithMany(p => p.PostCommentMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__Membe__6DCC4D03");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__PostComme__Paren__6EC0713C");

            entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__PostI__6FB49575");
        });

        modelBuilder.Entity<PostCommentReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostComm__3214EC074ECDFEB1");

            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.ReviewDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.PostCommentReports)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__Membe__690797E6");

            entity.HasOne(d => d.PostComment).WithMany(p => p.PostCommentReports)
                .HasForeignKey(d => d.PostCommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__PostC__69FBBC1F");

            entity.HasOne(d => d.ReviewerBackenMember).WithMany(p => p.PostCommentReports)
                .HasForeignKey(d => d.ReviewerBackenMemberId)
                .HasConstraintName("FK__PostComme__Revie__6AEFE058");
        });

        modelBuilder.Entity<PostCommentUpDownVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostComm__3214EC070F6B5F47");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.PostCommentUpDownVotes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__Membe__70A8B9AE");

            entity.HasOne(d => d.PostComment).WithMany(p => p.PostCommentUpDownVotes)
                .HasForeignKey(d => d.PostCommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__PostC__719CDDE7");
        });

        modelBuilder.Entity<PostEditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostEdit__3214EC070273BADC");

            entity.Property(e => e.ContentBeforeEdit).HasMaxLength(1500);
            entity.Property(e => e.EditDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.PostEditLogs)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostEditL__PostI__72910220");
        });

        modelBuilder.Entity<PostReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostRepo__3214EC07AC02F5AE");

            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.ReviewDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.PostReports)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostRepor__Membe__73852659");

            entity.HasOne(d => d.Post).WithMany(p => p.PostReports)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostRepor__PostI__74794A92");

            entity.HasOne(d => d.ReviewerBackenMember).WithMany(p => p.PostReports)
                .HasForeignKey(d => d.ReviewerBackenMemberId)
                .HasConstraintName("FK__PostRepor__Revie__756D6ECB");
        });

        modelBuilder.Entity<PostUpDownVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostUpDo__3214EC07A644DA37");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.PostUpDownVotes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostUpDow__Membe__7A3223E8");

            entity.HasOne(d => d.Post).WithMany(p => p.PostUpDownVotes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostUpDow__PostI__7B264821");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07E374D36C");

            entity.HasIndex(e => e.Index, "UQ__Products__9A5B6229DCFF5AFE").IsUnique();

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Index).HasMaxLength(20);
            entity.Property(e => e.ModifiedTime).HasColumnType("datetime");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.SystemRequire).HasMaxLength(1500);

            entity.HasOne(d => d.CreatedBackendMember).WithMany(p => p.ProductCreatedBackendMembers)
                .HasForeignKey(d => d.CreatedBackendMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Create__7D0E9093");

            entity.HasOne(d => d.Game).WithMany(p => p.Products)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Products__GameId__7E02B4CC");

            entity.HasOne(d => d.GamePlatform).WithMany(p => p.Products)
                .HasForeignKey(d => d.GamePlatformId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__GamePl__7EF6D905");

            entity.HasOne(d => d.ModifiedBackendMember).WithMany(p => p.ProductModifiedBackendMembers)
                .HasForeignKey(d => d.ModifiedBackendMemberId)
                .HasConstraintName("FK__Products__Modifi__7FEAFD3E");

            entity.HasOne(d => d.ProductStatus).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Produc__00DF2177");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC07FDC9D9D6");

            entity.Property(e => e.Image).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__7C1A6C5A");
        });

        modelBuilder.Entity<ProductStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC0727578243");

            entity.Property(e => e.Name).HasMaxLength(5);
        });

        modelBuilder.Entity<Reply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Replies__3214EC07447FE450");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.BackendMember).WithMany(p => p.Replies)
                .HasForeignKey(d => d.BackendMemberId)
                .HasConstraintName("FK__Replies__Backend__01D345B0");

            entity.HasOne(d => d.Issue).WithMany(p => p.Replies)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK__Replies__IssueId__02C769E9");
        });

        modelBuilder.Entity<ShipmentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC076B30CAD2");

            entity.HasIndex(e => e.Name, "UQ__Shipment__737584F63E3825D6").IsUnique();

            entity.Property(e => e.Cost).HasColumnType("decimal(8, 0)");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<ShipmentStatusesCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC07EA314522");

            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<StandardProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Standard__3214EC07163E9827");

            entity.HasOne(d => d.Product).WithMany(p => p.StandardProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StandardP__Produ__03BB8E22");
        });

        modelBuilder.Entity<StockInSheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockInS__3214EC0723599546");

            entity.HasIndex(e => e.Index, "UQ__StockInS__9A5B6229166A9A16").IsUnique();

            entity.Property(e => e.ArrivedAt).HasColumnType("datetime");
            entity.Property(e => e.Index).HasMaxLength(20);
            entity.Property(e => e.OrderRequestDate).HasColumnType("datetime");

            entity.HasOne(d => d.StockInStatus).WithMany(p => p.StockInSheets)
                .HasForeignKey(d => d.StockInStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockInSh__Stock__04AFB25B");

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockInSheets)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockInSh__Suppl__05A3D694");
        });

        modelBuilder.Entity<StockInStatusCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockInS__3214EC0721C2C7C9");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC0799F6DE96");

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<TrackProduct>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.TrackProducts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrackProducts_Members");

            entity.HasOne(d => d.Product).WithMany(p => p.TrackProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrackProducts_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
