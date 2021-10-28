using JustTradeIt.Software.API.Repositories.Entities;
using Microsoft.EntityFrameworkCore;


namespace JustTradeIt.Software.API.Repositories.Contexts
{
    public class TradeItDbContext : DbContext
    {
        public TradeItDbContext(DbContextOptions<TradeItDbContext> options) : base(options) {}
        
        
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Item> Items  {get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>()
                .HasOne(p => p.Sender)
                .WithMany(p => p.TradeSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            
            modelBuilder.Entity<Trade>()
                .HasOne(p => p.Receiver)
                .WithMany(p => p.TradeReceived)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TradeItem>()
                .HasKey(t => new {t.TradeId, t.UserId, t.ItemId});

            modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemConditionNav)
                .WithMany(c => c.Items)
                .HasForeignKey(l => l.ItemConditionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            modelBuilder.Entity<ItemImage>()
                .HasOne(im => im.ItemNav)
                .WithMany(c=> c.ItemImages)
                .HasForeignKey(im => im.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    
    }
}