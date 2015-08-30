using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using StaticFunctions;

namespace StaticFunctionsTest
{
    [TestFixture]
    public class AuthServiceTest
    {
        [TestFixtureSetUp]
        public void TestInit()
        {
            ProfileDao.MyProfileDao = null;
        }

        [Test]
        public void Test_IsValid_joey_1234666666_Should_Return_True()
        {
            var target = new AuthService();
            var account = "cash";
            var password = "1234666666";

            IProfileDao stubProfileDao = Substitute.For<IProfileDao>();
            stubProfileDao.GetPassword("cash").ReturnsForAnyArgs("1234");
            stubProfileDao.GetToken("cash").ReturnsForAnyArgs("666666");

            target.MyProfileDao = stubProfileDao;

            var actual = target.IsValid(account, password);

            var expected = true;

            actual.Should().Be(expected);
        }
    }
}
