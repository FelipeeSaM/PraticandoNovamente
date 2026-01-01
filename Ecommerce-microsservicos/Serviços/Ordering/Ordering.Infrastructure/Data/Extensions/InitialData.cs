namespace Ordering.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer> {
                Customer.Create(CustomerId.Of(new Guid("c9d1f2c6-e8ff-4a6d-aee8-93f73e77e199")), "User1","user1@hotmail.com"),
                Customer.Create(CustomerId.Of(new Guid("455e2eae-0d1e-4db4-9f97-93fb8a2607c2")), "User2","user2@hotmail.com")
            };

        public static IEnumerable<Product> Products =>
            new List<Product> {
                Product.Create(ProductId.Of(new Guid("3a54130e-22f8-4bcd-a7d5-c3681cf3e33f")), "Iphone X", 400),
                Product.Create(ProductId.Of(new Guid("506a0669-d8b7-44a8-848c-189cb13598e7")), "Samsung 10", 500),
                Product.Create(ProductId.Of(new Guid("853092ce-5e33-4588-a130-7e8b610f0513")), "Iphone 17", 400),
                Product.Create(ProductId.Of(new Guid("210cd0cf-a54e-4da0-abfa-2b2e70d0ed6a")), "Samsung 22", 500)
            };

        public static IEnumerable<Order> OrderWithItem
        {
            get {
                var address1 = Address.Of("User1", "LastName1","user1lastname@hotmail.com", "Street1", "Country1", "Pernambuco", "12345");
                var address2 = Address.Of("User2", "LastName2", "user2lastname@hotmail.com", "Street2", "Country2", "Pernambuco", "54321");

                var payment1 = Payment.Of("Card1", "1234567890123456", "12/28", "355", 1);
                var payment2 = Payment.Of("Card2", "5134667902613276", "10/28", "365", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("c9d1f2c6-e8ff-4a6d-aee8-93f73e77e199")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);

                order1.Add(ProductId.Of(new Guid("3a54130e-22f8-4bcd-a7d5-c3681cf3e33f")), 2, 400);
                order1.Add(ProductId.Of(new Guid("506a0669-d8b7-44a8-848c-189cb13598e7")), 2, 400);

                var address3 = Address.Of("User3", "LastName3", "user1lastname@hotmail.com", "Street3", "Country3", "Pernambuco", "61236737");
                var address4 = Address.Of("User4", "LastName4", "user2lastname@hotmail.com", "Street4", "Country4", "Pernambuco", "83417891");

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("455e2eae-0d1e-4db4-9f97-93fb8a2607c2")),
                    OrderName.Of("ORD_2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment2);

                order2.Add(ProductId.Of(new Guid("853092ce-5e33-4588-a130-7e8b610f0513")), 2, 300);
                order2.Add(ProductId.Of(new Guid("210cd0cf-a54e-4da0-abfa-2b2e70d0ed6a")), 2, 500);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
