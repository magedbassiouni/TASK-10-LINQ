using TASK_10_LINQ.Models;

namespace TASK_10_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using BikeStoreContext context = new BikeStoreContext();

            //====================================================================//
            // 1- List all customers' first and last names along with their email addresses.

            var customers = context.Customers
                .Select(c => new
                {
                    c.FirstName,
                    c.LastName,
                    c.Email
                })
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"{customer.FirstName} {customer.LastName} - {customer.Email}"
                );
            }


            //====================================================================//
            // 2- Retrieve all orders processed by a specific staff member (staff_id = 3).

            var staffOrdersById = context.Orders
                .Where(o => o.StaffId == 3)
                .ToList();

            foreach (var order in staffOrdersById)
            {
                Console.WriteLine($"Order ID: {order.OrderId}");
            }


            //====================================================================//
            // 3- Get all products that belong to a category named "Mountain Bikes".

            var mountainBikeProducts = context.Products
                .Where(p => p.Category.CategoryName == "Mountain Bikes")
                .ToList();

            foreach (var mountainProduct in mountainBikeProducts)
            {
                Console.WriteLine(
                    $"Product ID: {mountainProduct.ProductId} - {mountainProduct.ProductName}"
                );
            }


            //====================================================================//
            // 4- Count the total number of orders per store.

            var ordersPerStore = context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new
                {
                    StoreId = g.Key,
                    OrderCount = g.Count()
                })
                .ToList();

            foreach (var store in ordersPerStore)
            {
                Console.WriteLine(
                    $"Store ID: {store.StoreId} - Orders: {store.OrderCount}"
                );
            }


            //====================================================================//
            // 5- List all orders that have not been shipped yet (shipped_date is null).

            var unshippedOrders = context.Orders
                .Where(o => o.ShippedDate == null)
                .ToList();

            foreach (var unshippedOrder in unshippedOrders)
            {
                Console.WriteLine(
                    $"Order ID: {unshippedOrder.OrderId} - Order Date: {unshippedOrder.OrderDate}"
                );
            }


            //====================================================================//
            // 6- Display each customer's full name and the number of orders they have placed.

            var customerOrderCounts = context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    OrderCount = g.Count()
                })
                .ToList();

            foreach (var customerOrder in customerOrderCounts)
            {
                var customer = context.Customers
                    .FirstOrDefault(c => c.CustomerId == customerOrder.CustomerId);

                if (customer != null)
                {
                    Console.WriteLine(
                        $"{customer.FirstName} {customer.LastName} - Orders: {customerOrder.OrderCount}"
                    );
                }
            }


            //====================================================================//
            // 7- List all products that have never been ordered.

            var neverOrderedProducts = context.Products
                .Where(p => !context.OrderItems
                    .Any(oi => oi.ProductId == p.ProductId))
                .ToList();

            foreach (var neverOrderedProduct in neverOrderedProducts)
            {
                Console.WriteLine(
                    $"Product ID: {neverOrderedProduct.ProductId} - {neverOrderedProduct.ProductName}"
                );
            }


            //====================================================================//
            // 8- Display products that have a quantity of less than 5 in any store stock.

            var lowStockProducts = context.Products
                .Where(p => p.Stocks.Any(s => s.Quantity < 5))
                .ToList();

            foreach (var lowStockProduct in lowStockProducts)
            {
                Console.WriteLine(
                    $"Product ID: {lowStockProduct.ProductId} - {lowStockProduct.ProductName}"
                );
            }


            //====================================================================//
            // 9- Retrieve the first product from the products table.

            var firstProduct = context.Products.FirstOrDefault();

            if (firstProduct != null)
            {
                Console.WriteLine(
                    $"Product ID: {firstProduct.ProductId} - {firstProduct.ProductName}"
                );
            }


            //====================================================================//
            // 10- Retrieve all products from the products table with a certain model year.

            var productsByYear = context.Products
                .Where(p => p.ModelYear == 2018)
                .ToList();

            foreach (var productByYear in productsByYear)
            {
                Console.WriteLine(
                    $"{productByYear.ProductName} - {productByYear.ModelYear}"
                );
            }


            //====================================================================//
            // 11- Display each product with the number of times it was ordered.

            var productOrderCounts = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    OrderCount = p.OrderItems.Count()
                })
                .ToList();

            foreach (var productOrder in productOrderCounts)
            {
                Console.WriteLine(
                    $"{productOrder.ProductName} - Ordered: {productOrder.OrderCount} times"
                );
            }


            //====================================================================//
            // 12- Count the number of products in a specific category.

            int productCountInCategory = context.Products
                .Count(p => p.CategoryId == 6);

            Console.WriteLine(
                $"Number of products: {productCountInCategory}"
            );


            //====================================================================//
            // 13- Calculate the average list price of products.

            var averageListPrice = context.Products
                .Average(p => p.ListPrice);

            Console.WriteLine(
                $"Average List Price: {averageListPrice}"
            );


            //====================================================================//
            // 14- Retrieve a specific product from the products table by ID.

            var productById = context.Products
                .FirstOrDefault(p => p.ProductId == 10);

            if (productById != null)
            {
                Console.WriteLine(
                    $"Product ID: {productById.ProductId} - {productById.ProductName}"
                );
            }


            //====================================================================//
            // 15- List all products that were ordered with a quantity greater than 3 in any order.

            var productsQuantityGreaterThan3 = context.Products
                .Where(p => p.OrderItems.Any(oi => oi.Quantity > 3))
                .ToList();

            foreach (var productQuantity in productsQuantityGreaterThan3)
            {
                Console.WriteLine(
                    $"Product ID: {productQuantity.ProductId} - {productQuantity.ProductName}"
                );
            }


            //====================================================================//
            // 16- Display each staff member's name and how many orders they processed.

            var staffOrderCounts = context.Staffs
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    OrderCount = s.Orders.Count()
                })
                .ToList();

            foreach (var staffOrder in staffOrderCounts)
            {
                Console.WriteLine(
                    $"{staffOrder.FullName} - Orders: {staffOrder.OrderCount}"
                );
            }


            //====================================================================//
            // 17- List active staff members only (active = true) along with their phone numbers.

            var activeStaffMembers = context.Staffs
                .Where(s => s.Active == 1)
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    s.Phone
                })
                .ToList();

            foreach (var activeStaff in activeStaffMembers)
            {
                Console.WriteLine(
                    $"{activeStaff.FullName} - {activeStaff.Phone}"
                );
            }


            //====================================================================//
            // 18- List all products with their brand name and category name.

            var productsWithBrandAndCategory = context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    BrandName = p.Brand.BrandName,
                    CategoryName = p.Category.CategoryName
                })
                .ToList();

            foreach (var productInfo in productsWithBrandAndCategory)
            {
                Console.WriteLine(
                    $"{productInfo.ProductName} - Brand: {productInfo.BrandName} - Category: {productInfo.CategoryName}"
                );
            }


            //====================================================================//
            // 19- Retrieve orders that are completed.

            var completedOrders = context.Orders
                .Where(o => o.OrderStatus == 4)
                .ToList();

            foreach (var completedOrder in completedOrders)
            {
                Console.WriteLine(
                    $"Order ID: {completedOrder.OrderId} - Status: {completedOrder.OrderStatus}"
                );
            }


            //====================================================================//
            // 20- List each product with the total quantity sold.

            var productSales = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    TotalQuantitySold =
                        p.OrderItems
                         .Select(oi => (int?)oi.Quantity)
                         .Sum() ?? 0
                })
                .ToList();

            foreach (var productSale in productSales)
            {
                Console.WriteLine(
                    $"{productSale.ProductName} - Total Sold: {productSale.TotalQuantitySold}"
                );
            }


            //====================================================================//

            Console.WriteLine("\nDone!");
        }
    }
}using TASK_10_LINQ.Models;

namespace TASK_10_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using BikeStoreContext context = new BikeStoreContext();

            //====================================================================//
            // 1- List all customers' first and last names along with their email addresses.

            var customers = context.Customers
                .Select(c => new
                {
                    c.FirstName,
                    c.LastName,
                    c.Email
                })
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"{customer.FirstName} {customer.LastName} - {customer.Email}"
                );
            }


            //====================================================================//
            // 2- Retrieve all orders processed by a specific staff member (staff_id = 3).

            var staffOrdersById = context.Orders
                .Where(o => o.StaffId == 3)
                .ToList();

            foreach (var order in staffOrdersById)
            {
                Console.WriteLine($"Order ID: {order.OrderId}");
            }


            //====================================================================//
            // 3- Get all products that belong to a category named "Mountain Bikes".

            var mountainBikeProducts = context.Products
                .Where(p => p.Category.CategoryName == "Mountain Bikes")
                .ToList();

            foreach (var mountainProduct in mountainBikeProducts)
            {
                Console.WriteLine(
                    $"Product ID: {mountainProduct.ProductId} - {mountainProduct.ProductName}"
                );
            }


            //====================================================================//
            // 4- Count the total number of orders per store.

            var ordersPerStore = context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new
                {
                    StoreId = g.Key,
                    OrderCount = g.Count()
                })
                .ToList();

            foreach (var store in ordersPerStore)
            {
                Console.WriteLine(
                    $"Store ID: {store.StoreId} - Orders: {store.OrderCount}"
                );
            }


            //====================================================================//
            // 5- List all orders that have not been shipped yet (shipped_date is null).

            var unshippedOrders = context.Orders
                .Where(o => o.ShippedDate == null)
                .ToList();

            foreach (var unshippedOrder in unshippedOrders)
            {
                Console.WriteLine(
                    $"Order ID: {unshippedOrder.OrderId} - Order Date: {unshippedOrder.OrderDate}"
                );
            }


            //====================================================================//
            // 6- Display each customer's full name and the number of orders they have placed.

            var customerOrderCounts = context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    OrderCount = g.Count()
                })
                .ToList();

            foreach (var customerOrder in customerOrderCounts)
            {
                var customer = context.Customers
                    .FirstOrDefault(c => c.CustomerId == customerOrder.CustomerId);

                if (customer != null)
                {
                    Console.WriteLine(
                        $"{customer.FirstName} {customer.LastName} - Orders: {customerOrder.OrderCount}"
                    );
                }
            }


            //====================================================================//
            // 7- List all products that have never been ordered.

            var neverOrderedProducts = context.Products
                .Where(p => !context.OrderItems
                    .Any(oi => oi.ProductId == p.ProductId))
                .ToList();

            foreach (var neverOrderedProduct in neverOrderedProducts)
            {
                Console.WriteLine(
                    $"Product ID: {neverOrderedProduct.ProductId} - {neverOrderedProduct.ProductName}"
                );
            }


            //====================================================================//
            // 8- Display products that have a quantity of less than 5 in any store stock.

            var lowStockProducts = context.Products
                .Where(p => p.Stocks.Any(s => s.Quantity < 5))
                .ToList();

            foreach (var lowStockProduct in lowStockProducts)
            {
                Console.WriteLine(
                    $"Product ID: {lowStockProduct.ProductId} - {lowStockProduct.ProductName}"
                );
            }


            //====================================================================//
            // 9- Retrieve the first product from the products table.

            var firstProduct = context.Products.FirstOrDefault();

            if (firstProduct != null)
            {
                Console.WriteLine(
                    $"Product ID: {firstProduct.ProductId} - {firstProduct.ProductName}"
                );
            }


            //====================================================================//
            // 10- Retrieve all products from the products table with a certain model year.

            var productsByYear = context.Products
                .Where(p => p.ModelYear == 2018)
                .ToList();

            foreach (var productByYear in productsByYear)
            {
                Console.WriteLine(
                    $"{productByYear.ProductName} - {productByYear.ModelYear}"
                );
            }


            //====================================================================//
            // 11- Display each product with the number of times it was ordered.

            var productOrderCounts = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    OrderCount = p.OrderItems.Count()
                })
                .ToList();

            foreach (var productOrder in productOrderCounts)
            {
                Console.WriteLine(
                    $"{productOrder.ProductName} - Ordered: {productOrder.OrderCount} times"
                );
            }


            //====================================================================//
            // 12- Count the number of products in a specific category.

            int productCountInCategory = context.Products
                .Count(p => p.CategoryId == 6);

            Console.WriteLine(
                $"Number of products: {productCountInCategory}"
            );


            //====================================================================//
            // 13- Calculate the average list price of products.

            var averageListPrice = context.Products
                .Average(p => p.ListPrice);

            Console.WriteLine(
                $"Average List Price: {averageListPrice}"
            );


            //====================================================================//
            // 14- Retrieve a specific product from the products table by ID.

            var productById = context.Products
                .FirstOrDefault(p => p.ProductId == 10);

            if (productById != null)
            {
                Console.WriteLine(
                    $"Product ID: {productById.ProductId} - {productById.ProductName}"
                );
            }


            //====================================================================//
            // 15- List all products that were ordered with a quantity greater than 3 in any order.

            var productsQuantityGreaterThan3 = context.Products
                .Where(p => p.OrderItems.Any(oi => oi.Quantity > 3))
                .ToList();

            foreach (var productQuantity in productsQuantityGreaterThan3)
            {
                Console.WriteLine(
                    $"Product ID: {productQuantity.ProductId} - {productQuantity.ProductName}"
                );
            }


            //====================================================================//
            // 16- Display each staff member's name and how many orders they processed.

            var staffOrderCounts = context.Staffs
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    OrderCount = s.Orders.Count()
                })
                .ToList();

            foreach (var staffOrder in staffOrderCounts)
            {
                Console.WriteLine(
                    $"{staffOrder.FullName} - Orders: {staffOrder.OrderCount}"
                );
            }


            //====================================================================//
            // 17- List active staff members only (active = true) along with their phone numbers.

            var activeStaffMembers = context.Staffs
                .Where(s => s.Active == 1)
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    s.Phone
                })
                .ToList();

            foreach (var activeStaff in activeStaffMembers)
            {
                Console.WriteLine(
                    $"{activeStaff.FullName} - {activeStaff.Phone}"
                );
            }


            //====================================================================//
            // 18- List all products with their brand name and category name.

            var productsWithBrandAndCategory = context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    BrandName = p.Brand.BrandName,
                    CategoryName = p.Category.CategoryName
                })
                .ToList();

            foreach (var productInfo in productsWithBrandAndCategory)
            {
                Console.WriteLine(
                    $"{productInfo.ProductName} - Brand: {productInfo.BrandName} - Category: {productInfo.CategoryName}"
                );
            }


            //====================================================================//
            // 19- Retrieve orders that are completed.

            var completedOrders = context.Orders
                .Where(o => o.OrderStatus == 4)
                .ToList();

            foreach (var completedOrder in completedOrders)
            {
                Console.WriteLine(
                    $"Order ID: {completedOrder.OrderId} - Status: {completedOrder.OrderStatus}"
                );
            }


            //====================================================================//
            // 20- List each product with the total quantity sold.

            var productSales = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    TotalQuantitySold =
                        p.OrderItems
                         .Select(oi => (int?)oi.Quantity)
                         .Sum() ?? 0
                })
                .ToList();

            foreach (var productSale in productSales)
            {
                Console.WriteLine(
                    $"{productSale.ProductName} - Total Sold: {productSale.TotalQuantitySold}"
                );
            }


            //====================================================================//

            Console.WriteLine("\nDone!");
        }
    }
}