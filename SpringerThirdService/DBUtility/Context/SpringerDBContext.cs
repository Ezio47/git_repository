namespace TLW.AH.Business.DBUtility
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SpringerDBContext : DbContext
    {
        public SpringerDBContext()
            : base("name=SpringerDBContext")
        {
        }

        public virtual DbSet<JC_FIRE> JC_FIRE { get; set; }
        public virtual DbSet<JC_MONITOR_BASICINFO> JC_MONITOR_BASICINFO { get; set; }
        public virtual DbSet<T_SYS_ORG> T_SYS_ORG { get; set; }
        public virtual DbSet<T_VIDEO_ORIGINAL> T_VIDEO_ORIGINAL { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.FIRENAME)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.BYORGNO)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.FIREFROM)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.MARK)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.ZQWZ)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.WXBH)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.DQRDBH)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.DL)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.YY)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.JXHQSJ)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.PFUSERID)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.PFORGNO)
                .IsUnicode(false);

            modelBuilder.Entity<JC_FIRE>()
                .Property(e => e.FIREFROMWEATHER)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.TTBH)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.EMNAME)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.BYORGNO)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.LOGINUSERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.USERPWD)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.XH)
                .IsUnicode(false);

            modelBuilder.Entity<JC_MONITOR_BASICINFO>()
                .Property(e => e.PP)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.ORGNO)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.ORGNAME)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.ORGDUTY)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.LEADER)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.AREACODE)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.ORGJC)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.WXJC)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.SYSFLAG)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.COMMANDNAME)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.CITYID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.WEATHERJC)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.POSTCODE)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.DUTYTELL)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.MOBILEPARAMLIST)
                .IsUnicode(false);

            modelBuilder.Entity<T_SYS_ORG>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.DEVICEID)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.ALARMID)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.HOR)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.PIT)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.VIEW)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.LONGTITUE)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.LATITUE)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.ELEVATION)
                .IsUnicode(false);

            modelBuilder.Entity<T_VIDEO_ORIGINAL>()
                .Property(e => e.BAK)
                .IsUnicode(false);
        }
    }
}
