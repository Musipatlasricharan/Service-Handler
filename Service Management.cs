using System;
using System.Collections;
using System.Collections.Generic;

class Customer
{
    public string customer_name;
    public string customer_id;
    public string customer_address;
    public int customer_phone;
    public string customer_email;

    public Customer(
        string customer_name,
        string customer_id,
        string customer_address,
        int customer_phone,
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

    ArrayList customer_list = new ArrayList();

    public void add_customer(Customer customer)
    {
        customer_list.Add(customer);
    }

    public void Remove_customer(Customer customer)
    {
        customer_list.Remove(customer);
    }

    public void search_customer(string customer_id1)
    {
        for (int i = 0; i < customer_list.Count; i++)
        {
            Customer cust = (Customer)customer_list[i];
            if (cust.customer_id == customer_id1)
            {
                Console.WriteLine("the customer is found in the list");
                return;
            }
        }
        Console.WriteLine("the customer is not found in the list");
    }

    public void update_customer(
        string customer_name1,
        string customer_id1,
        string customer_address1,
        int customer_phone1,
        string customer_email1
    )
    {
        for (int i = 0; i < customer_list.Count; i++)
        {
            Customer cust = (Customer)customer_list[i];
            if (cust.customer_id == customer_id1)
            {
                cust.customer_name = customer_name1;
                cust.customer_id = customer_id1;
                cust.customer_address = customer_address1;
                cust.customer_phone = customer_phone1;
                cust.customer_email = customer_email1;
                Console.WriteLine("the customer details are updated");
                return;
            }
        }
        Console.WriteLine("the customer details are not updated");
    }

    public void DisplayCustomer()
    {
        for (int i = 0; i < customer_list.Count; i++)
        {
            Console.WriteLine("the customers present in the list is" + customer_list[i]);
        }
    }

    public void customer_info()
    {
        Console.WriteLine(
            "the name of the customer is "
                + customer_name
                + "\n the id of the customer is "
                + customer_id
                + "\n the address of the customer is "
                + customer_address
                + "\n the phone number of the customer is"
                + customer_phone
                + "\n the email of the customer is "
                + customer_email
        );
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
        int customer_phone,
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
        Console.WriteLine("This is a Customer Service Website >>>> WWW.CUSTOMERSERVICE.COM");
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
            Console.WriteLine("the customer is a regular customer");
        }
        else
        {
            Console.WriteLine("the customer is not a regular customer");
        }
    }

    List<RegularCustomer> regularcustomer = new List<RegularCustomer>();

    public void add_regular_customer(RegularCustomer regular_customer)
    {
        regularcustomer.Add(regular_customer);
    }

    public void remove_regular_customer(RegularCustomer regular_customer)
    {
        regularcustomer.Remove(regular_customer);
    }

    public void update_regular_customer(
        int Number_of_days_active1,
        int Number_of_items_purchased1,
        int Number_of_items_returned1,
        int Number_of_items_inactive1
    )
    {
        for (int i = 0; i < regularcustomer.Count; i++)
        {
            if (regularcustomer[i].Number_of_days_active == Number_of_days_active1)
            {
                regularcustomer[i].Number_of_days_active = Number_of_days_active1;
                regularcustomer[i].Number_of_days_inactive = Number_of_items_inactive1;
                regularcustomer[i].Number_of_items_purchased = Number_of_items_purchased1;
                regularcustomer[i].Number_of_items_returned = Number_of_items_returned1;
            }
        }
    }

    public void sort_on_number_of_items_purchased()
    {
        //in understandable way
        regularcustomer.Sort(
            (x, y) => x.Number_of_items_purchased.CompareTo(y.Number_of_items_purchased)
        );
    }

    public void dispaly_regular_customer()
    {
        for (int i = 0; i < regularcustomer.Count; i++)
        {
            Console.WriteLine("the regular customer is " + regularcustomer[i]);
        }
    }

    public void regular_customer_info()
    {
        Console.WriteLine(
            "the number of items purchased by the customer is "
                + Number_of_items_purchased
                + "\n the number of days active is "
                + Number_of_days_active
                + "\n the number of items returned is "
                + Number_of_items_returned
                + "\n the number of days inactive is "
                + Number_of_days_inactive
        );
    }
}

class PremiumCustomer : RegularCustomer
{
    protected string Premium_id;

    public PremiumCustomer(
        string customer_name,
        string customer_id,
        string customer_address,
        int customer_phone,
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
        Console.WriteLine("This is a Customer Service Website >>>> WWW.CUSTOMERSERVICE.COM");
    }

    public void check_premium_customer()
    {
        if (Number_of_items_purchased > 45 && Number_of_days_active == 31)
        {
            Premium_id = customer_id;
            Console.WriteLine("the premium customer is updated");
        }
        else
        {
            Console.WriteLine("the premium customer is not updated");
        }
    }

    public void premium_customer_info()
    {
        Console.WriteLine(
            "the name of the premium customer is "
                + customer_name
                + "\n the premium id is "
                + Premium_id
        );
    }
}

class Employee
{
    private string emp_name;
    private string emp_id;
    private int emp_phone;
    private string emp_address;
    private string emp_dept;
    private string emp_role;

    public Employee(
        string emp_name,
        string emp_id,
        int emp_phone,
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

    public int get_empphone()
    {
        return emp_phone;
    }

    public int set_empphone(int emp_phone)
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
        Console.WriteLine("This is a Employee Service Website >>>> WWW.EMPLOYEESERVICE.COM");
    }

    LinkedList<Employee> emp = new LinkedList<Employee>();

    public void add_employee(Employee employee)
    {
        emp.AddFirst(employee);
    }

    public void Remove_employee(Employee employee)
    {
        emp.Remove(employee);
    }

    public void search_employee(string emp_id1)
    {
        bool found = false;
        foreach (Employee employee in emp)
        {
            if (employee.get_empid() == emp_id1)
            {
                Console.WriteLine("the employee present in the company");
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("the employee is not present in the comapany");
        }
    }

    public void update_employee(
        string emp_name1,
        string emp_id1,
        int emp_phone1,
        string emp_address1,
        string emp_dept1,
        string emp_role1
    )
    {
        foreach (Employee employee in emp)
        {
            if (employee.get_empid() == emp_id1)
            {
                employee.set_empname(emp_name1);
                employee.set_empid(emp_id1);
                employee.set_empphone(emp_phone1);
                employee.set_empaddress(emp_address1);
                employee.set_empdept(emp_dept1);
                employee.set_emprole(emp_role1);
                break;
            }
        }
    }

    public void display_employee()
    {
        foreach (Employee employee in emp)
        {
            Console.WriteLine(
                "the elements present in the linkedlist is " + employee.get_empname()
            );
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
            Console.WriteLine("the employee is in the department " + emp_dept);
        }
        else
        {
            Console.WriteLine(
                "the employee is not in a valid department. Current department: " + emp_dept
            );
        }
    }

    public void employeeinfo()
    {
        Console.WriteLine("the name of the employee is " + emp_name);
        Console.WriteLine("the id of the employee is " + emp_id);
        Console.WriteLine("the phone number of the employee is " + emp_phone);
        Console.WriteLine("the department of the employee is " + emp_dept);
        Console.WriteLine("the role of the employee is " + emp_role);
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

    public override void payment_info()
    {
        Console.WriteLine("the payment type is " + payment_type);
        Console.WriteLine("the payment success is " + payment_success);
        Console.WriteLine("the payment amount is " + payment_amount);
        Console.WriteLine("the name of the bank is " + bank_name);
        Console.WriteLine("the account number is " + account_number);
    }

    public override void payment_successful()
    {
        if (payment_success == true)
        {
            Console.WriteLine("the payment is successful");
        }
        else
        {
            Console.WriteLine("the payment is not successful");
        }
    }

    LinkedList<Payment1> payment_list = new LinkedList<Payment1>();

    public void add_payment(Payment1 payment)
    {
        payment_list.AddFirst(payment);
    }

    public void Remove_payment(Payment1 payment)
    {
        payment_list.Remove(payment);
    }

    public void search_payment(string account_number1)
    {
        bool found = false;
        foreach (Payment1 payment in payment_list)
        {
            if (payment.get_accountnumber() == account_number1)
            {
                Console.WriteLine("the payment is found in the list");
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("the payment is not found in the list");
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
        foreach (Payment1 payment in payment_list)
        {
            if (payment.get_accountnumber() == account_number1)
            {
                payment.set_paymenttype(payment_type1);
                payment.set_accountnumber(account_number1);
                payment.set_paymentamount(payment_amount1);
                payment.set_bankname(bank_name1);
                payment.set_paymentsuccess(payment_success1);
                Console.WriteLine("the payment details are updated");
                break;
            }
        }
    }

    public void DisplayPayment()
    {
        foreach (Payment1 payment in payment_list)
        {
            Console.WriteLine(
                "the payments present in the list: Payment Type: "
                    + payment.get_paymenttype()
                    + ", Account Number: "
                    + payment.get_accountnumber()
                    + ", Amount: "
                    + payment.get_paymentamount()
            );
        }
    }
}

interface Shipping
{
    public void shipping_details();
}

interface Shipping1 : Shipping
{
    public void shipping_sucessful();
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

    public void shipping_details()
    {
        Console.WriteLine("the id of the shipping is " + shipping_id);
        Console.WriteLine("the mode of the shipping is " + shipping_mode);
        Console.WriteLine("the cost of the shipping is " + shipping_cost);
        Console.WriteLine("the address of the shipping is " + shipping_address);
        Console.WriteLine("the shipping is success/not: " + shipping_success);
    }

    public void shipping_sucessful()
    {
        if (shipping_success == true)
        {
            Console.WriteLine("the shipping is success");
        }
        else
        {
            Console.WriteLine("the shipping is not success");
        }
    }

    List<Shipping2> shipping_list = new List<Shipping2>();

    public void add_shipping(Shipping2 shipping)
    {
        shipping_list.Add(shipping);
    }

    public void Remove_shipping(Shipping2 shipping)
    {
        shipping_list.Remove(shipping);
    }

    public void search_shipping(string shipping_id1)
    {
        bool found = false;
        for (int i = 0; i < shipping_list.Count; i++)
        {
            if (shipping_list[i].shipping_id == shipping_id1)
            {
                Console.WriteLine("the shipping is found in the list");
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("the shipping is not found in the list");
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
        for (int i = 0; i < shipping_list.Count; i++)
        {
            if (shipping_list[i].shipping_id == shipping_id1)
            {
                shipping_list[i].shipping_id = shipping_id1;
                shipping_list[i].shipping_mode = shipping_mode1;
                shipping_list[i].shipping_cost = shipping_cost1;
                shipping_list[i].shipping_address = shipping_address1;
                shipping_list[i].shipping_success = shipping_success1;
                Console.WriteLine("the shipping details are updated");
                break;
            }
        }
    }

    public void DisplayShipping()
    {
        for (int i = 0; i < shipping_list.Count; i++)
        {
            Console.WriteLine(
                "the shipping present in the list: Shipping ID: "
                    + shipping_list[i].shipping_id
                    + ", Mode: "
                    + shipping_list[i].shipping_mode
                    + ", Cost: "
                    + shipping_list[i].shipping_cost
            );
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Service Management System");
        Console.WriteLine("Program compiled and ready to run!");
    }
}
