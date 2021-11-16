using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Oak.Events;
using Oak.Events.Implementations;

namespace Oak.Tests.Events
{
    public class TestAmbiguousEventDispatcher
    {
        private AmbiguousEventDispatcher _eventDispatcher(IServiceProvider provider = null)
        {
            if (provider == null)
            {
                var service = new MockServiceProvider();
                provider = service.Provider((s) => 
                {
                    s.AddEvent<MockEventHandler, MockEvent>();
                    s.AddAsyncEvent<MockEventHandler, MockEvent>();
                });

            }
            return new AmbiguousEventDispatcher(provider);
        }

        [Test]
        public void Test_Event_Emit()
        {
            var dispatcher = this._eventDispatcher();
            bool wasCalled = false;
            Type typeOf = typeof(string);
            int count = 0;
            dispatcher.Emit(new MockEvent(this, (o) => {
                count += 1;
                wasCalled = true;
                typeOf = o.GetType();
            }));

            Assert.AreEqual(2, count);
            Assert.IsTrue(wasCalled);
            Assert.AreEqual(this.GetType(), typeOf);
        }

        [Test]
        public async Task Test_Event_EmitAsync()
        {
            var dispatcher = this._eventDispatcher();
            bool wasCalled = false;
            Type typeOf = typeof(string);
            int count = 0;
            await dispatcher.EmitAsync(new MockEvent(this, (o) => {
                count += 1;
                wasCalled = true;
                typeOf = o.GetType();
            }));

            Assert.AreEqual(2, count);
            Assert.IsTrue(wasCalled);
            Assert.AreEqual(this.GetType(), typeOf);
        }

        [Test]
        public void Test_Multiple_Events_Emit()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                for (int i = 0; i < 10; i++)
                    s.AddEvent<MockEventHandler, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            int calls = 0;
            dispatcher.Emit(new MockEvent(this, (o) => {
                calls += 1;
            }));

            Assert.AreEqual(10, calls);
        }

        [Test]
        public async Task Test_Multiple_Events_EmitAsync()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                for (int i = 0; i < 10; i++)
                    s.AddAsyncEvent<MockEventHandler, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            int calls = 0;
            await dispatcher.EmitAsync(new MockEvent(this, (o) => {
                calls += 1;
            }));

            Assert.AreEqual(10, calls);
        }

        [Test]
        public void Test_Multiple_Handlers_Emit()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                s.AddEvent<MockEventHandler, MockEvent>();
                s.AddEvent<MockEventHandler2, MockEvent>();
                s.AddEvent<MockEventHandler3, MockEvent>();
                s.AddEvent<MockEventHandler4, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            int calls = 0;
            dispatcher.Emit(new MockEvent(this, (o) => {
                calls += 1;
            }));

            Assert.AreEqual(4, calls);
        }

        [Test]
        public void Test_Event_Execption_Emit()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                s.AddEvent<MockEventHandler, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            dispatcher.Emit(new MockEvent(this, (o) => {
                throw new Exception();
            }));
        }

        [Test]
        public async Task Test_Event_Execption_EmitAsync()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                s.AddAsyncEvent<MockEventHandler, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            await dispatcher.EmitAsync(new MockEvent(this, (o) => {
                throw new Exception();
            }));
        }

        [Test]
        public void Test_Event_EmitOnDispose()
        {
            var service = new MockServiceProvider();
            var provider = service.Provider((s) => 
            {
                s.AddEvent<MockEventHandler, MockEvent>();
                s.AddAsyncEvent<MockEventHandler, MockEvent>();
            });

            var dispatcher = this._eventDispatcher(provider);
            int calls = 0;
            dispatcher.EmitOnDispose(new MockEvent(this, (o) => {
                calls += 1;
            }));

            dispatcher.DisposeAsync().GetAwaiter().GetResult();
            Assert.AreEqual(2, calls);
        }
    }
}