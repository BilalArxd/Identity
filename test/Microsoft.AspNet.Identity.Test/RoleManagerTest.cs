using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.AspNet.Identity.Test
{
    public class RoleManagerTest
    {
        [Fact]
        public void ConstructorThrowsWithNullStore()
        {
            Assert.Throws<ArgumentNullException>("store", () => new RoleManager<TestRole>(null));
        }

        [Fact]
        public void RolesQueryableFailWhenStoreNotImplemented()
        {
            var manager = new RoleManager<TestRole>(new NoopRoleStore());
            Assert.False(manager.SupportsQueryableRoles);
            Assert.Throws<NotSupportedException>(() => manager.Roles.Count());
        }

        [Fact]
        public void DisposeAfterDisposeDoesNotThrow()
        {
            var manager = new RoleManager<TestRole>(new NoopRoleStore());
            manager.Dispose();
            manager.Dispose();
        }

        [Fact]
        public async Task RoleManagerPublicNullChecks()
        {
            Assert.Throws<ArgumentNullException>("store",
                () => new RoleManager<TestRole>(null));
            var manager = new RoleManager<TestRole>(new NotImplementedStore());
            await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await manager.CreateAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await manager.UpdateAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await manager.DeleteAsync(null));
            await Assert.ThrowsAsync<ArgumentNullException>("roleName", async () => await manager.FindByName(null));
            await Assert.ThrowsAsync<ArgumentNullException>("roleName", async () => await manager.RoleExistsAsync(null));
        }

        [Fact]
        public async Task RoleStoreMethodsThrowWhenDisposed()
        {
            var manager = new RoleManager<TestRole>(new NoopRoleStore());
            manager.Dispose();
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.FindByIdAsync(null));
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.FindByName(null));
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.RoleExistsAsync(null));
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.CreateAsync(null));
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.UpdateAsync(null));
            await Assert.ThrowsAsync<ObjectDisposedException>(() => manager.DeleteAsync(null));
        }

        private class NotImplementedStore : IRoleStore<TestRole>
        {
            public Task CreateAsync(TestRole role, CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(TestRole role, CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new NotImplementedException();
            }

            public Task DeleteAsync(TestRole role, CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new NotImplementedException();
            }

            public Task<string> GetRoleIdAsync(TestRole role, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<string> GetRoleNameAsync(TestRole role, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<TestRole> FindByIdAsync(string roleId, CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new NotImplementedException();
            }

            public Task<TestRole> FindByNameAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}