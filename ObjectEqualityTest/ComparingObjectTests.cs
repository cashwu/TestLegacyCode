using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExpectedObjects;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectEqualityTest
{
    /// <summary>
    /// http://www.codedata.com.tw/social-coding/csharp-test-legacy-code-3-compare-object-equality
    /// </summary>
    [TestFixture]
    public class ComparingObjectTests
    {
        #region -- Override Order Equals --

        [Test]
        public void Test_Order_Equals_by_Assert_Equals()
        {
            var expected = new Order
            {
                Id = 1,
                Price = 10
            };

            var actual = new Order
            {
                Id = 1,
                Price = 10
            };

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region -- Person --

        [Test]
        public void Test_Person_Equals_Flat_all_properties_by_Assert_Equals()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Age, actual.Age);
        }

        [Test]
        public void Test_Person_Equals_with_AnonymousType()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            //project expected Person to anonymous type
            var expectedAnonymous = new
            {
                Id = expected.Id,
                Name = expected.Name,
                Age = expected.Age
            };

            //project actual Person to anonymous type
            var actualAnonymous = new
            {
                Id = actual.Id,
                Name = actual.Name,
                Age = actual.Age,
            };

            Assert.AreEqual(expectedAnonymous, actualAnonymous);
        }

        [Test]
        public void Test_Person_Equals_with_ExpectedObjects()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            expected.ShouldEqual(actual);
        }

        #endregion

        #region -- List of Person --

        [Test]
        public void Test_PersonCollection_Equals_with_AnonymousType_by_CollectionAssert()
        {
            //project collection from List<Person> to List<AnonymousType> by Select()
            var expected = new List<Person>
            {
                new Person {Id = 1, Name = "A", Age = 10},
                new Person {Id = 2, Name = "B", Age = 20},
                new Person {Id = 3, Name = "C", Age = 30},
            }.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age
            })
                .ToList();

            //project collection from List<Person> to List<AnonymousType> by Select()
            var actual = new List<Person>
            {
                new Person {Id = 1, Name = "A", Age = 10},
                new Person {Id = 2, Name = "B", Age = 20},
                new Person {Id = 3, Name = "C", Age = 30},
            }.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age
            }).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_PersonCollection_Equals_with_ExpectedObjects()
        {
            var expected = new List<Person>
            {
                new Person {Id = 1, Name = "A", Age = 10},
                new Person {Id = 2, Name = "B", Age = 20},
                new Person {Id = 3, Name = "C", Age = 30},
            }.ToExpectedObject();

            var actual = new List<Person>
            {
                new Person {Id = 1, Name = "A", Age = 10},
                new Person {Id = 2, Name = "B", Age = 20},
                new Person {Id = 3, Name = "C", Age = 30},
            };

            expected.ShouldEqual(actual);
        }

        #endregion

        #region -- Composed Person Compare --

        [Test]
        public void Test_ComposedPerson_Equals_with_ExpectedObjects()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order
                {
                    Id = 91,
                    Price = 910
                }
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order
                {
                    Id = 91,
                    Price = 910
                }
            };

            expected.ShouldEqual(actual);
        }

        #endregion

        #region -- Partial Person Compare --

        [Test]
        public void Test_PartialCompare_Person_Equals_with_ExpectedObjects()
        {
            var expected = new
            {
                Id = 1,
                Age = 10,
                Order = new
                {
                    Id = 91
                }
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "B",
                Age = 10,
                Order = new Order
                {
                    Id = 91,
                    Price = 910
                }
            };

            expected.ShouldMatch(actual);
        }

        #endregion

        #region -- DataTable --

        [Test]
        public void Test_DataTable_Equals_with_ExpectedObjects()
        {
            var expected = new DataTable();
            expected.Columns.Add("Id");
            expected.Columns.Add("Name");
            expected.Columns.Add("Age");

            expected.Rows.Add(1, "A", 10);
            expected.Rows.Add(2, "B", 20);
            expected.Rows.Add(3, "C", 30);

            var actual = new DataTable();
            actual.Columns.Add("Id");
            actual.Columns.Add("Name");
            actual.Columns.Add("Age");

            actual.Rows.Add(1, "A", 10);
            actual.Rows.Add(2, "B", 20);
            actual.Rows.Add(3, "C", 30);

            var expectedDictionary =
                expected.AsEnumerable()
                    .Select(dr => expected.Columns.Cast<DataColumn>().ToDictionary(dc => dc.ColumnName, dc => dr[dc]));

            var actualDictionary =
                actual.AsEnumerable()
                    .Select(dr => actual.Columns.Cast<DataColumn>().ToDictionary(dc => dc.ColumnName, dc => dr[dc]));

            expectedDictionary.ToExpectedObject().ShouldEqual(actualDictionary);
        }

        [Test]
        public void Test_DataTable_Equals_with_ExpectedObjects_and_ItemArray()
        {
            var expected = new DataTable();
            expected.Columns.Add("Id");
            expected.Columns.Add("Name");
            expected.Columns.Add("Age");

            expected.Rows.Add(1, "A", 10);
            expected.Rows.Add(2, "B", 20);
            expected.Rows.Add(3, "C", 30);

            var actual = new DataTable();
            actual.Columns.Add("Id");
            actual.Columns.Add("Name");
            actual.Columns.Add("Age");

            actual.Rows.Add(1, "A", 10);
            actual.Rows.Add(2, "B", 20);
            actual.Rows.Add(3, "C", 30);

            var expectedItemArrayCollection = expected.AsEnumerable().Select(dr => dr.ItemArray);
            var actualItemArrayCollection = actual.AsEnumerable().Select(dr => dr.ItemArray);

            expectedItemArrayCollection.ToExpectedObject().ShouldEqual(actualItemArrayCollection);
        }

        #endregion
    }
}