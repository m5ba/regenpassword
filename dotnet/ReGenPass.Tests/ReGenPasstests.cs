namespace ReGenPass.Tests
{
    public class ReGenPasstests
    {
        [Theory]
        [InlineData("Abc", "MyLogin", 1, "My-best-kept-secret", "B9h5g$1b$Cr47e~Tp")]
        [InlineData("MyMailServer", "me", 1, "12345", "oODgd9oh4tUj!^m!")]
        public void VerifyPasswordComputationForGivenContext(string resource, string login, int iteration, string secret, string expectedPwd)
        {
            PasswordContext cntx = new()
            {
                Resource = resource,
                Login = login,
                Iteration = iteration,
                Secret = secret
            };
            string pwd = PassComputer.Compute(cntx);
            Assert.Equal(expectedPwd, pwd);
        }

        [Fact]
        public void VerifyPassLength()
        {
            const int N = 1000;
            HashSet<string> results = [];
            for(int i=0;i<N;i++)
            {
                PasswordContext cntx = new()
                {
                    Resource = Guid.NewGuid().ToString(),
                    Login = Guid.NewGuid().ToString(),
                    Iteration = i,
                    Secret = Guid.NewGuid().ToString()
                };
                string pwd = PassComputer.Compute(cntx);
                Assert.NotNull(pwd);
                Assert.True(pwd.Length > 15);
                Assert.True(pwd.Length < 21);
                Assert.True(results.Add(pwd));
            }
        }

        [Fact]
        public void VerifyThatPasswordDependsOnEachElement()
        {
            PasswordContext cntx0 = new()
            {
                Resource = Guid.NewGuid().ToString(),
                Login = Guid.NewGuid().ToString(),
                Iteration = 1,
                Secret = Guid.NewGuid().ToString()
            };
            string pwd0 = PassComputer.Compute(cntx0);

            PasswordContext updated;
            string newPwd;

            updated = CopyContext(cntx0);
            Assert.Equal(pwd0, PassComputer.Compute(updated));
            updated.Resource = Guid.NewGuid().ToString();
            newPwd = PassComputer.Compute(updated);
            Assert.NotEqual(pwd0, newPwd);

            updated = CopyContext(cntx0);
            Assert.Equal(pwd0, PassComputer.Compute(updated));
            updated.Login = Guid.NewGuid().ToString();
            newPwd = PassComputer.Compute(updated);
            Assert.NotEqual(pwd0, newPwd);

            updated = CopyContext(cntx0);
            Assert.Equal(pwd0, PassComputer.Compute(updated));
            updated.Secret = Guid.NewGuid().ToString();
            newPwd = PassComputer.Compute(updated);
            Assert.NotEqual(pwd0, newPwd);

            updated = CopyContext(cntx0);
            Assert.Equal(pwd0, PassComputer.Compute(updated));
            updated.Iteration++;
            newPwd = PassComputer.Compute(updated);
            Assert.NotEqual(pwd0, newPwd);
        }

        private PasswordContext CopyContext(PasswordContext context) => new()
        {
            Resource = context.Resource,
            Login = context.Login,
            Iteration = context.Iteration,
            Secret = context.Secret
        };
    }
}