using Auction.Domain.Auctions;
using Auction.Domain.Items;
using Auction.Infrastructure.Middleware;
using Carsties.Core;
using Carsties.Core.Interfaces;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Data;

public sealed class AuctionDbContext : DbContext
{
    public DbSet<AuctionEntity> Auctions { get; init; }
    public DbSet<ItemEntity> Items { get; init; }

    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        modelBuilder.AddInboxStateEntity(ConfigureInboxState);
        modelBuilder.AddOutboxMessageEntity(ConfigureOutboxMessage);
        modelBuilder.AddOutboxStateEntity(ConfigureOutboxState);
    }

    #region OutboxEntityConfigurations

    private static void ConfigureInboxState(EntityTypeBuilder<InboxState> builder)
    {
        builder.ToTable("inbox_state");

        builder.HasKey(i => i.Id)
            .HasName("pk_inbox_state");

        builder.HasAlternateKey(i => new { i.MessageId, i.ConsumerId })
            .HasName("ak_inbox_state_message_id_consumer_id");

        builder.HasIndex(i => i.Delivered)
            .HasDatabaseName("ix_inbox_state_delivered");

        builder.Property(i => i.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName("id");
        builder.Property(i => i.MessageId)
            .IsRequired()
            .HasColumnName("message_id");
        builder.Property(i => i.ConsumerId)
            .IsRequired()
            .HasColumnName("consumer_id");
        builder.Property(i => i.LockId)
            .IsRequired()
            .HasColumnName("lock_id");
        builder.Property(i => i.RowVersion)
            .IsRowVersion()
            .HasColumnName("row_version");
        builder.Property(i => i.Received)
            .IsRequired()
            .HasColumnName("received");
        builder.Property(i => i.ReceiveCount)
            .IsRequired()
            .HasColumnName("receive_count");
        builder.Property(i => i.ExpirationTime)
            .HasColumnName("expiration_time");
        builder.Property(i => i.Consumed)
            .HasColumnName("consumed");
        builder.Property(i => i.Delivered)
            .HasColumnName("delivered");
        builder.Property(i => i.LastSequenceNumber)
            .HasColumnName("last_sequence_number");
    }

    private static void ConfigureOutboxMessage(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_message");

        builder.HasKey(m => m.SequenceNumber)
            .HasName("pk_outbox_message");

        builder.HasIndex(m => m.EnqueueTime)
            .HasDatabaseName("ix_outbox_message_enqueue_time");

        builder.HasIndex(m => m.ExpirationTime)
            .HasDatabaseName("ix_outbox_message_expiration_time");

        builder.HasIndex(m => new { m.InboxMessageId, m.InboxConsumerId, m.SequenceNumber })
            .IsUnique()
            .HasDatabaseName("ix_outbox_message_inbox_message_id_inbox_consumer_id_sequence_number");

        builder.HasIndex(m => new { m.OutboxId, m.SequenceNumber })
            .IsUnique()
            .HasDatabaseName("ix_outbox_message_outbox_id_sequence_number");

        builder.HasOne<InboxState>()
            .WithMany()
            .HasForeignKey(m => new { m.InboxMessageId, m.InboxConsumerId })
            .HasPrincipalKey(i => new { i.MessageId, i.ConsumerId })
            .HasConstraintName("fk_outbox_message_inbox_state_inbox_message_id_inbox_consumer_id")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<OutboxState>()
            .WithMany()
            .HasForeignKey(m => m.OutboxId)
            .HasPrincipalKey(s => s.OutboxId)
            .HasConstraintName("fk_outbox_message_outbox_state_outbox_id")
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(m => m.SequenceNumber)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName("sequence_number");
        builder.Property(m => m.EnqueueTime)
            .HasColumnName("enqueue_time");
        builder.Property(m => m.SentTime)
            .IsRequired()
            .HasColumnName("sent_time");
        builder.Property(m => m.Headers)
            .HasColumnName("headers");
        builder.Property(m => m.Properties)
            .HasColumnName("properties");
        builder.Property(m => m.InboxMessageId)
            .HasColumnName("inbox_message_id");
        builder.Property(m => m.InboxConsumerId)
            .HasColumnName("inbox_consumer_id");
        builder.Property(m => m.OutboxId)
            .HasColumnName("outbox_id");
        builder.Property(m => m.MessageId)
            .IsRequired()
            .HasColumnName("message_id");
        builder.Property(m => m.ContentType)
            .HasMaxLength(256)
            .IsRequired()
            .HasColumnName("content_type");
        builder.Property(m => m.MessageType)
            .IsRequired()
            .HasColumnName("message_type");
        builder.Property(m => m.Body)
            .IsRequired()
            .HasColumnName("body");
        builder.Property(m => m.ConversationId)
            .HasColumnName("conversation_id");
        builder.Property(m => m.CorrelationId)
            .HasColumnName("correlation_id");
        builder.Property(m => m.InitiatorId)
            .HasColumnName("initiator_id");
        builder.Property(m => m.RequestId)
            .HasColumnName("request_id");
        builder.Property(m => m.SourceAddress)
            .HasMaxLength(256)
            .HasColumnName("source_address");
        builder.Property(m => m.DestinationAddress)
            .HasMaxLength(256)
            .HasColumnName("destination_address");
        builder.Property(m => m.ResponseAddress)
            .HasMaxLength(256)
            .HasColumnName("response_address");
        builder.Property(m => m.FaultAddress)
            .HasMaxLength(256)
            .HasColumnName("fault_address");
        builder.Property(m => m.ExpirationTime)
            .HasColumnName("expiration_time");
    }

    private static void ConfigureOutboxState(EntityTypeBuilder<OutboxState> builder)
    {
        builder.ToTable("outbox_state");

        builder.HasKey(s => s.OutboxId)
            .HasName("pk_outbox_state");

        builder.HasIndex(s => s.Created)
            .HasDatabaseName("ix_outbox_state_created");

        builder.Property(s => s.OutboxId)
            .IsRequired()
            .HasColumnName("outbox_id");
        builder.Property(s => s.LockId)
            .IsRequired()
            .HasColumnName("lock_id");
        builder.Property(s => s.RowVersion)
            .IsRowVersion()
            .HasColumnName("row_version");
        builder.Property(s => s.Created)
            .IsRequired()
            .HasColumnName("created");
        builder.Property(s => s.Delivered)
            .HasColumnName("delivered");
        builder.Property(s => s.LastSequenceNumber)
            .HasColumnName("last_sequence_number");
    }

    #endregion
}