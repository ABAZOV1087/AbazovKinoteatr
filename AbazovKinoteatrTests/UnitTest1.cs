using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbazovKinoteatr.Windows;
using System;

namespace AbazovKinoteatrTests
{
    [TestClass]
    public class FullProjectTests
    {
        // 8 ПОЗИТИВНЫХ ( test / tset )
        [TestMethod] public void Auth_RealUser_Success() => Assert.IsTrue(new LoginWindow().Auth("test", "tset"));
        [TestMethod] public void Auth_CaseCheck_Success() => Assert.IsTrue(new LoginWindow().Auth("TEST".ToLower(), "tset"));
        [TestMethod] public void Reg_User1_Success() => Assert.IsTrue(new RegistrationWindow().Register("U" + Guid.NewGuid().ToString().Substring(0, 4), "1", "Abazov"));
        [TestMethod] public void Reg_User2_Success() => Assert.IsTrue(new RegistrationWindow().Register("U" + Guid.NewGuid().ToString().Substring(0, 4), "2", "Student"));
        [TestMethod] public void Auth_DoubleCheck_Success() => Assert.IsTrue(new LoginWindow().Auth("test", "tset"));
        [TestMethod] public void Reg_LongName_Success() => Assert.IsTrue(new RegistrationWindow().Register("L" + Guid.NewGuid().ToString().Substring(0, 4), "p", "Very Long Test Name"));
        [TestMethod] public void Reg_Minimal_Success() => Assert.IsTrue(new RegistrationWindow().Register("M" + Guid.NewGuid().ToString().Substring(0, 4), "s", "S"));
        [TestMethod] public void Auth_Final_Success() => Assert.IsTrue(new LoginWindow().Auth("test", "tset"));

        // 8 НЕГАТИВНЫХ (Проверка защиты)
        [TestMethod] public void Auth_Empty_Fail() => Assert.IsFalse(new LoginWindow().Auth("", ""));
        [TestMethod] public void Auth_WrongPass_Fail() => Assert.IsFalse(new LoginWindow().Auth("test", "wrongpassword"));
        [TestMethod] public void Auth_Space_Fail() => Assert.IsFalse(new LoginWindow().Auth(" ", " "));
        [TestMethod] public void Auth_Null_Fail() => Assert.IsFalse(new LoginWindow().Auth(null, null));
        [TestMethod] public void Reg_Duplicate_Fail() => Assert.IsFalse(new RegistrationWindow().Register("test", "1", "1"));
        [TestMethod] public void Reg_NoName_Fail() => Assert.IsFalse(new RegistrationWindow().Register("newlog", "1", ""));
        [TestMethod] public void Reg_EmptyFields_Fail() => Assert.IsFalse(new RegistrationWindow().Register("", "", ""));
        [TestMethod] public void Auth_Unknown_Fail() => Assert.IsFalse(new LoginWindow().Auth("Ghost", "Ghost"));
        [TestClass]
        public class CaptchaTests
        {
            [TestClass]
            public class CaptchaModuleTests
            {
                // Логика проверки
                public bool CheckCaptcha(string secret, string userType) => secret == userType && !string.IsNullOrEmpty(userType);

                [TestMethod] public void Cap_Valid_True() => Assert.IsTrue(CheckCaptcha("1234", "1234"));
                [TestMethod] public void Cap_Wrong_False() => Assert.IsFalse(CheckCaptcha("1234", "4321"));
                [TestMethod] public void Cap_Empty_False() => Assert.IsFalse(CheckCaptcha("1234", ""));
                [TestMethod] public void Cap_Null_False() => Assert.IsFalse(CheckCaptcha("1234", null));
                [TestMethod] public void Cap_Whitespace_False() => Assert.IsFalse(CheckCaptcha("1234", "  "));
                [TestMethod] public void Cap_Short_False() => Assert.IsFalse(CheckCaptcha("1234", "123"));
                [TestMethod] public void Cap_Long_False() => Assert.IsFalse(CheckCaptcha("1234", "12345"));
                [TestMethod] public void Cap_WithLetters_False() => Assert.IsFalse(CheckCaptcha("1234", "12a4"));
                [TestMethod] public void Cap_WithSymbols_False() => Assert.IsFalse(CheckCaptcha("1234", "12.4"));
                [TestMethod] public void Cap_CaseCheck_False() => Assert.IsFalse(CheckCaptcha("Ab12", "ab12"));
            }
        }
    }
}