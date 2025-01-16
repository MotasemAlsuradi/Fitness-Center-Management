    using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Center_Management.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Blogsection> Blogsections { get; set; }

    public virtual DbSet<Classtype> Classtypes { get; set; }

    public virtual DbSet<Contactform> Contactforms { get; set; }

    public virtual DbSet<Featuressection> Featuressections { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Landingsection> Landingsections { get; set; }

    public virtual DbSet<Maininformationoffitnesscenter> Maininformationoffitnesscenters { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Subscriptionplan> Subscriptionplans { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Trainerclass> Trainerclasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }

    public virtual DbSet<Usersubscription> Usersubscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=localhost:1521;USER ID=C##FitnessCenterManagement;PASSWORD=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##FITNESSCENTERMANAGEMENT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Adminid).HasName("SYS_C008657");

            entity.ToTable("ADMIN");

            entity.HasIndex(e => e.Firstname, "SYS_C008658").IsUnique();

            entity.HasIndex(e => e.Lastname, "SYS_C008659").IsUnique();

            entity.HasIndex(e => e.Email, "SYS_C008660").IsUnique();

            entity.Property(e => e.Adminid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ADMINID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Picture)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PICTURE");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Bankid).HasName("SYS_C008768");

            entity.ToTable("BANK");

            entity.Property(e => e.Bankid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("BANKID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardholdername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARDHOLDERNAME");
            entity.Property(e => e.Cardnumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("CVV");
            entity.Property(e => e.Expirydate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRYDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Banks)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008769");
        });

        modelBuilder.Entity<Blogsection>(entity =>
        {
            entity.HasKey(e => e.Blogsectionid).HasName("SYS_C008742");

            entity.ToTable("BLOGSECTION");

            entity.Property(e => e.Blogsectionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("BLOGSECTIONID");
            entity.Property(e => e.Firsttiptext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTTIPTEXT");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Secoundtiptext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SECOUNDTIPTEXT");
            entity.Property(e => e.Tiptitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPTITLE");
        });

        modelBuilder.Entity<Classtype>(entity =>
        {
            entity.HasKey(e => e.Classtypeid).HasName("SYS_C008689");

            entity.ToTable("CLASSTYPES");

            entity.HasIndex(e => e.Classname, "SYS_C008690").IsUnique();

            entity.Property(e => e.Classtypeid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSTYPEID");
            entity.Property(e => e.Classdescription)
                .HasColumnType("CLOB")
                .HasColumnName("CLASSDESCRIPTION");
            entity.Property(e => e.Classname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.Dayofweek)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DAYOFWEEK");
            entity.Property(e => e.Endtime)
                .HasPrecision(6)
                .HasColumnName("ENDTIME");
            entity.Property(e => e.Starttime)
                .HasPrecision(6)
                .HasColumnName("STARTTIME");
        });

        modelBuilder.Entity<Contactform>(entity =>
        {
            entity.HasKey(e => e.Contactformid).HasName("SYS_C008747");

            entity.ToTable("CONTACTFORM");

            entity.Property(e => e.Contactformid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACTFORMID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message)
                .HasColumnType("CLOB")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Featuressection>(entity =>
        {
            entity.HasKey(e => e.Featuressectionid).HasName("SYS_C008738");

            entity.ToTable("FEATURESSECTION");

            entity.Property(e => e.Featuressectionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("FEATURESSECTIONID");
            entity.Property(e => e.Button)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BUTTON");
            entity.Property(e => e.Firstlefttext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTLEFTTEXT");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Secoundlefttext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SECOUNDLEFTTEXT");
            entity.Property(e => e.Thirdlefttext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("THIRDLEFTTEXT");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Feedbackid).HasName("SYS_C008773");

            entity.ToTable("FEEDBACK");

            entity.Property(e => e.Feedbackid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("FEEDBACKID");
            entity.Property(e => e.Comments)
                .HasColumnType("CLOB")
                .HasColumnName("COMMENTS");
            entity.Property(e => e.Isactive)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("0 ")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Userregistrationdate)
                .HasPrecision(6)
                .HasColumnName("USERREGISTRATIONDATE");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FEEDBACK_USER");
        });

        modelBuilder.Entity<Landingsection>(entity =>
        {
            entity.HasKey(e => e.Landingsectionid).HasName("SYS_C008732");

            entity.ToTable("LANDINGSECTION");

            entity.Property(e => e.Landingsectionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("LANDINGSECTIONID");
            entity.Property(e => e.Firstbutton)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTBUTTON");
            entity.Property(e => e.Firstwelcometext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTWELCOMETEXT");
            entity.Property(e => e.Mainimage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAINIMAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Secoundbutton)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SECOUNDBUTTON");
            entity.Property(e => e.Secoundwelcometext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SECOUNDWELCOMETEXT");
        });

        modelBuilder.Entity<Maininformationoffitnesscenter>(entity =>
        {
            entity.HasKey(e => e.Maininformationoffitnesscenterid).HasName("SYS_C008715");

            entity.ToTable("MAININFORMATIONOFFITNESSCENTER");

            entity.HasIndex(e => e.Name, "SYS_C008716").IsUnique();

            entity.HasIndex(e => e.Openday, "SYS_C008717").IsUnique();

            entity.HasIndex(e => e.Worktime, "SYS_C008718").IsUnique();

            entity.HasIndex(e => e.Testamoinaltext, "SYS_C008719").IsUnique();

            entity.HasIndex(e => e.Welcomelocationtext, "SYS_C008720").IsUnique();

            entity.HasIndex(e => e.Locationtext, "SYS_C008721").IsUnique();

            entity.HasIndex(e => e.Locationsource, "SYS_C008722").IsUnique();

            entity.HasIndex(e => e.Copyrighttext, "SYS_C008723").IsUnique();

            entity.HasIndex(e => e.Email, "SYS_C008724").IsUnique();

            entity.HasIndex(e => e.Phone, "SYS_C008725").IsUnique();

            entity.Property(e => e.Maininformationoffitnesscenterid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MAININFORMATIONOFFITNESSCENTERID");
            entity.Property(e => e.Closedday)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CLOSEDDAY");
            entity.Property(e => e.Copyrighttext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COPYRIGHTTEXT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstabouttext)
                .HasColumnType("CLOB")
                .HasColumnName("FIRSTABOUTTEXT");
            entity.Property(e => e.Locationsource)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LOCATIONSOURCE");
            entity.Property(e => e.Locationtext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LOCATIONTEXT");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Openday)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPENDAY");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Secoundabouttext)
                .HasColumnType("CLOB")
                .HasColumnName("SECOUNDABOUTTEXT");
            entity.Property(e => e.Testamoinaltext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TESTAMOINALTEXT");
            entity.Property(e => e.Thirdabouttext)
                .HasColumnType("CLOB")
                .HasColumnName("THIRDABOUTTEXT");
            entity.Property(e => e.Welcomelocationtext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WELCOMELOCATIONTEXT");
            entity.Property(e => e.Worktime)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WORKTIME");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("SYS_C008782");

            entity.ToTable("PAYMENTS");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PAYMENTID");
            entity.Property(e => e.Paymentdate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("PAYMENTDATE");
            entity.Property(e => e.Subscriptionid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUBSCRIPTIONID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Subscriptionid)
                .HasConstraintName("SYS_C008784");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008783");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008669");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.Rolename, "SYS_C008670").IsUnique();

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Scheduleid).HasName("SYS_C008698");

            entity.ToTable("SCHEDULES");

            entity.Property(e => e.Scheduleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SCHEDULEID");
            entity.Property(e => e.Classtypeid)
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSTYPEID");
            entity.Property(e => e.Isactive)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("0 ")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Trainersid)
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERSID");

            entity.HasOne(d => d.Classtype).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Classtypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCHEDULE_CLASS");

            entity.HasOne(d => d.Trainers).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Trainersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCHEDULE_TRAINER");
        });

        modelBuilder.Entity<Subscriptionplan>(entity =>
        {
            entity.HasKey(e => e.Planid).HasName("SYS_C008683");

            entity.ToTable("SUBSCRIPTIONPLANS");

            entity.Property(e => e.Planid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PLANID");
            entity.Property(e => e.Advicetext)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADVICETEXT");
            entity.Property(e => e.Buttontext)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BUTTONTEXT");
            entity.Property(e => e.Descripelineeghit)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINEEGHIT");
            entity.Property(e => e.Descripelinefive)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINEFIVE");
            entity.Property(e => e.Descripelinefour)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINEFOUR");
            entity.Property(e => e.Descripelinenine)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINENINE");
            entity.Property(e => e.Descripelineone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINEONE");
            entity.Property(e => e.Descripelineseven)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINESEVEN");
            entity.Property(e => e.Descripelinesix)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINESIX");
            entity.Property(e => e.Descripelinethree)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINETHREE");
            entity.Property(e => e.Descripelinetwo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPELINETWO");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Trainersid).HasName("SYS_C008666");

            entity.ToTable("TRAINERS");

            entity.Property(e => e.Trainersid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERSID");
            entity.Property(e => e.Expertise)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EXPERTISE");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PICTURE");
            entity.Property(e => e.Socialfacebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SOCIALFACEBOOK");
            entity.Property(e => e.Sociallinkedin)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SOCIALLINKEDIN");
            entity.Property(e => e.Socialtwitter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SOCIALTWITTER");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Trainerclass>(entity =>
        {
            entity.HasKey(e => e.Trainerclassesid).HasName("SYS_C008692");

            entity.ToTable("TRAINERCLASSES");

            entity.Property(e => e.Trainerclassesid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERCLASSESID");
            entity.Property(e => e.Classtypeid)
                .HasColumnType("NUMBER")
                .HasColumnName("CLASSTYPEID");
            entity.Property(e => e.Trainersid)
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINERSID");

            entity.HasOne(d => d.Classtype).WithMany(p => p.Trainerclasses)
                .HasForeignKey(d => d.Classtypeid)
                .HasConstraintName("SYS_C008694");

            entity.HasOne(d => d.Trainers).WithMany(p => p.Trainerclasses)
                .HasForeignKey(d => d.Trainersid)
                .HasConstraintName("SYS_C008693");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C008757");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "SYS_C008758").IsUnique();

            entity.HasIndex(e => e.Phonenumber, "SYS_C008759").IsUnique();

            entity.HasIndex(e => e.Username, "SYS_C008760").IsUnique();

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Age)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AGE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PICTURE");
            entity.Property(e => e.Registrationdate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP\n")
                .HasColumnName("REGISTRATIONDATE");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity.HasKey(e => e.Userloginid).HasName("SYS_C008675");

            entity.ToTable("USERLOGIN");

            entity.Property(e => e.Userloginid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("SYS_C008676");
        });

        modelBuilder.Entity<Usersubscription>(entity =>
        {
            entity.HasKey(e => e.Subscriptionid).HasName("SYS_C008778");

            entity.ToTable("USERSUBSCRIPTIONS");

            entity.Property(e => e.Subscriptionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SUBSCRIPTIONID");
            entity.Property(e => e.Enddate)
                .HasPrecision(6)
                .HasColumnName("ENDDATE");
            entity.Property(e => e.Planid)
                .HasColumnType("NUMBER")
                .HasColumnName("PLANID");
            entity.Property(e => e.Startdate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("STARTDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Plan).WithMany(p => p.Usersubscriptions)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_SUBSCRIPTION_PLAN");

            entity.HasOne(d => d.User).WithMany(p => p.Usersubscriptions)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_SUBSCRIPTION_USER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
