using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class ErrorDetailsTests
    {
        [Fact]
        public void ErrorDetail_TestProperties()
        {
            ErrorDetails error = new ErrorDetails(400,"Error Reported");
            Assert.Equal(400, error.StatusCode);
            Assert.Equal("Error Reported", error.Message);
        }
    }
}
