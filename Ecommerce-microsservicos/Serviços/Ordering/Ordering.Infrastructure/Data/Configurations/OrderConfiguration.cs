
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(orderItemId => orderItemId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .IsRequired();

            builder.HasMany(c => c.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                c => c.OrderName, nameBuilder => {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder => {
                    addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                    addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder => {
                    addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                    addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.Payment, paymentBuilder => {
                    paymentBuilder.Property(c => c.CardName)
                    .HasMaxLength(50);

                    paymentBuilder.Property(c => c.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                    paymentBuilder.Property(c => c.Expiration)
                    .HasMaxLength(10);

                    paymentBuilder.Property(c => c.CVV)
                    .HasMaxLength(3);

                    paymentBuilder.Property(c => c.PaymentMethod);
                }
            );

            builder.Property(c => c.Status)
                .HasDefaultValue(OrderStatusEnum.Draft)
                .HasConversion(
                    d => d.ToString(),
                    dbStatus => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), dbStatus));

            builder.Property(c => c.TotalPrice);
        }
    }
}
