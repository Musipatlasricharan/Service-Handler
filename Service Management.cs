using System;
using System.Collections.Generic;
using System.Data.SqlClient;

// Database Configuration
public static class DatabaseConfig
{
    // Update this connection string with your SQL Server details
    public static string ConnectionString =
        "Server=localhost;Database=ServiceManagementDB;Integrated Security=true;";

    public static void InitializeDatabase()
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();

            // Create Customers table
            string createCustomersTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
                CREATE TABLE Customers (
                    customer_id VARCHAR(50) PRIMARY KEY,
                    customer_name VARCHAR(100),
                    customer_address VARCHAR(200),
                    customer_phone VARCHAR(20),
                    customer_email VARCHAR(100)
                )";

            // Create RegularCustomers table
            string createRegularCustomersTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RegularCustomers' AND xtype='U')
                CREATE TABLE RegularCustomers (
                    customer_id VARCHAR(50) PRIMARY KEY,
                    customer_name VARCHAR(100),
                    customer_address VARCHAR(200),
                    customer_phone VARCHAR(20),
                    customer_email VARCHAR(100),
                    items_purchased INT,
                    items_returned INT,
                    days_inactive INT,
                    days_active INT,
                    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
                )";

            // Create PremiumCustomers table
            string createPremiumCustomersTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PremiumCustomers' AND xtype='U')
                CREATE TABLE PremiumCustomers (
                    customer_id VARCHAR(50) PRIMARY KEY,
                    premium_id VARCHAR(50),
                    customer_name VARCHAR(100),
                    items_purchased INT,
                    days_active INT,
                    FOREIGN KEY (customer_id) REFERENCES RegularCustomers(customer_id)
                )";

            // Create Employees table
            string createEmployeesTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
                CREATE TABLE Employees (
                    emp_id VARCHAR(50) PRIMARY KEY,
                    emp_name VARCHAR(100),
                    emp_phone VARCHAR(20),
                    emp_address VARCHAR(200),
                    emp_dept VARCHAR(50),
                    emp_role VARCHAR(50)
                )";

            // Create Products table
            string createProductsTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
                CREATE TABLE Products (
                    product_id VARCHAR(50) PRIMARY KEY,
                    product_name VARCHAR(100),
                    product_price INT,
                    product_type VARCHAR(50),
                    product_quantity INT
                )";

            // Create Reviews table
            string createReviewsTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Reviews' AND xtype='U')
                CREATE TABLE Reviews (
                    review_id INT IDENTITY(1,1) PRIMARY KEY,
                    product_id VARCHAR(50),
                    review_good BIT,
                    review_bad BIT,
                    review_rating INT,
                    FOREIGN KEY (product_id) REFERENCES Products(product_id)
                )";

            // Create Payments table
            string createPaymentsTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' AND xtype='U')
                CREATE TABLE Payments (
                    payment_id INT IDENTITY(1,1) PRIMARY KEY,
                    payment_type VARCHAR(50),
                    account_number VARCHAR(50),
                    payment_amount INT,
                    bank_name VARCHAR(100),
                    payment_success BIT
                )";

            // Create Shipping table
            string createShippingTable =
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Shipping' AND xtype='U')
                CREATE TABLE Shipping (
                    shipping_id VARCHAR(50) PRIMARY KEY,
                    shipping_mode VARCHAR(50),
                    shipping_cost INT,
                    shipping_address VARCHAR(200),
                    shipping_success BIT
                )";

            SqlCommand cmd = new SqlCommand(createCustomersTable, conn);
            cmd.ExecuteNonQuery();

            cmd.CommandText = createRegularCustomersTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createPremiumCustomersTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createEmployeesTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createProductsTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createReviewsTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createPaymentsTable;
            cmd.ExecuteNonQuery();

            cmd.CommandText = createShippingTable;
            cmd.ExecuteNonQuery();

            Console.WriteLine("Database initialized successfully!");
        }
    }
}

class Customer
{
    public string customer_name;
    public string customer_id;
    public string customer_address;
    public string customer_phone;
    public string customer_email;

    public Customer(
        string customer_name,
        string customer_id,
        string customer_address,
        string customer_phone,
        string customer_email
    )
    {
        this.customer_name = customer_name;
        this.customer_id = customer_id;
        this.customer_address = customer_address;
        this.customer_phone = customer_phone;
        this.customer_email = customer_email;
    }

    public virtual void Website()
    {
        Console.WriteLine("This is a Customer Service Website >>>> WWW.CUSTOMERSERVICE.COM");
    }

    public void add_customer()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Customers (customer_id, customer_name, customer_address, 
                                customer_phone, customer_email) 
                                VALUES (@id, @name, @address, @phone, @email)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id);
                cmd.Parameters.AddWithValue("@name", customer_name);
                cmd.Parameters.AddWithValue("@address", customer_address);
                cmd.Parameters.AddWithValue("@phone", customer_phone);
                cmd.Parameters.AddWithValue("@email", customer_email);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Customer added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding customer: " + ex.Message);
        }
    }

    public void Remove_customer(string customer_id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Customers WHERE customer_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Customer removed successfully!");
                else
                    Console.WriteLine("Customer not found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error removing customer: " + ex.Message);
        }
    }

    public void search_customer(string customer_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE customer_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id1);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Customer found in database!");
                    Console.WriteLine(
                        $"Name: {reader["customer_name"]}, ID: {reader["customer_id"]}"
                    );
                }
                else
                {
                    Console.WriteLine("Customer not found in database!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error searching customer: " + ex.Message);
        }
    }

    public void update_customer(
        string customer_name1,
        string customer_id1,
        string customer_address1,
        string customer_phone1,
        string customer_email1
    )
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"UPDATE Customers SET customer_name = @name, 
                                customer_address = @address, customer_phone = @phone, 
                                customer_email = @email WHERE customer_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id1);
                cmd.Parameters.AddWithValue("@name", customer_name1);
                cmd.Parameters.AddWithValue("@address", customer_address1);
                cmd.Parameters.AddWithValue("@phone", customer_phone1);
                cmd.Parameters.AddWithValue("@email", customer_email1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Customer updated successfully!");
                else
                    Console.WriteLine("Customer not found to update!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating customer: " + ex.Message);
        }
    }

    public void DisplayCustomers()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Customers ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Name: {reader["customer_name"]}, ID: {reader["customer_id"]}, "
                            + $"Phone: {reader["customer_phone"]}, Email: {reader["customer_email"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying customers: " + ex.Message);
        }
    }

    public void customer_info()
    {
        Console.WriteLine($"Customer Name: {customer_name}");
        Console.WriteLine($"Customer ID: {customer_id}");
        Console.WriteLine($"Address: {customer_address}");
        Console.WriteLine($"Phone: {customer_phone}");
        Console.WriteLine($"Email: {customer_email}");
    }
}

class RegularCustomer : Customer
{
    protected int Number_of_items_purchased;
    protected int Number_of_days_active;
    protected int Number_of_items_returned;
    protected int Number_of_days_inactive;

    public RegularCustomer(
        string customer_name,
        string customer_id,
        string customer_address,
        string customer_phone,
        string customer_email,
        int Number_of_items_purchased,
        int Number_of_items_returned,
        int Number_of_days_inactive,
        int Number_of_days_active
    )
        : base(customer_name, customer_id, customer_address, customer_phone, customer_email)
    {
        this.Number_of_items_purchased = Number_of_items_purchased;
        this.Number_of_days_active = Number_of_days_active;
        this.Number_of_items_returned = Number_of_items_returned;
        this.Number_of_days_inactive = Number_of_days_inactive;
    }

    public override void Website()
    {
        Console.WriteLine(
            "This is a Customer Service Website >>>> WWW.CUSTOMERSERVICE.COM/REGULAR"
        );
    }

    public void add_regular_customer()
    {
        try
        {
            // First add as regular customer
            add_customer();

            // Then add regular customer specific data
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO RegularCustomers (customer_id, customer_name, customer_address, 
                                customer_phone, customer_email, items_purchased, items_returned, 
                                days_inactive, days_active) 
                                VALUES (@id, @name, @address, @phone, @email, @purchased, @returned, 
                                @inactive, @active)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id);
                cmd.Parameters.AddWithValue("@name", customer_name);
                cmd.Parameters.AddWithValue("@address", customer_address);
                cmd.Parameters.AddWithValue("@phone", customer_phone);
                cmd.Parameters.AddWithValue("@email", customer_email);
                cmd.Parameters.AddWithValue("@purchased", Number_of_items_purchased);
                cmd.Parameters.AddWithValue("@returned", Number_of_items_returned);
                cmd.Parameters.AddWithValue("@inactive", Number_of_days_inactive);
                cmd.Parameters.AddWithValue("@active", Number_of_days_active);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Regular customer added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding regular customer: " + ex.Message);
        }
    }

    public void check_regularcustomer()
    {
        if (
            Number_of_days_active == 24
            && Number_of_days_inactive == 8
            && Number_of_items_purchased == 14
            && Number_of_items_returned == 3
        )
        {
            Console.WriteLine("The customer is a regular customer");
        }
        else
        {
            Console.WriteLine("The customer is not a regular customer");
        }
    }

    public void DisplayRegularCustomers()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM RegularCustomers";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Regular Customers ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Name: {reader["customer_name"]}, ID: {reader["customer_id"]}, "
                            + $"Items Purchased: {reader["items_purchased"]}, Days Active: {reader["days_active"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying regular customers: " + ex.Message);
        }
    }

    public void regular_customer_info()
    {
        Console.WriteLine($"Items Purchased: {Number_of_items_purchased}");
        Console.WriteLine($"Days Active: {Number_of_days_active}");
        Console.WriteLine($"Items Returned: {Number_of_items_returned}");
        Console.WriteLine($"Days Inactive: {Number_of_days_inactive}");
    }
}

class PremiumCustomer : RegularCustomer
{
    protected string Premium_id;

    public PremiumCustomer(
        string customer_name,
        string customer_id,
        string customer_address,
        string customer_phone,
        string customer_email,
        int Number_of_items_purchased,
        int Number_of_items_returned,
        int Number_of_days_inactive,
        int Number_of_days_active,
        string Premium_id
    )
        : base(
            customer_name,
            customer_id,
            customer_address,
            customer_phone,
            customer_email,
            Number_of_items_purchased,
            Number_of_items_returned,
            Number_of_days_inactive,
            Number_of_days_active
        )
    {
        this.Premium_id = Premium_id;
    }

    public override void Website()
    {
        Console.WriteLine(
            "This is a Customer Service Website >>>> WWW.CUSTOMERSERVICE.COM/PREMIUM"
        );
    }

    public void add_premium_customer()
    {
        try
        {
            // First add as regular customer
            add_regular_customer();

            // Then add premium customer specific data
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO PremiumCustomers (customer_id, premium_id, customer_name, 
                                items_purchased, days_active) 
                                VALUES (@id, @premium_id, @name, @purchased, @active)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer_id);
                cmd.Parameters.AddWithValue("@premium_id", Premium_id);
                cmd.Parameters.AddWithValue("@name", customer_name);
                cmd.Parameters.AddWithValue("@purchased", Number_of_items_purchased);
                cmd.Parameters.AddWithValue("@active", Number_of_days_active);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Premium customer added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding premium customer: " + ex.Message);
        }
    }

    public void check_premium_customer()
    {
        if (Number_of_items_purchased > 45 && Number_of_days_active == 31)
        {
            Premium_id = customer_id;
            Console.WriteLine("The premium customer is qualified");
        }
        else
        {
            Console.WriteLine("The premium customer is not qualified");
        }
    }

    public void premium_customer_info()
    {
        Console.WriteLine($"Premium Customer Name: {customer_name}");
        Console.WriteLine($"Premium ID: {Premium_id}");
    }
}

class Employee
{
    private string emp_name;
    private string emp_id;
    private string emp_phone;
    private string emp_address;
    private string emp_dept;
    private string emp_role;

    public Employee(
        string emp_name,
        string emp_id,
        string emp_phone,
        string emp_address,
        string emp_dept,
        string emp_role
    )
    {
        this.emp_name = emp_name;
        this.emp_id = emp_id;
        this.emp_phone = emp_phone;
        this.emp_address = emp_address;
        this.emp_dept = emp_dept;
        this.emp_role = emp_role;
    }

    public string get_empname()
    {
        return emp_name;
    }

    public string set_empname(string emp_name)
    {
        this.emp_name = emp_name;
        return emp_name;
    }

    public string get_empid()
    {
        return emp_id;
    }

    public string set_empid(string emp_id)
    {
        this.emp_id = emp_id;
        return emp_id;
    }

    public string get_empphone()
    {
        return emp_phone;
    }

    public string set_empphone(string emp_phone)
    {
        this.emp_phone = emp_phone;
        return emp_phone;
    }

    public string get_empaddress()
    {
        return emp_address;
    }

    public string set_empaddress(string emp_address)
    {
        this.emp_address = emp_address;
        return emp_address;
    }

    public string get_empdept()
    {
        return emp_dept;
    }

    public string set_empdept(string emp_dept)
    {
        this.emp_dept = emp_dept;
        return emp_dept;
    }

    public string get_emprole()
    {
        return emp_role;
    }

    public string set_emprole(string emp_role)
    {
        this.emp_role = emp_role;
        return emp_role;
    }

    public virtual void Website()
    {
        Console.WriteLine("This is an Employee Service Website >>>> WWW.EMPLOYEESERVICE.COM");
    }

    public void add_employee()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Employees (emp_id, emp_name, emp_phone, 
                                emp_address, emp_dept, emp_role) 
                                VALUES (@id, @name, @phone, @address, @dept, @role)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@name", emp_name);
                cmd.Parameters.AddWithValue("@phone", emp_phone);
                cmd.Parameters.AddWithValue("@address", emp_address);
                cmd.Parameters.AddWithValue("@dept", emp_dept);
                cmd.Parameters.AddWithValue("@role", emp_role);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Employee added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding employee: " + ex.Message);
        }
    }

    public void Remove_employee(string emp_id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE emp_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", emp_id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Employee removed successfully!");
                else
                    Console.WriteLine("Employee not found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error removing employee: " + ex.Message);
        }
    }

    public void search_employee(string emp_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees WHERE emp_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", emp_id1);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Employee found in database!");
                    Console.WriteLine($"Name: {reader["emp_name"]}, Dept: {reader["emp_dept"]}");
                }
                else
                {
                    Console.WriteLine("Employee not found in database!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error searching employee: " + ex.Message);
        }
    }

    public void update_employee(
        string emp_name1,
        string emp_id1,
        string emp_phone1,
        string emp_address1,
        string emp_dept1,
        string emp_role1
    )
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"UPDATE Employees SET emp_name = @name, emp_phone = @phone, 
                                emp_address = @address, emp_dept = @dept, emp_role = @role 
                                WHERE emp_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", emp_id1);
                cmd.Parameters.AddWithValue("@name", emp_name1);
                cmd.Parameters.AddWithValue("@phone", emp_phone1);
                cmd.Parameters.AddWithValue("@address", emp_address1);
                cmd.Parameters.AddWithValue("@dept", emp_dept1);
                cmd.Parameters.AddWithValue("@role", emp_role1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Employee updated successfully!");
                else
                    Console.WriteLine("Employee not found to update!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating employee: " + ex.Message);
        }
    }

    public void display_employees()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Employees ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Name: {reader["emp_name"]}, ID: {reader["emp_id"]}, "
                            + $"Dept: {reader["emp_dept"]}, Role: {reader["emp_role"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying employees: " + ex.Message);
        }
    }

    public void check_department()
    {
        if (
            emp_dept == "IT"
            || emp_dept == "HR"
            || emp_dept == "FINANCE"
            || emp_dept == "MARKETING"
            || emp_dept == "SALES"
        )
        {
            Console.WriteLine($"The employee is in the department: {emp_dept}");
        }
        else
        {
            Console.WriteLine($"The employee is not in a valid department. Current: {emp_dept}");
        }
    }

    public void employeeinfo()
    {
        Console.WriteLine($"Employee Name: {emp_name}");
        Console.WriteLine($"Employee ID: {emp_id}");
        Console.WriteLine($"Phone: {emp_phone}");
        Console.WriteLine($"Department: {emp_dept}");
        Console.WriteLine($"Role: {emp_role}");
    }
}

class Product
{
    public string product_name;
    public string product_id;
    public int product_price;
    public string product_type;
    public int product_quantity;

    public Product(
        string product_name,
        string product_id,
        int product_price,
        string product_type,
        int product_quantity
    )
    {
        this.product_name = product_name;
        this.product_id = product_id;
        this.product_price = product_price;
        this.product_type = product_type;
        this.product_quantity = product_quantity;
    }

    public virtual void Website()
    {
        Console.WriteLine("This is a Product Service Website >>>> WWW.PRODUCTSERVICE.COM");
    }

    public void add_product()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Products (product_id, product_name, product_price, 
                                product_type, product_quantity) 
                                VALUES (@id, @name, @price, @type, @quantity)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", product_id);
                cmd.Parameters.AddWithValue("@name", product_name);
                cmd.Parameters.AddWithValue("@price", product_price);
                cmd.Parameters.AddWithValue("@type", product_type);
                cmd.Parameters.AddWithValue("@quantity", product_quantity);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Product added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding product: " + ex.Message);
        }
    }

    public void Remove_product(string product_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Products WHERE product_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", product_id1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product removed successfully!");
                else
                    Console.WriteLine("Product not found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error removing product: " + ex.Message);
        }
    }

    public void search_product(string product_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE product_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", product_id1);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Product found in database!");
                    Console.WriteLine(
                        $"Name: {reader["product_name"]}, Price: {reader["product_price"]}"
                    );
                }
                else
                {
                    Console.WriteLine("Product not found in database!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error searching product: " + ex.Message);
        }
    }

    public void update_product(
        string product_id1,
        int product_price1,
        string product_type1,
        int product_quantity1
    )
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"UPDATE Products SET product_price = @price, 
                                product_type = @type, product_quantity = @quantity 
                                WHERE product_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", product_id1);
                cmd.Parameters.AddWithValue("@price", product_price1);
                cmd.Parameters.AddWithValue("@type", product_type1);
                cmd.Parameters.AddWithValue("@quantity", product_quantity1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product updated successfully!");
                else
                    Console.WriteLine("Product not found to update!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating product: " + ex.Message);
        }
    }

    public void DisplayProducts()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Products";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Products ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Name: {reader["product_name"]}, ID: {reader["product_id"]}, "
                            + $"Price: {reader["product_price"]}, Quantity: {reader["product_quantity"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying products: " + ex.Message);
        }
    }

    public void product_info()
    {
        Console.WriteLine($"Product Name: {product_name}");
        Console.WriteLine($"Product ID: {product_id}");
        Console.WriteLine($"Price: {product_price}");
        Console.WriteLine($"Type: {product_type}");
        Console.WriteLine($"Quantity: {product_quantity}");
    }
}

class Review : Product
{
    protected bool review_good = false;
    protected bool review_bad = false;
    protected int review_rating;

    public Review(
        string product_name,
        string product_id,
        int product_price,
        string product_type,
        int product_quantity,
        bool review_good,
        bool review_bad,
        int review_rating
    )
        : base(product_name, product_id, product_price, product_type, product_quantity)
    {
        this.review_good = review_good;
        this.review_bad = review_bad;
        this.review_rating = review_rating;
    }

    public override void Website()
    {
        Console.WriteLine("This is a Product Service Website >>>> WWW.PRODUCTSERVICE.COM/REVIEW");
    }

    public void add_review()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Reviews (product_id, review_good, review_bad, review_rating) 
                                VALUES (@product_id, @good, @bad, @rating)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@product_id", product_id);
                cmd.Parameters.AddWithValue("@good", review_good);
                cmd.Parameters.AddWithValue("@bad", review_bad);
                cmd.Parameters.AddWithValue("@rating", review_rating);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Review added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding review: " + ex.Message);
        }
    }

    public void check_review()
    {
        if (review_good == true)
        {
            Console.WriteLine("The item is so good!!!");
        }
        else if (review_bad == true)
        {
            Console.WriteLine("The item is not good!!!!");
        }
        else
        {
            Console.WriteLine("The review is invalid");
        }
    }

    public void review_info()
    {
        Console.WriteLine($"Item Rating: {review_rating}");
        Console.WriteLine($"Good Review: {review_good}");
        Console.WriteLine($"Bad Review: {review_bad}");
    }
}

abstract class Payment
{
    public abstract void payment_info();
    public abstract void payment_successful();

    public virtual void Website()
    {
        Console.WriteLine("This is a Payment Service >>>>> WWW.PAYMENTSERVICE.COM");
    }
}

class Payment1 : Payment
{
    private string payment_type;
    private string account_number;
    private int payment_amount;
    private string bank_name;
    private bool payment_success;

    public Payment1(
        string payment_type,
        string account_number,
        int payment_amount,
        string bank_name,
        bool payment_success
    )
    {
        this.payment_type = payment_type;
        this.payment_success = payment_success;
        this.payment_amount = payment_amount;
        this.bank_name = bank_name;
        this.account_number = account_number;
    }

    public override void Website()
    {
        Console.WriteLine("This is a Payment Service >>>>> WWW.PAYMENTSERVICE.COM");
    }

    public string get_paymenttype()
    {
        return payment_type;
    }

    public string set_paymenttype(string payment_type)
    {
        this.payment_type = payment_type;
        return this.payment_type;
    }

    public string get_accountnumber()
    {
        return account_number;
    }

    public string set_accountnumber(string account_number)
    {
        this.account_number = account_number;
        return account_number;
    }

    public int get_paymentamount()
    {
        return payment_amount;
    }

    public int set_paymentamount(int payment_amount)
    {
        this.payment_amount = payment_amount;
        return payment_amount;
    }

    public string get_bankname()
    {
        return bank_name;
    }

    public string set_bankname(string bank_name)
    {
        this.bank_name = bank_name;
        return bank_name;
    }

    public bool get_paymentsuccess()
    {
        return payment_success;
    }

    public bool set_paymentsuccess(bool payment_success)
    {
        this.payment_success = payment_success;
        return payment_success;
    }

    public void add_payment()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Payments (payment_type, account_number, payment_amount, 
                                bank_name, payment_success) 
                                VALUES (@type, @account, @amount, @bank, @success)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@type", payment_type);
                cmd.Parameters.AddWithValue("@account", account_number);
                cmd.Parameters.AddWithValue("@amount", payment_amount);
                cmd.Parameters.AddWithValue("@bank", bank_name);
                cmd.Parameters.AddWithValue("@success", payment_success);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Payment added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding payment: " + ex.Message);
        }
    }

    public void Remove_payment(string account_number1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Payments WHERE account_number = @account";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@account", account_number1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Payment removed successfully!");
                else
                    Console.WriteLine("Payment not found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error removing payment: " + ex.Message);
        }
    }

    public void search_payment(string account_number1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Payments WHERE account_number = @account";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@account", account_number1);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Payment found in database!");
                    Console.WriteLine(
                        $"Type: {reader["payment_type"]}, Amount: {reader["payment_amount"]}"
                    );
                }
                else
                {
                    Console.WriteLine("Payment not found in database!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error searching payment: " + ex.Message);
        }
    }

    public void update_payment(
        string payment_type1,
        string account_number1,
        int payment_amount1,
        string bank_name1,
        bool payment_success1
    )
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"UPDATE Payments SET payment_type = @type, payment_amount = @amount, 
                                bank_name = @bank, payment_success = @success 
                                WHERE account_number = @account";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@type", payment_type1);
                cmd.Parameters.AddWithValue("@account", account_number1);
                cmd.Parameters.AddWithValue("@amount", payment_amount1);
                cmd.Parameters.AddWithValue("@bank", bank_name1);
                cmd.Parameters.AddWithValue("@success", payment_success1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Payment updated successfully!");
                else
                    Console.WriteLine("Payment not found to update!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating payment: " + ex.Message);
        }
    }

    public void DisplayPayments()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Payments";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Payments ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Type: {reader["payment_type"]}, Account: {reader["account_number"]}, "
                            + $"Amount: {reader["payment_amount"]}, Success: {reader["payment_success"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying payments: " + ex.Message);
        }
    }

    public override void payment_info()
    {
        Console.WriteLine($"Payment Type: {payment_type}");
        Console.WriteLine($"Payment Success: {payment_success}");
        Console.WriteLine($"Payment Amount: {payment_amount}");
        Console.WriteLine($"Bank Name: {bank_name}");
        Console.WriteLine($"Account Number: {account_number}");
    }

    public override void payment_successful()
    {
        if (payment_success == true)
        {
            Console.WriteLine("The payment is successful");
        }
        else
        {
            Console.WriteLine("The payment is not successful");
        }
    }
}

interface Shipping
{
    void shipping_details();
}

interface Shipping1 : Shipping
{
    void shipping_successful();
}

class Shipping2 : Shipping1
{
    protected string shipping_id;
    protected string shipping_mode;
    protected int shipping_cost;
    protected string shipping_address;
    protected bool shipping_success;

    public Shipping2(
        string shipping_id,
        string shipping_mode,
        int shipping_cost,
        string shipping_address,
        bool shipping_success
    )
    {
        this.shipping_id = shipping_id;
        this.shipping_mode = shipping_mode;
        this.shipping_cost = shipping_cost;
        this.shipping_address = shipping_address;
        this.shipping_success = shipping_success;
    }

    public void add_shipping()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"INSERT INTO Shipping (shipping_id, shipping_mode, shipping_cost, 
                                shipping_address, shipping_success) 
                                VALUES (@id, @mode, @cost, @address, @success)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", shipping_id);
                cmd.Parameters.AddWithValue("@mode", shipping_mode);
                cmd.Parameters.AddWithValue("@cost", shipping_cost);
                cmd.Parameters.AddWithValue("@address", shipping_address);
                cmd.Parameters.AddWithValue("@success", shipping_success);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Shipping added successfully to database!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding shipping: " + ex.Message);
        }
    }

    public void Remove_shipping(string shipping_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Shipping WHERE shipping_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", shipping_id1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Shipping removed successfully!");
                else
                    Console.WriteLine("Shipping not found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error removing shipping: " + ex.Message);
        }
    }

    public void search_shipping(string shipping_id1)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Shipping WHERE shipping_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", shipping_id1);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Shipping found in database!");
                    Console.WriteLine(
                        $"Mode: {reader["shipping_mode"]}, Cost: {reader["shipping_cost"]}"
                    );
                }
                else
                {
                    Console.WriteLine("Shipping not found in database!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error searching shipping: " + ex.Message);
        }
    }

    public void update_shipping(
        string shipping_id1,
        string shipping_mode1,
        int shipping_cost1,
        string shipping_address1,
        bool shipping_success1
    )
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query =
                    @"UPDATE Shipping SET shipping_mode = @mode, shipping_cost = @cost, 
                                shipping_address = @address, shipping_success = @success 
                                WHERE shipping_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", shipping_id1);
                cmd.Parameters.AddWithValue("@mode", shipping_mode1);
                cmd.Parameters.AddWithValue("@cost", shipping_cost1);
                cmd.Parameters.AddWithValue("@address", shipping_address1);
                cmd.Parameters.AddWithValue("@success", shipping_success1);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Shipping updated successfully!");
                else
                    Console.WriteLine("Shipping not found to update!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating shipping: " + ex.Message);
        }
    }

    public void DisplayShipping()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Shipping";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Shipping Records ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["shipping_id"]}, Mode: {reader["shipping_mode"]}, "
                            + $"Cost: {reader["shipping_cost"]}, Success: {reader["shipping_success"]}"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error displaying shipping: " + ex.Message);
        }
    }

    public void shipping_details()
    {
        Console.WriteLine($"Shipping ID: {shipping_id}");
        Console.WriteLine($"Shipping Mode: {shipping_mode}");
        Console.WriteLine($"Shipping Cost: {shipping_cost}");
        Console.WriteLine($"Shipping Address: {shipping_address}");
        Console.WriteLine($"Shipping Success: {shipping_success}");
    }

    public void shipping_successful()
    {
        if (shipping_success == true)
        {
            Console.WriteLine("The shipping is successful");
        }
        else
        {
            Console.WriteLine("The shipping is not successful");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Service Management System with Database ===");
        Console.WriteLine();

        try
        {
            // Initialize database
            DatabaseConfig.InitializeDatabase();
            Console.WriteLine();

            // Demonstrate Customer
            Console.WriteLine("--- Customer Demo ---");
            Customer cust1 = new Customer("Alice", "C001", "Pune", "9876543210", "alice@email.com");
            cust1.add_customer();
            cust1.customer_info();
            cust1.Website();

            Customer cust2 = new Customer("Bob", "C002", "Delhi", "8765432190", "bob@email.com");
            cust2.add_customer();

            cust1.DisplayCustomers();
            cust1.search_customer("C002");
            cust1.update_customer("Bobby", "C002", "Mumbai", "1112223330", "bobby@email.com");
            cust1.DisplayCustomers();

            // Demonstrate RegularCustomer
            Console.WriteLine("\n--- RegularCustomer Demo ---");
            RegularCustomer regCust = new RegularCustomer(
                "Carol",
                "C003",
                "Hyderabad",
                "7654321980",
                "carol@email.com",
                10,
                2,
                5,
                20
            );
            regCust.add_regular_customer();

            RegularCustomer regCust2 = new RegularCustomer(
                "Dan",
                "C004",
                "Chennai",
                "6543219870",
                "dan@email.com",
                14,
                3,
                8,
                24
            );
            regCust2.add_regular_customer();

            regCust.DisplayRegularCustomers();
            regCust2.check_regularcustomer();

            // Demonstrate PremiumCustomer
            Console.WriteLine("\n--- PremiumCustomer Demo ---");
            PremiumCustomer premCust = new PremiumCustomer(
                "Eva",
                "C005",
                "Bangalore",
                "1234567890",
                "eva@email.com",
                50,
                2,
                0,
                31,
                "P001"
            );
            premCust.add_premium_customer();
            premCust.check_premium_customer();
            premCust.premium_customer_info();

            // Demonstrate Employee
            Console.WriteLine("\n--- Employee Demo ---");
            Employee emp1 = new Employee(
                "Frank",
                "E001",
                "9876543210",
                "Indore",
                "IT",
                "Developer"
            );
            emp1.add_employee();

            Employee emp2 = new Employee("Grace", "E002", "8765432100", "Jaipur", "HR", "Manager");
            emp2.add_employee();

            emp1.display_employees();
            emp1.search_employee("E002");
            emp1.update_employee(
                "Grace Smith",
                "E002",
                "1234567890",
                "Lucknow",
                "HR",
                "Senior Manager"
            );
            emp1.display_employees();
            emp1.check_department();

            // Demonstrate Product
            Console.WriteLine("\n--- Product Demo ---");
            Product prod1 = new Product("Laptop", "P001", 50000, "Electronics", 10);
            prod1.add_product();

            Product prod2 = new Product("Mouse", "P002", 500, "Electronics", 50);
            prod2.add_product();

            prod1.DisplayProducts();
            prod1.search_product("P002");
            prod1.update_product("P002", 600, "Electronics", 45);
            prod1.DisplayProducts();

            // Demonstrate Review
            Console.WriteLine("\n--- Review Demo ---");
            Review rev1 = new Review("Laptop", "P001", 50000, "Electronics", 10, true, false, 5);
            rev1.add_review();
            rev1.check_review();
            rev1.review_info();

            // Demonstrate Payment
            Console.WriteLine("\n--- Payment Demo ---");
            Payment1 pay1 = new Payment1("Card", "ACC123", 5000, "Axis", true);
            pay1.add_payment();

            Payment1 pay2 = new Payment1("UPI", "ACC456", 2000, "SBI", false);
            pay2.add_payment();

            pay1.DisplayPayments();
            pay1.payment_info();
            pay1.payment_successful();
            pay1.search_payment("ACC456");
            pay1.update_payment("UPI", "ACC456", 3000, "ICICI", true);
            pay1.DisplayPayments();

            // Demonstrate Shipping
            Console.WriteLine("\n--- Shipping Demo ---");
            Shipping2 ship1 = new Shipping2("SHIP123", "Air", 1000, "Noida", true);
            ship1.add_shipping();

            Shipping2 ship2 = new Shipping2("SHIP456", "Road", 500, "Surat", false);
            ship2.add_shipping();

            ship1.DisplayShipping();
            ship1.shipping_details();
            ship1.shipping_successful();
            ship1.search_shipping("SHIP456");
            ship1.update_shipping("SHIP456", "Rail", 800, "Surat Station", true);
            ship1.DisplayShipping();

            Console.WriteLine("\n=== End of Service Management System Demo ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}
