using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
           
            HasMany(x => x.Following).WithMany().Map(x => { x.ToTable("AspNetUserFollowing");
                x.MapLeftKey("UserId");
                x.MapRightKey("FollowingId");
            });
            HasMany(x => x.Followers).WithMany().Map(x=> { x.ToTable("AspNetUserFollower"); x.MapLeftKey("UserId");
                x.MapRightKey("FollowerId");
            });

            HasOptional(x => x.Creator)
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .WillCascadeOnDelete(false);

            HasMany(x=>x.CreatedEntities)
                .WithRequired(x=>x.Creator)
                .HasForeignKey(x=>x.CreatorId)
                .WillCascadeOnDelete(false);
            HasMany(x => x.Addresses).WithRequired().HasForeignKey(x=>x.UserId);
            HasMany(x=>x.Attributes).WithRequired().HasForeignKey(x=>x.UserId).WillCascadeOnDelete(true);
            HasMany(x=>x.ShoppingCartItems).WithRequired(x=>x.Creator).WillCascadeOnDelete(true);
        }
    }
}