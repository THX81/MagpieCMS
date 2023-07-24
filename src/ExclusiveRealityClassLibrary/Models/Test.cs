namespace ExclusiveReality.Models
{
	using System;
	using Castle.ActiveRecord;
    using Castle.ActiveRecord.Queries;
    using System.Web;
    using System.Collections;

    [ActiveRecord]
    [ExclusiveReality.Models.Attributes.ClassLocalization("cs", "Test", "Testy")]
    public class Test : Base.BusinessObjectBase<Test>
	{
        private string name;
        private TestPerson person;
        private TestAddress address;
        private TestAddressAnother addressOther;

        public Test()
        {
        }

        [ExclusiveReality.Models.Attributes.PropertyLocalization("cs", "Id")]
        [PrimaryKey(PrimaryKeyType.Native, "TestId")]
        public override int Id
        {
            get { return id; }
            set { id = value; }
        }

        [OneToOne]
        public TestPerson TestPerson
        {
            get { return this.person; }
            set { this.person = value; }
        }

        [Nested(ColumnPrefix = "TestAddress")]
        public TestAddress TestAddress
        {
            get { return address; }
            set { address = value; }
        }

        [Nested(ColumnPrefix = "TestAddressAnother")]
        public TestAddressAnother TestAddressAnother
        {
            get { return addressOther; }
            set { addressOther = value; }
        }

        [ExclusiveReality.Models.Attributes.PropertyLocalization("cs", "Název")]
        [Property("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }



    [ExclusiveReality.Models.Attributes.ClassLocalization("cs", "Jiná adresa", "Jiné adresy")]
    public class TestAddressAnother : TestAddress
    {
    }



    [ExclusiveReality.Models.Attributes.ClassLocalization("cs", "Adresa", "Adresy")]
    public class TestAddress
    {
        private string street;
        private string city;

        [ExclusiveReality.Models.Attributes.PropertyLocalization("cs", "Ulice")]
        [Property("Street")]
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        [ExclusiveReality.Models.Attributes.PropertyLocalization("cs", "Mìsto")]
        [Property("City")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }


    }



    [ActiveRecord]
    [ExclusiveReality.Models.Attributes.ClassLocalization("cs", "TestOsoba", "TestOsoby")]
    public class TestPerson : ActiveRecordBase<TestPerson>
    {
        private int id;
        private Test test;
        private string name;

        public TestPerson()
        {
        }

        public TestPerson(Test test)
        {
            this.test = test;
        }

        [PrimaryKey(PrimaryKeyType.Foreign, "TestId")]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [OneToOne]
        public Test Test
        {
            get { return this.test; }
            set { this.test = value; }
        }


        [ExclusiveReality.Models.Attributes.PropertyLocalization("cs", "Jméno")]
        [Property("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public new static TestPerson[] FindAll()
        {
            return ((TestPerson[])(ActiveRecordBase.FindAll(typeof(TestPerson))));
        }

        public new static void DeleteAll()
        {
            ActiveRecordBase.DeleteAll(typeof(TestPerson));
        }

    }


    [ActiveRecord("Customer")]
    public class Customer : ActiveRecordBase
    {
        private int id;
        private string name;
        private CustomerAddress addr;
        private CustomerHobby hobby;

        [PrimaryKey(PrimaryKeyType.Native)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [OneToOne]
        public CustomerAddress CustomerAddress
        {
            get { return addr; }
            set { addr = value; }
        }

        [OneToOne]
        public CustomerHobby CustomerHobby
        {
            get { return hobby; }
            set { hobby = value; }
        }

        [Property]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    [ActiveRecord("CustomerAddress")]
    public class CustomerAddress : ActiveRecordBase
    {
        private int id;
        private string address;
        private Customer cust;

        [PrimaryKey(PrimaryKeyType.Foreign)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [OneToOne]
        public Customer Customer
        {
            get { return cust; }
            set { cust = value; }
        }

        [Property]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }

    [ActiveRecord("CustomerHobby")]
    public class CustomerHobby : ActiveRecordBase
    {
        private int id;
        private string hobby;
        private Customer cust;

        [PrimaryKey(PrimaryKeyType.Foreign)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [OneToOne]
        public Customer Customer
        {
            get { return cust; }
            set { cust = value; }
        }

        [Property]
        public string Hobby
        {
            get { return hobby; }
            set { hobby = value; }
        }
    }


}
