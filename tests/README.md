# Testing Frameworks

This document generally explain how to use the framework for contract and unit testing.

## Contract Testing using xUnit

We have added a new class named `ComplexLoadInlineData`attribute to read complex data from an external file
and serialize the input parameters of a test function. Here is an example:

``` csharp
using Microsoft.Azure.Functions.Tests.xUnit;

namespace Microsoft.Azure.Functions.Tests.Services
{
    public class CalculatorService : BaseTest
    {
        [Theory]
        [ComplexLoadInlineData("TestData/test.json")]
        [Trait("Category", "Category1")]
        public void Add_TwoInt_ReturnsSum(int i, int j, int expected)
        {
            Test(
                arrange: () => {
                    return (i,j);
                },
                act: (input) => {
                    var d = input.Item1 + input.Item2 ;
                    return d ;
                },
                assert: (result) => {
                    Assert.Equal(expected, result);
                }

            );
        }
    }
}
```

where `i`, `j` and `expected` will be read from the specified json file located in `TestData/test.json'.
The input parameters can be any serializable complex types such as EventData or streaming data models.
This can be easily used for the contract testing too if needed.

``` json
{
    "i": 10,
    "j": 20,
    "expected": 30
}
```

Note: in this example, `i`, `j`, `expected` can be a complex type, such as a payload for a streaming data. The framework will read the filed and
match the property name in the json file with the input variable name in the test function and then deserializes the content of the property
based on the data type of the input variables.
